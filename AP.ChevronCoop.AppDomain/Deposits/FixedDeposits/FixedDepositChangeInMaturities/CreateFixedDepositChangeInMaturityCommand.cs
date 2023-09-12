using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;

public partial class CreateFixedDepositChangeInMaturityCommand :  IRequest<CommandResult<FixedDepositChangeInMaturityViewModel>>
{

    public string CustomerId { get; set; }
    public string FixedDepositAccountId { get; set; }
    public MaturityInstructionType MaturityInstructionType { get; set; }
    public WithdrawalAccountType LiquidationAccountType { get; set; }
    public string LiquidationAccountId { get; set; }
    public string CreatedByUserId { get; set; }

   // public string ApprovalId { get; set; }
}