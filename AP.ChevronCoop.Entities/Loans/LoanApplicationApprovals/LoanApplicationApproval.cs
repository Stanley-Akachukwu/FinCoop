using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplications;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationApprovals;

// To remove 
public class LoanApplicationApproval : BaseEntity<string>
{
    public LoanApplicationApproval()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public ApprovalStatus Status { get; set; }
    public string LoanApplicationId { get; set; }
    public virtual LoanApplication LoanApplication { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}