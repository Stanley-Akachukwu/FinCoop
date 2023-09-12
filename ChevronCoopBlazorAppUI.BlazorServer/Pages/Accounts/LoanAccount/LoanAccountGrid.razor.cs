using AntDesign;
using AP.ChevronCoop.AppDomain.Documents.CustomerDocuments;
using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Util;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Security.Claims;
using System.Text.Json;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount
{
    public partial class LoanAccountGrid
    {
        [Inject]
        ILogger<LoanAccountGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        Drawer createDrawer;
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

        CreateLoanOffsetCommand Command { get; set; }
        public CreateCustomerPaymentDocumentCommand CreateCommand { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        public EventCallback<bool> OnUpdateDocumentChanged { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        MemberProfileMasterView MemberProfile { get; set; }
        LoanApplicationMasterView LoanApplicationMasterView { get; set; }
        string ApplicationUserId { get; set; }
        bool AccountIsProvided { get; set; }
        List<string> allowedOffsetType { get; set; } = new List<string>();
        List<string> loanRepaymentMode { get; set; } = new List<string>();
        public string companyTransferAccount { get; set; }

        string combobox_preferredSpecialAccounts;
        string combobox_preferredAccounts;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;
        bool showIfTransfer { get; set; } = false;
        bool showPopup { get; set; } = false;
        bool showFileUploadError { get; set; } = false;
        bool showFileUploadSuccess { get; set; } = false;
        bool showDocumentError { get; set; } = false;
        bool showUploadSuccessful { get; set; } = false;
        string ErrorMessage { get; set; } = string.Empty;
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        string All { get; set; } = string.Empty;
        string ActiveAccounts { get; set; } = string.Empty;
        string InActiveAccounts { get; set; } = string.Empty;
        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<LoanAccountMasterView> grid;
        public LoanOffSetDTO Model { get; set; }
        public UploadReceiptViewModel UploadModel { get; set; }
        public EventCallback<LoanOffSetDTO> ModelChanged { get; set; }
        public List<LoanAccountMasterView> LoanAccountMasterView1 = new List<LoanAccountMasterView>();
        public List<LoanAccountMasterView> LoanAccountMasterViewSrc = new List<LoanAccountMasterView>();
        [Parameter]
        public string filter { get; set; }
        private WhereFilter statusFilter { get; set; }
        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'&$orderby=DateCreated desc";

        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;

        private string bearToken { get; set; }
        public string CustomerId { get; set; }

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

                //await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            Model = new LoanOffSetDTO();
            Model.DeductionStartAfter = DateTime.Now.Date;
            MemberProfile = new MemberProfileMasterView();
            await GetCurrentUser();
            QueryGrid = new Query();
            await GetProfile();
            await LoadDropDown();
            await GetLoanAccountMasterView();
            await ExecuteFilteringAsync();
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
        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task LoadDropDown()
        {
            combobox_preferredAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(SavingsAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
            Logger.LogInformation($"url->{System.Text.Json.JsonSerializer.Serialize(combobox_preferredAccounts)}");
            combobox_preferredSpecialAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(SpecialDepositAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
            allowedOffsetType = System.Enum.GetNames(typeof(AllowedOffsetType)).ToList();
            loanRepaymentMode = System.Enum.GetNames(typeof(LoanRepaymentMode)).ToList();

        }
        private async Task ExecuteFilteringAsync()
        {
           
        }

        public void ActivateTab()
        {
            All = Inactive;
            ActiveAccounts = Inactive;
            InActiveAccounts = Inactive;
        }
        public void ShowSpecialDepositAccounts()
        {
            showSpecialDepositAccount = true;
            showPreferredAccount = false;
            showIfTransfer = false;
            Model.LoanRepaymentMode = LoanRepaymentMode.SPECIAL_DEPOSIT;
            StateHasChanged();
        }

        public void ShowPreferredAccounts()
        {
            showSpecialDepositAccount = false;
            showPreferredAccount = true;
            showIfTransfer = false;
            Model.LoanRepaymentMode = LoanRepaymentMode.SAVINGS;
            StateHasChanged();
        }

        private void SpecialBankValueChangeHandler()
        {
            showSpecialDepositAccount = false;
            showPreferredAccount = false;
            showIfTransfer = true;
            Model.LoanRepaymentMode = LoanRepaymentMode.BANK_TRANSFER;
            StateHasChanged();
        }

        private void BankValueChangeHandler(ChangeEventArgs<string, SavingsAccountMasterView> args)
        {
            Model.SavingsAccountId = args.Value;
            Model.CustomerPaymentDocumentId = null;
            Model.SpecialDepositAccountId = null;
            Model.UseSpecialDeposit = false;
        }

        public async Task OnChangeDocumentUpload(UploadChangeEventArgs args)
        {
            UploadModel = new UploadReceiptViewModel();
            ErrorMessage = string.Empty;
            showFileUploadError = false;
            await InvokeAsync(StateHasChanged);

            var file = args?.Files?.FirstOrDefault();
            if (file != null)
            {
                var response = UploadUtility.ValidateFile(file);
                if (string.IsNullOrEmpty(response))
                {

                    UploadModel.Document = file.Stream.ToArray();
                    UploadModel.FileName = file.FileInfo.Name;
                    UploadModel.MimeType = file.FileInfo.Type;
                    UploadModel.FileSize = file.FileInfo.Size;
                    //push to the server
                    await MapToCreateCommand();
                    var rsp = await DataService
                        .Create<CreateCustomerPaymentDocumentCommand, CommandResult<CustomerPaymentDocumentViewModel>>(
                            nameof(CustomerPaymentDocument), CreateCommand);

                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreateCommand)}");
                    if (!rsp.IsSuccessStatusCode)
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                        {
                            msg = rspContent.ValidationErrors.FirstOrDefault().Error;
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
                        showUploadSuccessful = true;
                        await OnUpdateDocumentChanged.InvokeAsync(true);
                        CustomerPaymentDocumentViewModel rspResponse =
                            JsonSerializer.Deserialize<CustomerPaymentDocumentViewModel>(rsp.Content.Response.ToJson());
                        Model.CustomerPaymentDocumentId = rspResponse.Id;
                    }

                }
                else
                {
                    ErrorMessage = response;
                    showFileUploadError = true;
                }

                ErrorMessage = "Upload was successful";
                showFileUploadSuccess = true;
            }
            else
            {
                ErrorMessage = "File not found";
                showFileUploadError = true;
            }

            await InvokeAsync(StateHasChanged);
        }

        private void TransferValueChangeHandler(ChangeEventArgs<string, SavingsAccountMasterView> args)
        {
            Model.CustomerPaymentDocumentId = args.Value;
            Model.SavingsAccountId = null;
            Model.SpecialDepositAccountId = null;
            Model.UseSpecialDeposit = false;
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

        public async Task GetProfile()
        {
            ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(ApplicationUserId))
            {
                _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
            }

            var rsp = await DataService.GetValue<List<MemberProfileMasterView>>(
                nameof(MemberProfileMasterView), ApplicationUserId);


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
                List<MemberProfileMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new MemberProfileMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    MemberProfile = Mapper.Map<MemberProfileMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }

        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();
        }

        private async Task ProceedToOffsetAsync()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                MapToCommand();
                if (Command.AllowedOffsetType == AllowedOffsetType.FULL && Model.PrincipalBalance < Command.OffsetAmount)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Offset amount can ot be greater than the loan principal balance!",
                        NotificationType = NotificationType.Error
                    });
                    StateHasChanged();
                }

                Logger.LogInformation($"rsp submitpayload->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                var rsp =
                    await DataService.Create<CreateLoanOffsetCommand, CommandResult<LoanOffsetViewModel>>(
                        nameof(LoanOffset), Command);
                Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Applied for Executive Loan Product",
                        "Applied for Executive Loan Product", "Accounts", "NA, readonly request", CurrentUser);
                    showPopup = true;
                    StateHasChanged();
                }
                else
                {
                    var rspContent =
                        System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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
                    StateHasChanged();
                }
            }

            showCreateDrawer = false;
        }

        //private async Task OnRefresh()
        //{
        //    searchText = string.Empty;
        //    QueryGrid = new Query();
        //    grid.Refresh();

        //    await Task.CompletedTask;
        //}

        private async Task Done()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/account/loanproductsapplications", true);
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


                QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));
            }
        }

        private async Task OnClearSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                
            }
        }

        async Task onShowOffSetDrawer(LoanAccountMasterView row)
        {
            var rsp = await DataService.GetValue<List<LoanProductMasterView>>(
               nameof(LoanProductMasterView), "id", row.LoanApplicationId_LoanProductId);

            if (!rsp.IsSuccessStatusCode)
            {
                LoanProductMasterView = new LoanProductMasterView();
            }
            else
            {
                List<LoanProductMasterView> rspResponse =
                   JsonSerializer.Deserialize<List<LoanProductMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().Id) &&
                    rspResponse.FirstOrDefault().Id == row.LoanApplicationId_LoanProductId)
                {
                    LoanProductMasterView = Mapper.Map<LoanProductMasterView>(rspResponse.FirstOrDefault());
                }
            }

            Model.PrincipalBalance = row.Principal;
            Model.OffSetRepaymentDate = row.RepaymentCommencementDate.Date;
            Model.OffsetToBeCalculatedAfter = row.RepaymentCommencementDate.Date;
            Model.LoanAccountId = row.Id;
            showCreateDrawer = true;
        }

        async Task onCreate()
        {
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
        }

        private async Task OnProceedToTopUp(LoanAccountMasterView row)
        {
            var loanId = row.Id;
            var url = $"/account/loanTopUp/{loanId}";
            _navigationManager.NavigateTo(url, true);
        }

        private async Task OnProceedLoanAccountDetails(LoanAccountMasterView row)
        {
            var url = $"/LoanAccountDetails/{row.Id}";
            _navigationManager.NavigateTo(url, true);
        }

        public void MapToCommand()
        {
            Command = new CreateLoanOffsetCommand()
            {
                LoanAccountId = Model.LoanAccountId,
                OffsetAmount = Model.OffsetAmount,
                SpecialDepositAccountId = Model.SpecialDepositId,
                CustomerPaymentDocumentId = Model.CustomerPaymentDocumentId,
                SavingsAccountId = Model.SavingsAccountId,
                AllowedOffsetType =
                    (AllowedOffsetType)System.Enum.Parse(typeof(AllowedOffsetType), Model.AllowedOffsetType, true),
                PrincipalBalance = Model.PrincipalBalance,
                OffsetToBeCalculatedAfter = Model.OffsetToBeCalculatedAfter,
                DeductionStartAfter = Model.DeductionStartAfter,
                LoanRepaymentMode = Model.LoanRepaymentMode,
                OffSetRepaymentDate = Model.OffSetRepaymentDate,
                //TotalOffsetCharges = Model.TotalOffsetCharges,
            };
        }

        private async Task MapToCreateCommand()
        {
            CreateCommand = new CreateCustomerPaymentDocumentCommand();
            CreateCommand.FileSize = (int)UploadModel.FileSize;
            CreateCommand.MimeType = UploadModel.MimeType;
            CreateCommand.FileName = UploadModel.FileName;
            CreateCommand.DateCreated = DateTime.UtcNow;
            CreateCommand.CustomerId = CustomerId;
            CreateCommand.Document = UploadModel.Document.ToString();
            CreateCommand.DocumentType = MemberPaymentUploadType.LOAN_OFFSET;
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<LoanAccountMasterView> Args)
        {
            Args.Data.AccountNo = Args.Data.AccountNo != null ? Args.Data.AccountNo.Replace("_", " ") : "";
        }

        private async Task CloseUploadPopUp()
        {
            showUploadSuccessful = false;
            await InvokeAsync(StateHasChanged);
        }
        public async Task GetLoanAccountMasterView()
        {
            var rsp = await DataService.GetValue<List<LoanAccountMasterView>>(
                nameof(LoanAccountMasterView), "CustomerId", CustomerId);
            if (rsp.IsSuccessStatusCode)
            {
                LoanAccountMasterViewSrc = new List<LoanAccountMasterView>();

                LoanAccountMasterViewSrc =
                    JsonSerializer.Deserialize<List<LoanAccountMasterView>>(rsp.Content.ToJson());
                LoanAccountMasterView1 =
                    LoanAccountMasterViewSrc.OrderByDescending(c => c.DateCreated).ToList();
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