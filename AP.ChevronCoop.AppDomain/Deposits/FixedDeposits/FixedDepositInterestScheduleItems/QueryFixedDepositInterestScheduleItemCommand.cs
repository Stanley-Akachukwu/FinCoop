using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
public class QueryFixedDepositInterestScheduleItemCommand : IRequest<CommandResult<IQueryable<FixedDepositInterestScheduleItem>>>
{

}


