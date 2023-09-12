using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges
{
    public partial class UpdateDepositProductInterestRangeCommand :UpdateCommand, IRequest<CommandResult<DepositProductInterestRangeViewModel>>
    {
        public string Id { get; set; }

        public string ProductId { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal InterestRate { get; set; }

    }

}


