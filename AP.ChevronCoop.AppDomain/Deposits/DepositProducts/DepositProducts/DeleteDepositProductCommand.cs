using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public partial class DeleteDepositProductCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }

}

