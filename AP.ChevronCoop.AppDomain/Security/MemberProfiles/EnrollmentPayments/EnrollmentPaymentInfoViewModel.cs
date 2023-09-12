using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments
{
    public partial class EnrollmentPaymentInfoViewModel : BaseViewModel
    {
        [MaxLength(80)]
        public string ProfileId { get; set; }


        [Required]
        public byte[] Evidence { get; set; }

        [MaxLength(128)]
        [Required]
        public string MimeType { get; set; }

        [MaxLength(512)]
        [Required]
        public string FileName { get; set; }


        [Required]
        public int FileSize { get; set; }

    }



}
