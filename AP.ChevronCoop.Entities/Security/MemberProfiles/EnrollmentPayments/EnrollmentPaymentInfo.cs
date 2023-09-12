using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments
{
    public class EnrollmentPaymentInfo :BaseEntity<string>
    {

        public EnrollmentPaymentInfo()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public string ProfileId { get; set; }
        public virtual MemberProfile MemberProfile { get; set; }
        public byte[] Evidence { get; set; }
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
