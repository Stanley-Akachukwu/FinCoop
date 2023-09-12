using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges
{
    public partial class DepositProductInterestRangeViewModel : BaseViewModel
    {
        public string ProductId { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal InterestRate { get; set; }

    }

}


