using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.CustomerDepositProductPublications
{
    public class QueryCustomerDepositProductPublicationCommand : IRequest<CommandResult<IQueryable<CustomerDepositProductPublication>>>
    {

    }

}

