using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.DataMigration
{
    public partial class DataMigrationForApprovalGrid
    {
        public List<MemberBulkUploadTemp> MemberBulkUploadTempModel { get; set; }

        [Parameter]
        public List<MemberBulkUploadTemp> Model { get; set; }

        private MemberBulkUploadTemp[] ModelList { get; set; }
        string ErrorDetails = "";
        ErrorDetails errors;

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        [Parameter]
        public EventCallback<MemberBulkUploadTemp> ModelChanged { get; set; }

        int TotalRecord = 0;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        string LoggedInUserId = string.Empty;
        string notificationText;
        bool showPopup = false;

        [Inject]

        NavigationManager NavigationManager { get; set; }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        public void ActionCompletedHandler(ActionEventArgs<MemberBulkUploadTemp> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            ModelList = Model.OrderBy(f => f.DateCreated).ToArray();
            if (ModelList != null)
                TotalRecord = ModelList.Count();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
            }

            ModelList = Model.ToArray();
            if (ModelList != null)
                TotalRecord = ModelList.Count();
        }

        private async Task onApproval()
        {
            if (Model != null && Model.Count > 0)
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity.IsAuthenticated)
                {
                    LoggedInUserId = user.FindFirstValue(ClaimTypes.Sid);
                    if (!string.IsNullOrEmpty(LoggedInUserId))
                    {
                        ApproveMemberBulkUploadCommand command = new ApproveMemberBulkUploadCommand()
                        {
                            ApprovedById = LoggedInUserId,
                            MemberBulkUploadSessionId = Model.FirstOrDefault().SessionId,
                            ApprovalId = Model.FirstOrDefault().ApprovalId,
                        };
                        var j = JsonSerializer.Serialize(command);
                        var rsp = await DataService
                            .ProcessRequest<ApproveMemberBulkUploadCommand, CommandResult<MemberBulkUploadViewModel>>(
                                "MemberBulkUploads", "approve", command);

                        if (!rsp.IsSuccessStatusCode)
                        {
                            var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                            var msg = rspContent?.Response;
                            if (rspContent != null && rspContent.ValidationErrors != null &&
                                rspContent.ValidationErrors.Any())
                            {
                                msg = rspContent.ValidationErrors[0].Error;
                            }

                            if (msg == null && rspContent?.Message != null)
                                msg = rspContent.Message;

                            await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Error",
                                Description = msg,
                                NotificationType = NotificationType.Error
                            });
                        }
                        else
                        {
                            notificationText = $"Operation waas successfull";
                            showPopup = true;
                            NavigationManager.NavigateTo("/Security/MigrationApproval", true);
                        }
                    }
                }
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "No record to approve",
                    NotificationType = NotificationType.Info
                });
            }
        }
    }
}