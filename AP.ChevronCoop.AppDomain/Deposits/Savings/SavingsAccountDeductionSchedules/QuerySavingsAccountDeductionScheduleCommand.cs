
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountDeductionSchedules;
public class QuerySavingsAccountDeductionScheduleCommand : IRequest<CommandResult<IQueryable<SavingsAccountDeductionSchedule>>>
{

}


