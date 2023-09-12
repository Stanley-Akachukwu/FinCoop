using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{

    public partial class CustomerLoanAccountComponent
    {

        [Inject]
        WebConfigHelper Config { get; set; }
        [Parameter]
        public string LoanProductId { get; set; }
        [Parameter]
        public EventCallback<string> LoanProductIdChanged { get; set; }

        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        SfGrid<LoanAccountMasterView> grid;
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanAccountMasterView)}?$filter=LoanApplicationId_LoanProductId eq '{LoanProductId}' and IsClosed eq false &$orderby=DateCreated desc";
        [Inject]
        IEntityDataService DataService { get; set; }
        [Inject]
        NotificationService notificationService { get; set; }
        public LoanApplicationMasterView LoanApplicationMasterView { get; set; }
        [Inject]
        AutoMapper.IMapper Mapper { get; set; }
        [Parameter]
        public EventCallback<int> OnCustomerAccountsChanged { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            LoanApplicationMasterView = new LoanApplicationMasterView();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {

                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }
        public void ActionCompletedHandler(ActionEventArgs<LoanAccountMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridLoanApplication_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Loan_Account.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridLoanApplication_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Loan_Account.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }
        public void CellInfoHandler(QueryCellInfoEventArgs<LoanAccountMasterView> Args)
        {
            Args.Data.AccountNo = Args.Data.AccountNo != null ? Args.Data.AccountNo.Replace("_", " ") : "";
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
                    Field = nameof(CurrencyMasterView.Code),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = "Name",
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = "Description",
                    Operator = "contains",
                    value = searchText
                };


                QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));
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
        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query();
            grid.Refresh();

            await Task.CompletedTask;
        }

        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();
        }
        private async Task OnProceedLoanAccountDetails(LoanAccountMasterView row)
        {
            var Id = row.LoanApplicationId;

            var rsp = await DataService.GetValue<List<LoanApplicationMasterView>>(
                nameof(LoanApplicationMasterView), "id", Id);
            if (!rsp.IsSuccessStatusCode)
            {
                LoanApplicationMasterView = new LoanApplicationMasterView();
            }
            else
            {
                List<LoanApplicationMasterView> rspResponseLoanApplication = JsonSerializer.Deserialize<List<LoanApplicationMasterView>>(rsp.Content.ToJson());
                if (rspResponseLoanApplication != null && rspResponseLoanApplication.Count > 0 && !string.IsNullOrEmpty(rspResponseLoanApplication.FirstOrDefault().Id) && rspResponseLoanApplication.FirstOrDefault().Id == Id)
                {
                    LoanApplicationMasterView = Mapper.Map<LoanApplicationMasterView>(rspResponseLoanApplication.FirstOrDefault());
                }
            }
            var url = $"/AdminLoanDashboard/{LoanApplicationMasterView.LoanProductId_LoanProductType}/{Id}";
            _navigationManager.NavigateTo(url, true);
        }

        public async Task OnPrevious()
        {
            await OnCustomerAccountsChanged.InvokeAsync(8);
        }


    }
}
