using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges
{
    public class QueryDepositProductInterestRangeCommand : IRequest<CommandResult<IQueryable<DepositProductInterestRange>>>
    {

    }

}

