using AP.ChevronCoop.Entities.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Documents.DocumentTypes
{
    public class DocumentType : BaseEntity<string>
    {

        public DocumentType()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }


        public string Name { get; set; }
        public bool SystemFlag { get; set; }

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