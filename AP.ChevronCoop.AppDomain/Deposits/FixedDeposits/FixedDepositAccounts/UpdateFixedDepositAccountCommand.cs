using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
public partial class UpdateFixedDepositAccountCommand : UpdateCommand, IRequest<CommandResult<FixedDepositAccountViewModel>>
{

    public string ApplicationId { get; set; }
    public string AccountNo { get; set; }
    public string CustomerId { get; set; }
    public string DepositProductId { get; set; }
    public string DepositAccountId { get; set; }
    public string ChargesAccruedAccountId { get; set; }
    public string InterestEarnedAccountId { get; set; }
    public string InterestPayoutAccountId { get; set; }
    public decimal Amount { get; set; }
    public string TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public decimal InterestRate { get; set; }
    public string MaturityInstructionType { get; set; }
    public string LiquidationAccountType { get; set; }
    public string SavingsLiquidationAccountId { get; set; }
    public string SpecialDepositLiquidationAccountId { get; set; }
    public string CustomerBankLiquidationAccountId { get; set; }
    public bool IsClosed { get; set; }
    public string ClosedByUserId { get; set; }
    public decimal MaximumBalanceLimit { get; set; }
    public decimal MinimumBalanceLimit { get; set; }
    public decimal SingleWithdrawalLimit { get; set; }
    public decimal DailyWithdrawalLimit { get; set; }
    public decimal WeeklyWithdrawalLimit { get; set; }
    public decimal MonthlyWithdrawalLimit { get; set; }

}


