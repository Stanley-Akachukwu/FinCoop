using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerNextOfKins;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.CustomerNextOfKins;

public class QueryCustomerNextOfKinCommand : IRequest<CommandResult<IQueryable<CustomerNextOfKin>>>
{
}