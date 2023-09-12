using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Audit
{
    public partial class AuditTrailGrid
    {
        string notificationText;
        bool showPopup = false;
        public int RowCounter = 0;

        [Inject]
        ILogger<AuditTrailGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }


        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        AuditViewForm viewForm;
        Drawer viewDrawer;

        private WhereFilter activeFilter;
        bool showViewDrawer { get; set; } = false;


        string viewFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;

        ErrorDetails errors;

        protected DateTime startDate { get; set; }

        protected DateTime endDate { get; set; }


        AuditTrailMasterView ViewModel { get; set; }


        SfGrid<AuditTrailMasterView> grid;
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(AuditTrailMasterView)}";


        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        private WhereFilter kycFilter;

        private WhereFilter startDateFilter;

        private WhereFilter endDateFilter;

        string ErrorDetails = "";

        string QueryParameter;

        [Inject]

        NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();
                viewDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                await OnRefresh();
            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        protected override async Task OnInitializedAsync()
        {
            endDate = DateTime.Now;
            startDate = DateTime.Now.AddDays(-5); //five days ago set as start date
            ViewModel = new AuditTrailMasterView();

            QueryGrid = new Query();
        }


        public void ActionCompletedHandler(ActionEventArgs<AuditTrailMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }


        private async Task SearchByDate()
        {
            //QueryGrid = new Query();

            startDateFilter = new WhereFilter
            {
                Field = nameof(AuditTrailMasterView.DateCreated),
                //Operator = "greater than or equal",
                Operator = "greaterthanorequal",
                value = startDate,
                IgnoreCase = true
            };

            endDateFilter = new WhereFilter
            {
                Field = nameof(AuditTrailMasterView.DateCreated),
                Operator = "lessthanorequal",
                value = endDate,
                IgnoreCase = true
            };
            QueryGrid = new Query().Where(startDateFilter.Or(endDateFilter));
            //await CheckState();
        }


        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query();
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
                WhereFilter actionFilter = new WhereFilter
                {
                    Field = nameof(AuditTrailMasterView.Action),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter moduleFilter = new WhereFilter
                {
                    Field = nameof(AuditTrailMasterView.Module),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter emailFilter = new WhereFilter
                {
                    Field = nameof(AuditTrailMasterView.ApplicationUserId_Email),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter ipAddressFilter = new WhereFilter
                {
                    Field = nameof(AuditTrailMasterView.IPAddress),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter fullTextFilter = new WhereFilter
                {
                    Field = nameof(AuditTrailMasterView.FullText),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter statusFilter = new WhereFilter
                {
                    Field = nameof(AuditTrailMasterView.Action),
                    Operator = "contains",
                    value = searchText
                };
                QueryGrid = new Query().Where(ipAddressFilter.Or(fullTextFilter).Or(statusFilter).Or(moduleFilter)
                    .Or(emailFilter).Or(actionFilter));
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

        async Task onViewDone()
        {
            showViewDrawer = false;
            // editForm.Errors = null;
        }

        private async Task OnViewRow(AuditTrailMasterView row)
        {
            //UpdateModel = Mapper.Map<UpdateApplicationRoleCommand>(row);

            ViewModel = row;


            await onView();
        }

        async Task onView()
        {
            showViewDrawer = true;
        }
    }
}