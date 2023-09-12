using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Accounting.PaymentModes
{
    public class PaymentMode : BaseEntity<string>
    {
        public PaymentMode()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public string Name { get; set; }

        public PaymentChannel Channel { get; set; } = PaymentChannel.CASH;

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