using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Accounting.LienTypes
{
    public class LienType : BaseEntity<string>
    {

        public LienType()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public string Code
        { get; set; }


        public string Name
        { get; set; }


        // [NotMapped]
        public override string DisplayCaption
        {
            get
            {
                return $"{Name}/{Code}";
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