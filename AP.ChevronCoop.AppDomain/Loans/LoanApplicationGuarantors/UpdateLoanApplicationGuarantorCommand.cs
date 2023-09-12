using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class UpdateLoanApplicationGuarantorCommand : UpdateCommand,
  IRequest<CommandResult<LoanApplicationGuarantorViewModel>>
{
  public string LoanApplicationId { get; set; }

  public string GuarantorType { get; set; }
  public string GuarantorProfileId { get; set; }

  public string BrandName { get; set; }
  public string Model { get; set; }
  public string Color { get; set; }
  public decimal Amount { get; set; }
}