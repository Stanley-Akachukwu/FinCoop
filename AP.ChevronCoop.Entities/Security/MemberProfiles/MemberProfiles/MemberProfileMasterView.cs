using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles
{
    public partial class MemberProfileMasterView
    {
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsKycStarted { get; set; }
        public bool IsKycCompleted { get; set; }
        public DateTime? KycStartDate { get; set; }
        public DateTime? KycCompletedDate { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? PassportUrl { get; set; }
        public int? YearsOfService { get; set; }
        public string? FirstName { get; set; }
        public string MemberType { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? CAI { get; set; }
        public string? RetireeNumber { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? PrimaryEmail { get; set; }
        public string? SecondaryEmail { get; set; }
        public string? PrimaryPhone { get; set; }
        public string? SecondaryPhone { get; set; }
        public string? ResidentialAddress { get; set; }
        public string? OfficeAddress { get; set; }
        public string? Rank { get; set; }
        public string? JobRole { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? MembershipId { get; set; }
        public string DepartmentId { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DOB { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public string? DeletedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
        public Guid RowVersion { get; set; }
        public string? FullText { get; set; }
        public string? Tags { get; set; }
        public string? Caption { get; set; }
        public bool ApplicationUserId_IsAdmin { get; set; }
        public string? ApplicationUserId_AdObjectId { get; set; }
        public string? ApplicationUserId_SecondaryPhone { get; set; }
        public bool? ApplicationUserId_SecondaryPhoneConfirmed { get; set; }
        public string? ApplicationUserId_UserName { get; set; }
        // [MaxLength(512)]
        // public string? ApplicationUserId_NormalizedUserName { get; set; }
        public string? ApplicationUserId_Email { get; set; }
        // [MaxLength(512)]
        // public string? ApplicationUserId_NormalizedEmail { get; set; }
        public bool? ApplicationUserId_EmailConfirmed { get; set; }
        public string? IdentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? IdentificationUrl { get; set; }
        // public string? ApplicationUserId_PasswordHash { get; set; }
        // public string? ApplicationUserId_SecurityStamp { get; set; }
        // public string? ApplicationUserId_ConcurrencyStamp { get; set; }
        public string? ApplicationUserId_PhoneNumber { get; set; }
        public bool? ApplicationUserId_PhoneNumberConfirmed { get; set; }
        public bool? ApplicationUserId_TwoFactorEnabled { get; set; }
        public DateTimeOffset? ApplicationUserId_LockoutEnd { get; set; }
        public bool? ApplicationUserId_LockoutEnabled { get; set; }
        public int? ApplicationUserId_AccessFailedCount { get; set; }
        public string? DepartmentId_Name { get; set; }
        public bool? DepartmentId_IsActive { get; set; }
        public string? DepartmentId_CreatedByUserId { get; set; }
        public string? DepartmentId_UpdatedByUserId { get; set; }
        public string? DepartmentId_DeletedByUserId { get; set; }
        public bool? DepartmentId_IsDeleted { get; set; }
        public string? DepartmentId_Tags { get; set; }
        public string? DepartmentId_Caption { get; set; }
        // public List<string> UserRoleIds { get; set; }
        public string FullName
        {
            get
            {
                return $"{LastName} {MiddleName} {FirstName}";
            }
        }

    }


















}
