using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments
{
    public partial class CheckEnrollmentPaymentInfoCommand :  IRequest<CommandResult<bool>>
    {
        [Required]
        public virtual string MembershipId { get; set; }
        [Required]
        public virtual string Email { get; set; }
    }
}
