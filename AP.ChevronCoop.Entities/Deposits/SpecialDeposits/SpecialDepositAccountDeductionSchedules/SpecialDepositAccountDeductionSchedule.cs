using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules
{
    public class SpecialDepositAccountDeductionSchedule : BaseEntity<string>
    {

        public SpecialDepositAccountDeductionSchedule()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public string SpecialDepositAccountId { get; set; }
        public SpecialDepositAccount SpecialDepositAccount { get; set; }
        public string BatchRefNo { get; set; }
        public string MemberId { get; set; }
        public string EmployeeNo { get; set; }
        public string MemberName { get; set; }
        public string AccountNo { get; set; }
        public decimal Amount { get; set; }
        public string PayrollCode { get; set; }// Should this be what we use to identify the account type
        public string Narration { get; set; }
        public DateTime DueDate { get; set; }
        public string CurrentStatus { get; set; }
        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }
}



