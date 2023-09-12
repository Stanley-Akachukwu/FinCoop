using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;

public partial class MemberBeneficiaryMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string ProfileId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public string UpdatedByUserId { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public string DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
    public Guid RowVersion { get; set; }
    public string FullText { get; set; }
    public string Tags { get; set; }
    public string Caption { get; set; }
    public string ProfileId_ApplicationUserId { get; set; }
    public bool? ProfileId_IsKycStarted { get; set; }
    public bool? ProfileId_IsKycCompleted { get; set; }
    public DateTime? ProfileId_KycStartDate { get; set; }
    public DateTime? ProfileId_KycCompletedDate { get; set; }
    public string ProfileId_Status { get; set; }
    public string ProfileId_Gender { get; set; }
    public string ProfileId_ProfileImageUrl { get; set; }
    public string ProfileId_FirstName { get; set; }
    public string ProfileId_LastName { get; set; }
    public string ProfileId_MiddleName { get; set; }
    public string ProfileId_CAI { get; set; }
    public string ProfileId_RetireeNumber { get; set; }
    public string ProfileId_Address { get; set; }
    public string ProfileId_Country { get; set; }
    public string ProfileId_State { get; set; }
    public string ProfileId_DepartmentId { get; set; }
    public bool? ProfileId_IsActive { get; set; }
    public string ProfileId_CreatedByUserId { get; set; }
    public string ProfileId_UpdatedByUserId { get; set; }
    public string ProfileId_DeletedByUserId { get; set; }
    public bool? ProfileId_IsDeleted { get; set; }
    public string ProfileId_Tags { get; set; }
    public string ProfileId_Caption { get; set; }
}