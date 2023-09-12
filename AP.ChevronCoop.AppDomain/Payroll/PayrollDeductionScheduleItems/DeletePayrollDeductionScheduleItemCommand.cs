using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionScheduleItems;

public partial class DeletePayrollDeductionScheduleItemCommand : DeleteCommand, IRequest<CommandResult<string>>
{ 
 
}