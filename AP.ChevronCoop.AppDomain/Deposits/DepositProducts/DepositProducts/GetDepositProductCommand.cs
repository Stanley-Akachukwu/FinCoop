using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public record GetDepositProductCommand(string Id) : IRequest<CommandResult<GetDepositProductViewModel>>;
}