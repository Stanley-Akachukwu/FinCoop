using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.Customers
{
    public partial class DeleteCustomerCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }







}
