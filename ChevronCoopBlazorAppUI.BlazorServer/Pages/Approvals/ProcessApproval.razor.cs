using AntDesign;
using AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.Approval;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using DocumentFormat.OpenXml.Vml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Approvals
{

    public partial class ProcessApproval
    {
        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }
        [Inject]
        protected IJSRuntime _jsRuntime { get; set; }
        [Inject]
        NotificationService _notificationService { get; set; }
        [Inject]
        ApprovalStateContainerService _stateService { get; set; }

        [Inject]
        ILogger<ProcessApproval> Logger { get; set; }
        [Inject]
        IEntityDataService DataService { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        [Inject]
        WebConfigHelper Config { get; set; }

        string Single_API_RESOURCE => $"{Config.API_HOST}/{nameof(ApprovalMasterView)}/getById/";
        private ClaimsPrincipal CurrentUser { get; set; }
        private MemberProfileMasterView MemberProfile { get; set; }
        private HandleApprovalCommand Command { get; set; }
        public ApprovalDTO Model { get; set; }
        private FluentValidationValidator? _fluentValidationValidator;
        public ChevronCoopApprovalViewModel ViewModel { get; set; }
        public UserApprovalViewModel UserApproval { get; set; }
        public string ApprovalId { get; set; }
        public string ApprovalWorkFlowId { get; set; }
        private bool isLoading = true;
        bool showApprovalPopup { get; set; } = false;
        bool showApprovalSuccessPopup { get; set; } = false;
        bool showRejectPopup { get; set; } = false;
        bool showRejectSuccessPopup { get; set; } = false;
        string ApplicationUserId { get; set; } = string.Empty;
        private string bearToken { get; set; }
        public List<ApprovalMasterView> ApprovalMasterViewList { get; set; }
        public List<ApprovalView> ApprovalViews { get; set; }
        public ApprovalMasterView approvalMasterView { get; set; }
        public JObject keyValuePairs = new JObject(); 
        public string JsonVM { get; set; }

        bool DisableButton = false;
        protected override async Task OnInitializedAsync()
        {

            await GetCurrentUser();
            try
            {
                Model = new ApprovalDTO();
                ViewModel = new ChevronCoopApprovalViewModel();
                approvalMasterView = new ApprovalMasterView();

                approvalMasterView = _stateService?.SelectedApprovalMasterView;
                ViewModel = System.Text.Json.JsonSerializer.Deserialize<ChevronCoopApprovalViewModel>(approvalMasterView?.ApprovalViewModelPayload);
                ViewModel.ApprovalId = approvalMasterView?.Id;
                ViewModel.Status = (ApprovalStatus)System.Enum.Parse(typeof(ApprovalStatus), approvalMasterView.Status, true);
                ApprovalType type = (ApprovalType)System.Enum.Parse(typeof(ApprovalType), approvalMasterView.ApprovalType, true);
            }
            catch (Exception ex)
            {
                await _notificationService.Open(new NotificationConfig()
                {
                    Message = "An Error Occurred.",
                    NotificationType = NotificationType.Error,
                    Description = "Your request could not be completed. Please contact thee administrator"
                });
            }
        }

        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }
                isLoading = false;

                StateHasChanged();
            }

            await _jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }
        private void GoToApprovals()
        {
            _navigationManager.NavigateTo("/approvals/all", true);
            showApprovalSuccessPopup = false;
            showRejectSuccessPopup = false;
        }
         
        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
                ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
        }

        private void Cancel()
        {
            showApprovalSuccessPopup = false;
            showRejectSuccessPopup = false;
            showApprovalPopup = false;
            showRejectPopup = false;
            StateHasChanged();
        }

        public void MapToCommand()
        {
            Command = new HandleApprovalCommand()
            {
                ApprovalId = ViewModel.ApprovalId,
                ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid)
            };
        }

        private async Task ApproveAsync()
        {
            DisableButton = true;
            MapToCommand();
            Command.Status = AP.ChevronCoop.Entities.ApprovalStatus.APPROVED;
            Command.Comment = Model.Comment;
            if (Command.ApprovalId == null)
            {
                await _notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Approval Id cannot be null",
                    NotificationType = NotificationType.Info
                });
                DisableButton = false;
                return;
                
            }
            else
            {
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                var rsp =
                    await DataService.Approval<HandleApprovalCommand, CommandResult<HandleApprovalCommand>>(
                        nameof(Approval), Command);
                Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Request Approval", "Request Approval", "Approval",
                        "NA, readonly request", CurrentUser);
                    showApprovalSuccessPopup = true;
                    showApprovalPopup = false;
                    StateHasChanged();
                }
                else
                {
                    var rspContent =
                        System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = "";
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    var response = "";
                    var description = "";
                    if (string.IsNullOrEmpty(rspContent?.Response))
                        response = "Internal Server Error";
                    else response = rspContent?.Response;

                    if (string.IsNullOrEmpty(rspContent?.Message))
                        description = $"Error occurred while processing your request. {msg}. Contact the administrator.";
                    else description = rspContent?.Message;

                    Logger.LogInformation($"ErrorMessage->{msg}");
                    await _notificationService.Open(new NotificationConfig()
                    {
                        Message = response,
                        Description = description,
                        NotificationType = NotificationType.Info,
                        Duration = 8,
                    });
                    StateHasChanged();
                    showApprovalPopup = false;
                }
            }
            DisableButton = false;
            StateHasChanged();
        }

        private async Task RejectAsync()
        {
            DisableButton = true;
            MapToCommand();
            Command.Status = AP.ChevronCoop.Entities.ApprovalStatus.REJECTED;
            Command.Comment = Model.Comment;
            if (Command.ApprovalId == null)
            {
                await _notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Approval Id cannot be null",
                    NotificationType = NotificationType.Info
                });
                DisableButton = false;
                return;
            }
            else
            {
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                var rsp =
                    await DataService.Approval<HandleApprovalCommand, CommandResult<HandleApprovalCommand>>(
                        nameof(Approval), Command);
                Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Request Approval", "Request Approval", "Approval",
                        "NA, readonly request", CurrentUser);
                    showRejectSuccessPopup = true;
                    showRejectPopup = false;
                    StateHasChanged();
                }
                else
                {
                    var rspContent =
                        System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;
                    Logger.LogInformation($"ErrorMessage->{msg}");
                    await _notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                    DisableButton = false;
                    StateHasChanged();
                    showApprovalPopup = false;
                }
            }

            StateHasChanged();
        }

        private async Task ShowApproval()
        {
            showApprovalPopup = true;
        }

        private async Task ShowApprovalPopUpMessage()
        {
            showApprovalSuccessPopup = true;
        }

        private async Task ShowReject()
        {
            showRejectPopup = true;
        }

        private async Task ShowRejectPopUpMessage()
        {
            showRejectSuccessPopup = true;
        }

        

    }
}
