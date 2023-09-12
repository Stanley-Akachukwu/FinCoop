using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using System;

namespace AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications
{
    public class CustomerDepositProductPublication : BaseEntity<string>
    {

        public CustomerDepositProductPublication()
        {
            Id = NUlid.Ulid.NewUlid().ToString();

        }
        public PublicationType PublicationType { get; set; }
        public string ProductId { get; set; }
        public virtual DepositProduct Product { get; set; }

        public string? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
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
