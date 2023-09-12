using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.Customers;

public class CreateCustomerCommand : CreateCommand, IRequest<CommandResult<CustomerViewModel>>
{
  public string ProfileId { get; set; }
}