using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;

public class  QueryPayrollCronJobConfigCommand : IRequest<CommandResult<IQueryable<PayrollCronJobConfig>>>
{ 
 
}