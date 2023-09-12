using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;
using ApplicationRoleClaimResponse = ChevronCoop.Web.AppUI.BlazorServer.Data.ApplicationRoleClaimResponse;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Roles
{
    public partial class RoleGrid
    {
        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        [Inject]
        ILogger<RoleGrid> Logger { get; set; }

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

        //[Inject]
        //SessionService SessionService { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }


        SfGrid<ApplicationRoleMasterView> grid;

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApplicationRoleMasterView)}";


        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        CreateApplicationRoleCommand CreateModel { get; set; }
        UpdateApplicationRoleCommand UpdateModel { get; set; }
        DeleteApplicationRoleCommand DeleteModel { get; set; }

        RoleCreatorForm createForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        RoleEditForm editForm;
        Drawer editDrawer;

        private WhereFilter activeFilter;
        bool showEditDrawer { get; set; } = false;


        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;


        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }
        public string[] StoredPermissionIds { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            CreateModel = new CreateApplicationRoleCommand();
            UpdateModel = new UpdateApplicationRoleCommand();
            DeleteModel = new DeleteApplicationRoleCommand();
            QueryGrid = new Query();

            //activeFilter = new WhereFilter
            //{
            //    Field = nameof(ApplicationRoleMasterView.IsSystemRole),
            //    Operator = "equal",
            //    value = false
            //};
            await _auditLogService.LogAudit("Accessed Roles' list.", "Accessed the system roles.", "Security",
                "NA, readonly request", CurrentUser);

            //QueryGrid = new Query().Where(activeFilter);
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


        public void ActionCompletedHandler(ActionEventArgs<ApplicationRoleMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        private async Task atRedirectToPermissions()
        {
            UriHelper.NavigateTo("/security/permission");
        }


        async Task onCreate()
        {
            showCreateDrawer = true;
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
            //createForm.Errors = null;
        }

        async Task onEdit()
        {
            var result = await sessionStorage.GetAsync<List<ApplicationRoleClaimResponse>>("Permissions");
            if (result.Success)
            {
                var resultData = result.Value;
                UpdateModel.PermissionIds = new List<string>();
                UpdateModel.PermissionIds.AddRange(
                    resultData.Where(f => f.Id != null).Select(f => f.Id.Trim()).ToList());
            }

            showEditDrawer = true;
        }

        async Task onEditDone()
        {
            UpdateModel = new UpdateApplicationRoleCommand();
            showEditDrawer = false;
            // editForm.Errors = null;
        }


        private async Task OnAddRow()
        {
            CreateModel = new CreateApplicationRoleCommand();
            await onCreate();
        }


        private async Task OnViewRow(ApplicationRoleMasterView row)
        {
        }

        private async Task OnEditRow(ApplicationRoleMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateApplicationRoleCommand>(row);

            var rsp = await DataService.GetRolesPermissions<CommandResult<ApplicationRoleModel>>(row.Id);

            if (rsp.IsSuccessStatusCode)
            {
                ApplicationRoleModel rspResponse =
                    JsonSerializer.Deserialize<ApplicationRoleModel>(rsp.Content.Response.ToJson());
                if (rspResponse != null)
                {
                    if (rspResponse.RoleClaims.Any())
                    {
                        var result =
                            JsonSerializer.Deserialize<List<ApplicationRoleClaimResponse>>(
                                rspResponse.RoleClaims.ToJson());
                        //var resultData = result.Value;
                        UpdateModel.PermissionIds = new List<string>();
                        UpdateModel.PermissionIds.AddRange(result.Where(f => f.Id != null).Select(f => f.Id.Trim())
                            .ToList());
                        StoredPermissionIds = new string[] { };
                        StoredPermissionIds = UpdateModel.PermissionIds.ToArray();
                        //await sessionStorage.SetAsync("Permissions", rspResponse.RoleClaims);
                    }
                }
            }

            showEditDrawer = true;


            //await onEdit();
        }


        private async Task OnDeleteRow(ApplicationRoleMasterView row)
        {
            bool isOk = false;

            isOk = await modalService.ConfirmAsync(new ConfirmOptions()
            {
                Title = "Are you sure?",
                //Icon = icon,
                Content = "You will not be able to recover this record after deleting!",
            });


            if (isOk)
            {
                DeleteModel.Id = row.Id;
                //DeleteModel.DeleteKey = EntityUtils.GenerateSequentialId().ToString(); ;

                var rsp =
                    await DataService.Delete<DeleteApplicationRoleCommand, CommandResult<string>>(nameof(Permission),
                        DeleteModel);


                if (!rsp.IsSuccessStatusCode)
                {
                    //Console.WriteLine($"rsp content->{rsp.Content}");
                    //Console.WriteLine($"error->{rsp.Error.Content}");

                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    //Console.WriteLine($"error content->{rspContent}");
                    //Console.WriteLine($"error msg->{rspContent.Response}");

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
            //Console.WriteLine($"OnRefresh(), searchText->{searchText}");

            searchText = string.Empty;
            QueryGrid = new Query();
            grid.Refresh();

            await Task.CompletedTask;
        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            //Console.WriteLine($"OnSearchEnter, searchText->{searchText}");

            //Console.WriteLine($"code->{e.Code}, key-> {e.Key}");

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
            //Console.WriteLine($"OnSearch, searchText->{searchText}");

            //if (!string.IsNullOrWhiteSpace(searchText))
            //{

            //	WhereFilter roleFilter = new WhereFilter
            //	{
            //		Field = nameof(ApplicationRoleMasterView.Name),
            //		Operator = "contains",
            //		value = searchText
            //	};

            //	WhereFilter nameFilter = new WhereFilter
            //	{
            //		Field = "Name",
            //		Operator = "contains",
            //		value = searchText
            //	};

            //	WhereFilter descriptionFilter = new WhereFilter
            //	{
            //		Field = "Description",
            //		Operator = "contains",
            //		value = searchText
            //	};


            //	QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));

            //	//this.grid.Refresh();
            //}
            //else
            //{
            //	await OnRefresh();
            //}

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter nameFilter = new WhereFilter
                {
                    Field = nameof(ApplicationRoleMasterView.Name),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter codeFilter = new WhereFilter
                {
                    Field = nameof(ApplicationRoleMasterView.Code),
                    Operator = "contains",
                    value = searchText
                };

                QueryGrid = new Query().Where(activeFilter.And(nameFilter.Or(codeFilter)));
            }
            else
            {
                await OnRefresh();
            }
        }

        private async Task OnClearSearch()
        {
            //Console.WriteLine($"OnClearSearch, searchText->{searchText}");

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();
            }
        }
    }
}