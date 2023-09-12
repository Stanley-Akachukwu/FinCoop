using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.MasterData.Charges;

namespace AP.ChevronCoop.Entities.Loans.LoanProductCharges
{
    public class LoanProductCharge : BaseEntity<string>
    {

        public LoanProductCharge()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public ChargeType ChargeType { get; set; }
        public string ProductId { get; set; }
        
        public virtual LoanProduct Product { get; set; }
        
        public string ChargeId { get; set; }

        public virtual Charge Charge { get; set; }

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
