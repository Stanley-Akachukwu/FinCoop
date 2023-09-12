using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Syncfusion.Blazor.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Security.Claims;
using System.Text.Json;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using CurrieTechnologies.Razor.SweetAlert2;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.ApprovalGroups
{
    public partial class EditApprovalGroup
    {
        [Inject]
        NavigationManager _navigationManager { get; set; }

        AddGroupMemberDrawerForm addGroupMemberForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        BrowserDimension BrowserDimension;

        [Parameter]
        public string navigateBackUrl { get; set; }

        [Parameter]
        public string id { get; set; }

        [Parameter]

        public EventCallback<CreateOrUpdateGroupMemberCommand> ModelChanged { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        private Query Query_Combo;
        private Query Query_Dept_Combo;

        public string getMemebrs_User_Url { get; set; }
        public string getDepartment_Url { get; set; }

        string notificationText;
        bool showPopup = false;
        public CreateOrUpdateGroupMemberCommand Model { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        [Inject]
        ISessionStorageService _sessionStorage { get; set; }

        [Inject]
        ILogger<AddApprovalGroup> Logger { get; set; }

        private string bearToken { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        private bool recordAdded { get; set; } = false;
        private bool groupNameEntered { get; set; } = false;
        private bool groupMembersExist { get; set; } = false;
        private bool hasSelectedDepartment { get; set; } = false;
        public string GroupName { get; set; }
        public string GroupId { get; set; }
        private bool showRemoveMemberWarningVisibility { get; set; } = false;
        public ApprovalGroupViewModel approvalGroupViewModel { get; set; } = new ApprovalGroupViewModel();
        public string WorkflowCreationSessionId { get; set; }
        public string DepartmentId { get; set; }
        public List<CustomerMasterView> MembersByDepartment = new List<CustomerMasterView>();
        public List<DepartmentMasterView> Departments = new List<DepartmentMasterView>();
        public SelectedMemberApprovalGroup removeMemberSelection { get; set; } = new SelectedMemberApprovalGroup();
        public CreateApprovalGroupCommand CreateApprovalGroupModel { get; set; } = new CreateApprovalGroupCommand();
        public UpdateApprovalGroupCommand UpdateApprovalGroupModel { get; set; } = new UpdateApprovalGroupCommand();

        public SelectedMemberApprovalGroup CreateApprovalGroupMemberModel { get; set; } =
            new SelectedMemberApprovalGroup();

        public bool showSuccessCreationVisibility { get; set; } = false;
        Drawer creatMemberDrawer;
        AddGroupMemberDrawerForm addGroupMemberDrawerForm;
        bool showCreateApprovalGroupDrawer { get; set; } = false;

        public List<SelectedMemberApprovalGroup> approvalGroupMembers { get; set; } =
            new List<SelectedMemberApprovalGroup>();

        public List<string> approvalGroupMemberIds { get; set; } = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (navigateBackUrl == "navigate-to-approval")
                navigateBackUrl = "/approval/group/list";

            if (navigateBackUrl == "navigate-to-workflow")
                navigateBackUrl = "/approval/workflows/create";

            getMemebrs_User_Url = $"{Config.ODATA_VIEWS_HOST}/{nameof(CustomerMasterView)}";
            getDepartment_Url = $"{Config.ODATA_VIEWS_HOST}/{nameof(DepartmentMasterView)}";

            await GetCurrentUser();
            Model = new CreateOrUpdateGroupMemberCommand();
            await GetApprovalGroupById();
        }

        public async Task GetApprovalGroupById()
        {
            if (string.IsNullOrEmpty(id))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Invalid Approval Group ID.",
                    NotificationType = NotificationType.Error
                });
            }


            var rsp =
                await DataService.GetApprovalGroupById<CommandResult<GetApprovalGroupViewModel>>(nameof(ApprovalGroup),
                    id);

            if (!rsp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(rsp.Error.Content))
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
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
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Oops! Something Went Wrong. Please try again later. Thanks",
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                GetApprovalGroupViewModel rspResponse =
                    JsonSerializer.Deserialize<GetApprovalGroupViewModel>(rsp.Content.Response.ToJson());
                if (rspResponse != null)
                {
                    GroupName = rspResponse.Name;
                    if (rspResponse.Members != null && rspResponse.Members.Any())
                    {
                        approvalGroupMembers = rspResponse.Members.Select(x => new
                            SelectedMemberApprovalGroup()
                        {
                            Email = x.Email,
                            MemberName = x.FirstName + " " + x.LastName,
                            CustomerId = x.Id,
                            MembershipId = x.MemberId,
                            ApplicationUserId = x.ApplicationUserId
                        }).ToList();
                        groupMembersExist = true;
                        groupNameEntered = true;
                        hasSelectedDepartment = true;
                        StateHasChanged();
                    }
                }
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

                HeaderData.Add("Bearer", bearToken);
                WorkflowCreationSessionId = await _sessionStorage.GetItemAsync<string>("CURRENTAPPROVALWORKFLOWID");
                await OnRefresh();
                StateHasChanged();
                await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
            }

    
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        private async Task OnRefresh()
        {
            await GetCurrentUser();
            await Task.CompletedTask;
        }

        public async Task AddGroupMemberToList(SelectedMemberApprovalGroup m)
        {
            if (approvalGroupMembers.Any(c => c.ApplicationUserId == m.ApplicationUserId))
            {
                return;
            }
            approvalGroupMembers.Add(m);
            recordAdded = true;
            groupMembersExist = true;
            showPopup = true;
            await OnCancel();
            StateHasChanged();
        }

        private async Task OnSave()
        {
            if (!approvalGroupMembers.Any())
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Validation Error",
                    NotificationType = NotificationType.Error,
                    Description = "Approval Group Members List is empty, add a member then save again"
                });

            }
            UpdateApprovalGroupCommand updateApprovalGroup = new UpdateApprovalGroupCommand()
            {
                Id = id,
                UpdatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid),
                Name = GroupName,
                ApprovalGroupMemberIds = approvalGroupMembers.Select(m => m.ApplicationUserId).ToList(),
            };

            var payload = JsonSerializer.Serialize(updateApprovalGroup);

            Logger.LogInformation($"updateApprovalGroup->{payload}");

            var rsp = await DataService
                .UpdateApprovalGroup<UpdateApprovalGroupCommand, CommandResult<ApprovalGroupViewModel>>(
                    updateApprovalGroup);

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
                await _auditLogService.LogAudit("Approval Group Update.", $"Created approval group -", "Security",
                    payload, CurrentUser);
                this.showSuccessCreationVisibility = true;
                navigateBackUrl = "/approval/group/list";
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task OnNavigateBackafterCreation()
        {
            this.showSuccessCreationVisibility = false;
            _navigationManager.NavigateTo(navigateBackUrl, forceLoad: true);
        }

        private async Task OnCreateApprovalGroup()
        {
            CreateApprovalGroupMemberModel = new SelectedMemberApprovalGroup();

            await onAddApprovalGroupMember();
        }

        async Task onAddApprovalGroupMember()
        {
            showCreateApprovalGroupDrawer = true;
        }

        async Task onAddApprovalGroupMemberDone()
        {
            showCreateApprovalGroupDrawer = false;
            groupNameEntered = true;
        }

        public void OnGroupNameInputChange(InputEventArgs args)
        {
            groupNameEntered = true;
            StateHasChanged();
        }


        private async Task OnClickDeleteMember(SelectedMemberApprovalGroup member)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Are you sure?",
                Text = $"You want to delete {member.MemberName}?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Yes",
                CancelButtonText = "No, keep it"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
               // showRemoveMemberWarningVisibility = true;
                removeMemberSelection = member;
               await OnClickProceedRemoval();
            }
        }

        private async Task OnClickProceedRemoval()
        {
            this.showRemoveMemberWarningVisibility = false;
            await ProcessMemberDeletion(removeMemberSelection);
            await InvokeAsync(StateHasChanged);
        }

        private async Task ProcessMemberDeletion(SelectedMemberApprovalGroup member)
        {
            approvalGroupMembers.Remove(member);
        }

        private async Task OnClickCancelRemoval()
        {
            this.showRemoveMemberWarningVisibility = false;
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnProceed()
        {
        }

        public void ActionCompletedHandler(ActionEventArgs<MemberProfileMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        async Task onAddMember()
        {
            showCreateDrawer = true;
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
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
            Model = new CreateOrUpdateGroupMemberCommand();
            await ModelChanged.InvokeAsync(Model);
            showPopup = false;
        }
    }
}