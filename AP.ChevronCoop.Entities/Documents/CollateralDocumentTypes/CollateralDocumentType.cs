using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Documents.CollateralDocumentTypes
{
    public class CollateralDocumentType : BaseEntity<string>
    {

        public CollateralDocumentType()
        {
            Id = NUlid.Ulid.NewUlid().ToString();

        }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool SystemFlag { get; set; } = false;

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