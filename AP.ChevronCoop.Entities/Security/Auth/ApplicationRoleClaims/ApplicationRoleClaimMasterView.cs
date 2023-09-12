namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims
{
    public partial class ApplicationRoleClaimMasterView
    {

        public long? RowNumber { get; set; }
        public int Id { get; set; }
        public string? PermissionId { get; set; }
        public string RoleId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public bool? RoleId_IsSystemRole { get; set; }
        public string? RoleId_Name { get; set; }
        public string? RoleId_NormalizedName { get; set; }
        public string? RoleId_ConcurrencyStamp { get; set; }
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
