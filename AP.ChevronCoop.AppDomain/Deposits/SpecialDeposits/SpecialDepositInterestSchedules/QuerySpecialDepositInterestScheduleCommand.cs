using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
    public class QuerySpecialDepositInterestScheduleCommand : IRequest<CommandResult<IQueryable<SpecialDepositInterestSchedule>>>
    {

    }
}
