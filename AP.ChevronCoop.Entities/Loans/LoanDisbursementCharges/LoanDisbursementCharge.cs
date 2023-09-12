using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.MasterData.Charges;

namespace AP.ChevronCoop.Entities.Loans.LoanDisbursements;

public class LoanDisbursementCharge : BaseEntity<string>
{
    public LoanDisbursementCharge()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }


    public string LoanDisbursementId { get; set; }
    public virtual LoanDisbursement LoanDisbursement { get; set; }


    public string DisbursementChargeId { get; set; }
    public virtual Charge DisbursementCharge { get; set; }

    public ChargeType ChargeType { get; set; }

    public decimal TotalCharge { get; set; }
    
    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }
    


    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}


