using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments
{
    public partial class EnrollmentPaymentInfoMasterView
    {

        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string? ProfileId { get; set; }
        public string? MemberProfileId { get; set; }
        public byte[] Evidence { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public string? DeletedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
        public Guid RowVersion { get; set; }
        public string? FullText { get; set; }
        public string? Tags { get; set; }
        public string? Caption { get; set; }
        public string? MemberProfileId_ApplicationUserId { get; set; }
        public int? MemberProfileId_YearsOfService { get; set; }
        public bool? MemberProfileId_IsKycStarted { get; set; }
        public bool? MemberProfileId_IsKycCompleted { get; set; }
        public DateTime? MemberProfileId_KycStartDate { get; set; }
        public DateTime? MemberProfileId_KycCompletedDate { get; set; }
        public string? MemberProfileId_Status { get; set; }
        public string? MemberProfileId_MemberType { get; set; }
        public string? MemberProfileId_Gender { get; set; }
        public string? MemberProfileId_ProfileImageUrl { get; set; }
        public string? MemberProfileId_PassportUrl { get; set; }
        public string? MemberProfileId_IdentificationType { get; set; }
        public string? MemberProfileId_IdentificationNumber { get; set; }
        public string? MemberProfileId_IdentificationUrl { get; set; }
        public bool? MemberProfileId_KycSubmitted { get; set; }
        public DateTime? MemberProfileId_KycSubmittedOn { get; set; }
        public bool? MemberProfileId_KycApproved { get; set; }
        public DateTime? MemberProfileId_KycApprovedOn { get; set; }
        public bool? MemberProfileId_KycApprovedBy { get; set; }
        public string? MemberProfileId_FirstName { get; set; }
        public string? MemberProfileId_LastName { get; set; }
        public string? MemberProfileId_MiddleName { get; set; }
        public string? MemberProfileId_DepartmentId { get; set; }
        public string? MemberProfileId_MembershipId { get; set; }
        public string? MemberProfileId_CAI { get; set; }
        public string? MemberProfileId_RetireeNumber { get; set; }
        public string? MemberProfileId_StateOfOrigin { get; set; }
        public string? MemberProfileId_PrimaryEmail { get; set; }
        public string? MemberProfileId_SecondaryEmail { get; set; }
        public string? MemberProfileId_PrimaryPhone { get; set; }
        public string? MemberProfileId_SecondaryPhone { get; set; }
        public string? MemberProfileId_ResidentialAddress { get; set; }
        public string? MemberProfileId_OfficeAddress { get; set; }
        public string? MemberProfileId_Rank { get; set; }
        public bool? MemberProfileId_SwitchToRetireeRequested { get; set; }
        public string? MemberProfileId_JobRole { get; set; }
        public DateTimeOffset? MemberProfileId_DOB { get; set; }
        public string? MemberProfileId_Address { get; set; }
        public string? MemberProfileId_Country { get; set; }
        public string? MemberProfileId_State { get; set; }
        public bool? MemberProfileId_IsActive { get; set; }
        public string? MemberProfileId_CreatedByUserId { get; set; }
        public string? MemberProfileId_UpdatedByUserId { get; set; }
        public string? MemberProfileId_DeletedByUserId { get; set; }
        public bool? MemberProfileId_IsDeleted { get; set; }
        public string? MemberProfileId_Tags { get; set; }
        public string? MemberProfileId_Caption { get; set; }
    }
}
