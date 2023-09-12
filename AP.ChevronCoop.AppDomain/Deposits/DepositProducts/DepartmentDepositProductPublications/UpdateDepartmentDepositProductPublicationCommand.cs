using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public partial class UpdateDepartmentDepositProductPublicationCommand : UpdateCommand, IRequest<CommandResult<DepartmentDepositProductPublicationViewModel>>
    {
        public PublicationType PublicationType { get; set; }
        public string ProductId { get; set; }
        public string DepartmentId { get; set; }

    }

}

