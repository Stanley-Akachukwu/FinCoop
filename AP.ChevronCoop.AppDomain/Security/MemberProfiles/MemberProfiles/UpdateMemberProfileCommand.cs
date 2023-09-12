using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles
{
    public partial class UpdateMemberProfileCommand : UpdateCommand, IRequest<CommandResult<MemberProfileViewModel>>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int YearsOfService { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string Gender { get; set; }
        // public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string MembershipId { get; set; }
        public string CAI { get; set; }
        public string RetireeNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string OfficeAddress { get; set; }
        public string Rank { get; set; }
        public string DepartmentId { get; set; }
        public string JobRole { get; set; }
        public string ApplicationUserId { get; set; }
        public string Status { get; set; }
        public string ProfileImageUrl { get; set; }
        public string PassportUrl { get; set; }
        public DateTimeOffset? DOB { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string IdentificationUrl { get; set; }
        
        public bool SubmitKyc { get; set; } = false;

        
        // public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string StateOfOrigin { get; set; }
        public bool IsKycStarted { get; set; }
        public bool IsKycCompleted { get; set; }
        public DateTime? KycStartDate { get; set; }
        public DateTime? KycCompletedDate { get; set; }
    }
}
