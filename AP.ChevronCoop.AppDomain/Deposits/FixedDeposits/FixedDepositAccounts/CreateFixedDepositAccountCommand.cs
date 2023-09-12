using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
public partial class CreateFixedDepositAccountCommand : CreateCommand, IRequest<CommandResult<FixedDepositAccountViewModel>>
{

    public string ApplicationId { get; set; }
    public string AccountNo { get; set; }
    public string CustomerId { get; set; }
    public string DepositProductId { get; set; }
    //public string DepositAccountId { get; set; }
    //public string ChargesAccruedAccountId { get; set; }
    //public string InterestEarnedAccountId { get; set; }
    //public string InterestPayoutAccountId { get; set; }
    public decimal Amount { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public decimal InterestRate { get; set; }
    public MaturityInstructionType MaturityInstructionType { get; set; }
    public WithdrawalAccountType LiquidationAccountType { get; set; }
    public string LiquidationAccountId { get; set; }

    public bool IsClosed { get; set; }
    public string ClosedByUserId { get; set; }

    public decimal MaximumBalanceLimit { get; set; }
    public decimal MinimumBalanceLimit { get; set; }
    public decimal SingleWithdrawalLimit { get; set; }
    public decimal DailyWithdrawalLimit { get; set; }
    public decimal WeeklyWithdrawalLimit { get; set; }
    public decimal MonthlyWithdrawalLimit { get; set; }

    public string RootParentAccountId { get; set; }
    public string ParentAccountId { get; set; }

}


