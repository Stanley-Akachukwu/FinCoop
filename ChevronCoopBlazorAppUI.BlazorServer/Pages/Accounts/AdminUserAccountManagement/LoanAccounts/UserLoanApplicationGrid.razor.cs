using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.AdminUserAccountManagement.LoanAccounts
{

    public partial class UserLoanApplicationGrid
    {
        [Inject]
        ILogger<UserLoanApplicationGrid> Logger { get; set; }

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

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        string combobox_preferredSpecialAccounts;
        string combobox_preferredAccounts;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;

        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<LoanApplicationMasterView> grid;

        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanApplicationMasterView)}";

        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;

        private string bearToken { get; set; }
        public string CustomerId { get; set; }
        [Parameter]
        public string filter { get; set; }

        private WhereFilter statusFilter { get; set; }
        private WhereFilter searchApprovedFilter { get; set; }
        private WhereFilter searchRejectedFilter { get; set; }
        private WhereFilter searchAwaitingGuarantorApprovalFilter { get; set; }
        private WhereFilter searchAwaitingAdminApprovalFilter { get; set; }
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        string All { get; set; } = string.Empty;
        string PendingGuarantor { get; set; } = string.Empty;
        string Pending { get; set; } = string.Empty;
        string Approved { get; set; } = string.Empty;
        string Rejected { get; set; } = string.Empty;
        string Published { get; set; } = string.Empty;
        string LoanProductId { get; set; } = string.Empty;
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

                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {

            await GetCurrentUser();
            QueryGrid = new Query();
            await ExecuteFilteringAsync();

        }



        public void ShowSpecialDepositAccounts()
        {
            showSpecialDepositAccount = true;
            showPreferredAccount = false;
            StateHasChanged();
        }

        public void ShowPreferredAccounts()
        {
            showSpecialDepositAccount = false;
            showPreferredAccount = true;
            StateHasChanged();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public void ActionCompletedHandler(ActionEventArgs<LoanApplicationMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridLoanApplication_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Loan_Application.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridLoanApplication_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Loan_Application.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }

        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();
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
                WhereFilter roleFilter = new WhereFilter
                {
                    Field = nameof(LoanApplicationMasterView.CustomerId_LastName),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = nameof(LoanApplicationMasterView.CustomerId_FirstName),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = nameof(LoanApplicationMasterView.LoanProductId_LoanProductType),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter statusSearchFilter = new WhereFilter
                {
                    Field = nameof(LoanApplicationMasterView.Status),
                    Operator = "contains",
                    value = searchText
                };


                QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter).Or(statusSearchFilter));
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

        async Task onCreate()
        {
        }


        public void CellInfoHandler(QueryCellInfoEventArgs<LoanApplicationMasterView> Args)
        {
            Args.Data.LoanProductId_LoanProductType = Args.Data.LoanProductId_LoanProductType != null
                ? Args.Data.LoanProductId_LoanProductType.Replace("_", " ")
                : "";
        }
        async Task onFiltering(string filter)
        {
            _navigationManager.NavigateTo($"/account/userloanapplications/{filter}", forceLoad: true);
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<LoanProductMasterView> Args)
        {
            Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }

        private async Task ExecuteFilteringAsync()
        {
            switch (filter)
            {
                case nameof(LoanApplicationStatus.APPROVED):
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(LoanApplicationMasterView.Status),
                        Operator = "equal",
                        value = nameof(LoanApplicationStatus.APPROVED)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Approved = Active;
                    break;
                case nameof(LoanApplicationStatus.REJECTED):
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(LoanApplicationMasterView.Status),
                        Operator = "equal",
                        value = nameof(LoanApplicationStatus.REJECTED)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Rejected = Active;
                    break;
                case nameof(LoanApplicationStatus.AWAITING_GUARANTOR_APPROVAL):
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(LoanApplicationMasterView.Status),
                        Operator = "equal",
                        value = nameof(LoanApplicationStatus.AWAITING_GUARANTOR_APPROVAL)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    PendingGuarantor = Active;
                    break;
                case nameof(LoanApplicationStatus.PENDING):
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(LoanApplicationMasterView.Status),
                        Operator = "equal",
                        value = nameof(LoanApplicationStatus.PENDING)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Pending = Active;
                    break;
                default:
                case "all":
                    QueryGrid = new Query();
                    ActivateTab();
                    All = Active;
                    break;
            }
        }
        public void ActivateTab()
        {
            All = Inactive;
            Approved = Inactive;
            Pending = Inactive;
            Rejected = Inactive;
            PendingGuarantor = Inactive;
        }
    }
}
