using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public partial class CreateDepartmentDepositProductPublicationCommand : CreateCommand, IRequest<CommandResult<DepartmentDepositProductPublicationViewModel>>
    {
        public PublicationType PublicationType { get; set; }
        public string ProductId { get; set; }
        public string DepartmentId { get; set; }

    }

}

