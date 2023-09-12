using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;

public class SpecialDepositIncreaseDecrease : BaseEntity<string>
{
    public SpecialDepositIncreaseDecrease()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }


    public string SpecialDepositAccountId { get; set; }
    public SpecialDepositAccount SpecialDepositAccount { get; set; }
    public decimal Amount { get; set; }
    public ContributionChangeRequest ContributionChangeRequest { get; set; }

    public string ApprovalId { get; set; }
    public Approval Approval { get; set; }

    public override string DisplayCaption { get; }

    public override string DropdownCaption { get; }

    public override string ShortCaption { get; }
}