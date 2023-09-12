using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public class SpecialDepositAccount : BaseEntity<string>
    {
        public SpecialDepositAccount()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApplicationId { get; set; }
        public SpecialDepositAccountApplication Application { get; set; }
        public string AccountNo { get; set; }//To be mapped to ledger acc code 
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string DepositProductId { get; set; }
        public DepositProduct DepositProduct { get; set; }

        public string DepositAccountId { get; set; }
        public virtual LedgerAccount DepositAccount { get; set; }
        public string ChargesAccruedAccountId { get; set; }
        public virtual LedgerAccount ChargesAccruedAccount { get; set; }

        public string ChargesIncomeAccountId { get; set; }
        public virtual LedgerAccount ChargesIncomeAccount { get; set; }

        public string ChargesWaivedAccountId { get; set; }
        public virtual LedgerAccount ChargesWaivedAccount { get; set; }

        public string InterestEarnedAccountId { get; set; }
        public virtual LedgerAccount InterestEarnedAccount { get; set; }
        public string InterestPayoutAccountId { get; set; }
        public virtual LedgerAccount InterestPayoutAccount { get; set; }



        //savings acc fields
        public decimal FundingAmount { get; set; }
        //special deposit/fixed deposit fields
        public decimal InterestRate { get; set; } //read-only from products

        public DateTime? LastInterestComputationDate { get; set; }


        public decimal MaximumBalanceLimit { get; set; }
        public decimal MinimumBalanceLimit { get; set; }
        public decimal SingleWithdrawalLimit { get; set; }
        public decimal DailyWithdrawalLimit { get; set; }
        public decimal WeeklyWithdrawalLimit { get; set; }
        public decimal MonthlyWithdrawalLimit { get; set; }

        public bool IsClosed { get; set; } = false;
        public DateTime? DateClosed { get; set; }
        public string? ClosedByUserId { get; set; }
        public ApplicationUser? ClosedByUser { get; set; }

        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }

}

