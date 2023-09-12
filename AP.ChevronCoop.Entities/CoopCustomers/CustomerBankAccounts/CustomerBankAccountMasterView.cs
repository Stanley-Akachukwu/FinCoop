namespace AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;

public class CustomerBankAccountMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string LedgerAccountId { get; set; }
    public string CustomerId { get; set; }
    public string BankId { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string? BVN { get; set; }
    public string? Branch { get; set; }
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
    public string? LedgerAccountId_AccountType { get; set; }
    public string? LedgerAccountId_UOM { get; set; }
    public string? LedgerAccountId_CurrencyId { get; set; }
    public string? LedgerAccountId_Code { get; set; }
    public string? LedgerAccountId_Name { get; set; }
    public string? LedgerAccountId_ParentId { get; set; }
    public decimal? LedgerAccountId_ClearedBalance { get; set; }
    public decimal? LedgerAccountId_UnclearedBalance { get; set; }
    public decimal? LedgerAccountId_LedgerBalance { get; set; }
    public decimal? LedgerAccountId_AvailableBalance { get; set; }
    public bool? LedgerAccountId_IsOfficeAccount { get; set; }
    public bool? LedgerAccountId_AllowManualEntry { get; set; }
    public bool? LedgerAccountId_IsClosed { get; set; }
    public DateTime? LedgerAccountId_DateClosed { get; set; }
    public string? LedgerAccountId_ClosedByUserName { get; set; }
    public bool? LedgerAccountId_IsActive { get; set; }
    public string? LedgerAccountId_CreatedByUserId { get; set; }
    public string? LedgerAccountId_UpdatedByUserId { get; set; }
    public string? LedgerAccountId_DeletedByUserId { get; set; }
    public bool? LedgerAccountId_IsDeleted { get; set; }
    public string? LedgerAccountId_Tags { get; set; }
    public string? LedgerAccountId_Caption { get; set; }
    public string? CustomerId_CustomerNo { get; set; }
    public string? CustomerId_ApplicationUserId { get; set; }
    public string? CustomerId_CashAccountId { get; set; }
    public int? CustomerId_YearsOfService { get; set; }
    public bool? CustomerId_IsKycStarted { get; set; }
    public bool? CustomerId_IsKycCompleted { get; set; }
    public DateTime? CustomerId_KycStartDate { get; set; }
    public DateTime? CustomerId_KycCompletedDate { get; set; }
    public string? CustomerId_Status { get; set; }
    public string? CustomerId_MemberType { get; set; }

    public string? CustomerId_Gender { get; set; }

// public string? CustomerId_ProfileImageUrl { get; set; } 
// public string? CustomerId_PassportUrl { get; set; } 
    public string? CustomerId_IdentificationType { get; set; }

    public string? CustomerId_IdentificationNumber { get; set; }

// public string? CustomerId_IdentificationUrl { get; set; } 
    public bool? CustomerId_KycSubmitted { get; set; }
    public DateTime? CustomerId_KycSubmittedOn { get; set; }
    public bool? CustomerId_KycApproved { get; set; }
    public DateTime? CustomerId_KycApprovedOn { get; set; }
    public bool? CustomerId_KycApprovedBy { get; set; }
    public string? CustomerId_FirstName { get; set; }
    public string? CustomerId_LastName { get; set; }
    public string? CustomerId_MiddleName { get; set; }
    public string? CustomerId_DepartmentId { get; set; }
    public string? CustomerId_MemberId { get; set; }
    public string? CustomerId_CAI { get; set; }
    public string? CustomerId_RetireeNumber { get; set; }
    public string? CustomerId_StateOfOrigin { get; set; }
    public string? CustomerId_PrimaryEmail { get; set; }
    public string? CustomerId_SecondaryEmail { get; set; }
    public string? CustomerId_PrimaryPhone { get; set; }
    public string? CustomerId_SecondaryPhone { get; set; }
    public string? CustomerId_ResidentialAddress { get; set; }
    public string? CustomerId_OfficeAddress { get; set; }
    public string? CustomerId_Rank { get; set; }
    public string? CustomerId_JobRole { get; set; }
    public DateTimeOffset? CustomerId_DOB { get; set; }
    public string? CustomerId_Address { get; set; }
    public string? CustomerId_Country { get; set; }
    public string? CustomerId_State { get; set; }
    public bool? CustomerId_IsActive { get; set; }
    public string? CustomerId_CreatedByUserId { get; set; }
    public string? CustomerId_UpdatedByUserId { get; set; }
    public string? CustomerId_DeletedByUserId { get; set; }
    public bool? CustomerId_IsDeleted { get; set; }
    public string? CustomerId_Tags { get; set; }
    public string? CustomerId_Caption { get; set; }
    public DateTime? CustomerId_DateOfEmployment { get; set; }
    public string? BankId_Code { get; set; }
    public string? BankId_SortCode { get; set; }
    public string? BankId_Name { get; set; }
    public string? BankId_Address { get; set; }
    public string? BankId_ContactName { get; set; }
    public string? BankId_ContactDetails { get; set; }
    public bool? BankId_IsActive { get; set; }
    public string? BankId_CreatedByUserId { get; set; }
    public string? BankId_UpdatedByUserId { get; set; }
    public string? BankId_DeletedByUserId { get; set; }
    public bool? BankId_IsDeleted { get; set; }
    public string? BankId_Tags { get; set; }
    public string? BankId_Caption { get; set; }

    public string Details => $"{AccountNumber} - {BankId_Name} ({AccountName})";
}