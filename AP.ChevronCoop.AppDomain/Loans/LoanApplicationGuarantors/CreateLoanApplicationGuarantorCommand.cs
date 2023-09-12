using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class CreateLoanApplicationGuarantorCommand : IRequest<CommandResult<LoanApplicationGuarantorViewModel>>
{
    public string LoanApplicationId { get; set; }
    public GuarantorType GuarantorType { get; set; }
    public GuarantorApprovalType GuarantorApprovalType { get; set; }
    public string GuarantorProfileId { get; set; }
}