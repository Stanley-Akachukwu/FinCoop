using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
public partial class DeleteFixedDepositChangeInMaturityCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


