using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Payroll;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;

public class  QueryPayrollDeductionItemCommand : IRequest<CommandResult<IQueryable<PayrollDeductionItem>>>
{ 
 
}