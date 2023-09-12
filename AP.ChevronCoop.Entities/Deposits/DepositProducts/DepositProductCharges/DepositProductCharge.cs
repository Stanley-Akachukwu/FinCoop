using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.MasterData.Charges;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges
{
    public class DepositProductCharge : BaseEntity<string>
    {

        public DepositProductCharge()
        {
            Id = NUlid.Ulid.NewUlid().ToString();

        }

        public string ProductId { get; set; }

        public virtual DepositProduct Product { get; set; }

        public string ChargeId { get; set; }

        public virtual Charge Charge { get; set; }

        public DepositChargeType? ChargeType { get; set; } = DepositChargeType.NONE;

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
