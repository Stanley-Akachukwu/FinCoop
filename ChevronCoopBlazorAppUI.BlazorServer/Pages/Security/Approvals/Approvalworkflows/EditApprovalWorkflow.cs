using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using AntDesign;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using AP.ChevronCoop.Commons;
using System.Security.Claims;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.Approvalworkflows
{
    public partial class EditApprovalWorkflow
    {
        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public ApprovalGroupViewModel approvalGroupMember { get; set; }
        public UpdateApprovalWorkflowCommand Model { get; set; }

        public string ApprovalWorkflowType { get; set; }
        public string WorkflowName { get; set; }
        public string ApprovalWorkflowId { get; set; }


        bool showPopup = false;
        bool groupEntered = false;

        public EventCallback<UpdateApprovalWorkflowCommand> ModelChanged { get; set; }

        [Inject]
        ISessionStorageService _sessionStorage { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        public ApprovalGroupViewModel approvalGroupViewModel { get; set; } = new ApprovalGroupViewModel();
        public string WorkflowCreationSessionId { get; set; }
        public List<string> ApprovalWorkflowTypes { get; set; } = new List<string>();
        public List<ApprovalGroupViewModel> approvalGroupMembers { get; set; } = new List<ApprovalGroupViewModel>();
        public List<GroupToApproveViewModel> selectedGroups { get; set; } = new List<GroupToApproveViewModel>();

        public GroupToApproveViewModel selectedGroupModel { get; set; } = new GroupToApproveViewModel();
        public List<string> ApprovalGroupSelectionList { get; set; } = new List<string>();
        string FETCH_APPROVAL_GRPS_BY_SESSION => $"{Config.API_HOST}/ApprovalGroup/groupMembersBySessionId";
        string APPROVE_ALL_GROUPS => $"{Config.API_HOST}/ApprovalGroup/fetchAll";
        private Query Query_Combo;

        string APPROVE_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalGroupMasterView)}?$orderby=DateCreated desc";

        public List<ApprovalGroupMasterView> ApprovalGroups = new List<ApprovalGroupMasterView>();

        public List<ApprovalGroupViewModel> allGroups { get; set; } = new List<ApprovalGroupViewModel>();

        private bool groupMembersExist { get; set; } = false;
        private bool groupChosen { get; set; } = false;
        private Query Query_Grp_Combo;
        public int GroupApprovalSequence { get; set; } = 1;
        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        [Inject]
        ILogger<CreateApprovalWorkflow> Logger { get; set; }

        public bool hasSelectedDepartment { get; set; } = false;
        Drawer creatGroupDrawer;
        AddGroupDrawerForm addGroupDrawerForm;
        bool showCreateApprovalGroupDrawer { get; set; } = false;

        [Parameter]
        public string id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentUser();
            //getApprovalWorkflow
            await GetApprovalWorkflowById();
            Query_Grp_Combo = new Query();
        }

        public async Task GetApprovalWorkflowById()
        {
            var rsp = await DataService
                .GetApprovalWorkflowById<GetApprovalWorkflowByIdCommand, CommandResult<GetApprovalWorkflowViewModel>>(
                    new GetApprovalWorkflowByIdCommand { Id = id });
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
                GetApprovalWorkflowViewModel rspResponse =
                    JsonSerializer.Deserialize<GetApprovalWorkflowViewModel>(rsp.Content.Response.ToJson());
                if (rspResponse != null)
                {
                    WorkflowName = rspResponse.WorkflowName;
                    if (rspResponse.ApprovalGroups != null && rspResponse.ApprovalGroups.Any())
                    {
                        selectedGroups = rspResponse.ApprovalGroups.Select(x => new GroupToApproveViewModel()
                        {
                            Id = x.GroupId, GroupName = x.GroupName, GroupApprovalSequence = x.ApprovalSequence,
                            RequiredApprovers = x.RequiredApprovers, NumberOfApprovers = x.RequiredApprovers
                        }).ToList();
                        groupMembersExist = true;
                        groupEntered = true;
                        hasSelectedDepartment = true;
                        StateHasChanged();
                    }
                }
            }
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
            //    //await notificationService.Open(new NotificationConfig()
            //    //{
            //    //    Message = "Invalid number of approvers.",
            //    //    Description = $"Number of approvers should not exceed {g.RequiredApprovers}",
            //    //    NotificationType = NotificationType.Warning
            //    //});
            //    //return;
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
                // StateHasChanged();
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

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //   // base.OnAfterRender(firstRender);
        //    CurrentUser.FindFirstValue(ClaimTypes.Sid);
        //    if (firstRender)
        //    {
        //        WorkflowCreationSessionId = await _sessionStorage.GetItemAsync<string>("CURRENTAPPROVALWORKFLOWID");

        //       // StateHasChanged();
        //    }
        //    await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        //}

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
            //  approvalGroup.NumberOfApprovers = group.NumberOfApprovers;

            if (selectedGroups.FirstOrDefault(g => g.Id == approvalGroup.Id) == null)
            {
                selectedGroups.Add(approvalGroup);
                groupEntered = true;
            }

            hasSelectedDepartment = true;
            // StateHasChanged();
        }

        private async Task OnTypeValueSelectedhandler(SelectEventArgs<string> args)
        {
            WorkflowName = $"{args.ItemData} Workflow";
            groupEntered = true;
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
                    GroupId = k.Id, GroupName = k.GroupName
                };
                workflowApprovalGroups.Add(w);
            }

            Model = new UpdateApprovalWorkflowCommand
            {
                ApprovalGroups = workflowApprovalGroups,
                UpdatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid),
                WorkflowName = WorkflowName,
            };
            await OnCreateWorkFlow(Model);
        }

        public async Task OnCreateWorkFlow(UpdateApprovalWorkflowCommand model)
        {
            model.Id = id;
            var payload = JsonSerializer.Serialize(model);

            Logger.LogInformation($"rsp content->{payload}");

            var rsp = await DataService
                .UpdateApprovalWorkflow<UpdateApprovalWorkflowCommand, CommandResult<ApprovalWorkflowViewModel>>(model);

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
                await _auditLogService.LogAudit("Workflow Updated.", $"Updated {ApprovalWorkflowType} workflow-",
                    "Security", payload, CurrentUser);
                //await _sessionStorage.SetItemAsync("APPROVALWORKFLOWNAME", rsp.Content.Response.WorkflowName);
                //await _sessionStorage.SetItemAsync("APPROVALWORKFLOWID", rsp.Content.Response.Id);


                // if (groupMembersExist)
                _navigationManager.NavigateTo("/approval/workflows/list", true);
                //StateHasChanged();
            }
            // await InvokeAsync(StateHasChanged);
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
            Model = new UpdateApprovalWorkflowCommand();
            await ModelChanged.InvokeAsync(Model);
            showPopup = false;
        }
    }
}