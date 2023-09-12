using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User
{
    public partial class UserGrid
    {
        string notificationText;
        bool showPopup = false;
        public int RowCounter = 0;

        [Inject]
        ILogger<UserGrid> Logger { get; set; }

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


        SfGrid<MemberProfileMasterView> grid;

        //string GRID_API_RESOURCE =>
        //$"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileMasterView)}?$orderby=DateCreated desc";

        private Query QueryGrid;

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        CreateApplicationUserCommand CreateModel { get; set; }
        UpdateApplicationUserCommand UpdateModel { get; set; }
        DeleteApplicationUserCommand DeleteModel { get; set; }

        List<ApplicationUserRoleViewModel> UserRoleModel { get; set; }
        //UpdateApplicationUserRoleCommand UpdateUserRoleModel { get; set; }


        UserCreateForm createForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        UserEditForm editForm;
        Drawer editDrawer;
        bool showEditDrawer { get; set; } = false;
        private WhereFilter kycFilter;


        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        public string[] SelectedRoles { get; set; }
        bool showEditRoleDrawer { get; set; } = false;
        Drawer editRoleDrawer;
        EditUserRoleForm editUserRoleForm;
        string ApplicationUserId;
        string QueryParameter;

        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        bool disableStaffPermission { get; set; } = false;
        bool enableStaffPermission { get; set; } = false;
        bool addStaffPermission { get; set; } = false;
        bool updateStaffPermission { get; set; } = false;
        bool updateStaffRolePermission { get; set; } = false;
        public int RowCountAI = 0;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IUserDownLoadService UserDownLoadService { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        public UpdateRolesForUser UpdateRolesForUserModel { get; set; }

        private string bearToken { get; set; }

        [Inject] IMasterViews _MasterViews { get; set; }

        public List<MemberProfileMasterView> _MemberProfileMasterView { get; set; } = new List<MemberProfileMasterView>();
        public List<MemberProfileMasterView> _MemberProfileMasterViewSrc { get; set; } = new List<MemberProfileMasterView>();

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();

            CreateModel = new CreateApplicationUserCommand();

            UpdateModel = new UpdateApplicationUserCommand();

            DeleteModel = new DeleteApplicationUserCommand();
            UserRoleModel = new List<ApplicationUserRoleViewModel>();


            //QueryGrid = new Query();
            //await _auditLogService.LogAudit("Accessed Users' list.", "Accessed Admin Users list.", "Security", "NA, readonly request", CurrentUser);

            kycFilter = new WhereFilter
            {
                Field = nameof(MemberProfileMasterView.ApplicationUserId_IsAdmin),
                Operator = "equal",
                value = true
            };
            QueryGrid = new Query().Where(kycFilter);
            await _auditLogService.LogAudit("Accessed Users' list.", "Accessed Admin Users list.", "Security",
                "NA, readonly request", CurrentUser);
            await CheckState();

            await GetUsers();
        }

        private async Task GetUsers()
        {
            _MemberProfileMasterViewSrc = await _MasterViews.GetCustomMasterViewEntityWithFilterAndAllFieldsUsingBool<MemberProfileMasterView>(nameof(MemberProfileMasterView), "ApplicationUserId_IsAdmin");

            _MemberProfileMasterView = _MemberProfileMasterViewSrc.OrderByDescending(dt => dt.DateCreated).ToList();
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
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);
                BrowserDimension = await BrowserService.GetDimensions();
                createDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                editDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                editRoleDrawer.Width = (int)(BrowserDimension.Width * 0.20);



                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task OnEditRole(MemberProfileMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateApplicationUserCommand>(row);
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

        async Task onEditRole()
        {
            showEditRoleDrawer = true;
        }

        async Task onEditRoleDone()
        {
            showEditRoleDrawer = false;
        }

        private async Task DownloadList()
        {
            await _auditLogService.LogAudit("User List Download", "Executed bulk download of user list", "Security",
                "NA, file download request", CurrentUser);
            await UserDownLoadService.ExportUsersWhoHaveNotCompletedKYCToCSV();
        }


        public void ActionCompletedHandler(ActionEventArgs<MemberProfileMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");

        }

        public void CellInfoHandler(QueryCellInfoEventArgs<MemberProfileMasterView> Args)
        {
            Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }


        async Task onCreate()
        {
            showCreateDrawer = true;
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
        }

        async Task onEdit()
        {
            showEditDrawer = true;
        }

        async Task onEditDone()
        {
            showEditDrawer = false;
            // editForm.Errors = null;
        }


        private async Task OnAddRow()
        {
            CreateModel = new CreateApplicationUserCommand();
            await onCreate();
        }


        private async Task OnViewRow(MemberProfileMasterView row)
        {
        }

        private async Task OnEditRow(MemberProfileMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateApplicationUserCommand>(row);


            await onEdit();
        }


        private async Task OnDeleteRow(MemberProfileMasterView row)
        {
            bool isOk = false;

            isOk = await modalService.ConfirmAsync(new ConfirmOptions()
            {
                Title = "Are you sure?",
                //Icon = icon,
                Content = "You will not be able to recover this record after deleting!",
            });

            await _auditLogService.LogAudit("User Deletion.", $"Deleted user with ID- {row.ApplicationUserId}.",
                "Security", $"userId:{row.ApplicationUserId} and Email:{row.PrimaryEmail} ", CurrentUser);


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
            QueryGrid = new Query().Where(kycFilter);
            grid.Refresh();
            await Task.CompletedTask;
        }


        private async Task OnSearch(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _MemberProfileMasterView = _MemberProfileMasterViewSrc.OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                _MemberProfileMasterView = _MemberProfileMasterViewSrc
                    .Where(c => (c.FullName?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ApplicationUserId_Email?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.MembershipId?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.PrimaryPhone?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                )

                    .Take(5)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
        }

        private async Task OnClearSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();
            }
        }

        private async Task onChangeStatus(MemberProfileMasterView row, string status)
        {
            UpDateStatus Model = new UpDateStatus()
            {
                UserId = row.ApplicationUserId,
                Status = status
            };
            var rsp = await DataService.ChangeStatus<UpDateStatus, CommandResult<ApplicationUserRoleViewModel>>(Model);

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
                    Message = "Error",
                    Description = msg,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                notificationText = $"Operation was successfull";
                showPopup = true;
                _navigationManager.NavigateTo("/security/users", true);
                var payload = JsonSerializer.Serialize(Model);
                await _auditLogService.LogAudit("Status Update.",
                    $"Updated status for user with ID- {row.PrimaryEmail}.", "Security", payload, CurrentUser);
            }
        }

        private async Task CheckState()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                if (user.HasClaim(ClaimTypes.Role, PermissionConfig.SecurityStaffDisable))
                    disableStaffPermission = true;
                if (user.HasClaim(ClaimTypes.Role, PermissionConfig.SecurityStaffEnable))
                    enableStaffPermission = true;
                if (user.HasClaim(ClaimTypes.Role, PermissionConfig.SecurityStaffAddnew))
                    addStaffPermission = true;
                if (user.HasClaim(ClaimTypes.Role, PermissionConfig.SecurityStaffUpdate))
                    updateStaffPermission = true;
                if (user.HasClaim(ClaimTypes.Role, PermissionConfig.SecurityStaffRole))
                    updateStaffRolePermission = true;
            }
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridMemberProfile_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Staff_Users.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridMemberProfile_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Staff_Users.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }

        public async Task OnPage() => _navigationManager.NavigateTo("/Security/users", true);


    }
}