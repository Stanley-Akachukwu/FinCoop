using System;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositLoanTransaction
{
    public class SpecialDepositLoanTransaction : BaseEntity<string>
    {
        public SpecialDepositLoanTransaction()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public virtual SpecialDepositAccount SpecialDepositAccount { get; set; }
        public string SpecialDepositAccountId { get; set; }

        public virtual LoanRepaymentSchedule LoanRepaymentSchedule { get; set; }
        public string LoanRepaymentScheduleId { get; set; }

        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }

    }
}

