using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;

public class DeletePayrollCronJobConfigCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}