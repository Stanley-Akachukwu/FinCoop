
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public partial class DepositProductViewModel : BaseViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public Tenure Tenure { get; set; }
        public decimal TenureValue { get; set; }
        public string DefaultCurrencyId { get; set; }
        public string BankDepositAccountId { get; set; }
        public string ProductDepositAccountId { get; set; }
        public string ChargesIncomeAccountId { get; set; }
        public DepositProductType ProductType { get; set; }
        public string ChargesAccrualAccountId { get; set; }
        public string InterestPayableAccountId { get; set; }
        public string InterestPayoutAccountId { get; set; }
        public bool IsInterestEnabled { get; set; }

        public string Status { get; set; }

        public decimal? MinimumContributionRetiree { get; set; }

        public decimal? MinimumContributionRegular { get; set; }

        public string ApprovalWorkflowId { get; set; }
        public string ApprovalWorkflowName { get; set; }

        public List<DepositProductChargeViewModel> ProductCharges { get; set; }
        public List<DepositProductInterestRangeViewModel> InterestRanges { get; set; }

    }

}

