using AP.ChevronCoop.Commons;
using MediatR;
namespace AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges
{
    public class QueryDepositProductChargeCommand : IRequest<CommandResult<IQueryable<DepositProductCharge>>>
    {

    }

}

