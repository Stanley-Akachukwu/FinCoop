using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.MasterData.Departments;
using System;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications
{
    public class DepartmentDepositProductPublication : BaseEntity<string>
    {
        public DepartmentDepositProductPublication()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public PublicationType PublicationType { get; set; }
       // public ProductType ProductType { get; set; }
        public string ProductId { get; set; }
        public virtual DepositProduct Product { get; set; }
        public string DepartmentId { get; set; }

        public virtual Department Department { get; set; }
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
