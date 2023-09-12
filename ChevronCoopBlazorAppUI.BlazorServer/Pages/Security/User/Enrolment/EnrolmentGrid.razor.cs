using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment
{
    public partial class EnrolmentGrid
    {
        [Inject]
        ILogger<EnrolmentGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        ISessionStorageService _sessionStorage { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        SfGrid<MemberProfileMasterView> grid;

        [Parameter] public string EnrolmentFIlter { get; set; }
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileMasterView)}";
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        private Query QueryGrid;
        private WhereFilter kycFilter { get; set; }
        private WhereFilter statusFilter { get; set; }
        private WhereFilter isAdminFilter { get; set; }

        bool showEditRoleDrawer { get; set; } = false;
        Drawer editRoleDrawer;
        EditUserRoleForm editUserRoleForm;
        public UpdateRolesForUser UpdateRolesForUserModel { get; set; }
        UpdateApplicationUserCommand UpdateApplicationUserModel { get; set; }
        string QueryParameter;

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        MemberApprovalDTO ApproveModel { get; set; }
        UpdateMemberProfileCommand UpdateModel { get; set; }
        string MemberEmail { get; set; }

        DeleteApplicationUserCommand DeleteModel { get; set; }
        ApproveEnrolmentForm approveForm;
        Drawer approveDrawer;
        bool showApproveDrawer { get; set; } = false;

        EditEnrolmentForm editEnrolmentForm;
        Drawer editDrawer;
        bool showEditDrawer { get; set; } = false;
        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        string ApprovalWorkFlow = string.Empty;
        string LoggedInUserId = string.Empty;
        bool showStatusType { get; set; } = false;
        ClaimsPrincipal CurrentUser { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //if (EnrolmentFIlter == "PENDING_APPROVAL")
            //{
            //	showStatusType = true;
            //}
            await GetCurrentUser();
            ApproveModel = new MemberApprovalDTO();
            UpdateModel = new UpdateMemberProfileCommand();
            DeleteModel = new DeleteApplicationUserCommand();

            kycFilter = new WhereFilter
            {
                Field = nameof(MemberProfileMasterView.Status), // awaiting Ky
                Operator = "equal",
                value = EnrolmentFIlter
            };
            statusFilter = new WhereFilter
            {
                Field = nameof(MemberProfileMasterView.Status),
                Operator = "equal",
                value = EnrolmentFIlter
            };
            isAdminFilter = new WhereFilter
            {
                Field = nameof(MemberProfileMasterView.ApplicationUserId_IsAdmin),
                Operator = "equal",
                value = false
            };

            QueryGrid = new Query().Where(kycFilter);
            QueryGrid = new Query().Where(kycFilter.And(statusFilter.And(isAdminFilter)));
            await _auditLogService.LogAudit("Accessed Members' list.", "Accessed Cooperate members' list.", "Security",
                "NA, readonly request", CurrentUser);
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
                BrowserDimension = await BrowserService.GetDimensions();
                approveDrawer.Width = (int)(BrowserDimension.Width * 0.25);
                editDrawer.Width = (int)(BrowserDimension.Width * 0.48);
                editRoleDrawer.Width = (int)(BrowserDimension.Width * 0.20);
                var bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);
                //StateHasChanged();
                await OnRefresh();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected async Task ApproveActionRefreshGrid()
        {
            StateHasChanged();
            await OnRefresh();
        }

        public void ActionCompletedHandler(ActionEventArgs<MemberProfileMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<MemberProfileMasterView> Args)
        {
            //Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        async Task onApprove()
        {
            showApproveDrawer = true;
        }

        async Task onApproveDone()
        {
            showApproveDrawer = false;
        }

        async Task onEdit()
        {
            showEditDrawer = true;
        }

        async Task onEditDone()
        {
            showEditDrawer = false;
        }

        async Task onCreate()
        {
            showEditDrawer = true;
        }

        async Task onEditRole()
        {
            showEditRoleDrawer = true;
        }

        async Task onEditRoleDone()
        {
            showEditRoleDrawer = false;
        }
        async Task onCreateDone()
        {
            showEditDrawer = false;
        }

        private async Task OnApproveEnrolment(MemberProfileMasterView row)
        {
            ApproveModel = new MemberApprovalDTO();
            ApproveModel.Status = row.Status;
            ApproveModel.UserId = row.ApplicationUserId;
            ApproveModel.ProfileId = row.Id;
            var payload = JsonSerializer.Serialize(ApproveModel);
            await _auditLogService.LogAudit("Member Enrollment Approval.",
                $"Approved member with ID- {row.PrimaryEmail}.", "Security", payload, CurrentUser);
            await onApprove();
        }

        private async Task OnViewRow(MemberProfileMasterView row)
        {
        }
        private async Task OnEditRole(MemberProfileMasterView row)
        {
            UpdateApplicationUserModel = Mapper.Map<UpdateApplicationUserCommand>(row);
            QueryParameter = $"$filter=userid eq '{row.ApplicationUserId}'";
            var rsp = await DataService.GetResponse<List<ApplicationUserRoleViewModel>>(nameof(ApplicationUserRole),
                QueryParameter);
            UpdateRolesForUserModel = new UpdateRolesForUser();
            if (rsp.IsSuccessStatusCode)
            {
                List<ApplicationUserRoleViewModel> rspResponse =
                    JsonSerializer.Deserialize<List<ApplicationUserRoleViewModel>>(rsp.Content.ToJson());
                if (rspResponse != null)
                {
                    if (rspResponse.Any())
                    {
                        var userRoles = rspResponse.Where(f => f.UserId == row.ApplicationUserId).ToList();
                        List<string> roleIds = new List<string>();
                        foreach (var item in userRoles)
                        {
                            roleIds.Add(item.RoleId);
                        }

                        UpdateRolesForUserModel.RoleId = roleIds.ToArray();
                        UpdateRolesForUserModel.UserId = row.ApplicationUserId;
                    }

                    await onEditRole();
                }
            }
            else
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }

                await onEditRole();
            }

            var payload = JsonSerializer.Serialize(UpdateModel);
            await _auditLogService.LogAudit("User Role Update.", $"Updated roles for user with ID- {row.PrimaryEmail}.",
                "Security", payload, CurrentUser);
        }
        private async Task OnEditRow(MemberProfileMasterView row)
        {
            var user = CurrentUser;
            if (user.Identity.IsAuthenticated)
            {
                MemberEmail = row.ApplicationUserId_Email;
                UpdateModel = Mapper.Map<UpdateMemberProfileCommand>(row);
                UpdateModel.UpdatedByUserId = user.FindFirstValue(ClaimTypes.Sid);
                UpdateModel.RowVersion = row.RowVersion;
                UpdateModel.RetireeNumber = row.RetireeNumber;
                UpdateModel.MembershipId = row.MembershipId;
                UpdateModel.SecondaryPhone = row.SecondaryPhone ?? row.ApplicationUserId_SecondaryPhone;
                UpdateModel.PrimaryPhone = row.PrimaryPhone ?? row.ApplicationUserId_PhoneNumber;
                UpdateModel.DepartmentId = row.DepartmentId;
            }

            var payload = JsonSerializer.Serialize(UpdateModel);
            await _auditLogService.LogAudit("Member Enrollment Edit.",
                $"Initiated edit of information for member with ID- {row.PrimaryEmail}.", "Security", payload,
                CurrentUser);
            await onApprove();
            await onEdit();
        }

        private async Task OnDeleteRow(MemberProfileMasterView row)
        {
            bool isOk = false;
            isOk = await modalService.ConfirmAsync(new ConfirmOptions()
            {
                Title = "Are you sure?",
                Content = "You will not be able to recover this record after deleting!",
            });


            if (isOk)
            {
                DeleteModel.Id = row.Id;

                var rsp = await DataService.Delete<DeleteApplicationUserCommand, CommandResult<string>>(
                    nameof(ApplicationUser), DeleteModel);

                if (!rsp.IsSuccessStatusCode)
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error deleting record",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Deleted",
                        Description = rsp.Content.Response, //"Record deleted.",
                        NotificationType = NotificationType.Success
                    });
                    await OnRefresh();
                }
            }
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query();
            QueryGrid = new Query().Where(kycFilter.And(statusFilter.And(isAdminFilter)));
            grid.Refresh();

            await Task.CompletedTask;
        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
                else
                    await OnRefresh();
            }
        }

        private async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter roleFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileMasterView.FirstName),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = "RetireeNumber",
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = "FirstName",
                    Operator = "contains",
                    value = searchText
                };


                QueryGrid = new Query().Where(
                    isAdminFilter.And(
                        kycFilter.And(statusFilter.And(nameFilter.Or(roleFilter).Or(descriptionFilter)))));
            }
            else
            {
                await OnRefresh();
            }
        }

        private async Task OnClearSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();
            }
        }

        private async Task OnRetireMemberRow(MemberProfileMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateMemberProfileCommand>(row);
            var payload = JsonSerializer.Serialize(row);
            await _auditLogService.LogAudit("Member Retirement.",
                $"Executed retirement for member with ID- {row.PrimaryEmail}.", "Security", payload, CurrentUser);

            await SwitchToRetiree();
        }

        private async Task SwitchToRetiree()
        {
            try
            {
                var user = CurrentUser;

                if (user.Identity.IsAuthenticated)
                {
                    LoggedInUserId = user.FindFirstValue(ClaimTypes.Sid);
                    if (!string.IsNullOrEmpty(LoggedInUserId))
                    {
                        await GetApprovalWorkFlow();
                        CreateRetireeSwitchCommand command = new CreateRetireeSwitchCommand()
                        {
                            MemberProfileId = UpdateModel.Id,
                            //ApprovalWorkflowId = ApprovalWorkFlow,
                            Description = "Member Retirement Request",
                            InitiatedBy = LoggedInUserId
                        };

                        var payload = JsonSerializer.Serialize(command);
                        await _auditLogService.LogAudit("Retiree Switching.",
                            $"Initiated retirement switch for member with ID- {command.MemberProfileId}.", "Security",
                            payload, CurrentUser);

                        var rsp = await DataService
                            .ProcessRequest<CreateRetireeSwitchCommand, CommandResult<MemberProfileViewModel>>(
                                "MemberProfile", "switchToRetiree", command);

                        Logger.LogInformation($"rsp.IsSuccessStatusCode->{rsp.IsSuccessStatusCode}");
                        Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(rsp?.Content)}");
                        Logger.LogInformation($"error->{JsonSerializer.Serialize(rsp?.Error?.Content)}");

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
                            await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Success",
                                Description = "Request to switch to Retiree was successful",
                                NotificationType = NotificationType.Success
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetApprovalWorkFlow()
        {
            var rsp = await DataService.GetMasterView<List<ApprovalWorkflowViewModel>>("ApprovalWorkflows");

            if (rsp.IsSuccessStatusCode)
            {
                List<ApprovalWorkflowViewModel> rspResponse =
                    JsonSerializer.Deserialize<List<ApprovalWorkflowViewModel>>(rsp.Content.ToJson());
                if (rspResponse != null)
                {
                    if (rspResponse.Any())
                    {
                        ApprovalWorkFlow = rspResponse.FirstOrDefault().Id;
                    }
                }
            }
            else
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }
            }
        }
    }
}