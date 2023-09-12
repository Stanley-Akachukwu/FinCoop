namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;

public partial class SavingsAccountViewModel : BaseViewModel
{
    public string? ApplicationId { get; set; }
    public string AccountNo { get; set; }
    public string? CustomerId { get; set; }
    public string? DepositProductId { get; set; }
    public string? LedgerDepositAccountId { get; set; }
    public string? ChargesPayableAccountId { get; set; }
    public string ChargesAccruedAccountId { get; set; }
    public string ChargesWaivedAccountId { get; set; }
    public string ChargesIncomeAccountId { get; set; }
    public decimal PayrollAmount { get; set; }
    public bool IsClosed { get; set; }
    public string? ClosedByUserId { get; set; }
    public decimal MaximumBalanceLimit { get; set; }
    public decimal MinimumBalanceLimit { get; set; }
    public decimal SingleWithdrawalLimit { get; set; }
    public decimal DailyWithdrawalLimit { get; set; }
    public decimal WeeklyWithdrawalLimit { get; set; }
    public decimal MonthlyWithdrawalLimit { get; set; }
    public DateTime? DateClosed { get; set; }

}
//public partial class SavingsAccountViewModel : BaseViewModel
//{

//    public string ApplicationId { get; set; }
//    public string AccountNo { get; set; }

//    public string CustomerId { get; set; }

//    public string DepositProductId { get; set; }

//    public string LedgerDepositAccountId { get; set; }

//    public string ChargesPayableAccountId { get; set; }
//    public decimal PayrollAmount { get; set; }
//    public bool IsClosed { get; set; }

//    public string ClosedByUserId { get; set; }

//    public decimal MaximumBalanceLimit { get; set; }
//    public decimal MinimumBalanceLimit { get; set; }

//    public decimal SingleWithdrawalLimit { get; set; }
//    public decimal DailyWithdrawalLimit { get; set; }

//    public decimal WeeklyWithdrawalLimit { get; set; }

//    public decimal MonthlyWithdrawalLimit { get; set; }

//}


