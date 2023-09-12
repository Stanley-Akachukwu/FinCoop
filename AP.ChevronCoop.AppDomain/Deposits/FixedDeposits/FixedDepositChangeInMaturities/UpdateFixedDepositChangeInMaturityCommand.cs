using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
public partial class UpdateFixedDepositChangeInMaturityCommand : UpdateCommand, IRequest<CommandResult<FixedDepositChangeInMaturityViewModel>>
{

    public string FixedDepositAccountId { get; set; }
    public MaturityInstructionType MaturityInstructionType { get; set; }
    public WithdrawalAccountType LiquidationAccountType { get; set; }
    
    public string LiquidationAccountId { get; set; }


}


