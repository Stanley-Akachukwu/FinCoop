using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public class QuerySpecialDepositInterestScheduleItemCommand : IRequest<CommandResult<IQueryable<SpecialDepositInterestScheduleItem>>>
    {

    }

}

