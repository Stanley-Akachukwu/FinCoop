using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;

public class SavingsIncreaseDecrease : BaseEntity<string>
{
    public SavingsIncreaseDecrease()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }


    public string SavingsAccountId { get; set; }
    public SavingsAccount SavingsAccount { get; set; }
    public decimal Amount { get; set; }
    public ContributionChangeRequest ContributionChangeRequest { get; set; }
    
    public string? ApprovalId { get; set; }
    public Approval? Approval { get; set; }

    public override string DisplayCaption { get; }

    public override string DropdownCaption { get; }

    public override string ShortCaption { get; }
}