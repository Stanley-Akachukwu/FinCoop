using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public partial class DeleteDepartmentDepositProductPublicationCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }

}

