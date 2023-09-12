using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.Customers
{
    public class QueryCustomerCommand : IRequest<CommandResult<IQueryable<Customer>>>
    {

    }







}
