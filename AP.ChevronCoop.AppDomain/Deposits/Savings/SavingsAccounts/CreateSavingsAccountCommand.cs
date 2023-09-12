using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
public partial class CreateSavingsAccountCommand : CreateCommand, IRequest<CommandResult<SavingsAccountViewModel>>
{

    public string ApplicationId { get; set; }

    public string AccountNo { get; set; }

    public string CustomerId { get; set; }

    public string DepositProductId { get; set; }

    public decimal PayrollAmount { get; set; }

   // public bool IsClosed { get; set; }

    //public string ClosedByUserId { get; set; }

    //public decimal MaximumBalanceLimit { get; set; }

    //public decimal MinimumBalanceLimit { get; set; }

    //public decimal SingleWithdrawalLimit { get; set; }

    //public decimal DailyWithdrawalLimit { get; set; }

    //public decimal WeeklyWithdrawalLimit { get; set; }

    //public decimal MonthlyWithdrawalLimit { get; set; }

}


