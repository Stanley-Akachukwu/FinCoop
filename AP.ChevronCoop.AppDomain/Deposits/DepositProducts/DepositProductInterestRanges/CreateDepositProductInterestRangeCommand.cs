using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges
{
    public partial class CreateDepositProductInterestRangeCommand :  IRequest<CommandResult<DepositProductInterestRangeViewModel>>
    {
        public string ProductId { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal InterestRate { get; set; }
        public string CreatedByUserId { get; set; }
    }

}

