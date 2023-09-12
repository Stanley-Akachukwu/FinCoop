using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments
{
    public partial class CreateEnrollmentPaymentInfoCommand : CreateCommand, IRequest<CommandResult<EnrollmentPaymentInfoViewModel>>
    {
        public string ProfileId { get; set; }
        public byte[] Document { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }

    }






}
