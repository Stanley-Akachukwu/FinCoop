using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
public partial class UpdateFixedDepositLiquidationCommand : UpdateCommand, IRequest<CommandResult<FixedDepositLiquidationViewModel>>
{
    
    public string FixedDepositAccountId { get; set; }
 
    public WithdrawalAccountType LiquidationAccountType { get; set; }

    public string LiquidationAccountId { get; set; }

   

}


