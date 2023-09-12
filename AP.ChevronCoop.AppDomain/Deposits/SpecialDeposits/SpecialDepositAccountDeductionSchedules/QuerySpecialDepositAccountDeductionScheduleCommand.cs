using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules
{
    public class QuerySpecialDepositAccountDeductionScheduleCommand : IRequest<CommandResult<IQueryable<SpecialDepositAccountDeductionSchedule>>>
    {

    }

}

