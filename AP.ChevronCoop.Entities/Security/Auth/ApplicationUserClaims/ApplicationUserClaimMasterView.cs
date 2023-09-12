namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims
{
    public partial class ApplicationUserClaimMasterView
    {
        public long? RowNumber { get; set; }
        public int Id { get; set; }
        public string? PermissionId { get; set; }
        public string UserId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public string? UserId_AdObjectId { get; set; }
        public string? UserId_SecondaryPhone { get; set; }
        public bool? UserId_SecondaryPhoneConfirmed { get; set; }
        public string? UserId_UserName { get; set; }
        public string? UserId_NormalizedUserName { get; set; }
        public string? UserId_Email { get; set; }
        public string? UserId_NormalizedEmail { get; set; }
        public bool? UserId_EmailConfirmed { get; set; }
        public string? UserId_PasswordHash { get; set; }
        public string? UserId_SecurityStamp { get; set; }
        public string? UserId_ConcurrencyStamp { get; set; }
        public string? UserId_PhoneNumber { get; set; }
        public bool? UserId_PhoneNumberConfirmed { get; set; }
        public bool? UserId_TwoFactorEnabled { get; set; }
        public DateTimeOffset? UserId_LockoutEnd { get; set; }
        public bool? UserId_LockoutEnabled { get; set; }
        public int? UserId_AccessFailedCount { get; set; }
        public string? PermissionId_Code { get; set; }
        public string? PermissionId_Name { get; set; }
        public string? PermissionId_Group { get; set; }
        public string? PermissionId_Category { get; set; }
        public string? PermissionId_Module { get; set; }
        public string? PermissionId_Owner { get; set; }
        public bool? PermissionId_IsActive { get; set; }
        public string? PermissionId_CreatedByUserId { get; set; }
        public string? PermissionId_UpdatedByUserId { get; set; }
        public string? PermissionId_DeletedByUserId { get; set; }
        public bool? PermissionId_IsDeleted { get; set; }
        public string? PermissionId_Tags { get; set; }
        public string? PermissionId_Caption { get; set; }
    }
}