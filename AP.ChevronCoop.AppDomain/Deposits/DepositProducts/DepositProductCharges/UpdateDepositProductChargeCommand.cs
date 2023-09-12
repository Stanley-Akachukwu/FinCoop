using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges
{
    public partial class UpdateDepositProductChargeCommand :UpdateCommand,  IRequest<CommandResult<DepositProductChargeViewModel>>
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ChargeId { get; set; }
    }

}

