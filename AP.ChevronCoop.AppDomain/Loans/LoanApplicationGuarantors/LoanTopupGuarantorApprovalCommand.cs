using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors
{
    public class LoanTopupGuarantorApprovalCommand : IRequest<CommandResult<LoanTopupGuarantorApprovalViewModel>>
    {
        public string LoanAccountId { get; set; }
        public string GuarantorId { get; set; }
        public bool IsApproved { get; set; }
        public string Comment { get; set; }
    }
}
