using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals
{
    public partial class ChevronCoopApprovalForm
    {
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        private bool groupMembersExist { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        [Inject]
        ILogger<ChevronCoopApprovalForm> Logger { get; set; }

        private string bearToken { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        //[Parameter]
        //public string navigateBackUrl { get; set; }
        [Parameter]
        public string entityId { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        private Dictionary<string, string> keyValuePairs { get; set; } = new Dictionary<string, string>();
        public List<ApprovalGroupMember> approvalGroupMembers { get; set; } = new List<ApprovalGroupMember>();
        public ApprovalViewModel ApprovalViewModel { get; set; } = new ApprovalViewModel();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentUser();
        }

        public async Task OnClickReturn()
        {
            _navigationManager.NavigateTo("/approval/list", forceLoad: true);
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

                HeaderData.Add("Bearer", bearToken);
                await GetApprovalByEntityId();

                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        private async Task GetApprovalByEntityId()
        {
            if (string.IsNullOrEmpty(entityId))
            {
                bool dd = true;
            }

            var model = new GetApprovalByEntityIdCommand { EntityId = entityId };

            try
            {
                var rsp = await DataService
                    .GetApprovalByEntityId<GetApprovalByEntityIdCommand, CommandResult<ApprovalViewModel>>(model);
                //Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");

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
                    ApprovalViewModel = rsp.Content.Response;
                    if (!string.IsNullOrEmpty(ApprovalViewModel.RequestAttributesDetails))
                        keyValuePairs =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(ApprovalViewModel
                                .RequestAttributesDetails);


                    if (ApprovalViewModel.ApprovalGroupMembers.Count > 0)
                        approvalGroupMembers = ApprovalViewModel.ApprovalGroupMembers;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task OnClickProcessApproval(ApprovalGroupMember member)
        {
            //var rsp = await DataService.ProcessApproval<ProcessApprovalCommand, CommandResult<string>>(Model);
            ////Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");

            //if (!rsp.IsSuccessStatusCode)
            //{
            //    var serverErrorMessages = "Server Error.";
            //    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
            //    var msg = rsp.ReasonPhrase;

            //    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
            //    {
            //        serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
            //    }
            //    if (!string.IsNullOrEmpty(rspContent.Message))
            //    {
            //        serverErrorMessages += " " + rspContent.Message;
            //    }

            //    await notificationService.Open(new NotificationConfig()
            //    {
            //        Message = msg,
            //        NotificationType = NotificationType.Error,
            //        Description = serverErrorMessages
            //    });
            //}
            //else
            //{
            //    var payload = JsonSerializer.Serialize(Model);
            //    await _auditLogService.LogAudit("Approval Group Member Addition.", $"Added member with Email- {Model.Email} for approval processing.", "Security", payload, CurrentUser);

            //    if (string.IsNullOrEmpty(rsp.Content.Message))
            //    {
            //        notificationText = $"Record successfully updated!";
            //    }
            //    else { notificationText = $"{rsp.Content.Message}"; }

            //    approvalGroupViewModel = rsp.Content.Response;
            //    approvalGroupMembers = approvalGroupViewModel.GroupMembers;
            //    groupMembersExist = true;
            //    showPopup = true;
            //    GroupId = approvalGroupViewModel.ApprovalGroupId;
            //    await OnCancel();
            //    StateHasChanged();
            //}
        }

        private async Task OnRefresh()
        {
            // grid.Refresh();
            //  await GetGroupMembers();
            await GetCurrentUser();
            //await ApplyDefaultQueryFilter();
            await Task.CompletedTask;
        }
    }
}