using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems
{
    public partial class ImportPayrollDeductionItemCommand : IRequest<CommandResult<string>>
    {
        public List<PayrollDeductionItemViewModel> PayrollDeductionItems { get; set; }  
        public string CreatedByUserId { get; set; }
    }

}
