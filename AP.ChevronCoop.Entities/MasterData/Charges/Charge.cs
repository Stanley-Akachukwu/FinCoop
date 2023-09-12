using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Entities.MasterData.Currencies;

namespace AP.ChevronCoop.Entities.MasterData.Charges
{
    public class Charge : BaseEntity<string>
    {

        public Charge()
        {
            Id = NUlid.Ulid.NewUlid().ToString();

        }
        
        public string Code { get; set; }

        public string Name { get; set; }

        public ChargeMethod Method { get; set; } = ChargeMethod.FLAT;

        public ChargeTarget Target { get; set; } = ChargeTarget.VALUE;

        public ChargeCalculationMethod CalculationMethod { get; set; } = ChargeCalculationMethod.ADD;

        public string CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public decimal ChargeValue { get; set; } = 0; //1,2
        //public decimal FlatFee { get; set; } = 0;

        //public decimal Percent { get; set; } = 0;

        public decimal? MaximumCharge { get; set; }
        public decimal? MinimimumCharge { get; set; }



        public decimal CalculateCharge(decimal targetValue)
        {
            if (Method == ChargeMethod.FLAT)
            {
                //return FlatFee;
                return ChargeValue;
            }
            else
            {
                if (ChargeValue == 0) return 0;
                return targetValue * (ChargeValue / 100);
            }

        }


        [NotMapped]
        public bool IsValidPercent
        {
            get
            {
                if (Method == ChargeMethod.PERCENT && ChargeValue < 0)
                {
                    return false;
                }

                return true;
            }
        }



        [NotMapped]
        public bool IsValidFlatFee
        {
            get
            {
                if (Method == ChargeMethod.FLAT && ChargeValue < 0)
                {
                    return false;
                }

                return true;
            }
        }

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