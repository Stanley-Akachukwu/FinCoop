namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
public partial class SavingsCashAdditionViewModel : BaseViewModel
{
    public string? SavingsAccountId { get; set; }
    public decimal Amount { get; set; }
    public string ModeOfPayment { get; set; }
    public string? CustomerPaymentDocumentId { get; set; }
    public string? BatchRefNo { get; set; }
    public string? TransactionJournalId { get; set; }
    public string? ApprovalId { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string Status { get; set; }

}



//public partial class SavingsCashAdditionViewModel : BaseViewModel
//{

//    public string SavingsAccountId { get; set; }
//    public decimal Amount { get; set; }
//    public string ModeOfPayment { get; set; }

//    public string CustomerPaymentDocumentId { get; set; }

//    public string BatchRefNo { get; set; }

//    public string TransactionJournalId { get; set; }

//    public bool IsProcessed { get; set; }
//    public DateTime ProcessedDate { get; set; }

//}


