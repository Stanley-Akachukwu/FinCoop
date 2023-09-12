using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts
{
    public partial class CreateDepositProductCommand : IRequest<CommandResult<DepositProductViewModel>>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public DepositProductType ProductType { get; set; }
        public Tenure Tenure { get; set; }
        public decimal TenureValue { get; set; }
        public string DefaultCurrencyId { get; set; }
        public string BankDepositAccountId { get; set; }
        public string ApprovalWorkflowId { get; set; }
        public bool IsInterestEnabled { get; set; }
        public string ApplicationUserId { get; set; }
        public List<CreateDepositProductChargeCommand> ProductCharges { get; set; }
        public List<CreateDepositProductInterestRangeCommand> InterestRanges { get; set; }
        public ProductStatus Status { get; set; }
        public decimal? MinimumContributionRetiree { get; set; } = 5000;
        public decimal? MinimumContributionRegular { get; set; } = 50000;
        public string CreatedByUserId { get; set; }
        public bool? IsDefaultProduct { get; set; } = false;

    }

}

