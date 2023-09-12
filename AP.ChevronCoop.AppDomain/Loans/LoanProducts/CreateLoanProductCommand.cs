using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public class CreateLoanProductCommand : CreateCommand, IRequest<CommandResult<LoanProductViewModel>>
{
    public string Code { get; set; }
    public string PayrollCode { get; set; }
    public string Name { get; set; }
    public LoanProductType LoanProductType { get; set; }
    public string DefaultCurrencyId { get; set; }
    public string ApplicationUserId { get; set; }
    public string ShortName { get; set; }
    public string PrincipalLimitType { get; set; }
    public decimal PrincipalMultiple { get; set; }
    public decimal PrincipalMinLimit { get; set; }
    public decimal PrincipalMaxLimit { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal MinTenureValue { get; set; }
    public decimal MaxTenureValue { get; set; }
    public Tenure? RepaymentPeriod { get; set; } = Tenure.MONTHLY;
    public InterestMethod InterestMethod { get; set; } = InterestMethod.SIMPLE;
    public InterestCalculationMethod InterestCalculationMethod { get; set; } = InterestCalculationMethod.FLAT_RATE;
    public decimal? DaysInYear { get; set; } //360,365,366
    public decimal InterestRate { get; set; }
    public bool HasAdminCharges { get; set; }
    public bool IsTargetLoan { get; set; }
    public string BenefitCode { get; set; }
    public AllowedOffsetType AllowedOffsetType { get; set; }
    public Tenure OffsetPeriodUnit { get; set; }
    public decimal OffsetPeriodValue { get; set; }
    public bool EnableSavingsOffset { get; set; }
    public bool EnableChargeWaiver { get; set; }
    public bool EnableTopUp { get; set; }
    public bool EnableTopUpCharges { get; set; }
    public bool EnableWaitingPeriod { get; set; }
    public Tenure WaitingPeriodUnit { get; set; }
    public decimal WaitingPeriodValue { get; set; }
    public bool EnableWaitingPeriodCharge { get; set; }
    public bool IsGuarantorRequired { get; set; }
    public int GuarantorMinYear { get; set; }
    public decimal GuarantorAmountLimit { get; set; }
    public int EmployeeGuarantorCount { get; set; }
    public int NonEmployeeGuarantorCount { get; set; }
    public string ApprovalWorkFlowId { get; set; }
    public DepositProductType QualificationTargetProduct { get; set; }
    public decimal QualificationMinBalancePercentage { get; set; }
    public bool EnableAdminOffsetCharge { get; set; }
    public string DisbursementAccountId { get; set; }
    public string BankDepositAccountId { get; set; }
    public List<string> AdminOffsetCharges { get; set; }
    public List<string> AdminCharges { get; set; }
    public List<string> WaitingPeriodCharges { get; set; }
    public List<string> TopUpCharges { get; set; }
    public List<string> WaiverCharges { get; set; }

    public List<string> SavingsOffSets { get; set; }
    public List<string> MemberTypes { get; set; }
}