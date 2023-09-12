using AntDesign;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Inputs;
using System.Text.Json;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Services;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment
{
    public partial class EditEnrolmentForm
    {
        public EditEnrolmentForm()
        {
        }

        private Query Query_Combo;
        string notificationText;
        bool showPopup = false;


        Tabs editTab;

        [Parameter]
        public string ActiveTabKey { get; set; }

        [Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }


        [Parameter]
        public UpdateMemberProfileCommand Model { get; set; }

        [Parameter]
        public string MemberEmail { get; set; }

        [Parameter]
        public EventCallback<UpdateMemberProfileCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<UserEditForm> Logger { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        private WhereFilter baseFilter;

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        bool reloadGrid = false;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            await base.OnInitializedAsync();
            Query_Combo = new Query();
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
            var checkModel = Model;
            var modelString = JsonSerializer.Serialize(Model);
            Logger.LogInformation($"model content->{modelString}");
            var checkIsSuccessfull = await ValidateEntry();
            if (checkIsSuccessfull)
            {
                var rsp =
                    await DataService.Update<UpdateMemberProfileCommand, CommandResult<MemberProfileViewModel>>(
                        nameof(MemberProfile), Model);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");

                if (!rsp.IsSuccessStatusCode)
                {
                    var serverErrorMessages = "Server Error.";
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                    var msg = rsp.ReasonPhrase;

                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                    }

                    if (!string.IsNullOrEmpty(rspContent.Message))
                    {
                        serverErrorMessages += " " + rspContent.Message;
                    }

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = msg,
                        NotificationType = NotificationType.Error,
                        Description = serverErrorMessages
                    });
                }
                else
                {
                    var payload = JsonSerializer.Serialize(Model);
                    await _auditLogService.LogAudit("Self Enrollment.",
                        $"Updated enrollment for memeber with ID- {Model.ApplicationUserId}.", "Security", payload,
                        CurrentUser);

                    if (string.IsNullOrEmpty(rsp.Content.Message))
                    {
                        notificationText = $"Record successfully updated!";
                    }
                    else
                    {
                        notificationText = $"{rsp.Content.Message}";
                    }

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = notificationText,
                        NotificationType = NotificationType.Success
                    });
                    reloadGrid = true;
                    OnCancel();
                    showPopup = true;
                }
            }
        }


        public async Task OnCancel()
        {
            if (reloadGrid)
            {
                NavigationManager.NavigateTo("/security/enrollments", true);
                reloadGrid = false;
            }

            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new UpdateMemberProfileCommand();
            await ModelChanged.InvokeAsync(Model);

            showPopup = false;
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

        public async Task<bool> ValidateEntry()
        {
            if (string.IsNullOrEmpty(Model.Status))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Please, Provide staff Status",
                    Description = "Please, Provide staff Status",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            if (string.IsNullOrEmpty(Model.LastName))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Please, Provide staff Last Name",
                    Description = "Please, Provide staff Last Name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            if (string.IsNullOrEmpty(Model.FirstName))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Please, Provide staff First Name",
                    Description = "Please, Provide staff First Name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }


            if (string.IsNullOrEmpty(Model.MembershipId))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Please, Provide staff Registration No",
                    Description = "Please, Provide staff Registration No",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            if (string.IsNullOrEmpty(Model.Gender))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Please, Provide staff Gender",
                    Description = "Please, Provide staff Gender",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            if (string.IsNullOrEmpty(Model.DepartmentId))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Please, Provide staff Department",
                    Description = "Please, Provide staff Department",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            if (string.IsNullOrEmpty(Model.ResidentialAddress))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Please, Provide staff Address",
                    Description = "Please, Provide staff Address",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            return true;
        }
    }
}