using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts
{
    public class QueryCustomerBankAccountCommand : IRequest<CommandResult<IQueryable<CustomerBankAccount>>>
    {

    }






}
