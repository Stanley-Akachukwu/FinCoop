using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public class SpecialDepositInterestAddition : BaseEntity<string>
    {
        public SpecialDepositInterestAddition()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string SpecialDepositAccountId { get; set; }
        public SpecialDepositAccount SpecialDepositAccount { get; set; }


        public string InterestScheduleItemId { get; set; }
        public virtual SpecialDepositInterestScheduleItem InterestScheduleItem { get; set; }

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

}




