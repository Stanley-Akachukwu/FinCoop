using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;

public class QueryPayrollDeductionScheduleCommand : IRequest<CommandResult<IQueryable<Entities.Payroll.PayrollDeductionSchedules.PayrollDeductionSchedule>>>
{

}