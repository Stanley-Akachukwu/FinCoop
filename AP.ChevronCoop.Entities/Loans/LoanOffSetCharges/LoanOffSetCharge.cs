using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.MasterData.Charges;

namespace AP.ChevronCoop.Entities.LoanOffsetTransactions;

public class LoanOffSetCharge : BaseEntity<string>
{
    public LoanOffSetCharge()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string LoanOffsetId { get; set; }
    public virtual LoanOffset LoanOffset { get; set; }

    public string OffsetChargeId { get; set; }
    public virtual Charge OffsetCharge { get; set; }

    public ChargeType ChargeType { get; set; }

    public decimal TotalCharge { get; set; }
    
    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }


    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}
