using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBeneficiaries;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.CustomerBeneficiaries;

public class QueryCustomerBeneficiaryCommand : IRequest<CommandResult<IQueryable<CustomerBeneficiary>>>
{
}