using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public class UpdateDepositProductStatusCommand : IRequest<CommandResult<DepositProductViewModel>>
    {
        public string DepositProductId { get; set; }
        public ProductStatus Status { get; set; }

    }
}

 