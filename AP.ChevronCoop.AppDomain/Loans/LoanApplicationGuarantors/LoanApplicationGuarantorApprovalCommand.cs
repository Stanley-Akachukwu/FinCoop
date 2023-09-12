using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class LoanApplicationGuarantorApprovalCommand : IRequest<CommandResult<LoanApplicationGuarantorApprovalViewModel>>
{
    public string LoanApplicationId { get; set; }
    public string GuarantorId { get; set; }
    public bool IsApproved { get; set; }
    public string Comment { get; set; }
    public GuarantorApprovalType GuarantorApprovalType { get; set; }
}