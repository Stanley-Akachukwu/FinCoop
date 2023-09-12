using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public partial class SpecialDepositInterestScheduleItemViewModel : BaseViewModel
    {
        public string SpecialDepositAccountId { get; set; }
        public string SpecialDepositInterestScheduleId { get; set; }
        public decimal OldBalance { get; set; }
        public decimal PeriodCashAddition { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestEarned { get; set; }
        public decimal NewBalance { get; set; }
        public string SpecialDepositInterestScheduleId1 { get; set; }

    }

}
