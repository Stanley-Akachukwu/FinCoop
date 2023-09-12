using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.CustomerDepositProductPublications
{
    public partial class DeleteCustomerDepositProductPublicationCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }

}

