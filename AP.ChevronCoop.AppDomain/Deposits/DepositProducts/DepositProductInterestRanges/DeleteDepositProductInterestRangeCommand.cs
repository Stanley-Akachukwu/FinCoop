using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges
{
    public partial class DeleteDepositProductInterestRangeCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }

}

