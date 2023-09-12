using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public partial class DeleteSpecialDepositAccountApplicationCommand : DeleteCommand, IRequest<CommandResult<string>>
 { 
 
 }
}

