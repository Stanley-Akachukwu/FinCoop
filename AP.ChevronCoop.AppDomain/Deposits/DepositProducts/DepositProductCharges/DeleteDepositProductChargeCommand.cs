using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges
{
    public partial class DeleteDepositProductChargeCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}

