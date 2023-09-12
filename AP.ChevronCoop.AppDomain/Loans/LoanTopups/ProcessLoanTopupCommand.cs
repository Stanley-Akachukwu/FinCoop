using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopups
{
    public class ProcessLoanTopupCommand : IRequest<CommandResult<LoanTopupViewModel>>
    {
        public ApprovalStatus Status { get; set; }
        public string LoanTopupId { get; set; }
    }
}
