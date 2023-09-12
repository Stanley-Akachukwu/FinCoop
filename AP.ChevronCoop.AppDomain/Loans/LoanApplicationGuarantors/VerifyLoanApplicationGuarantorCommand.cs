using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class VerifyLoanApplicationGuarantorCommand: IRequest<CommandResult<VerifyLoanApplicationGuarantorViewModel>>
{
    public string MembershipId { get; set; }
    public DateTime CommencementDate { get; set; }
    public int TenureInMonths { get; set; }
}
