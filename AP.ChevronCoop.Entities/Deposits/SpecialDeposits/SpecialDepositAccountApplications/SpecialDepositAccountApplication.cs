using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;


namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public class SpecialDepositAccountApplication : BaseEntity<string>
    {
        public SpecialDepositAccountApplication()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApplicationNo { get; set; }  // auto gen from  NHilo lib. 
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string DepositProductId { get; set; }
        public DepositProduct DepositProduct { get; set; }

        public string ApprovalId { get; set; }
        public Approval Approval { get; set; }


        //savings acc fields
        public decimal Amount { get; set; }

        //special deposit/fixed deposit fields
        public decimal InterestRate { get; set; } //read-only from Product

        //funding source
        public DepositFundingSourceType ModeOfPayment { get; set; }
        public string? PaymentDocumentId { get; set; }
        public CustomerPaymentDocument? PaymentDocument { get; set; }
        public string PaymentAccountNumber { get; set; } //Chevron Coop Account
        public string PaymentBankName { get; set; } //Chevron Coop Account number
        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }

       
    }


}

