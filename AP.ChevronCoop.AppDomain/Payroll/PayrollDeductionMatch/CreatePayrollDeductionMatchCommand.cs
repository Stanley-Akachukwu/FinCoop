using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionMatch;

public class CreatePayrollDeductionMatchCommand : IRequest<CommandResult<bool>>
{
    public string ScheduleId { get; set; }
}