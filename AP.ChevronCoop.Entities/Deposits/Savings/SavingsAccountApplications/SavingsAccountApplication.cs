using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications
{
    public class SavingsAccountApplication : BaseEntity<string>
    {
        public SavingsAccountApplication()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApplicationNo { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string DepositProductId { get; set; }
        //TransactionType
        public DepositProduct DepositProduct { get; set; }
        // public ApprovalStatus Status { get; set; }
        
        
        public string? ApprovalId { get; set; }
        public Approval? Approval { get; set; }

        //savings acc fields
        public decimal Amount { get; set; }
        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }



}

