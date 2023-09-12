using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.MasterData.Charges;

namespace AP.ChevronCoop.Entities.Loans.LoanRepayment;

public class LoanRepaymentCharge : BaseEntity<string>
{
    public LoanRepaymentCharge()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }


    public string LoanRepaymentId { get; set; }
    public virtual LoanRepayment LoanRepayment { get; set; }


    public string RepaymentChargeId { get; set; }
    public virtual Charge RepaymentCharge { get; set; }

    public ChargeType ChargeType { get; set; }

    public decimal TotalCharge { get; set; }
    
    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }


    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}


