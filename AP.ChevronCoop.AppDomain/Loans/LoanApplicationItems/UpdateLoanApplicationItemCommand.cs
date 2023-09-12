using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;

public class UpdateLoanApplicationItemCommand : UpdateCommand, IRequest<CommandResult<LoanApplicationItemViewModel>>
{
  public string LoanApplicationId { get; set; }
  public string ItemType { get; set; }
  public string Name { get; set; }
  public string BrandName { get; set; }
  public string Model { get; set; }
  public string Color { get; set; }
  public decimal Amount { get; set; }
}