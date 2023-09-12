using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.MasterData.Charges;

namespace AP.ChevronCoop.Entities.LoanTopupTransactions;

public class LoanTopupCharge: BaseEntity<string>
{
    public LoanTopupCharge()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }
    public string LoanTopupId { get; set; }
    public virtual LoanTopup LoanTopup { get; set; }

    public string TopupChargeId { get; set; }
    public virtual Charge TopupCharge { get; set; }

    public ChargeType ChargeType { get; set; }
    
    public decimal TotalCharge { get; set; }
    
    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }
        
     

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}