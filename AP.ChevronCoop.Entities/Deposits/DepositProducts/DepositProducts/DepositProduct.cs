using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepositProducts
{
    public class DepositProduct : BaseEntity<string>
    {
        public DepositProduct()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
            Charges = new List<DepositProductCharge>();
            InterestRanges = new List<DepositProductInterestRange>();
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
        public string ApprovalWorkflowId { get; set; }
        public ApprovalWorkflow ApprovalWorkflow { get; set; }
        public string ApprovalId { get; set; }
        public Approval Approval { get; set; }
        public int? MinimumAge { get; set; }

        public int? MaximumAge { get; set; }

        public Tenure Tenure { get; set; } = Tenure.NONE;

        public decimal TenureValue { get; set; }
        public ProductStatus Status { get; set; }
        public PublicationType PublicationType { get; set; }
        public string? PublishedByUserId { get; set; }
        public ApplicationUser? PublishedByUser { get; set; }
        public string DefaultCurrencyId { get; set; }

        public Currency DefaultCurrency { get; set; }

        public string BankDepositAccountId { get; set; }

        public virtual CompanyBankAccount BankDepositAccount { get; set; }

        public string ProductDepositAccountId { get; set; }

        public virtual LedgerAccount ProductDepositAccount { get; set; }

        public string ChargesIncomeAccountId { get; set; }

        public virtual LedgerAccount ChargesIncomeAccount { get; set; }

        public string ChargesAccrualAccountId { get; set; }

        public virtual LedgerAccount ChargesAccrualAccount { get; set; }
        
        
        public string ChargesWaivedAccountId { get; set; }
        public virtual LedgerAccount ChargesWaivedAccount { get; set; }

        public string InterestPayableAccountId { get; set; }

        public virtual LedgerAccount InterestPayableAccount { get; set; }

        public string InterestPayoutAccountId { get; set; }

        public virtual LedgerAccount InterestPayoutAccount { get; set; }
        
        

        public bool IsInterestEnabled { get; set; } = false;
        public bool? IsDefaultProduct { get; set; } = false;


        public virtual List<DepositProductCharge> Charges { get; set; }
        public virtual List<DepositProductInterestRange> InterestRanges { get; set; }

        public decimal? MinimumContributionRegular { get; set; }

        public decimal? MinimumContributionRetiree { get; set; }
        
        public DepositProductType ProductType { get; set; }


        public override string DisplayCaption
        {
            get
            {
                return "";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }

    }


}
