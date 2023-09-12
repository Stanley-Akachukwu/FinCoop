using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;

public class DepositTransactionViewModel : BaseViewModel
{

    public DepositTransactionViewModel()
    {

    }

    public string DepositAccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public string EntityId { get; set; }
    public string EntityType { get; set; }
    public DateTime TransactionDate { get; set; }

    public bool IsApproved { get; set; } = false;
    public string ApprovalId { get; set; }
    public DateTime ApprovedOn { get; set; }

    public bool IsPosted { get; set; } = false;
    public string PostedId { get; set; }
    public DateTime PostedOn { get; set; }

    public bool IsProcessed { get; set; } = false;
    public string ProcessedId { get; set; }
    public DateTime ProcessedOn { get; set; }

    public string ErrorDetails { get; set; }
    public bool ErrorFlag { get; set; } = false;
    public DateTime ErrorDate { get; set; }
}