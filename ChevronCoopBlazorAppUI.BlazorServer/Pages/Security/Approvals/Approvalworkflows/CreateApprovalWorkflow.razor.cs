using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.Approvalworkflows
{
    public partial class CreateApprovalWorkflow
    {

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }
        private bool showWorkflowCreationSuccessVisibility { get; set; } = false;
        public CreateApprovalNotificationCommand ApprovalModel { get; set; } = new CreateApprovalNotificationCommand();
        [Parameter]
        public bool ShowModal { get; set; }
        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }
        public string ApprovalWorkflowType { get; set; }
        public string ApprovalWorkflowName { get; set; }
        public string ApprovalWorkflowId { get; set; }


        string notificationText;
        bool showPopup = false;
        public EventCallback<CreateApprovalNotificationCommand> ApprovalModelChanged { get; set; }
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
        /// <summary>
        /// ////
        /// </summary>
     
        public ApprovalGroupViewModel approvalGroupMember { get; set; }
        public CreateApprovalWorkflowCommand Model { get; set; }

    
        public string WorkflowName { get; set; }
     
        bool stage1 = true;
        bool stage2 = false;
         
        bool groupEntered = false;

        public EventCallback<CreateApprovalWorkflowCommand> ModelChanged { get; set; }
        
        public ApprovalGroupViewModel approvalGroupViewModel { get; set; } = new ApprovalGroupViewModel();
       
        public List<string> ApprovalWorkflowTypes { get; set; } = new List<string>();
        public List<ApprovalGroupViewModel> approvalGroupMembers { get; set; } = new List<ApprovalGroupViewModel>();
        public List<GroupToApproveViewModel> selectedGroups { get; set; } = new List<GroupToApproveViewModel>();

        public GroupToApproveViewModel selectedGroupModel { get; set; } = new GroupToApproveViewModel();
        public List<string> ApprovalGroupSelectionList { get; set; } = new List<string>();
        string FETCH_APPROVAL_GRPS_BY_SESSION => $"{Config.API_HOST}/ApprovalGroup/groupMembersBySessionId";
        string APPROVE_ALL_GROUPS => $"{Config.API_HOST}/ApprovalGroup/fetchAll";
      
        string APPROVE_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalGroupMasterView)}?$orderby=DateCreated desc";
        public List<ApprovalGroupMasterView> ApprovalGroups = new List<ApprovalGroupMasterView>();

        public List<ApprovalGroupViewModel> allGroups { get; set; } = new List<ApprovalGroupViewModel>();

        private bool groupMembersExist { get; set; } = false;
        private bool groupChosen { get; set; } = false;
        private Query Query_Grp_Combo;
        public int GroupApprovalSequence { get; set; } = 1;
        
        public bool hasSelectedDepartment { get; set; } = false;
        Drawer creatGroupDrawer;
        AddGroupDrawerForm addGroupDrawerForm;
        bool showCreateApprovalGroupDrawer { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentUser();
            //var appprovalTypes = System.Enum.GetNames(typeof(ApprovalWorkflowTypes)).ToList();
            //foreach(var item in appprovalTypes)
            //{
            //    ApprovalWorkflowTypes.Add(Regex.Replace(item, "(\\B[A-Z])", " $1"));
            //}
            //LoadApprovalGroups();
            Query_Grp_Combo = new Query();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        private async Task OnCreateApprovalGroup()
        {
            selectedGroupModel = new GroupToApproveViewModel();
            await onAddApprovalGroups();
        }

        async Task onAddApprovalGroups()
        {
            showCreateApprovalGroupDrawer = true;
        }

        async Task onAddApprovalGroupDone()
        {
            showCreateApprovalGroupDrawer = false;
        }

        public async Task AddGroupToList(GroupToApproveViewModel g)
        {
            bool seqExist = false;

            //if (g.NumberOfApprovers > g.RequiredApprovers)
            //{
            //    await notificationService.Open(new NotificationConfig()
            //    {
            //        Message = "Invalid number of approvers.",
            //        Description = $"Number of approvers should not exceed {g.RequiredApprovers}",
            //        NotificationType = NotificationType.Warning
            //    });
            //    return;
            //}

            if (selectedGroups.Count > 1)
                seqExist = await CheckSequnceDuplicate(g);


            if (seqExist)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Invalid Sequence.",
                    Description = $"Group should be entered in sequential order. Sequence {seqExist} already taken.",
                    NotificationType = NotificationType.Warning
                });
                return;
            }


            if (selectedGroups.Count == 1 &&
                selectedGroups.FirstOrDefault().GroupApprovalSequence == g.GroupApprovalSequence)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Sequential Order Violation.",
                    Description = $"Choose the sequence in orderly manner.",
                    NotificationType = NotificationType.Warning
                });
                return;
            }

            if (selectedGroups.FirstOrDefault(k => k.Id == g.Id) == null)
            {
                selectedGroups.Add(g);
                groupMembersExist = true;
                groupEntered = true;
                hasSelectedDepartment = true;
                StateHasChanged();
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Duplication.",
                    Description = "Group already added.",
                    NotificationType = NotificationType.Warning
                });
                return;
            }
        }

        private async Task<bool> CheckSequnceDuplicate(GroupToApproveViewModel g)
        {
            var seqExist = selectedGroups.Where(grp => grp.GroupApprovalSequence == g.GroupApprovalSequence).Any();
            if (seqExist)
            {
                return true;
            }

            return false;
        }


        public async Task OnClickDeleteGroup(GroupToApproveViewModel grp)
        {
            selectedGroups.Remove(grp);
        }

        private async Task LoadApprovalGroups()
        {
            try
            {
                var rsp = await DataService.GetApprovalGroupMasterView<List<ApprovalGroupMasterView>>();
                ApprovalGroups = rsp.Content;
            }
            catch (Exception exp)
            {
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            CurrentUser.FindFirstValue(ClaimTypes.Sid);
            if (firstRender)
            {
                WorkflowCreationSessionId = await _sessionStorage.GetItemAsync<string>("CURRENTAPPROVALWORKFLOWID");

                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task onAddApprovalGroup()
        {
            var navigateBackUrl = "navigate-to-workflow".ToLower();
            _navigationManager.NavigateTo($"/approval/group/add/{navigateBackUrl}", forceLoad: true);
        }

        private async Task OnGroupValueSelectedhandler(SelectEventArgs<ApprovalGroupMasterView> args)
        {
            var group = allGroups.FirstOrDefault(g => g.Id == args.ItemData.Id);
            var approvalGroup = new GroupToApproveViewModel();

            approvalGroup.Id = group.Id;
            approvalGroup.GroupName = group.Name;
            //approvalGroup.NumberOfApprovers = group.NumberOfApprovers;

            if (selectedGroups.FirstOrDefault(g => g.Id == approvalGroup.Id) == null)
            {
                selectedGroups.Add(approvalGroup);
                groupEntered = true;
            }

            hasSelectedDepartment = true;
            StateHasChanged();
        }

        private async Task OnTypeValueSelectedhandler(SelectEventArgs<string> args)
        {
            WorkflowName = $"{args.ItemData} Workflow";
            groupEntered = true;
        }
        private async Task BackToStage1()
        {
            stage1 = true; stage2 = false;
        }
        private async Task ValidateWorkFlowType()
        {
            if (!groupMembersExist)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Required Approval Group.",
                    NotificationType = NotificationType.Error,
                    Description = "Please create new or use old approval group to proceed..",
                    Duration = 6
                });
                return;
            }

            if (string.IsNullOrEmpty(WorkflowName))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Invalid Selection.",
                    NotificationType = NotificationType.Error,
                    Description = "Workflow name description is required.",
                    Duration = 6
                });
                return;
            }


            var workflowApprovalGroups = new List<WorkflowApprovalGroupModel>();
            foreach (var k in selectedGroups)
            {
                var w = new WorkflowApprovalGroupModel
                {
                    ApprovalSequence = k.GroupApprovalSequence,
                    RequiredApprovers = k.NumberOfApprovers,
                    GroupId = k.Id,  GroupName = k.GroupName
                };
                workflowApprovalGroups.Add(w);
            }

            Model = new CreateApprovalWorkflowCommand
            {
                ApprovalGroups = workflowApprovalGroups,
                CreatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid),
                WorkflowName = WorkflowName,
            };
            stage1 = false;
            stage2 = true;
            // await OnCreateWorkFlow(Model);
        }
        public async Task OnCreateWorkFlow(CreateApprovalWorkflowCommand model)
        {         

            var payload = JsonSerializer.Serialize(model);

            Logger.LogInformation($"rsp content->{payload}");

            var rsp = await DataService
                .CreateApprovalWorkflow<CreateApprovalWorkflowCommand, CommandResult<ApprovalWorkflowViewModel>>(model);

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
                await _auditLogService.LogAudit("Workflow Creation.", $"Created {ApprovalWorkflowType} workflow-",
                    "Security", payload, CurrentUser);
                await _sessionStorage.SetItemAsync("APPROVALWORKFLOWNAME", rsp.Content.Response.WorkflowName);
                // await _sessionStorage.SetItemAsync("APPROVALWORKFLOWID", rsp.Content.Response.Id);
                ApprovalWorkflowId = rsp.Content.Response.Id;



				if (groupMembersExist) 
                    await SaveApprovalNow();
				   // _navigationManager.NavigateTo($"/approval/notification/settings", forceLoad: true);
				StateHasChanged();
            }

            await InvokeAsync(StateHasChanged);
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
            Model = new CreateApprovalWorkflowCommand();
            await ModelChanged.InvokeAsync(Model);
            showPopup = false;
        }

        private void ExcludeForReminderValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedDeptIds = args.Value;
            if (selectedDeptIds.Count() > 0)
            {
                ApprovalModel.ExcludeFromReminderUserIds = new List<string>(selectedDeptIds);
            }
        }

        private void IncludeForEscalationValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {

            var selectedDeptIds = args.Value;
            if (selectedDeptIds.Count() > 0)
            {
                ApprovalModel.EscalateToUserIds = new List<string>(selectedDeptIds);
            }
        }

        public async Task OnSaveApprovalNotification()
        {
			await OnCreateWorkFlow(Model);
        }

        protected async Task SaveApprovalNow()
        {
			ApprovalModel.CreatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
			ApprovalModel.ApprovalWorkflowId = ApprovalWorkflowId;
			var payload = JsonSerializer.Serialize(ApprovalModel);

			Logger.LogInformation($"request->{payload}");

			var rsp = await DataService.CreateApprovalNotifications<CreateApprovalNotificationCommand, CommandResult<string>>(ApprovalModel);

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
				await _auditLogService.LogAudit("Workflow SLA Creation.", $"Created workflow approval notification settings -", "Security", payload, CurrentUser);
				notificationText = $"Approval workflow notification  created successfully!";
				showWorkflowCreationSuccessVisibility = true;
				StateHasChanged();
				await InvokeAsync(StateHasChanged);
			}
			 
		}


	}

    public class GroupToApproveViewModel
    {
        public string Id { get; set; }
        public string GroupName { get; set; }
        public int NumberOfApprovers { get; set; }
        public int GroupApprovalSequence { get; set; }
        public int RequiredApprovers { get; set; }
    }

     

 
}


