using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments
{
    public partial class UpdateEnrollmentPaymentInfoCommand : UpdateCommand, IRequest<CommandResult<EnrollmentPaymentInfoViewModel>>
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
