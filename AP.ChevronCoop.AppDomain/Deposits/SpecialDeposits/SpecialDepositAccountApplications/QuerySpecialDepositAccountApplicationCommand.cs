using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public class  QuerySpecialDepositAccountApplicationCommand : IRequest<CommandResult<IQueryable<SpecialDepositAccountApplication>>>
 { 
 
 }
}

