using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges
{
    public partial class CreateDepositProductChargeCommand :  IRequest<CommandResult<DepositProductChargeViewModel>>
    {
        public string ProductId { get; set; }
        public string ChargeId { get; set; }
        public string CreatedByUserId { get; set; }
    }

}

