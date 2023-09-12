using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup.EditLoanProduct
{
    public partial class EditLoanProductSetupForm
    {
        [Parameter]
        public string id { get; set; }

        bool showGLAccounts { get; set; } = false;
        bool showTopUpInfo { get; set; } = false;
        bool showBasicInformation { get; set; } = false;
        bool showTargetSetup { get; set; } = false;
        bool showOffset { get; set; } = false;
        bool showDueForLoan { get; set; } = false;
        bool showGuarantorInfo { get; set; } = false;
        bool showApproval { get; set; } = false;
        bool showBenefitCode { get; set; } = false;
        bool showPopup { get; set; } = false;

        string LoanProductId { get; set; }

        //string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        //string Inactive { get; set; } = "border-b-2 border-transparent";
        string Inactive { get; set; } =
            "class=\"inline-block p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300 text-gray-300\"";

        string Active { get; set; } =
            "class=\"inline-block p-4 text-blue-600 border-b-2 border-blue-600 rounded-t-lg active dark:text-blue-500 dark:border-blue-500\" aria-current=\"page\"";

        string ActiveArialPage { get; set; } = "aria-current=\"page\"";
        string InactiveArialPage { get; set; } = string.Empty;
        string GLAccountTab { get; set; } = string.Empty;
        string BasicInformationTab { get; set; } = string.Empty;
        string TargetSetupTab { get; set; } = string.Empty;
        string TopupTab { get; set; } = string.Empty;
        string OffsetTab { get; set; } = string.Empty;
        string DueLoanTab { get; set; } = string.Empty;
        string GuarantorTab { get; set; } = string.Empty;
        string ApprovalTab { get; set; } = string.Empty;
        public GetLoanProductViewModel Model { get; set; }

        [Inject]
        ILogger<ViewLoanProductSetup> Logger { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        IConfiguration _configuration { get; set; }

        [Inject]
        IClientAuditService _auditLogService { get; set; }

        public CreateLoanProductBasicInfoDTO BasicModel { get; set; }
        public CreateLoanProductTargetSetupDTO TargetModel { get; set; }
        public CreateLoanProductGuarantorDTO GuarantorModel { get; set; }
        public CreateLoanProductTopUpDTO TopUpModel { get; set; }
        public CreateLoanProductWhenDueDTO WhenDueModel { get; set; }
        public CreateLoanProductOffSetInfoDTO OffSetModel { get; set; }
        public string ApprovalWorkFlowId { get; set; }
        public UpdateLoanProductCommand updateLoanProductCommand { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            LoanProductId = id;
            Model = new GetLoanProductViewModel();
            BasicModel = new CreateLoanProductBasicInfoDTO();
            GuarantorModel = new CreateLoanProductGuarantorDTO();
            TargetModel = new CreateLoanProductTargetSetupDTO();
            TopUpModel = new CreateLoanProductTopUpDTO();
            WhenDueModel = new CreateLoanProductWhenDueDTO();
            OffSetModel = new CreateLoanProductOffSetInfoDTO();
            updateLoanProductCommand = new UpdateLoanProductCommand();

            await GetLoanProduct();
            ActivateTabs(1);
        }

        public void ActivateTabs(int tabPosition)
        {
            switch (tabPosition)
            {
                case 1:
                    showGLAccounts = true;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    GLAccountTab = Active;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;

                    StateHasChanged();
                    break;
                case 2:
                    showGLAccounts = false;
                    showBasicInformation = true;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Active;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    StateHasChanged();
                    break;
                case 3:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = true;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Active;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    StateHasChanged();
                    break;
                case 4:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = true;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Active;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    StateHasChanged();
                    break;
                case 5:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = true;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Active;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    StateHasChanged();
                    break;
                case 6:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = true;
                    showGuarantorInfo = false;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Active;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    StateHasChanged();
                    break;
                case 7:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = true;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Active;
                    StateHasChanged();
                    break;
                case 8:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = true;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Active;
                    GuarantorTab = Inactive;
                    StateHasChanged();
                    break;
            }
        }

        public async Task GetLoanProduct()
        {
            var rsp = await DataService.GetProduct<CommandResult<GetLoanProductViewModel>>(nameof(LoanProduct),
                LoanProductId);


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
                Model = JsonSerializer.Deserialize<GetLoanProductViewModel>(rsp.Content.Response.ToJson());
                MapToBasicModel();
                MapToGuarantor();
                MapToOffSet();
                MapToTarget();
                MapToTopUp();
                MapToWhenDue();
                MapToApprovalWorkFlow();
            }
        }

        private async Task OnProceedChangedHandler(int tabPosition)
        {
            ActivateTabs(tabPosition);
        }

        private async Task OnBasicInfoProceedChangedHandler(CreateLoanProductBasicInfoDTO basic)
        {
            BasicModel = basic;
            ActivateTabs(3);
        }

        private async Task OnBasicInfoPreviousChangedHandler(CreateLoanProductBasicInfoDTO basic)
        {
            BasicModel = basic;
            ActivateTabs(1);
        }

        private async Task OnTargetProceedChangedHandler(CreateLoanProductTargetSetupDTO target)
        {
            TargetModel = target;
            ActivateTabs(4);
        }

        private async Task OnTargetPreviousChangedHandler(CreateLoanProductTargetSetupDTO target)
        {
            TargetModel = target;
            ActivateTabs(2);
        }

        private async Task OnOffSetProceedChangedHandler(CreateLoanProductOffSetInfoDTO Offset)
        {
            OffSetModel = Offset;
            ActivateTabs(5);
        }

        private async Task OnOffSetPreviousChangedHandler(CreateLoanProductOffSetInfoDTO Offset)
        {
            OffSetModel = Offset;
            ActivateTabs(3);
        }

        private async Task OnTopUpProceedChangedHandler(CreateLoanProductTopUpDTO topup)
        {
            TopUpModel = topup;
            ActivateTabs(6);
        }

        private async Task OnTopUpPreviousChangedHandler(CreateLoanProductTopUpDTO topup)
        {
            TopUpModel = topup;
            ActivateTabs(4);
        }

        private async Task OnWhenDueProceedChangedHandler(CreateLoanProductWhenDueDTO whenDue)
        {
            WhenDueModel = whenDue;
            ActivateTabs(7);
        }

        private async Task OnWhenDuePreviousChangedHandler(CreateLoanProductWhenDueDTO whenDue)
        {
            WhenDueModel = whenDue;
            ActivateTabs(5);
        }

        private async Task OnGuarantorProceedChangedHandler(CreateLoanProductGuarantorDTO guarantor)
        {
            GuarantorModel = guarantor;
            ActivateTabs(8);
        }

        private async Task OnGuarantorPreviousChangedHandler(CreateLoanProductGuarantorDTO guarantor)
        {
            GuarantorModel = guarantor;
            ActivateTabs(6);
        }

        private async Task OnApprovalWorkFlowSubmitChangedHandler(string approvalWorkFlowId)
        {
            ApprovalWorkFlowId = approvalWorkFlowId;

            await SaveEdit();
            //reference submit method here
        }

        private async Task OnApprovalWorkFlowPreviousChangedHandler(string approvalWorkFlowId)
        {
            ApprovalWorkFlowId = approvalWorkFlowId;
            ActivateTabs(7);
        }

        public void MapToBasicModel()
        {
            BasicModel.AdminCharges = new List<string>();
            foreach (var adminCharge in Model.DisbursementCharges)
            {
                BasicModel.AdminCharges.Add(adminCharge.Id);
            }

            //BasicModel.BenefitCode = Model.BenefitCode;
            BasicModel.QualificationMinBalancePercentage = Model.QualificationMinBalancePercentage;
            BasicModel.PrincipalMaxLimit = Model.PrincipalMaxLimit;
            BasicModel.PrincipalMinLimit = Model.PrincipalMinLimit;
            // BasicModel.PrincipalLimitType = Model.PrincipalLimitType;
            BasicModel.Code = Model.Code;
            BasicModel.Description = Model.Description;
            BasicModel.DefaultCurrencyId = Model.DefaultCurrencyId;
            BasicModel.InterestMethod = Model.InterestMethod;
            BasicModel.HasAdminCharges = Model.HasAdminCharges;
            BasicModel.InterestRate = Model.InterestRate;
            BasicModel.MaxTenureValue = Model.MaxTenureValue;
            BasicModel.MinTenureValue = Model.MinTenureValue;
            BasicModel.IsTargetLoan = Model.IsTargetLoan;
            BasicModel.QualificationTargetProduct = Model.QualificationTargetProduct;
            BasicModel.ShortName = Model.ShortName;
            BasicModel.Name = Model.Name;
            BasicModel.TenureUnit = Model.TenureUnit;
            BasicModel.MemberTypes = Model.MemberTypes;
            BasicModel.LoanProductType = Model.LoanProductType;
            BasicModel.CompanyDepositAccountId = Model.BankDepositAccountId;
            BasicModel.CompanyDisbursementAccountId = Model.DisbursementAccountId;
            BasicModel.PrincipalMultiple = Model.PrincipalMultiple;
            BasicModel.PayrollCode = Model.PayrollCode;
            BasicModel.RepaymentPeriod = Model.RepaymentPeriod;
            BasicModel.DaysInYear = Model.DaysInYear != 0 ? (DaysInYear)Model.DaysInYear : DaysInYear.DAYS_365;
            BasicModel.InterestCalculationMethod = Model.InterestCalculationMethod;
        }

        public void MapToTarget()
        {
            TargetModel.IsTargetLoan = Model.IsTargetLoan;
            TargetModel.BenefitCode = Model.BenefitCode;
        }

        public void MapToOffSet()
        {
            OffSetModel.WaivedCharges = new List<string>();
            foreach (var waiver in Model.WaivedCharges)
            {
                OffSetModel.WaivedCharges.Add(waiver.Id);
            }

            OffSetModel.OffsetPeriodUnit = Model.OffsetPeriodUnit;
            OffSetModel.OffsetPeriodValue = Model.OffsetPeriodValue;
            OffSetModel.AllowedOffsetType = Model.AllowedOffsetType;
            OffSetModel.EnableChargeWaiver = Model.EnableChargeWaiver;
            OffSetModel.SavingsOffSets = Model.SavingsOffSets;
            OffSetModel.EnableSavingsOffset = Model.EnableSavingsOffset;
            OffSetModel.EnableOffSetAdminCharges = Model.EnableAdminOffsetCharge;
            OffSetModel.OffSetsAdminCharges = new List<string>();
            foreach (var charge in Model.WaitingPeriodCharges)
            {
                OffSetModel.OffSetsAdminCharges.Add(charge.Id);
            }
        }

        public void MapToTopUp()
        {
            TopUpModel.EnableTopUp = Model.EnableTopUp;
            TopUpModel.EnableTopUpCharges = Model.EnableTopUpCharges;
            TopUpModel.TopUpCharges = new List<string>();
            foreach (var topUp in Model.TopUpCharges)
            {
                TopUpModel.TopUpCharges.Add(topUp.Id);
            }
        }

        public void MapToWhenDue()
        {
            WhenDueModel.WaitingPeriodUnit = Model.WaitingPeriodUnit;
            WhenDueModel.EnableWaitingPeriod = Model.EnableWaitingPeriod;
            WhenDueModel.EnableWaitingPeriodCharge = Model.EnableWaitingPeriodCharge;
            WhenDueModel.WaitingPeriodValue = Model.WaitingPeriodValue;
            WhenDueModel.WaitingPeriodCharges = new List<string>();
            foreach (var due in Model.WaitingPeriodCharges)
            {
                WhenDueModel.WaitingPeriodCharges.Add(due.Id);
            }
        }

        public void MapToGuarantor()
        {
            GuarantorModel.GuarantorMinYear = Model.GuarantorMinYear;
            GuarantorModel.IsGuarantorRequired = Model.IsGuarantorRequired;
            GuarantorModel.EmployeeGuarantorCount = Model.EmployeeGuarantorCount;
            GuarantorModel.NonEmployeeGuarantorCount = Model.NonEmployeeGuarantorCount;
            GuarantorModel.GuarantorAmountLimit = Model.GuarantorAmountLimit;
        }

        public void MapToApprovalWorkFlow()
        {
            ApprovalWorkFlowId = Model.ApprovalWorkflowId;
        }

        public async Task SaveEdit()
        {
            try
            {
                MapToUpdateModel();
                updateLoanProductCommand.ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
                var rsp = await DataService.Update<UpdateLoanProductCommand, LoanProductViewModel>(nameof(LoanProduct),
                    updateLoanProductCommand);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(updateLoanProductCommand)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Edit Loan Product", "Edit Loan Product", "Security",
                        "NA, readonly request", CurrentUser);
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
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MapToUpdateModel()
        {
            updateLoanProductCommand.Id = Model.Id;
            updateLoanProductCommand.AdminCharges = BasicModel.AdminCharges;
            updateLoanProductCommand.AllowedOffsetType = OffSetModel.AllowedOffsetType;
            updateLoanProductCommand.ApprovalWorkFlowId = ApprovalWorkFlowId;
            updateLoanProductCommand.GuarantorAmountLimit = GuarantorModel.GuarantorAmountLimit;
            updateLoanProductCommand.GuarantorMinYear = GuarantorModel.GuarantorMinYear;
            updateLoanProductCommand.IsGuarantorRequired = GuarantorModel.IsGuarantorRequired;
            //updateLoanProductCommand.BenefitCode = BasicModel.BenefitCode;
            updateLoanProductCommand.Code = Model.Code;
            updateLoanProductCommand.PrincipalLimitType = "";
            updateLoanProductCommand.Description = BasicModel.Description;
            updateLoanProductCommand.DefaultCurrencyId = BasicModel.DefaultCurrencyId;
            updateLoanProductCommand.InterestCalculationMethod =
                (InterestCalculationMethod)System.Enum.Parse(typeof(InterestCalculationMethod),
                    BasicModel.InterestCalculationMethod, true);
            updateLoanProductCommand.InterestRate = BasicModel.InterestRate;
            updateLoanProductCommand.MaxTenureValue = BasicModel.MaxTenureValue;
            updateLoanProductCommand.MemberTypes = BasicModel.MemberTypes;
            updateLoanProductCommand.QualificationMinBalancePercentage = BasicModel.QualificationMinBalancePercentage;
            updateLoanProductCommand.QualificationTargetProduct = BasicModel.QualificationTargetProduct;
            updateLoanProductCommand.MemberTypes = BasicModel.MemberTypes;
            updateLoanProductCommand.MinTenureValue = BasicModel.MinTenureValue;
            updateLoanProductCommand.MaxTenureValue = BasicModel.MaxTenureValue;
            updateLoanProductCommand.Name = BasicModel.Name;
            updateLoanProductCommand.PrincipalMaxLimit = BasicModel.PrincipalMaxLimit;
            updateLoanProductCommand.PrincipalMinLimit = BasicModel.PrincipalMinLimit;
            updateLoanProductCommand.ShortName = BasicModel.ShortName;
            updateLoanProductCommand.EmployeeGuarantorCount = GuarantorModel.EmployeeGuarantorCount;
            updateLoanProductCommand.NonEmployeeGuarantorCount = GuarantorModel.NonEmployeeGuarantorCount;
            updateLoanProductCommand.OffsetPeriodUnit = OffSetModel.OffsetPeriodUnit != null ? OffSetModel.OffsetPeriodUnit : Tenure.NONE.ToString();
            updateLoanProductCommand.OffsetPeriodValue = OffSetModel.OffsetPeriodValue;
            updateLoanProductCommand.EnableSavingsOffset = OffSetModel.EnableSavingsOffset;
            updateLoanProductCommand.EnableChargeWaiver = OffSetModel.EnableChargeWaiver;
            updateLoanProductCommand.WaivedCharges = OffSetModel.WaivedCharges;
            updateLoanProductCommand.SavingsOffSets = OffSetModel.SavingsOffSets;
            updateLoanProductCommand.EnableTopUp = TopUpModel.EnableTopUp;
            updateLoanProductCommand.EnableTopUpCharges = TopUpModel.EnableTopUpCharges;
            updateLoanProductCommand.TopUpCharges = TopUpModel.TopUpCharges;
            updateLoanProductCommand.EnableWaitingPeriod = WhenDueModel.EnableWaitingPeriod;
            updateLoanProductCommand.EnableWaitingPeriodCharge = WhenDueModel.EnableWaitingPeriodCharge;
            updateLoanProductCommand.WaitingPeriodValue = WhenDueModel.WaitingPeriodValue;
            updateLoanProductCommand.WaitingPeriodUnit = WhenDueModel.WaitingPeriodUnit != null ? WhenDueModel.WaitingPeriodUnit : Tenure.NONE.ToString();
            updateLoanProductCommand.WaitingPeriodCharges = WhenDueModel.WaitingPeriodCharges;
            updateLoanProductCommand.IsTargetLoan = TargetModel.IsTargetLoan;
            updateLoanProductCommand.BenefitCode = TargetModel.BenefitCode;
            updateLoanProductCommand.HasAdminCharges = BasicModel.HasAdminCharges;
            updateLoanProductCommand.PrincipalMultiple = BasicModel.PrincipalMultiple;
            updateLoanProductCommand.TenureUnit
                = BasicModel.TenureUnit != null ? BasicModel.TenureUnit : Tenure.NONE.ToString();
            updateLoanProductCommand.PrincipalMultiple = BasicModel.PrincipalMultiple;
            updateLoanProductCommand.AdminOffsetCharges = OffSetModel.OffSetsAdminCharges;
            updateLoanProductCommand.BankDepositAccountId = BasicModel.CompanyDepositAccountId;
            updateLoanProductCommand.DisbursementAccountId = BasicModel.CompanyDisbursementAccountId;
            updateLoanProductCommand.DaysInYear = (decimal)BasicModel.DaysInYear;
            updateLoanProductCommand.RepaymentPeriod = BasicModel.RepaymentPeriod != null ? (Tenure)System.Enum.Parse(typeof(Tenure), BasicModel.RepaymentPeriod, true) : Tenure.NONE;
            updateLoanProductCommand.PayrollCode = BasicModel.PayrollCode;
        }

        private async Task Done()
        {
            showPopup = false;
            await InvokeAsync(StateHasChanged);
            _navigationManager.NavigateTo("/LoanProductSetup/Manage/all", true);
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }
    }
}