using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles
{
    public partial class CreateMemberProfileCommand : CreateCommand, IRequest<CommandResult<MemberProfileViewModel>>
    {

        // [MaxLength(900)]
        // [Required]
        public string ApplicationUserId { get; set; }

        // [Required]
        public bool IsKycStarted { get; set; }

        // [Required]
        public bool IsKycCompleted { get; set; }


        public DateTime? KycStartDate { get; set; }


        public DateTime? KycCompletedDate { get; set; }

        // [MaxLength(64)]
        // [Required]
        public string Status { get; set; }

        // [MaxLength(64)]
        // [Required]
        public string Gender { get; set; }

        [MaxLength(256)]

        public string ProfileImageUrl { get; set; }
        public string PassportUrl { get; set; }

        [MaxLength(256)]

        public string FirstName { get; set; }

        [MaxLength(256)]

        public string LastName { get; set; }

        [MaxLength(256)]

        public string MiddleName { get; set; }


        public string CAI { get; set; }


        public string RetireeNumber { get; set; }


        public string Address { get; set; }


        public string Country { get; set; }


        public string State { get; set; }

        // [MaxLength(80)]
        // [Required]
        public string DepartmentId { get; set; }

        [MaxLength(40)]
        [Required]
        public string RoleId { get; set; }

    }






}
