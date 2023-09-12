namespace AP.ChevronCoop.Entities.CoopCustomers.Customers;

public class CustomerMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string CustomerNo { get; set; }
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
    public string MemberType { get; set; }
    public string CashAccountId { get; set; }
    public DateTime? DateOfEmployment { get; set; }
    public string? FirstName { get; set; }
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
    public string? StateOfOrigin { get; set; }
    public string? MemberId { get; set; }
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
    public bool KycSubmitted { get; set; }
    public DateTime? KycSubmittedOn { get; set; }
    public bool KycApproved { get; set; }
    public DateTime? KycApprovedOn { get; set; }
    public bool KycApprovedBy { get; set; }
    public string? CashAccountId_AccountType { get; set; }
    public string? CashAccountId_UOM { get; set; }
    public string? CashAccountId_CurrencyId { get; set; }
    public string? CashAccountId_Code { get; set; }
    public string? CashAccountId_Name { get; set; }
    public string? CashAccountId_ParentId { get; set; }
    public decimal? CashAccountId_ClearedBalance { get; set; }
    public decimal? CashAccountId_UnclearedBalance { get; set; }
    public decimal? CashAccountId_LedgerBalance { get; set; }
    public decimal? CashAccountId_AvailableBalance { get; set; }
    public bool? CashAccountId_IsOfficeAccount { get; set; }
    public bool? CashAccountId_AllowManualEntry { get; set; }
    public bool? CashAccountId_IsClosed { get; set; }
    public DateTime? CashAccountId_DateClosed { get; set; }
    public string? CashAccountId_ClosedByUserName { get; set; }
    public bool? CashAccountId_IsActive { get; set; }
    public string? CashAccountId_CreatedByUserId { get; set; }
    public string? CashAccountId_UpdatedByUserId { get; set; }
    public string? CashAccountId_DeletedByUserId { get; set; }
    public bool? CashAccountId_IsDeleted { get; set; }
    public string? CashAccountId_Tags { get; set; }
    public string? CashAccountId_Caption { get; set; }


    public string FullName => $"{LastName} {MiddleName} {FirstName}";
}