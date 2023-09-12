using AntDesign;
using AP.ChevronCoop.AppDomain.Documents.CustomerDocuments;
using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Util;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.Drawals
{
    public partial class LoanOffset
    {
        [Parameter]
        public LoanOffSetDTO Model { get; set; }
        public bool IsPartialOffset { get; set; } = true;
        public UploadReceiptViewModel UploadModel { get; set; }
        public EventCallback<LoanOffSetDTO> ModelChanged { get; set; }
        [Inject]
        ILogger<LoanOffset> Logger { get; set; }
        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }
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
        [Parameter]
        public LoanProductMasterView LoanProductMasterView { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        CreateLoanOffsetCommand Command { get; set; }
        public CreateCustomerPaymentDocumentCommand CreateCommand { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        public EventCallback<bool> OnUpdateDocumentChanged { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        MemberProfileMasterView MemberProfile { get; set; }
        LoanApplicationMasterView LoanApplicationMasterView { get; set; }
        string ApplicationUserId { get; set; }
        bool? AccountIsProvided { get; set; } = null;
        List<string> allowedOffsetType { get; set; } = new List<string>();
        List<string> loanRepaymentMode { get; set; } = new List<string>();
        public string companyTransferAccount { get; set; }
        bool isPaymentWithPayroll { get; set; } = false;
        string combobox_preferredSpecialAccounts;
        string combobox_preferredAccounts;
        string combobox_loanSchedule;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;
        bool showIfTransfer { get; set; } = false;
        bool showPopup { get; set; } = false;
        bool showFileUploadError { get; set; } = false;
        bool showFileUploadSuccess { get; set; } = false;
        bool showDocumentError { get; set; } = false;
        bool showUploadSuccessful { get; set; } = false;
        string ErrorMessage { get; set; } = string.Empty;
        private string bearToken { get; set; }
        public string CustomerId { get; set; }
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        private bool isAdmin { get; set; } = false;
        string ErrorDetails = "";
        ErrorDetails errors;
        [Inject]
        WebConfigHelper Config { get; set; }
        public LoanAccountMasterView AccountsMasterView { get; set; }
        public LoanRepaymentScheduleMasterView RepaymentScheduleMasterView { get; set; }
        [Inject]
        TempObjectService tempService { get; set; }
        public string UploadedImage { get; set; } = "";
        List<LoanRepaymentScheduleMasterView> LoanRepaymentScheduleMasterViews = new List<LoanRepaymentScheduleMasterView>();
        [Inject]
        IMasterViews MasterViews { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Model = new LoanOffSetDTO();
            Model.DeductionStartAfter = DateTime.Now.Date;
            MemberProfile = new MemberProfileMasterView();
            AccountsMasterView = new LoanAccountMasterView();
            await HideButton();
            await GetCurrentUser();
            AccountsMasterView = (LoanAccountMasterView)tempService.GetLoanAccountMasterViewTempObject();
            await GetPaymentScheduleDropDown();
            await LoadDropDown();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
       
        public async Task GetPaymentScheduleDropDown()
        {
            if (AccountsMasterView != null && !string.IsNullOrEmpty(AccountsMasterView.Id))
            {
                LoanRepaymentScheduleMasterViews = await MasterViews.Get2CustomMasterVieWithBoolean<LoanRepaymentScheduleMasterView>(nameof(LoanRepaymentScheduleMasterView), "LoanAccountId", AccountsMasterView.Id, "IsPaid ", "false", DatabaseFields.LoanSchedulesProperties);

                LoanRepaymentScheduleMasterViews = LoanRepaymentScheduleMasterViews
                    .OrderBy(x=>x.DueDate)
                    .ToList();

                foreach (var item in LoanRepaymentScheduleMasterViews)
                {
                    item.Caption = $"[{item.RepaymentNo}] [{item.DueDate.ToString("dd-MMMM-yyyy")}] ({item.PeriodPayment.ToString("N2")})";
                }
            }
           
        }
        public async Task LoadDropDown()
        {
            combobox_preferredAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(SavingsAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
            Logger.LogInformation($"url->{System.Text.Json.JsonSerializer.Serialize(combobox_preferredAccounts)}");
            combobox_preferredSpecialAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(SpecialDepositAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
           var allowedOffsetTypeName = System.Enum.GetNames(typeof(AllowedOffsetType)).ToList();
            allowedOffsetType = allowedOffsetTypeName.Where(name => name != AllowedOffsetType.NONE.ToString()).ToList();
            loanRepaymentMode = System.Enum.GetNames(typeof(LoanRepaymentMode)).ToList();
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
            Model.BankTransferAmount = Model.OffsetAmount;
            StateHasChanged();
        }
        private void UpdateTransferAmount()
        {
            Model.BankTransferAmount = Model.OffsetAmount;
        }
        private async Task GetScheduledPaymentAmount(ChangeEventArgs<string, LoanRepaymentScheduleMasterView> args)
        {
            var loanScheduleId = args.Value;
            var rsp = await DataService.GetValue<List<LoanRepaymentScheduleMasterView>>(
                nameof(LoanRepaymentScheduleMasterView), "id", loanScheduleId);
            if (!rsp.IsSuccessStatusCode)
            {
                RepaymentScheduleMasterView = new LoanRepaymentScheduleMasterView();
            }
            else
            {
                List<LoanRepaymentScheduleMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<LoanRepaymentScheduleMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().Id) &&
                    rspResponse.FirstOrDefault().Id == loanScheduleId)
                {
                    RepaymentScheduleMasterView = Mapper.Map<LoanRepaymentScheduleMasterView>(rspResponse.FirstOrDefault());
                    Model.OffsetAmount = RepaymentScheduleMasterView.PeriodPayment;
                }
            }
        }
        private void GetOffsetType(ChangeEventArgs<string, string> args)
        {
            var val = (AllowedOffsetType)System.Enum.Parse(typeof(AllowedOffsetType), args.Value, true);
            if (val == AllowedOffsetType.FULL)
            {
                Model.OffsetAmount = Model.LedgerBalance;
                IsPartialOffset = false;
                isPaymentWithPayroll = false;
                StateHasChanged();
            }
            else if (val == AllowedOffsetType.IN_LIEU_OF_PAYROLL)
            {
                isPaymentWithPayroll = true;
                IsPartialOffset = false;
            }
            else
            {
                Model.OffsetAmount = 0;
                IsPartialOffset = true;
                isPaymentWithPayroll = false;
            }
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
            if (CurrentUser != null && !isAdmin)
            {
                CustomerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault().Value;
            }
        }

        public void ActionCompletedHandler(ActionEventArgs<LoanAccountMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task HideButton()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                var admin = CurrentUser.Claims.Where(f => f.Type == "IsAdmin").FirstOrDefault();
                if (admin != null && admin.Value.ToLower() == "true")
                    isAdmin = true;
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
                else if(Command.AllowedOffsetType == AllowedOffsetType.PARTIAL && Model.PrincipalBalance < Command.OffsetAmount)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Offset amount can ot be greater than the loan principal balance!",
                        NotificationType = NotificationType.Error
                    });
                    StateHasChanged();
                }
                else
                {
                    Logger.LogInformation($"rsp submitpayload->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                    var rsp =
                        await DataService.Create<CreateLoanOffsetCommand, CommandResult<LoanOffsetViewModel>>(
                            nameof(LoanOffset), Command);
                    Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                    if (rsp.IsSuccessStatusCode)
                    {
                        await _auditLogService.LogAudit("Loan Offset Application",
                            "Offset application successful", "Accounts", "NA, readonly request", CurrentUser);
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
            }

            showCreateDrawer = false;
        }

        private async Task Done()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                _navigationManager.NavigateTo("/account/loanproductsapplications", true);
            }
        }

        async Task onCreate()
        {
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
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
                RepaymentSchedules = new List<string> { Model.RepaymentScheduleId }
            };
        }

        private async Task OnRemoveHandler(RemovingEventArgs args)
        {
            UploadedImage = CreateCommand.Document = "";
            await InvokeAsync(StateHasChanged);

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
        private async Task CloseUploadPopUp()
        {
            showUploadSuccessful = false;
            await InvokeAsync(StateHasChanged);
        }
    }
}
