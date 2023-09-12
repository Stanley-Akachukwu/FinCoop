using AP.ChevronCoop.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.MasterData.Currencies
{
    public class Currency : BaseEntity<string>
    {

        public Currency()
        {

            Id = NUlid.Ulid.NewUlid().ToString();

        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string IsoSymbol { get; set; }

        public int? DecimalPlaces { get; set; } = 0;

        public string Format { get; set; }

        public override string DisplayCaption
        {
            get
            {
                return $"{Symbol} {Name}";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return $"{Name} ({Symbol})";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return $"{Name}";
            }
        }

    }
}