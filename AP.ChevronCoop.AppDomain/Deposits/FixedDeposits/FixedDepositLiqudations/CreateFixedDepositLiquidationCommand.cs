using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
public partial class CreateFixedDepositLiquidationCommand : CreateCommand, IRequest<CommandResult<FixedDepositLiquidationViewModel>>
{


    public string CustomerId { get; set; }
    public string FixedDepositAccountId { get; set; }

    public WithdrawalAccountType LiquidationAccountType { get; set; }
    public string LiquidationAccountId { get; set; }

    public bool IsImmediate { get; set; } = true;


}


