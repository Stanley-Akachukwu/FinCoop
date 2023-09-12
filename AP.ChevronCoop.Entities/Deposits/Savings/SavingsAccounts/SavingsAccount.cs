using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts
{
    public class SavingsAccount : BaseEntity<string>
    {
        public SavingsAccount()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApplicationId { get; set; }
        public SavingsAccountApplication Application { get; set; }
        public string AccountNo { get; set; }//11 nuban compatibility
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string DepositProductId { get; set; }
        public DepositProduct DepositProduct { get; set; }
        public string LedgerDepositAccountId { get; set; }
        public virtual LedgerAccount LedgerDepositAccount { get; set; }
        public string ChargesPayableAccountId { get; set; }
        public virtual LedgerAccount ChargesPayableAccount { get; set; }

        public string ChargesAccruedAccountId { get; set; }
        public virtual LedgerAccount ChargesAccruedAccount { get; set; }

        
        public string ChargesWaivedAccountId { get; set; }
        public virtual LedgerAccount ChargesWaivedAccount { get; set; }
        
        public string ChargesIncomeAccountId { get; set; }
        public virtual LedgerAccount ChargesIncomeAccount { get; set; }


        //savings acc fields
        public decimal PayrollAmount { get; set; }

        public bool IsClosed { get; set; } = false;
        public DateTime? DateClosed { get; set; }
        public string? ClosedByUserId { get; set; }
        public ApplicationUser? ClosedByUser { get; set; }


        public decimal MaximumBalanceLimit { get; set; }
        public decimal MinimumBalanceLimit { get; set; }
        public decimal SingleWithdrawalLimit { get; set; }
        public decimal DailyWithdrawalLimit { get; set; }
        public decimal WeeklyWithdrawalLimit { get; set; }
        public decimal MonthlyWithdrawalLimit { get; set; }
        
        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }
}
