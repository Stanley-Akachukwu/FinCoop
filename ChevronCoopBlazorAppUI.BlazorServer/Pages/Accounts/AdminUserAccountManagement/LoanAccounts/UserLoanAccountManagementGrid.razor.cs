using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using OneOf.Types;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.AdminUserAccountManagement.LoanAccounts
{

    public partial class UserLoanAccountManagementGrid
    {
        [Inject]
        ILogger<UserLoanAccountManagementGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        Drawer addDrawer;
        bool showCreateDrawer { get; set; } = false;

        BrowserDimension BrowserDimension;
        private FluentValidationValidator? _fluentValidationValidator;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public LoanProductMasterView LoanProductMasterView { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }
        LoanApplicationMasterView LoanApplicationMasterView { get; set; }
        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        public EventCallback<bool> OnUpdateDocumentChanged { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        string ApplicationUserId { get; set; }
        bool AccountIsProvided { get; set; }
        List<string> allowedOffsetType { get; set; } = new List<string>();
        List<string> loanRepaymentMode { get; set; } = new List<string>();
        public string companyTransferAccount { get; set; }

        string combobox_preferredSpecialAccounts;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;
        bool showIfTransfer { get; set; } = false;
        bool showPopup { get; set; } = false;
        bool showFileUploadError { get; set; } = false;
        bool showFileUploadSuccess { get; set; } = false;
        bool showDocumentError { get; set; } = false;
        bool showUploadSuccessful { get; set; } = false;
        string ErrorMessage { get; set; } = string.Empty;
        string All { get; set; } = string.Empty;
        string ActiveAccounts { get; set; } = string.Empty;
        string InActiveAccounts { get; set; } = string.Empty;
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        [Inject]
        private IMasterViews _MasterView { get; set; }
        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<LoanAccountMasterView> grid;
        public LoanProductMasterView loanProductMasterView { get; set; }
        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanAccountMasterView)}?$orderby=DateCreated desc";

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;

        private string bearToken { get; set; }
        public CreateLoanDisbursementCommand CreateModel { get; set; }
        public LoanDisbursementDTO Model { get; set; }

        public LoanAccountMasterView loanAccountMasterView { get; set; }
        string SaveBtn = "Save";
        bool SaveDisabled = false;
        bool showAddDrawer = false;
        public List<LoanAccountMasterView> LoanAccountMasterView1 = new List<LoanAccountMasterView>();
        public List<LoanAccountMasterView> LoanAccountMasterViewSrc = new List<LoanAccountMasterView>();
        string DROPDOWN_API_RESOURCE_COMPANY_ACCOUNT { get; set; }
        string DROPDOWN_API_RESOURCE_DEPOSIT_PRODUCT { get; set; }
        public GetLoanProductViewModel GetLoanProductViewModel { get; set; }
        string combobox_loanproducts;
        string CustomerId { get; set; }
        string combobox_preferredAccounts;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                await GetCurrentUser();
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);

                BrowserDimension = await BrowserService.GetDimensions();
                if (addDrawer != null)
                    addDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            GetLoanProductViewModel = new GetLoanProductViewModel();
            Model = new LoanDisbursementDTO();
            loanProductMasterView = new LoanProductMasterView();
            await LoadDropDown();
            await GetLoanAccountMasterView();
        }
        async Task onFiltering(string filter)
        {
            LoanAccountMasterView1 = new List<LoanAccountMasterView>();
            switch (filter)
            {
                case "active":
                    LoanAccountMasterView1 = LoanAccountMasterViewSrc.Where(x => !x.IsClosed).ToList();
                    ActivateTab();
                    ActiveAccounts = Active;
                    break;
                case "in-active":
                    LoanAccountMasterView1 = LoanAccountMasterViewSrc.Where(x => x.IsClosed).ToList();
                    ActivateTab();
                    InActiveAccounts = Active;
                    break;
                default:
                case "all":
                    LoanAccountMasterView1 = LoanAccountMasterViewSrc.ToList();
                    ActivateTab();
                    All = Active;
                    break;
            }
        }
        public async Task LoadDropDown()
        {
            DROPDOWN_API_RESOURCE_COMPANY_ACCOUNT = $"{Config.ODATA_VIEWS_HOST}/{nameof(CompanyBankAccountMasterView)}";

        }
        public void ActivateTab()
        {
            All = Inactive;
            ActiveAccounts = Inactive;
            InActiveAccounts = Inactive;
        }
        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
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


     

        private async Task Done()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                _navigationManager.NavigateTo("/account/userloanaccounts", true);
            }
        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
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
            }
        }

        private async Task OnClearSearch()
        {
            
        }



        async Task onCreate()
        {
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
        }







        public void CellInfoHandler(QueryCellInfoEventArgs<LoanAccountMasterView> Args)
        {
            Args.Data.AccountNo = Args.Data.AccountNo != null ? Args.Data.AccountNo.Replace("_", " ") : "";
        }

        public async Task LoadSpecificAccount(string loanProductId)
        {
            try
            {
                loanProductMasterView = new LoanProductMasterView();

                //              var rsp = await DataService.GetOdataRecord<ODataResponse<LoanProductMasterView>>(
                //nameof(LoanProductMasterView), "Id", loanProductId);
                var rsp = await DataService.GetProduct<CommandResult<GetLoanProductViewModel>>(nameof(LoanProduct),
                loanProductId);
                if (rsp.IsSuccessStatusCode)
                {

                    if (rsp.Content != null)
                    {
                        GetLoanProductViewModel = JsonSerializer.Deserialize<GetLoanProductViewModel>(rsp.Content.Response.ToJson());

                        Model.Amount = $"{GetLoanProductViewModel.CurrencySymbol}{loanAccountMasterView.Principal.ToString("N2")}";
                        Model.LoanAccountId = loanAccountMasterView.Id;
                        Model.CustomerBankAccountId = loanAccountMasterView.DestinationAccountId_BankId;
                        if (loanAccountMasterView.UseSpecialDeposit)
                        {
                            Model.CustomerDisburmentAccount_Name = $"{loanAccountMasterView.SpecialDepositAccountId_AccountNo}";
                            Model.DisbursementMode = AP.ChevronCoop.Entities.LoanDisbursementMode.SPECIAL_DEPOSIT.ToString();
                        }

                        else
                        {
                            Model.CustomerDisburmentAccount_Name = $"{loanAccountMasterView.DestinationAccountId_AccountNumber} - {loanAccountMasterView.DestinationAccountId_AccountName}";
                            Model.DisbursementMode = AP.ChevronCoop.Entities.LoanDisbursementMode.BANK_TRANSFER.ToString();
                        }
                        Model.DisbursementAccountId = GetLoanProductViewModel.DisbursementAccountId;

                        showAddDrawer = true;
                        StateHasChanged();

                    }
                }
            }
            catch (Exception exp)
            {
            }
        }
        public async Task Disbursement(LoanAccountMasterView row)
        {
            showAddDrawer = true;
            loanAccountMasterView = row;
            await LoadSpecificAccount(row.LoanApplicationId_LoanProductId);
        }
        async Task onAddDone()
        {
            Model = new LoanDisbursementDTO();
            SaveBtn = "Save";
            SaveDisabled = false;
            showAddDrawer = false;
        }
        public async Task OnSave()
        {
            MapToCommand();
            if (CreateModel.LoanAccountId == null)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Please retry",
                    NotificationType = NotificationType.Info
                });
                return;
            }
            else
            {


                SaveBtn = "Saving ...";
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{JsonSerializer.Serialize(CreateModel)}");
                var rsp = await DataService
                    .Create<CreateLoanDisbursementCommand, CommandResult<LoanDisbursementViewModel>>(
                        nameof(LoanDisbursement), CreateModel);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreateModel)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Loan disbursement",
                        "Loan disbursement", "Accounts", "NA, readonly request", CurrentUser);
                    showPopup = true;
                    StateHasChanged();
                }
                else
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;
                    Logger.LogInformation($"ErrorMessage->{msg}");
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                    SaveBtn = "Save";
                    StateHasChanged();
                }
            }

            StateHasChanged();
        }
        public void MapToCommand()
        {
            CreateModel = new CreateLoanDisbursementCommand()
            {
                DateCreated = DateTime.Now,
                Amount = loanAccountMasterView.Principal,
                SpecialDepositAccountId = loanAccountMasterView.SpecialDepositAccountId,
                CustomerBankAccountId = loanAccountMasterView.DestinationAccountId,
                DisbursementDate = DateTime.Now,
                LoanAccountId = loanAccountMasterView.Id,
                DisbursementMode = loanAccountMasterView.UseSpecialDeposit ? LoanDisbursementMode.SPECIAL_DEPOSIT : LoanDisbursementMode.BANK_TRANSFER,
                DisbursementAccountId = GetLoanProductViewModel.DisbursementAccountId,
                CreatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid)
            };
        }
        private async Task OnProceedLoanAccountDetails(LoanAccountMasterView row)
        {
            var url = $"/AdminLoanDashboard/{row.Id}";
            _navigationManager.NavigateTo(url, true);
        }
        public async Task GetLoanAccountMasterView()
        {
            LoanAccountMasterViewSrc = new List<LoanAccountMasterView>();
            LoanAccountMasterViewSrc = await _MasterView.GetCustomMasterViewEntityAllFields<LoanAccountMasterView>(nameof(LoanAccountMasterView));
           
            if (LoanAccountMasterViewSrc != null)
            {
                LoanAccountMasterView1 = LoanAccountMasterViewSrc.OrderBy(dt => dt.DateCreated).ToList();
                ActivateTab();
                All = Active;
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
        }
    }
}