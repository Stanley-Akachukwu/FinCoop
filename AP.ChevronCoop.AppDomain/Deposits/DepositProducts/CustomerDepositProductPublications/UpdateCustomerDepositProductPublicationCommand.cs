using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.CustomerDepositProductPublications
{
    public partial class UpdateCustomerDepositProductPublicationCommand : UpdateCommand, IRequest<CommandResult<CustomerDepositProductPublicationViewModel>>
    {
        public PublicationType PublicationType { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }

    }

}

