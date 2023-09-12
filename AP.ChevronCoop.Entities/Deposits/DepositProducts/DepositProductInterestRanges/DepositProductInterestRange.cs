using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges
{
    public class DepositProductInterestRange : BaseEntity<string>
    {

        public DepositProductInterestRange()
        {
            Id = NUlid.Ulid.NewUlid().ToString();

        }

        public string ProductId { get; set; }

        public virtual DepositProduct Product { get; set; }

        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal InterestRate { get; set; }


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
