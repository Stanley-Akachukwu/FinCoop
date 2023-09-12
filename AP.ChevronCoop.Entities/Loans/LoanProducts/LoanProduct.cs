using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Loans.LoanProducts;

public class LoanProduct : BaseEntity<string>
{
    public LoanProduct()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string Code { get; set; }
    public string PayrollCode { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }
    public string ApprovalWorkflowId { get; set; }
    public ApprovalWorkflow ApprovalWorkflow { get; set; }

    public string ApprovalId { get; set; }
    public Approval Approval { get; set; }

    public decimal PrincipalMultiple { get; set; }
    public decimal PrincipalMinLimit { get; set; }
    public decimal PrincipalMaxLimit { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal MinTenureValue { get; set; }
    public decimal MaxTenureValue { get; set; }
    public Tenure RepaymentPeriod { get; set; } = Tenure.MONTHLY; 

    public InterestMethod InterestMethod { get; set; } = InterestMethod.SIMPLE;
    public InterestCalculationMethod InterestCalculationMethod { get; set; } = InterestCalculationMethod.FLAT_RATE;
    public decimal DaysInYear { get; set; } //360,365,366
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
    public bool EnableAdminOffsetCharge { get; set; }
    public bool EnableWaitingPeriod { get; set; }
    public Tenure WaitingPeriodUnit { get; set; }
    public decimal WaitingPeriodValue { get; set; }
    public bool EnableWaitingPeriodCharge { get; set; }
    public bool IsGuarantorRequired { get; set; }
    public int GuarantorMinYear { get; set; }
    public decimal GuarantorAmountLimit { get; set; }
    public int EmployeeGuarantorCount { get; set; }
    public int NonEmployeeGuarantorCount { get; set; }
    public DepositProductType QualificationTargetProduct { get; set; }
    public decimal QualificationMinBalancePercentage { get; set; }

    public List<String> SavingsOffSetProductIdList { get; set; } = new();

    public List<String> MemberTypeIdList { get; set; } = new();

    public int? MinimumAge { get; set; }

    public int? MaximumAge { get; set; }

    public string? DefaultCurrencyId { get; set; }

    public Currency? DefaultCurrency { get; set; }

    // public string LoanProductTypeId { get; set; }
    public LoanProductType LoanProductType { get; set; }

    public string? BankDepositAccountId { get; set; }

    public virtual CompanyBankAccount? BankDepositAccount { get; set; }

    public string? DisbursementAccountId { get; set; }

    public virtual CompanyBankAccount? DisbursementAccount { get; set; }

    public string PrincipalAccountId { get; set; }

    public virtual LedgerAccount PrincipalAccount { get; set; } //10,000, 100,000, 1,000,000

    public string PrincipalLossAccountId { get; set; }

    public virtual LedgerAccount PrincipalLossAccount { get; set; }

    public string InterestIncomeAccountId { get; set; }

    public virtual LedgerAccount InterestIncomeAccount { get; set; } //1,000
    
    
    public string UnearnedInterestAccountId { get; set; }
    public virtual LedgerAccount UnearnedInterestAccount { get; set; }

    public string InterestLossAccountId { get; set; }

    public virtual LedgerAccount InterestLossAccount { get; set; }

    public string PenalInterestReceivableAccountId { get; set; }

    public virtual LedgerAccount PenalInterestReceivableAccount { get; set; }
    
    public string InterestWaivedAccountId { get; set; }
    public virtual LedgerAccount InterestWaivedAccount { get; set; }

    public string ChargesIncomeAccountId { get; set; }

    public virtual LedgerAccount ChargesIncomeAccount { get; set; }

    public string ChargesAccrualAccountId { get; set; }
    public virtual LedgerAccount ChargesAccrualAccount { get; set; }

    public string ChargesWaivedAccountId { get; set; }
    public virtual LedgerAccount ChargesWaivedAccount { get; set; }

    public ProductStatus Status { get; set; }
    public PublicationType PublicationType { get; set; }
    public string? PublishedByUserId { get; set; }
    public ApplicationUser? PublishedByUser { get; set; }

    ///public bool IsPublishedToAll { get; set; }

    public override string DisplayCaption => "";

    public override string DropdownCaption => "";

    public override string ShortCaption => "";
}
