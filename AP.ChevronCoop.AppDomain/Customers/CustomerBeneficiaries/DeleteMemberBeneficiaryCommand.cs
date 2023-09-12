using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.CustomerBeneficiaries;

public class DeleteCustomerBeneficiaryCommand: DeleteCommand, IRequest<CommandResult<string>>
{
  
}