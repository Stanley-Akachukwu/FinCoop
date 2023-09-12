using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;

public partial class DeletePayrollDeductionItemCommand : DeleteCommand, IRequest<CommandResult<string>>
{ 
 
}