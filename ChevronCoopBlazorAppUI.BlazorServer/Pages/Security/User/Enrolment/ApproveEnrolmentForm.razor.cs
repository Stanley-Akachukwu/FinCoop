using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Util;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.Approvalworkflows;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.Inputs;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment
{
    public partial class ApproveEnrolmentForm
    {
        public ApproveEnrolmentForm()
        {
        }

        string notificationText;
        bool showPopup = false;

        [Parameter]
        public string ActiveTabKey { get; set; }

        [Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }


        [Parameter]
        public MemberApprovalDTO Model { get; set; }


        [Parameter]
        public EventCallback<MemberApprovalDTO> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public bool ShowStatus { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Parameter]
        public EventCallback<bool> ShowStatusChanged { get; set; }

        [Parameter]
        public EventCallback<bool> OnApproveActionRefreshGrid { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        FileDownloader FileDownloader { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        ILogger<ApproveEnrolmentForm> Logger { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }
        public List<string> statusTypes { get; set; } = new List<string>();
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(EnrollmentPaymentInfoMasterView)}";

        public List<EnrollmentPaymentInfoMasterView> EnrollmentPaymentInfoMasterViews { get; set; } =
            new List<EnrollmentPaymentInfoMasterView>();

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            Model = new MemberApprovalDTO
            {
                Status = MemberProfileStatus.PENDING_APPROVAL.ToString(),
            };
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);
            }
        }

        public async Task OnSave()
        {
            var updateUserStatus = new UpdateUserStatusCommand { Status = Model.Status, UserId = Model.UserId };
            var rsp =
                await DataService.ApproveEnrolment<UpdateUserStatusCommand, CommandResult<string>>(updateUserStatus);

            Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Model)}");
            if (!rsp.IsSuccessStatusCode)
            {
                var rspContent = System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }

                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = msg,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                var payload = System.Text.Json.JsonSerializer.Serialize(updateUserStatus);
                await _auditLogService.LogAudit("Self Enrollment.",
                    $"Approved enrollment for memeber with ID- {Model.UserId}.", "Security", payload, CurrentUser);
                notificationText = $"Enrolment successfully approved!";
                showPopup = true;
            }
        }


        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);
            Model = new MemberApprovalDTO();
            await ModelChanged.InvokeAsync(Model);
            showPopup = false;
            navigationManager.NavigateTo("/security/enrollments", true);
        }


        private async Task OnDownloadPaymentReciept()
        {
            //navigationManager.NavigateTo("/enrolment/download", true);
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(GRID_API_RESOURCE);
                Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsAsync<PaymentInfoMasterView>();
                if (content != null)
                    EnrollmentPaymentInfoMasterViews = content.value;
                var enronllementPaymentFile = EnrollmentPaymentInfoMasterViews
                    .Where(x => x.ProfileId.Equals(Model.ProfileId)).FirstOrDefault();

                if (enronllementPaymentFile != null)
                {
                    await FileDownloader.DownloadFileFromBase64Async(enronllementPaymentFile.Evidence,
                        enronllementPaymentFile.MimeType);
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "No file was uploaded for this member",
                        Description = "No file was uploaded for this member",
                        NotificationType = NotificationType.Error
                    });
                }
            }
            catch (Exception exp)
            {
            }
        }

        private void OnFileUploaded(UploadChangeEventArgs args)
        {
        }

        public async Task OnInput(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private async Task onFocus(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
    }

    public class PaymentInfoMasterView
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@odata.count")]
        public int odatacount { get; set; }

        public List<EnrollmentPaymentInfoMasterView> value { get; set; }
    }
}