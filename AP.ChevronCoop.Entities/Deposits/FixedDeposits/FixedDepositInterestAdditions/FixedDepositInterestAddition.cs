using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;

public class FixedDepositInterestAddition : BaseEntity<string>
{
    public FixedDepositInterestAddition()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }
    public string FixedDepositAccountId { get; set; }
    public FixedDepositAccount FixedDepositAccount { get; set; }


    public string InterestScheduleItemId { get; set; }
    public FixedDepositInterestScheduleItem InterestScheduleItem { get; set; }

    public decimal InterestEarned { get; set; }//=INTEREST(rate * old balance)

    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal TransactionJournal { get; set; }

    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}


