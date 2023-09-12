using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public class PublishDepositProductCommand : IRequest<CommandResult<DepositProductViewModel>>
    {
        public string ProductId { get; set; }
        public PublicationType PublicationType { get; set; }
        public List<string> Targets { get; set; }
        public string PublishedByUserId { get; set; }
    }
}
