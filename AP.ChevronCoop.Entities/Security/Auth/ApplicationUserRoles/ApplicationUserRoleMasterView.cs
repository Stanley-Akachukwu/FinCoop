﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles
{
    public partial class ApplicationUserRoleMasterView
    {

        public long? RowNumber { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool? RoleId_IsSystemRole { get; set; }
        public string? RoleId_Name { get; set; }
        public string? RoleId_NormalizedName { get; set; }
        public string? RoleId_ConcurrencyStamp { get; set; }
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
    }
}