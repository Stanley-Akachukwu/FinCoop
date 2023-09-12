using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountDeductionSchedules;
public partial class DeleteSavingsAccountDeductionScheduleCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


