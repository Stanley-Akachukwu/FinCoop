using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class CreateLoanProductSetupForm
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private FluentValidationValidator? _fluentValidationValidator_TargetSetup;
        private FluentValidationValidator? _fluentValidationValidator_OffSet;
        private FluentValidationValidator? _fluentValidationValidator_TopUp;
        private FluentValidationValidator? _fluentValidationValidator_WhenDue;
        private FluentValidationValidator? _fluentValidationValidator_Guarantor;
        private Query Query_Combo;
        bool showPopup = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        ILogger<CreateLoanProductSetupForm> Logger { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        bool showTopUpInfo { get; set; } = false;
        bool showTopUpInfoForm { get; set; } = false;
        bool showBasicInformation { get; set; } = false;
        bool showTargetSetup { get; set; } = false;
        bool showTargetSetupForm { get; set; } = false;
        bool showOffset { get; set; } = false;
        bool showOffsetForm { get; set; } = false;
        bool showDueForLoan { get; set; } = false;
        bool showDueForLoanForm { get; set; } = false;
        bool showEnableChargeWaitingForm { get; set; } = false;
        bool showEnableWaitingForm { get; set; } = false;
        bool showGuarantorInfoForm { get; set; } = false;
        bool showGuarantorInfo { get; set; } = false;
        bool showApproval { get; set; } = false;
        bool showBenefitCode { get; set; } = false;
        bool showOffsetAdmin { get; set; } = false;
        bool showAdminCharge { get; set; } = false;


        bool showTopUpInfoComplete { get; set; } = false;
        bool showBasicInformationComplete { get; set; } = false;
        bool showTargetSetupComplete { get; set; } = false;
        bool showOffsetComplete { get; set; } = false;
        bool showDueForLoanComplete { get; set; } = false;
        bool showGuarantorInfoComplete { get; set; } = false;
        bool showApprovalComplete { get; set; } = false;
        string combobox_Currency_res;
        string combobox_Loan_Product_Code_res;
        string combobox_Workflow;
        List<string> tenureUnit { get; set; } = new List<string>();
        List<string> loanProductType { get; set; } = new List<string>();
        List<string> memberType { get; set; } = new List<string>();
        List<string> depositProductType { get; set; } = new List<string>();
        List<string> interestMethod { get; set; } = new List<string>();
        List<string> daysInYear { get; set; } = new List<string>();
        List<string> BindMemberType { get; set; } = new List<string>();
        List<string> interestCalculationMethod { get; set; } = new List<string>();

        public string[] AdminChargeArray { get; set; }
        public string[] MemberTypeArray { get; set; }
        public string[] OffsetWaiverChargeArray { get; set; }
        public string[] OffsetProductTypeArray { get; set; }
        public string[] OffsetAdminChargeArray { get; set; }
        public string[] TopupAdminChargeArray { get; set; }
        public string[] WhenDueAdminChargeArray { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public CreateLoanProductCommand CreatLoanProductCommand { get; set; }
        public CreateLoanProductBasicInfoDTO Model { get; set; }
        public CreateLoanProductTargetSetupDTO TargetModel { get; set; }
        public CreateLoanProductOffSetInfoDTO OffSetInfoModel { get; set; }
        public CreateLoanProductTopUpDTO TopUpModel { get; set; }
        public CreateLoanProductWhenDueDTO WhenDueModel { get; set; }
        public CreateLoanProductGuarantorDTO GuarantorModel { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductBasicInfoDTO> ModelChanged { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        string ApprovalWorkFlow = string.Empty;

        [Inject]
        IEntityDataService DataService { get; set; }

        string DROPDOWN_API_RESOURCE_CHARGE { get; set; }
        string DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW { get; set; }
        string DROPDOWN_API_RESOURCE_COMPANY_ACCOUNT { get; set; }
        string DROPDOWN_API_RESOURCE_DEPOSIT_PRODUCT { get; set; }
        string SubmitButtonText { get; set; } = "Submit";
        string ApprovalWorkFlowId { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Model = new CreateDepositProductDTO();
            //InterestRates = new List<CreateDepositProductInterestDTO>();
            await GetCurrentUser();
            LoadDropDown();
            Model = new CreateLoanProductBasicInfoDTO();
            TargetModel = new CreateLoanProductTargetSetupDTO();
            OffSetInfoModel = new CreateLoanProductOffSetInfoDTO();
            TopUpModel = new CreateLoanProductTopUpDTO();
            CreatLoanProductCommand = new CreateLoanProductCommand();
            WhenDueModel = new CreateLoanProductWhenDueDTO();
            GuarantorModel = new CreateLoanProductGuarantorDTO();
            //await base.OnInitializedAsync();
            showBasicInformation = true;
            Model.InterestMethod = InterestMethod.SIMPLE.ToString();
            Model.RepaymentPeriod = Tenure.MONTHLY.ToString();
            //CreateInterestRate = new CreateDepositProductInterestDTO();
            //InterestRateCommand = new List<CreateDepositProductInterestRangeCommand>();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        private async Task ProceedToTarget()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                showBasicInformationComplete = true;
                showBasicInformation = false;
                showTargetSetup = true;
                showTopUpInfo = false;
                showApproval = false;
                showDueForLoan = false;
                showBenefitCode = false;
                showGuarantorInfo = false;
                showOffset = false;
                StateHasChanged();
                MapBasicInforToCommand();
            }
        }

        private async Task BackToBasic()
        {
            showBasicInformationComplete = false;
            showBasicInformation = true;
            showTargetSetup = false;
            showTopUpInfo = false;
            showApproval = false;
            showDueForLoan = false;
            showBenefitCode = false;
            showGuarantorInfo = false;
            showOffset = false;
            StateHasChanged();
        }

        private async Task ProceedToOffset()
        {
            if (await _fluentValidationValidator_TargetSetup.ValidateAsync())
            {
                showTargetSetupComplete = true;
                showOffset = true;
                showTargetSetup = false;
                showTopUpInfo = false;
                showApproval = false;
                showDueForLoan = false;
                showBenefitCode = false;
                showGuarantorInfo = false;
                StateHasChanged();
                MapTargetSetupToCommand();
            }
        }

        private async Task BackToTarget()
        {
            showTargetSetupComplete = false;
            showOffset = false;
            showTargetSetup = true;
            showTopUpInfo = false;
            showApproval = false;
            showDueForLoan = false;
            showBenefitCode = false;
            showGuarantorInfo = false;
            showOffset = false;
            StateHasChanged();
        }

        private async Task ProceedToTopUp()
        {
            if (await _fluentValidationValidator_OffSet.ValidateAsync())
            {
                showOffsetComplete = true;
                showTopUpInfo = true;
                showOffset = false;
                showApproval = false;
                showDueForLoan = false;
                showBenefitCode = false;
                showGuarantorInfo = false;
                showOffset = false;
                showBasicInformation = false;
                StateHasChanged();
                MapOffsetToCommand();
            }
        }

        private async Task BackToOffset()
        {
            showOffsetComplete = false;
            showOffset = true;
            showTopUpInfo = false;
            showApproval = false;
            showDueForLoan = false;
            showBenefitCode = false;
            showGuarantorInfo = false;
            showBasicInformation = false;
            StateHasChanged();
        }

        private async Task ProceedToWhenDue()
        {
            if (await _fluentValidationValidator_TopUp.ValidateAsync())
            {
                showTopUpInfoComplete = true;
                showDueForLoan = true;
                showTopUpInfo = false;
                showOffset = false;
                showApproval = false;
                showBenefitCode = false;
                showGuarantorInfo = false;
                showOffset = false;
                showBasicInformation = false;
                StateHasChanged();
                MapToUpToCommand();
            }
        }

        private async Task BackToTopUp()
        {
            showTopUpInfoComplete = false;
            showTopUpInfo = true;
            showDueForLoan = false;
            StateHasChanged();
        }

        private async Task ProceedToGuarantor()
        {
            if (await _fluentValidationValidator_WhenDue.ValidateAsync())
            {
                showDueForLoanComplete = true;
                showGuarantorInfo = true;
                showDueForLoan = false;

                showTopUpInfo = false;
                showOffset = false;
                showApproval = false;
                showBenefitCode = false;
                showOffset = false;
                showBasicInformation = false;
                StateHasChanged();
                MapWhenDueToCommand();
            }
        }

        private async Task BackToDue()
        {
            showDueForLoanComplete = false;
            showDueForLoan = true;
            showTopUpInfo = false;
            showOffset = false;
            showApproval = false;
            showBenefitCode = false;
            showGuarantorInfo = false;
            showOffset = false;
            showBasicInformation = false;
            StateHasChanged();
        }

        private async Task ProceedToApproval()
        {
            if (await _fluentValidationValidator_Guarantor.ValidateAsync())
            {
                showGuarantorInfoComplete = true;
                showApproval = true;
                showGuarantorInfo = false;

                showTopUpInfo = false;
                showOffset = false;
                showDueForLoan = false;
                showBenefitCode = false;
                showOffset = false;
                showBasicInformation = false;
                StateHasChanged();
                MapGuarantorToCommand();
            }
        }

        private async Task BackToGuarantor()
        {
            showGuarantorInfoComplete = false;
            showGuarantorInfo = true;
            showApproval = false;
            showTopUpInfo = false;
            showOffset = false;
            showDueForLoan = false;
            showBenefitCode = false;
            showOffset = false;
            showBasicInformation = false;
            StateHasChanged();
        }

        private async Task Submit()
        {
            try
            {
                if (string.IsNullOrEmpty(ApprovalWorkFlowId))
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Please select Approval workflow to continue",
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    SubmitButtonText = "Submitting ...";
                    StateHasChanged();
                    CreatLoanProductCommand.ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
                    //CreatLoanProductCommand.ApprovalWorkFlowId = "21f88701-858e-b14d-508e-03bc9965b36d";
                    CreatLoanProductCommand.ApprovalWorkFlowId = ApprovalWorkFlowId;
                    var rsp = await DataService.Create<CreateLoanProductCommand, LoanProductViewModel>(
                        nameof(LoanProduct), CreatLoanProductCommand);
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreatLoanProductCommand)}");
                    if (rsp.IsSuccessStatusCode)
                    {
                        await _auditLogService.LogAudit("Create Loan Product", "Create Loan Product", "Security",
                            "NA, readonly request", CurrentUser);
                        showPopup = true;
                        StateHasChanged();
                    }
                    else
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
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
                        SubmitButtonText = "Submit";
                        StateHasChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadDropDown()
        {
            combobox_Loan_Product_Code_res =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(GlobalCodeMasterView)}?$CodeType=LOAN_PRODUCT_TYPE";
            combobox_Currency_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CurrencyMasterView)}";
            DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW = $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalWorkflowMasterView)}";
            DROPDOWN_API_RESOURCE_CHARGE = $"{Config.ODATA_VIEWS_HOST}/{nameof(ChargeMasterView)}";
            tenureUnit = System.Enum.GetNames(typeof(Tenure)).Remove("DAILY_360").Remove("DAILY_365").Remove("DAILY_366").ToList();
            memberType = System.Enum.GetNames(typeof(MemberType)).ToList();
            memberType.Remove("ADMIN");
            memberType.Remove("MFB");
            depositProductType = System.Enum.GetNames(typeof(DepositProductType)).ToList();
            interestMethod = System.Enum.GetNames(typeof(InterestMethod)).ToList();
            loanProductType = System.Enum.GetNames(typeof(LoanProductType)).ToList();
            DROPDOWN_API_RESOURCE_COMPANY_ACCOUNT = $"{Config.ODATA_VIEWS_HOST}/{nameof(CompanyBankAccountMasterView)}";
            DROPDOWN_API_RESOURCE_DEPOSIT_PRODUCT = $"{Config.ODATA_VIEWS_HOST}/{nameof(DepositProductMasterView)}";
            daysInYear = System.Enum.GetNames(typeof(DaysInYear)).ToList();
            interestCalculationMethod = System.Enum.GetNames(typeof(InterestCalculationMethod)).ToList();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private void ValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue != null)
            {
                BindMemberType = new List<string>();

                if (selectedValue.Count() > 0)
                {
                    foreach (var item in selectedValue)
                    {
                        BindMemberType.Add(item);
                    }
                    Array.Clear(MemberTypeArray, 0, MemberTypeArray.Length);
                    MemberTypeArray = BindMemberType.ToArray();
                }
            }

        }

        private void AdminChargeValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue != null)
            {
                if (selectedValue.Count() > 0)
                {
                    Model.AdminCharges = new List<string>();

                    foreach (var item in selectedValue)
                    {
                        Model.AdminCharges.Add(item);
                    }
                    Array.Clear(AdminChargeArray, 0, AdminChargeArray.Length);
                    AdminChargeArray = Model.AdminCharges.ToArray();
                }
            }
        }

        public void MapBasicInforToCommand()
        {
            CreatLoanProductCommand.AdminCharges = new List<string>();
            CreatLoanProductCommand.AdminCharges = Model.AdminCharges;
            CreatLoanProductCommand.Code = Model.Code;
            CreatLoanProductCommand.DefaultCurrencyId = Model.DefaultCurrencyId;
            CreatLoanProductCommand.HasAdminCharges = Model.HasAdminCharges;
            //CreatLoanProductCommand.InterestMethod = (InterestMethod)System.Enum.Parse(typeof(InterestMethod), Model.InterestMethod, true);
            CreatLoanProductCommand.MemberTypes = new List<string>();
            CreatLoanProductCommand.MemberTypes = BindMemberType;
            CreatLoanProductCommand.MaxTenureValue = Model.MaxTenureValue;
            CreatLoanProductCommand.MinTenureValue = Model.MinTenureValue;
            CreatLoanProductCommand.PrincipalMaxLimit = Model.PrincipalMaxLimit;
            CreatLoanProductCommand.PrincipalMultiple = Model.PrincipalMultiple;
            CreatLoanProductCommand.PrincipalLimitType = "sample"; //Model.PrincipalLimitType;
            CreatLoanProductCommand.PrincipalMinLimit = Model.PrincipalMinLimit;
            CreatLoanProductCommand.QualificationMinBalancePercentage = Model.QualificationMinBalancePercentage;
            CreatLoanProductCommand.QualificationTargetProduct =
                (DepositProductType)System.Enum.Parse(typeof(DepositProductType), Model.QualificationTargetProduct,
                    true);
            CreatLoanProductCommand.InterestRate = Model.InterestRate;
            //CreatLoanProductCommand.TenureUnit = Model.TenureUnit;
            CreatLoanProductCommand.TenureUnit =
                (Tenure)System.Enum.Parse(typeof(Tenure), Model.TenureUnit, true);
            CreatLoanProductCommand.Name = Model.Name;
            CreatLoanProductCommand.Description = Model.Description;
            CreatLoanProductCommand.ShortName = Model.ShortName;
            CreatLoanProductCommand.LoanProductType =
                (LoanProductType)System.Enum.Parse(typeof(LoanProductType), Model.LoanProductType, true);
            CreatLoanProductCommand.PayrollCode = Model.PayrollCode;
            CreatLoanProductCommand.BankDepositAccountId = Model.CompanyDepositAccountId;
            CreatLoanProductCommand.DisbursementAccountId = Model.CompanyDisbursementAccountId;
            CreatLoanProductCommand.InterestCalculationMethod =
                (InterestCalculationMethod)System.Enum.Parse(typeof(InterestCalculationMethod),
                    Model.InterestCalculationMethod, true);
            CreatLoanProductCommand.DaysInYear = (int)Model.DaysInYear;
            CreatLoanProductCommand.RepaymentPeriod = Model.RepaymentPeriod != null ? (Tenure)System.Enum.Parse(typeof(Tenure), Model.RepaymentPeriod, true) : Tenure.NONE;

        }

        public void MapTargetSetupToCommand()
        {
            CreatLoanProductCommand.IsTargetLoan = TargetModel.IsTargetLoan;
            CreatLoanProductCommand.BenefitCode = TargetModel.BenefitCode;
        }

        public void MapOffsetToCommand()
        {
            CreatLoanProductCommand.EnableChargeWaiver = OffSetInfoModel.EnableChargeWaiver;
            CreatLoanProductCommand.EnableSavingsOffset = OffSetInfoModel.EnableSavingsOffset;
            CreatLoanProductCommand.WaiverCharges = OffSetInfoModel.WaivedCharges;
            CreatLoanProductCommand.SavingsOffSets = OffSetInfoModel.SavingsOffSets;
            CreatLoanProductCommand.OffsetPeriodUnit = OffSetInfoModel.OffsetPeriodUnit != null ? (Tenure)System.Enum.Parse(typeof(Tenure), OffSetInfoModel.OffsetPeriodUnit, true) : Tenure.NONE;
            CreatLoanProductCommand.OffsetPeriodValue = OffSetInfoModel.OffsetPeriodValue;
            CreatLoanProductCommand.AllowedOffsetType = (AllowedOffsetType)System.Enum.Parse(typeof(AllowedOffsetType), OffSetInfoModel.AllowedOffsetType, true);
            CreatLoanProductCommand.AdminOffsetCharges = new List<string>();
            CreatLoanProductCommand.EnableAdminOffsetCharge = Model.EnableAdminOffsetCharge;
            CreatLoanProductCommand.AdminOffsetCharges = Model.OffsetAdminCharges;
        }

        public void MapToUpToCommand()
        {
            CreatLoanProductCommand.TopUpCharges = TopUpModel.TopUpCharges;
            CreatLoanProductCommand.EnableTopUp = TopUpModel.EnableTopUp;
            CreatLoanProductCommand.EnableTopUpCharges = TopUpModel.EnableTopUpCharges;
        }

        public void MapWhenDueToCommand()
        {
            CreatLoanProductCommand.WaitingPeriodValue = WhenDueModel.WaitingPeriodValue;
            CreatLoanProductCommand.WaitingPeriodUnit = WhenDueModel.WaitingPeriodUnit != null ? (Tenure)System.Enum.Parse(typeof(Tenure), WhenDueModel.WaitingPeriodUnit, true) : Tenure.NONE;
            CreatLoanProductCommand.WaitingPeriodCharges = WhenDueModel.WaitingPeriodCharges;
            CreatLoanProductCommand.EnableWaitingPeriodCharge = WhenDueModel.EnableWaitingPeriodCharge;
            CreatLoanProductCommand.EnableWaitingPeriod = WhenDueModel.EnableWaitingPeriod;
        }

        public void MapGuarantorToCommand()
        {
            CreatLoanProductCommand.GuarantorAmountLimit = GuarantorModel.GuarantorAmountLimit;
            CreatLoanProductCommand.GuarantorMinYear = GuarantorModel.GuarantorMinYear;
            CreatLoanProductCommand.IsGuarantorRequired = GuarantorModel.IsGuarantorRequired;
            CreatLoanProductCommand.EmployeeGuarantorCount = GuarantorModel.EmployeeGuarantorCount;
            CreatLoanProductCommand.NonEmployeeGuarantorCount = GuarantorModel.NonEmployeeGuarantorCount;
        }

        public void EnableAdminCharge()
        {
            showAdminCharge = true;
            Model.HasAdminCharges = true;
            StateHasChanged();
        }

        public void DisableAdminCharge()
        {
            showAdminCharge = false;
            Model.HasAdminCharges = false;
            StateHasChanged();
        }

        public void ShowBenefitCode()
        {
            showBenefitCode = true;
            TargetModel.IsTargetLoan = true;
            StateHasChanged();
        }

        public void hideBenefitCode()
        {
            TargetModel.IsTargetLoan = false;
            showBenefitCode = false;
            TargetModel.BenefitCode = string.Empty;
            StateHasChanged();
        }

        public void ShowOffsetForm()
        {
            showOffsetForm = true;
            OffSetInfoModel.AllowedOffsetType = AllowedOffsetType.NONE.ToString();
            StateHasChanged();
        }

        public void ShowOffsetForm_Partial()
        {
            showOffsetForm = true;
            OffSetInfoModel.AllowedOffsetType = AllowedOffsetType.PARTIAL.ToString();
            StateHasChanged();
        }

        public void ShowOffsetForm_Full()
        {
            showOffsetForm = true;
            OffSetInfoModel.AllowedOffsetType = AllowedOffsetType.FULL.ToString();
            StateHasChanged();
        }

        public void EnableOffsetSaving()
        {
            OffSetInfoModel.EnableSavingsOffset = true;
            StateHasChanged();
        }

        public void DisableOffsetSaving()
        {
            OffSetInfoModel.EnableSavingsOffset = false;
            StateHasChanged();
        }

        public void EnableOffsetWaiver()
        {
            OffSetInfoModel.EnableChargeWaiver = true;
            StateHasChanged();
        }

        public void DisableOffsetWaiver()
        {
            OffSetInfoModel.EnableChargeWaiver = false;
            StateHasChanged();
        }

        public void EnableOffsetAdminCharges()
        {
            OffSetInfoModel.EnableOffSetAdminCharges = true;
            showOffsetAdmin = true;
            StateHasChanged();
        }

        public void DisableOffsetAdminCharges()
        {
            OffSetInfoModel.EnableOffSetAdminCharges = false;
            showOffsetAdmin = false;
            StateHasChanged();
        }

        public void hideOffsetForm()
        {
            showOffsetForm = false;
            OffSetInfoModel.AllowedOffsetType = AllowedOffsetType.NONE.ToString();
            OffSetInfoModel.OffsetPeriodValue = 0;
            OffSetInfoModel.OffsetPeriodUnit = Tenure.NONE.ToString();
            OffSetInfoModel.EnableSavingsOffset = false;
            OffSetInfoModel.EnableChargeWaiver = false;
            OffSetInfoModel.EnableSavingsOffset = false;
            StateHasChanged();
        }

        public void ShowTopUpForm()
        {
            TopUpModel.EnableTopUpCharges = true;
            showTopUpInfoForm = true;
            StateHasChanged();
        }

        public void HideTopUpForm()
        {
            showTopUpInfoForm = false;
            TopUpModel.EnableTopUpCharges = false;
            TopUpModel.TopUpCharges = new List<string>();
            StateHasChanged();
        }

        public void ShowWaitingForm()
        {
            WhenDueModel.EnableWaitingPeriod = true;
            showEnableWaitingForm = true;
            StateHasChanged();
        }

        public void HideWaitingForm()
        {
            showEnableWaitingForm = false;
            WhenDueModel.EnableWaitingPeriod = false;
            WhenDueModel.WaitingPeriodUnit = Tenure.NONE.ToString();
            WhenDueModel.WaitingPeriodValue = 0;
            StateHasChanged();
        }

        public void ShowWaitingChargeForm()
        {
            WhenDueModel.EnableWaitingPeriodCharge = true;
            showEnableChargeWaitingForm = true;
            StateHasChanged();
        }

        public void HideWaitingChargeForm()
        {
            showEnableChargeWaitingForm = false;
            WhenDueModel.WaitingPeriodCharges = new List<string>();
            WhenDueModel.EnableWaitingPeriodCharge = false;
            StateHasChanged();
        }

        public void ShowGuarantorForm()
        {
            GuarantorModel.IsGuarantorRequired = true;
            showGuarantorInfoForm = true;
            StateHasChanged();
        }

        public void HideGuarantorForm()
        {
            showGuarantorInfoForm = false;
            GuarantorModel.EmployeeGuarantorCount = 0;
            GuarantorModel.IsGuarantorRequired = false;
            GuarantorModel.GuarantorMinYear = 0;
            GuarantorModel.NonEmployeeGuarantorCount = 0;
            GuarantorModel.GuarantorAmountLimit = 0;
            StateHasChanged();
        }

        private void SavingValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue != null)
            {
                if (selectedValue.Count() > 0)
                {
                    OffSetInfoModel.SavingsOffSets = new List<string>();
                    foreach (var item in selectedValue)
                    {
                        OffSetInfoModel.SavingsOffSets.Add(item);
                    }
                    Array.Clear(OffsetProductTypeArray, 0, OffsetProductTypeArray.Length);
                    OffsetProductTypeArray = OffSetInfoModel.SavingsOffSets.ToArray();
                }
            }

        }

        private void OffsetAdminValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                OffSetInfoModel.OffSetsAdminCharges = new List<string>();

                foreach (var item in selectedValue)
                {
                    OffSetInfoModel.OffSetsAdminCharges.Add(item);
                }
                Array.Clear(OffsetAdminChargeArray, 0, OffsetAdminChargeArray.Length);
                OffsetAdminChargeArray = OffSetInfoModel.OffSetsAdminCharges.ToArray();
            }
        }

        private void WaiverValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue != null && selectedValue.Count() > 0)
            {
                OffSetInfoModel.WaivedCharges = new List<string>();

                foreach (var item in selectedValue)
                {
                    OffSetInfoModel.WaivedCharges.Add(item);
                }
                Array.Clear(OffsetWaiverChargeArray, 0, OffsetWaiverChargeArray.Length);
                OffsetWaiverChargeArray = OffSetInfoModel.WaivedCharges.ToArray();
            }
        }

        private void TopupValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue != null && selectedValue.Count() > 0)
            {
                TopUpModel.TopUpCharges = new List<string>();

                foreach (var item in selectedValue)
                {
                    TopUpModel.TopUpCharges.Add(item);
                }
                Array.Clear(TopupAdminChargeArray, 0, TopupAdminChargeArray.Length);
                TopupAdminChargeArray = TopUpModel.TopUpCharges.ToArray();
            }
        }

        private void WaitingPeriodValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue != null && selectedValue.Count() > 0)
            {
                WhenDueModel.WaitingPeriodCharges = new List<string>();

                foreach (var item in selectedValue)
                {
                    WhenDueModel.WaitingPeriodCharges.Add(item);
                }
                Array.Clear(WhenDueAdminChargeArray, 0, WhenDueAdminChargeArray.Length);
                WhenDueAdminChargeArray = WhenDueModel.WaitingPeriodCharges.ToArray();
            }
        }

        private async Task Done()
        {
            showPopup = false;
            await InvokeAsync(StateHasChanged);
            _navigationManager.NavigateTo("/LoanProductSetup/Manage/all", true);
        }
    }
}