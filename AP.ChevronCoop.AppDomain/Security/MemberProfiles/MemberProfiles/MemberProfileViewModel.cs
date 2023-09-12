using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles
{
    public partial class MemberProfileViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int YearsOfService { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsKycStarted { get; set; }
        public bool IsKycCompleted { get; set; }
        public DateTime? KycStartDate { get; set; }
        public DateTime? KycCompletedDate { get; set; }
        public string Status { get; set; }
        public string ProfileImageUrl { get; set; }
        public string PassportUrl { get; set; }
        public DateTimeOffset DOB { get; set; }
        public string SecondaryEmail { get; set; }
        
        public string MemberType { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string IdentificationUrl { get; set; }
        public bool KycSubmitted { get; set; }
        public DateTime? KycSubmittedOn { get; set; }
        public bool KycApproved { get; set; }
        public DateTime? KycApprovedOn { get; set; }
        public bool KycApprovedBy { get; set; }

        public string CAI { get; set; }

        public string RetireeNumber { get; set; }
        // public string MembershipNumber { get; set; }
        public string MembershipId { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string State { get; set; }
        public string StateOfOrigin { get; set; }
        
        public string DepartmentId { get; set; }
    }
}
