using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;

public partial class DeletePayrollDeductionScheduleCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}