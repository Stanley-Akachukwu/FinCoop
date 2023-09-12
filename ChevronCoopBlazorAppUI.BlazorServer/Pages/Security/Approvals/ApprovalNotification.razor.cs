using AntDesign;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using System.Text.Json;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Data;
using AP.ChevronCoop.Commons;
using Microsoft.AspNetCore.Components.Authorization;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using System.Security.Claims;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals
{
    public partial class ApprovalNotification
    {
        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public CreateApprovalNotificationCommand Model { get; set; } = new CreateApprovalNotificationCommand();

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        public string ApprovalWorkflowType { get; set; }
        public string ApprovalWorkflowName { get; set; }
        public string ApprovalWorkflowId { get; set; }


        string notificationText;
        bool showPopup = false;
        public EventCallback<CreateApprovalNotificationCommand> ModelChanged { get; set; }

        [Inject]
        ISessionStorageService _sessionStorage { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        public string WorkflowCreationSessionId { get; set; }
        string Members_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileMasterView)}";

        private Query Query_Combo;
        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        [Inject]
        ILogger<ApprovalNotification> Logger { get; set; }

        private void ExcludeForReminderValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedDeptIds = args.Value;
            if (selectedDeptIds.Count() > 0)
            {
                Model.ExcludeFromReminderUserIds = new List<string>(selectedDeptIds);
            }
        }

        private void IncludeForEscalationValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedDeptIds = args.Value;
            if (selectedDeptIds.Count() > 0)
            {
                Model.EscalateToUserIds = new List<string>(selectedDeptIds);
            }
        }

        private bool groupMembersExist { get; set; } = false;
        private bool showWorkflowCreationSuccessVisibility { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentUser();
            Model = new CreateApprovalNotificationCommand();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                ApprovalWorkflowName = await _sessionStorage.GetItemAsync<string>("APPROVALWORKFLOWNAME");
                ApprovalWorkflowId = await _sessionStorage.GetItemAsync<string>("APPROVALWORKFLOWID");
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task OnSaveApprovalNotification()
        {
            Model.CreatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            Model.ApprovalWorkflowId = ApprovalWorkflowId;
            var payload = JsonSerializer.Serialize(Model);

            Logger.LogInformation($"request->{payload}");

            var rsp = await DataService
                .CreateApprovalNotifications<CreateApprovalNotificationCommand, CommandResult<string>>(Model);

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
                await _auditLogService.LogAudit("Workflow SLA Creation.",
                    $"Created workflow approval notification settings -", "Security", payload, CurrentUser);
                notificationText = $"Approval workflow notification  created successfully!";
                showWorkflowCreationSuccessVisibility = true;
                StateHasChanged();
                await InvokeAsync(StateHasChanged);
            }
            //notificationText = $"Approval workflow notification  created successfully!";
            //showWorkflowCreationSuccessVisibility = true;
            //StateHasChanged();
            //await InvokeAsync(StateHasChanged);
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

        public async Task OnCancel()
        {
            _navigationManager.NavigateTo("/approval/workflows/list", true);

            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);
            await ModelChanged.InvokeAsync(Model);
            showPopup = false;
        }
    }
}