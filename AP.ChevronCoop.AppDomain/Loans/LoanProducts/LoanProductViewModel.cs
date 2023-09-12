using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public class LoanProductViewModel : BaseViewModel
{
  public string Code { get; set; }
  public string PayrollCode { get; set; }
  public string Name { get; set; }
  public string ShortName { get; set; }

  public string LoanProductType { get; set; }


  public int MinimumAge { get; set; }


  public int MaximumAge { get; set; }

  public string DefaultCurrencyId { get; set; }


  public string PrincipalLimitType { get; set; }
  public decimal PrincipalMultiple { get; set; }
  public decimal PrincipalMinLimit { get; set; }
  public decimal PrincipalMaxLimit { get; set; }
  public string TenureUnit { get; set; }
  public decimal MinTenureValue { get; set; }
  public decimal MaxTenureValue { get; set; }
  public string InterestMethod { get; set; }
  public decimal InterestRate { get; set; }
  public bool HasAdminCharges { get; set; }
  public bool IsTargetLoan { get; set; }
  public string BenefitCode { get; set; }
  public string AllowedOffsetType { get; set; }
  public string OffsetPeriodUnit { get; set; }
  public decimal OffsetPeriodValue { get; set; }
  public bool EnableSavingsOffset { get; set; }
  public bool EnableChargeWaiver { get; set; }
  public bool EnableTopUp { get; set; }
  public bool EnableTopUpCharges { get; set; }
  public bool EnableWaitingPeriod { get; set; }
  public string WaitingPeriodUnit { get; set; }
  public decimal WaitingPeriodValue { get; set; }
  public bool EnableWaitingPeriodCharge { get; set; }
  public bool IsGuarantorRequired { get; set; }
  public int GuarantorMinYear { get; set; }
  public string GuarantorAmountLimit { get; set; }
  public int EmployeeGuarantorCount { get; set; }
  public int NonEmployeeGuarantorCount { get; set; }
  public string RepaymentPeriod { get; set; }
  public string InterestCalculationMethod { get; set; }
  public decimal DaysInYear { get; set; } //360,365,366
  public string ApprovalWorkflowId { get; set; }
  public string ApprovalWorkflowName { get; set; }
  public string Status { get; set; }
  public string QualificationTargetProduct { get; set; }
  public decimal QualificationMinBalancePercentage { get; set; }
  public bool EnableAdminOffsetCharge { get; set; }
  
  public string SavingsOffSetJson { get; set; }
  public string MemberTypeJson { get; set; }
  public List<LedgerAccountViewModel> LedgerAccounts { get; set; }
  public List<LedgerAccountViewModel> AssetLedgerAccounts { get; set; }
  public List<LedgerAccountViewModel> ExpenseLedgerAccounts { get; set; }
  public string? BankDepositAccountId { get; set; }
  public string? DisbursementAccountId { get; set; }
  public string PrincipalAccountId { get; set; }
  public string PrincipalLossAccountId { get; set; }
  public string InterestIncomeAccountId { get; set; }
  public string UnearnedInterestAccountId { get; set; }
  public string InterestLossAccountId { get; set; }
  public string PenalInterestReceivableAccountId { get; set; }
  public string InterestWaivedAccountId { get; set; }
  public string ChargesIncomeAccountId { get; set; }
  public string ChargesAccrualAccountId { get; set; }
  public string ChargesWaivedAccountId { get; set; }
  public string PrincipalReceivableAccountId { get; set; }
  public string InterestReceivableAccountId { get; set; }
  public string ChargesReceivableAccountId { get; set; }

  public string CreatedByUserId { get; set; }

  public string UpdatedByUserId { get; set; }

  public string DeletedByUserId { get; set; }
}