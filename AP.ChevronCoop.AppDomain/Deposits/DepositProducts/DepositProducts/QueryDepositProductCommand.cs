using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public class QueryDepositProductCommand : IRequest<CommandResult<IQueryable<DepositProduct>>>
    {

    }

}


