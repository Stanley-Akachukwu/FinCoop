using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.GuarantorApproval
{
    public partial class GuarantorApprovalGrid
    {
        bool showDetailPage { get; set; } = false;
        public LoanApplicationGuarantorApprovalMasterView MasterView { get; set; }
        string notificationText;
        bool showPopup = false;

        [Inject]
        ILogger<GuarantorApprovalGrid> Logger { get; set; }

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

        string statusName { get; set; }


        SfGrid<LoanApplicationGuarantorApprovalMasterView> grid;

        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanApplicationGuarantorApprovalMasterView)}?$filter=GuarantorId eq '{CustomerId}'&$orderby=DateCreated desc";

        private Query QueryGrid;

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        LoanApplicationGuarantorApprovalMasterView ApprovaleModel { get; set; }

        private WhereFilter guarantorFilter;

        string ErrorDetails = "";
        ErrorDetails errors;

        string ApplicationUserId;
        string QueryParameter;

        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }


        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        private string bearToken { get; set; }
        public string CustomerId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();

            ApprovaleModel = new LoanApplicationGuarantorApprovalMasterView();


            //guarantorFilter = new WhereFilter
            //{
            //    Field = nameof(LoanApplicationGuarantorMasterView.Id),
            //    Operator = "equal",
            //    value = true
            //};
            //QueryGrid = new Query().Where(guarantorFilter);
            await _auditLogService.LogAudit("Accessed Guarantor Request list.", "Accessed Guarantor Request list.",
                "Security", "NA, readonly request", CurrentUser);
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                CustomerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault().Value;
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

                //await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task ShowDetail()
        {
            showDetailPage = true;
            StateHasChanged();
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query().Where(guarantorFilter);
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
                WhereFilter memberFullNameFilter = new WhereFilter
                {
                    Field = nameof(LoanApplicationGuarantorApprovalMasterView.CustomerId_FirstName),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter membershipIdFilter = new WhereFilter
                {
                    Field = nameof(LoanApplicationGuarantorApprovalMasterView.LoanApplicationId_ApplicationNo),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter typeFilter = new WhereFilter
                {
                    Field = nameof(LoanApplicationGuarantorApprovalMasterView.GuarantorApprovalType),
                    Operator = "contains",
                    value = searchText
                };

                QueryGrid = new Query().Where(
                    guarantorFilter.And(memberFullNameFilter.Or(membershipIdFilter.Or(typeFilter))));
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

        public async Task OnTabCategory(string status)
        {
            statusName = status;
            await GroupFilter();
        }

        private async Task onShowDetailRow(LoanApplicationGuarantorApprovalMasterView row)
        {
            _navigationManager.NavigateTo($"/security/guarantorapprovaldetail/{row.Id}");
        }

        public async Task GroupFilter()
        {
            //switch (statusName)
            //{
            //    case nameof(BaseApprovalStatus.REJECTED):
            //        var statusFilter = new WhereFilter
            //        {
            //            Field = nameof(LoanApplicationGuarantorApprovalMasterView.Status),
            //            Operator = "equal",
            //            value = nameof(BaseApprovalStatus.REJECTED)
            //        };
            //        QueryGrid = new Query().Where(guarantorFilter.And(statusFilter));
            //        //ActivateTab();
            //        //Approved = Active;
            //        break;
            //    case nameof(BaseApprovalStatus.APPROVED):
            //        statusFilter = new WhereFilter
            //        {
            //            Field = nameof(LoanApplicationGuarantorApprovalMasterView.Status),
            //            Operator = "equal",
            //            value = nameof(BaseApprovalStatus.APPROVED)
            //        };
            //        QueryGrid = new Query().Where(guarantorFilter.And(statusFilter));
            //        //ActivateTab();
            //        //Approved = Active;
            //        break;
            //    case nameof(BaseApprovalStatus.PENDING):
            //        statusFilter = new WhereFilter
            //        {
            //            Field = nameof(LoanApplicationGuarantorApprovalMasterView.Status),
            //            Operator = "equal",
            //            value = nameof(BaseApprovalStatus.APPROVED)
            //        };
            //        QueryGrid = new Query().Where(guarantorFilter.And(statusFilter));
            //        //ActivateTab();
            //        //Approved = Active;
            //        break;

            //    default:
            //    case "all":
            //        QueryGrid = new Query().Where(guarantorFilter);
            //        break;
            //}
        }

        public void ActionCompletedHandler(ActionEventArgs<LoanApplicationGuarantorApprovalMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<LoanApplicationGuarantorApprovalMasterView> Args)
        {
            Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridGuarantorRequest_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Loan_Application_Guarantor_approval.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridGuarantorRequest_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "gridGuarantorRequest.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }
    }
}