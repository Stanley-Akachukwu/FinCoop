using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public partial class SpecialDepositAccountViewModel : BaseViewModel
    {
        public string ApplicationId { get; set; }
        public string AccountNo { get; set; }
        public string CustomerId { get; set; }
        public string DepositProductId { get; set; }
        public string DepositAccountId { get; set; }
        public string ChargesAccruedAccountId { get; set; }
        public string InterestEarnedAccountId { get; set; }
        public string InterestPayoutAccountId { get; set; }
        public decimal FundingAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime LastInterestComputationDate { get; set; }
        public decimal MaximumBalanceLimit { get; set; }
        public decimal MinimumBalanceLimit { get; set; }
        public decimal SingleWithdrawalLimit { get; set; }
        public decimal DailyWithdrawalLimit { get; set; }
        public decimal WeeklyWithdrawalLimit { get; set; }
        public decimal MonthlyWithdrawalLimit { get; set; }
        public bool IsClosed { get; set; }
        public string ClosedByUserId { get; set; }

    }

}

