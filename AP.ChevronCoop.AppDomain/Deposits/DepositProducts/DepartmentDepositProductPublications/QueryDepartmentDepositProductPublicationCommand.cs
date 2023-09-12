using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public class QueryDepartmentDepositProductPublicationCommand : IRequest<CommandResult<IQueryable<DepartmentDepositProductPublication>>>
    {

    }

}

