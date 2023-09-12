using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals
{
    public partial class ApprovalCategories
    {
        string notificationText;
        bool showPopup = false;
        public int RowCounter = 0;

        [Inject]
        ILogger<ApprovalStatsMasterView> Logger { get; set; }

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


        SfGrid<ApprovalStatsMasterView> grid;
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalStatsMasterView)}";
        string API_URL => $"{Config.API_HOST}";


        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        private WhereFilter kycFilter;


        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        public string[] SelectedRoles { get; set; }
        bool showEditRoleDrawer { get; set; } = false;

        string ApplicationUserId;
        string QueryParameter;

        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        bool disableStaffPermission { get; set; } = false;
        bool enableStaffPermission { get; set; } = false;
        bool addStaffPermission { get; set; } = false;
        bool updateStaffPermission { get; set; } = false;
        bool updateStaffRolePermission { get; set; } = false;
        public int RowCountAI = 0;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                await OnRefresh();
            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        protected override async Task OnInitializedAsync()
        {
            //Console.WriteLine($"API HOST->{Config.ODATA_VIEWS_HOST}, ODATA HOST-> {GRID_API_RESOURCE}");

            // UserRoleModel = new List<ApprovalConsolidationMasterView>();

            QueryGrid = new Query();


            //  QueryGrid = new Query().Where(kycFilter);
            await CheckState();
        }


        public void ActionCompletedHandler(ActionEventArgs<ApprovalStatsMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<ApprovalStatsMasterView> Args)
        {
            //Args.Data.ApprovalType = Regex.Replace(Args.Data.ApprovalType, "(\\B[A-Z])", " $1");
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }


        private async Task OnViewRow(ApprovalStatsMasterView row)
        {
            _navigationManager.NavigateTo($"/chevroncoop/approval/{row.ApprovalType}");
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            //  QueryGrid = new Query().Where(kycFilter);
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
                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = nameof(ApprovalStatsMasterView.Description),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter typeFilter = new WhereFilter
                {
                    Field = nameof(ApprovalStatsMasterView.ApprovalType),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter sizeFilter = new WhereFilter
                {
                    Field = nameof(ApprovalStatsMasterView.Size),
                    Operator = "contains",
                    value = searchText
                };
                QueryGrid = new Query().Where(kycFilter.And(descriptionFilter.Or(typeFilter).Or(sizeFilter)));
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
    }
}