using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.CustomerBeneficiaries;

public class UpdateCustomerBeneficiaryCommand : UpdateCommand, IRequest<CommandResult<CustomerBeneficiaryViewModel>>
{
  public string ProfileId { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string Address { get; set; }
}