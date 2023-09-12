using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public partial class GetDepositProductViewModel : DepositProductViewModel
    {
        public List<DepositProductChargeViewModel> ProductCharges { get; set; }
        public List<DepositProductInterestRangeViewModel> InterestRanges { get; set; }

        public List<LedgerAccountViewModel> LedgerAccounts { get; set; }

    }
}

