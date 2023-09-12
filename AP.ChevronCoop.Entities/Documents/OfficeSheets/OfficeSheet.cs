using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;

namespace AP.ChevronCoop.Entities.Documents.OfficeSheets
{
    public partial class OfficeSheet : BaseEntity<string>
    {

        public OfficeSheet()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        
        public string DocumentNo { get; set; }


        public string DocumentTypeId { get; set; }

        public virtual DocumentType DocumentType
        {
            get; set;
        }


        public string Name { get; set; }

        public virtual byte[] Document { get; set; }

        public string MimeType { get; set; }

        public string FilePath { get; set; }

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