using AP.ChevronCoop.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalDocuments
{
    public class ApprovalDocument : BaseEntity<string>
    {

        public ApprovalDocument()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        [Required]
        public string ApprovalId { get; set; }

        [ForeignKey(nameof(ApprovalId))]
        public virtual Approval Approval { get; set; }


        public byte[] Document { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }



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
