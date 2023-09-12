using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.CustomerNextOfKins;

public class DeleteCustomerNextOfKinCommand: DeleteCommand, IRequest<CommandResult<string>>
{
  
}