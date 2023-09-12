using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsets
{
    public class ProcessLoanOffsetCommand : IRequest<CommandResult<LoanOffsetViewModel>>
    {
        public ApprovalStatus Status { get; set; }
        public string LoanOffsetId { get; set; }
    }
}
