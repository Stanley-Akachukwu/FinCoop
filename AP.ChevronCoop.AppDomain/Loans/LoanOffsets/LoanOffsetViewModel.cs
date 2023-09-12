namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsets;

public class LoanOffsetViewModel : BaseViewModel
{
  public string LoanAccountId { get; set; }


  public decimal OffsetAmount { get; set; }


  public decimal OldPrincipalBalance { get; set; }


  public decimal NewPrincipalBalance { get; set; }


  public decimal OldInterestBalance { get; set; }


  public decimal NewInterestBalance { get; set; }


  public decimal TotalOffsetCharges { get; set; }


  public bool IsLiquidated { get; set; }

  public string AllowedOffsetType { get; set; }

  public string LoanRepaymentMode { get; set; }


  public DateTimeOffset OffSetRepaymentDate { get; set; }

  public string SavingsAccountId { get; set; }

  public string SpecialDepositAccountId { get; set; }

  public string CustomerBankAccountId { get; set; }


  public int ModeOfPayment { get; set; }

  public string CustomerPaymentDocumentId { get; set; }

  public string TransactionJournalId { get; set; }

  public string ApprovalId { get; set; }
}