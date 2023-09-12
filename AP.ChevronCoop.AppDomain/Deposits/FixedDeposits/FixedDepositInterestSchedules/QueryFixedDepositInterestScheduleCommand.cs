using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestSchedules;
public class QueryFixedDepositInterestScheduleCommand : IRequest<CommandResult<IQueryable<FixedDepositInterestSchedule>>>
{

}


