/****** Object:  View [Security].[ApplicationRoleClaimMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApplicationRoleClaimMasterView source

CREATE   VIEW [Security].[ApplicationRoleClaimMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApplicationRoleClaim.[Id]) AS RowNumber,
ApplicationRoleClaim.[Id],
 ApplicationRoleClaim.[PermissionId],
 ApplicationRoleClaim.[RoleId],
 ApplicationRoleClaim.[ClaimType],
 ApplicationRoleClaim.[ClaimValue],
PermissionId.[Code] as PermissionId_Code,
PermissionId.[Name] as PermissionId_Name,
PermissionId.[Group] as PermissionId_Group,
PermissionId.[Category] as PermissionId_Category,
PermissionId.[Module] as PermissionId_Module,
PermissionId.[Owner] as PermissionId_Owner,
PermissionId.[IsActive] as PermissionId_IsActive,
PermissionId.[CreatedByUserId] as PermissionId_CreatedByUserId,
PermissionId.[UpdatedByUserId] as PermissionId_UpdatedByUserId,
PermissionId.[DeletedByUserId] as PermissionId_DeletedByUserId,
PermissionId.[IsDeleted] as PermissionId_IsDeleted,
PermissionId.[Tags] as PermissionId_Tags,
PermissionId.[Caption] as PermissionId_Caption,
RoleId.[IsSystemRole] as RoleId_IsSystemRole,
RoleId.[Code] as RoleId_Code,
RoleId.[Name] as RoleId_Name,
RoleId.[NormalizedName] as RoleId_NormalizedName,
RoleId.[ConcurrencyStamp] as RoleId_ConcurrencyStamp

 from Security.[ApplicationRoleClaim] ApplicationRoleClaim

 left join Security.Permission PermissionId on PermissionId.Id=ApplicationRoleClaim.PermissionId
 left join Security.ApplicationRole RoleId on RoleId.Id=ApplicationRoleClaim.RoleId
GO
/****** Object:  View [Security].[ApplicationRoleMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApplicationRoleMasterView source

CREATE   VIEW [Security].[ApplicationRoleMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApplicationRole.[Id]) AS RowNumber,
ApplicationRole.[Id],
 ApplicationRole.[IsSystemRole],
 ApplicationRole.[Code],
 ApplicationRole.[Name],
 ApplicationRole.[NormalizedName],
 ApplicationRole.[ConcurrencyStamp]

 from Security.[ApplicationRole] ApplicationRole
GO
/****** Object:  View [Security].[ApplicationUserClaimMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApplicationUserClaimMasterView source

CREATE   VIEW [Security].[ApplicationUserClaimMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApplicationUserClaim.[Id]) AS RowNumber,
ApplicationUserClaim.[Id],
 ApplicationUserClaim.[PermissionId],
 ApplicationUserClaim.[UserId],
 ApplicationUserClaim.[ClaimType],
 ApplicationUserClaim.[ClaimValue],
PermissionId.[Code] as PermissionId_Code,
PermissionId.[Name] as PermissionId_Name,
PermissionId.[Group] as PermissionId_Group,
PermissionId.[Category] as PermissionId_Category,
PermissionId.[Module] as PermissionId_Module,
PermissionId.[Owner] as PermissionId_Owner,
PermissionId.[IsActive] as PermissionId_IsActive,
PermissionId.[CreatedByUserId] as PermissionId_CreatedByUserId,
PermissionId.[UpdatedByUserId] as PermissionId_UpdatedByUserId,
PermissionId.[DeletedByUserId] as PermissionId_DeletedByUserId,
PermissionId.[IsDeleted] as PermissionId_IsDeleted,
PermissionId.[Tags] as PermissionId_Tags,
PermissionId.[Caption] as PermissionId_Caption,
UserId.[AdObjectId] as UserId_AdObjectId,
UserId.[IsAdmin] as UserId_IsAdmin,
UserId.[SecondaryPhone] as UserId_SecondaryPhone,
UserId.[SecondaryPhoneConfirmed] as UserId_SecondaryPhoneConfirmed,
UserId.[UserName] as UserId_UserName,
UserId.[NormalizedUserName] as UserId_NormalizedUserName,
UserId.[Email] as UserId_Email,
UserId.[NormalizedEmail] as UserId_NormalizedEmail,
UserId.[EmailConfirmed] as UserId_EmailConfirmed,
UserId.[PasswordHash] as UserId_PasswordHash,
UserId.[SecurityStamp] as UserId_SecurityStamp,
UserId.[ConcurrencyStamp] as UserId_ConcurrencyStamp,
UserId.[PhoneNumber] as UserId_PhoneNumber,
UserId.[PhoneNumberConfirmed] as UserId_PhoneNumberConfirmed,
UserId.[TwoFactorEnabled] as UserId_TwoFactorEnabled,
UserId.[LockoutEnd] as UserId_LockoutEnd,
UserId.[LockoutEnabled] as UserId_LockoutEnabled,
UserId.[AccessFailedCount] as UserId_AccessFailedCount

 from Security.[ApplicationUserClaim] ApplicationUserClaim

 left join Security.Permission PermissionId on PermissionId.Id=ApplicationUserClaim.PermissionId
 left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserClaim.UserId
GO
/****** Object:  View [Security].[ApplicationUserLoginMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApplicationUserLoginMasterView source

CREATE   VIEW [Security].[ApplicationUserLoginMasterView]
  as select

 --ROW_NUMBER() OVER (ORDER BY ApplicationUserLogin.[Id]) AS RowNumber,
ApplicationUserLogin.[LoginProvider],
 ApplicationUserLogin.[ProviderKey],
 ApplicationUserLogin.[ProviderDisplayName],
 ApplicationUserLogin.[UserId],
UserId.[AdObjectId] as UserId_AdObjectId,
UserId.[IsAdmin] as UserId_IsAdmin,
UserId.[SecondaryPhone] as UserId_SecondaryPhone,
UserId.[SecondaryPhoneConfirmed] as UserId_SecondaryPhoneConfirmed,
UserId.[UserName] as UserId_UserName,
UserId.[NormalizedUserName] as UserId_NormalizedUserName,
UserId.[Email] as UserId_Email,
UserId.[NormalizedEmail] as UserId_NormalizedEmail,
UserId.[EmailConfirmed] as UserId_EmailConfirmed,
UserId.[PasswordHash] as UserId_PasswordHash,
UserId.[SecurityStamp] as UserId_SecurityStamp,
UserId.[ConcurrencyStamp] as UserId_ConcurrencyStamp,
UserId.[PhoneNumber] as UserId_PhoneNumber,
UserId.[PhoneNumberConfirmed] as UserId_PhoneNumberConfirmed,
UserId.[TwoFactorEnabled] as UserId_TwoFactorEnabled,
UserId.[LockoutEnd] as UserId_LockoutEnd,
UserId.[LockoutEnabled] as UserId_LockoutEnabled,
UserId.[AccessFailedCount] as UserId_AccessFailedCount

 from Security.[ApplicationUserLogin] ApplicationUserLogin

 left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserLogin.UserId
GO
/****** Object:  View [Security].[ApplicationUserMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApplicationUserMasterView source

CREATE   VIEW [Security].[ApplicationUserMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApplicationUser.[Id]) AS RowNumber,
ApplicationUser.[Id],
 ApplicationUser.[AdObjectId],
 ApplicationUser.[IsAdmin],
 ApplicationUser.[SecondaryPhone],
 ApplicationUser.[SecondaryPhoneConfirmed],
 ApplicationUser.[UserName],
 ApplicationUser.[NormalizedUserName],
 ApplicationUser.[Email],
 ApplicationUser.[NormalizedEmail],
 ApplicationUser.[EmailConfirmed],
 ApplicationUser.[PasswordHash],
 ApplicationUser.[SecurityStamp],
 ApplicationUser.[ConcurrencyStamp],
 ApplicationUser.[PhoneNumber],
 ApplicationUser.[PhoneNumberConfirmed],
 ApplicationUser.[TwoFactorEnabled],
 ApplicationUser.[LockoutEnd],
 ApplicationUser.[LockoutEnabled],
 ApplicationUser.[AccessFailedCount]

 from Security.[ApplicationUser] ApplicationUser
GO
/****** Object:  View [Security].[ApplicationUserRoleMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApplicationUserRoleMasterView source

CREATE   VIEW [Security].[ApplicationUserRoleMasterView]
  as select

 --ROW_NUMBER() OVER (ORDER BY ApplicationUserRole.[Id]) AS RowNumber,
ApplicationUserRole.[UserId],
 ApplicationUserRole.[RoleId],
UserId.[AdObjectId] as UserId_AdObjectId,
UserId.[IsAdmin] as UserId_IsAdmin,
UserId.[SecondaryPhone] as UserId_SecondaryPhone,
UserId.[SecondaryPhoneConfirmed] as UserId_SecondaryPhoneConfirmed,
UserId.[UserName] as UserId_UserName,
UserId.[NormalizedUserName] as UserId_NormalizedUserName,
UserId.[Email] as UserId_Email,
UserId.[NormalizedEmail] as UserId_NormalizedEmail,
UserId.[EmailConfirmed] as UserId_EmailConfirmed,
UserId.[PasswordHash] as UserId_PasswordHash,
UserId.[SecurityStamp] as UserId_SecurityStamp,
UserId.[ConcurrencyStamp] as UserId_ConcurrencyStamp,
UserId.[PhoneNumber] as UserId_PhoneNumber,
UserId.[PhoneNumberConfirmed] as UserId_PhoneNumberConfirmed,
UserId.[TwoFactorEnabled] as UserId_TwoFactorEnabled,
UserId.[LockoutEnd] as UserId_LockoutEnd,
UserId.[LockoutEnabled] as UserId_LockoutEnabled,
UserId.[AccessFailedCount] as UserId_AccessFailedCount,
RoleId.[IsSystemRole] as RoleId_IsSystemRole,
RoleId.[Code] as RoleId_Code,
RoleId.[Name] as RoleId_Name,
RoleId.[NormalizedName] as RoleId_NormalizedName,
RoleId.[ConcurrencyStamp] as RoleId_ConcurrencyStamp

 from Security.[ApplicationUserRole] ApplicationUserRole

 left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserRole.UserId
 left join Security.ApplicationRole RoleId on RoleId.Id=ApplicationUserRole.RoleId
GO
/****** Object:  View [Security].[ApplicationUserTokenMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApplicationUserTokenMasterView source

CREATE   VIEW [Security].[ApplicationUserTokenMasterView]
  as select

-- ROW_NUMBER() OVER (ORDER BY ApplicationUserToken.[Id]) AS RowNumber,
ApplicationUserToken.[UserId],
 ApplicationUserToken.[LoginProvider],
 ApplicationUserToken.[Name],
 ApplicationUserToken.[Value],
UserId.[AdObjectId] as UserId_AdObjectId,
UserId.[IsAdmin] as UserId_IsAdmin,
UserId.[SecondaryPhone] as UserId_SecondaryPhone,
UserId.[SecondaryPhoneConfirmed] as UserId_SecondaryPhoneConfirmed,
UserId.[UserName] as UserId_UserName,
UserId.[NormalizedUserName] as UserId_NormalizedUserName,
UserId.[Email] as UserId_Email,
UserId.[NormalizedEmail] as UserId_NormalizedEmail,
UserId.[EmailConfirmed] as UserId_EmailConfirmed,
UserId.[PasswordHash] as UserId_PasswordHash,
UserId.[SecurityStamp] as UserId_SecurityStamp,
UserId.[ConcurrencyStamp] as UserId_ConcurrencyStamp,
UserId.[PhoneNumber] as UserId_PhoneNumber,
UserId.[PhoneNumberConfirmed] as UserId_PhoneNumberConfirmed,
UserId.[TwoFactorEnabled] as UserId_TwoFactorEnabled,
UserId.[LockoutEnd] as UserId_LockoutEnd,
UserId.[LockoutEnabled] as UserId_LockoutEnabled,
UserId.[AccessFailedCount] as UserId_AccessFailedCount

 from Security.[ApplicationUserToken] ApplicationUserToken

 left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserToken.UserId
GO
/****** Object:  View [Security].[ApprovalDocumentMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalDocumentMasterView source

CREATE   VIEW [Security].[ApprovalDocumentMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalDocument.[Id]) AS RowNumber,
ApprovalDocument.[Id],
 ApprovalDocument.[ApprovalId],
 ApprovalDocument.[Document],
 ApprovalDocument.[MimeType],
 ApprovalDocument.[FileName],
 ApprovalDocument.[FileSize],
 ApprovalDocument.[Description],
 ApprovalDocument.[IsActive],
 ApprovalDocument.[CreatedByUserId],
 ApprovalDocument.[DateCreated],
 ApprovalDocument.[UpdatedByUserId],
 ApprovalDocument.[DateUpdated],
 ApprovalDocument.[DeletedByUserId],
 ApprovalDocument.[IsDeleted],
 ApprovalDocument.[DateDeleted],
 ApprovalDocument.[RowVersion],
 ApprovalDocument.[FullText],
 ApprovalDocument.[Tags],
 ApprovalDocument.[Caption],
ApprovalId.[Module] as ApprovalId_Module,
ApprovalId.[ApprovalType] as ApprovalId_ApprovalType,
ApprovalId.[Status] as ApprovalId_Status,
ApprovalId.[CurrentSequence] as ApprovalId_CurrentSequence,
ApprovalId.[ApprovalWorkflowId] as ApprovalId_ApprovalWorkflowId,
ApprovalId.[Payload] as ApprovalId_Payload,
ApprovalId.[IsApprovalCompleted] as ApprovalId_IsApprovalCompleted,
ApprovalId.[Comment] as ApprovalId_Comment,
ApprovalId.[EntityId] as ApprovalId_EntityId,
ApprovalId.[TriedCount] as ApprovalId_TriedCount,
ApprovalId.[IsActive] as ApprovalId_IsActive,
ApprovalId.[CreatedByUserId] as ApprovalId_CreatedByUserId,
ApprovalId.[UpdatedByUserId] as ApprovalId_UpdatedByUserId,
ApprovalId.[DeletedByUserId] as ApprovalId_DeletedByUserId,
ApprovalId.[IsDeleted] as ApprovalId_IsDeleted,
ApprovalId.[Tags] as ApprovalId_Tags,
ApprovalId.[Caption] as ApprovalId_Caption

 from Security.[ApprovalDocument] ApprovalDocument

 left join Security.Approval ApprovalId on ApprovalId.Id=ApprovalDocument.ApprovalId
GO
/****** Object:  View [Security].[ApprovalEmailAlertMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalEmailAlertMasterView source

CREATE   VIEW [Security].[ApprovalEmailAlertMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalEmailAlert.[Id]) AS RowNumber,
ApprovalEmailAlert.[Id],
 ApprovalEmailAlert.[ApprovalId],
 ApprovalEmailAlert.[ApprovalWorkflowId],
 ApprovalEmailAlert.[EmailTitle],
 ApprovalEmailAlert.[EmailBody],
 ApprovalEmailAlert.[TaskCompleted],
 ApprovalEmailAlert.[Description],
 ApprovalEmailAlert.[IsActive],
 ApprovalEmailAlert.[CreatedByUserId],
 ApprovalEmailAlert.[DateCreated],
 ApprovalEmailAlert.[UpdatedByUserId],
 ApprovalEmailAlert.[DateUpdated],
 ApprovalEmailAlert.[DeletedByUserId],
 ApprovalEmailAlert.[IsDeleted],
 ApprovalEmailAlert.[DateDeleted],
 ApprovalEmailAlert.[RowVersion],
 ApprovalEmailAlert.[FullText],
 ApprovalEmailAlert.[Tags],
 ApprovalEmailAlert.[Caption]

 from Security.[ApprovalEmailAlert] ApprovalEmailAlert
GO
/****** Object:  View [Security].[ApprovalGroupMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalGroupMasterView source

CREATE   VIEW [Security].[ApprovalGroupMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalGroup.[Id]) AS RowNumber,
ApprovalGroup.[Id],
 ApprovalGroup.[ApprovalWorkflowId],
 ApprovalGroup.[Name],
 ApprovalGroup.[Description],
 ApprovalGroup.[IsActive],
 ApprovalGroup.[CreatedByUserId],
 ApprovalGroup.[DateCreated],
 ApprovalGroup.[UpdatedByUserId],
 ApprovalGroup.[DateUpdated],
 ApprovalGroup.[DeletedByUserId],
 ApprovalGroup.[IsDeleted],
 ApprovalGroup.[DateDeleted],
 ApprovalGroup.[RowVersion],
 ApprovalGroup.[FullText],
 ApprovalGroup.[Tags],
 ApprovalGroup.[Caption],
ApprovalWorkflowId.[WorkflowName] as ApprovalWorkflowId_WorkflowName,
ApprovalWorkflowId.[IsDefaultApprovalWorkflow] as ApprovalWorkflowId_IsDefaultApprovalWorkflow,
ApprovalWorkflowId.[RequiredApprovers] as ApprovalWorkflowId_RequiredApprovers,
ApprovalWorkflowId.[RequiredGroups] as ApprovalWorkflowId_RequiredGroups,
ApprovalWorkflowId.[IsActive] as ApprovalWorkflowId_IsActive,
ApprovalWorkflowId.[CreatedByUserId] as ApprovalWorkflowId_CreatedByUserId,
ApprovalWorkflowId.[UpdatedByUserId] as ApprovalWorkflowId_UpdatedByUserId,
ApprovalWorkflowId.[DeletedByUserId] as ApprovalWorkflowId_DeletedByUserId,
ApprovalWorkflowId.[IsDeleted] as ApprovalWorkflowId_IsDeleted,
ApprovalWorkflowId.[Tags] as ApprovalWorkflowId_Tags,
ApprovalWorkflowId.[Caption] as ApprovalWorkflowId_Caption

 from Security.[ApprovalGroup] ApprovalGroup

 left join Security.ApprovalWorkflow ApprovalWorkflowId on ApprovalWorkflowId.Id=ApprovalGroup.ApprovalWorkflowId
GO
/****** Object:  View [Security].[ApprovalGroupMemberMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalGroupMemberMasterView source

CREATE   VIEW [Security].[ApprovalGroupMemberMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalGroupMember.[Id]) AS RowNumber,
ApprovalGroupMember.[Id],
 ApprovalGroupMember.[ApprovalGroupId],
 ApprovalGroupMember.[ApprovalSequence],
 ApprovalGroupMember.[UserId],
 ApprovalGroupMember.[ApprovalGroupId1],
 ApprovalGroupMember.[Description],
 ApprovalGroupMember.[IsActive],
 ApprovalGroupMember.[CreatedByUserId],
 ApprovalGroupMember.[DateCreated],
 ApprovalGroupMember.[UpdatedByUserId],
 ApprovalGroupMember.[DateUpdated],
 ApprovalGroupMember.[DeletedByUserId],
 ApprovalGroupMember.[IsDeleted],
 ApprovalGroupMember.[DateDeleted],
 ApprovalGroupMember.[RowVersion],
 ApprovalGroupMember.[FullText],
 ApprovalGroupMember.[Tags],
 ApprovalGroupMember.[Caption],
ApprovalGroupId.[ApprovalWorkflowId] as ApprovalGroupId_ApprovalWorkflowId,
ApprovalGroupId.[Name] as ApprovalGroupId_Name,
ApprovalGroupId.[IsActive] as ApprovalGroupId_IsActive,
ApprovalGroupId.[CreatedByUserId] as ApprovalGroupId_CreatedByUserId,
ApprovalGroupId.[UpdatedByUserId] as ApprovalGroupId_UpdatedByUserId,
ApprovalGroupId.[DeletedByUserId] as ApprovalGroupId_DeletedByUserId,
ApprovalGroupId.[IsDeleted] as ApprovalGroupId_IsDeleted,
ApprovalGroupId.[Tags] as ApprovalGroupId_Tags,
ApprovalGroupId.[Caption] as ApprovalGroupId_Caption,
UserId.[AdObjectId] as UserId_AdObjectId,
UserId.[IsAdmin] as UserId_IsAdmin,
UserId.[SecondaryPhone] as UserId_SecondaryPhone,
UserId.[SecondaryPhoneConfirmed] as UserId_SecondaryPhoneConfirmed,
UserId.[UserName] as UserId_UserName,
UserId.[NormalizedUserName] as UserId_NormalizedUserName,
UserId.[Email] as UserId_Email,
UserId.[NormalizedEmail] as UserId_NormalizedEmail,
UserId.[EmailConfirmed] as UserId_EmailConfirmed,
UserId.[PasswordHash] as UserId_PasswordHash,
UserId.[SecurityStamp] as UserId_SecurityStamp,
UserId.[ConcurrencyStamp] as UserId_ConcurrencyStamp,
UserId.[PhoneNumber] as UserId_PhoneNumber,
UserId.[PhoneNumberConfirmed] as UserId_PhoneNumberConfirmed,
UserId.[TwoFactorEnabled] as UserId_TwoFactorEnabled,
UserId.[LockoutEnd] as UserId_LockoutEnd,
UserId.[LockoutEnabled] as UserId_LockoutEnabled,
UserId.[AccessFailedCount] as UserId_AccessFailedCount,
ApprovalGroupId1.[ApprovalWorkflowId] as ApprovalGroupId1_ApprovalWorkflowId,
ApprovalGroupId1.[Name] as ApprovalGroupId1_Name,
ApprovalGroupId1.[IsActive] as ApprovalGroupId1_IsActive,
ApprovalGroupId1.[CreatedByUserId] as ApprovalGroupId1_CreatedByUserId,
ApprovalGroupId1.[UpdatedByUserId] as ApprovalGroupId1_UpdatedByUserId,
ApprovalGroupId1.[DeletedByUserId] as ApprovalGroupId1_DeletedByUserId,
ApprovalGroupId1.[IsDeleted] as ApprovalGroupId1_IsDeleted,
ApprovalGroupId1.[Tags] as ApprovalGroupId1_Tags,
ApprovalGroupId1.[Caption] as ApprovalGroupId1_Caption

 from Security.[ApprovalGroupMember] ApprovalGroupMember

 left join Security.ApprovalGroup ApprovalGroupId on ApprovalGroupId.Id=ApprovalGroupMember.ApprovalGroupId
 left join Security.ApplicationUser UserId on UserId.Id=ApprovalGroupMember.UserId
 left join Security.ApprovalGroup ApprovalGroupId1 on ApprovalGroupId1.Id=ApprovalGroupMember.ApprovalGroupId1
GO
/****** Object:  View [Security].[ApprovalGroupWorkflowMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [Security].[ApprovalGroupWorkflowMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalGroupWorkflow.[Id]) AS RowNumber,
ApprovalGroupWorkflow.[Id],
 ApprovalGroupWorkflow.[ApprovalWorkflowId],
 ApprovalGroupWorkflow.[ApprovalGroupId],
 ApprovalGroupWorkflow.[RequiredApprovers],
 ApprovalGroupWorkflow.[Description],
 ApprovalGroupWorkflow.[IsActive],
 ApprovalGroupWorkflow.[CreatedByUserId],
 ApprovalGroupWorkflow.[DateCreated],
 ApprovalGroupWorkflow.[UpdatedByUserId],
 ApprovalGroupWorkflow.[DateUpdated],
 ApprovalGroupWorkflow.[DeletedByUserId],
 ApprovalGroupWorkflow.[IsDeleted],
 ApprovalGroupWorkflow.[DateDeleted],
 ApprovalGroupWorkflow.[RowVersion],
 ApprovalGroupWorkflow.[FullText],
 ApprovalGroupWorkflow.[Tags],
 ApprovalGroupWorkflow.[Caption],
ApprovalWorkflowId.[WorkflowName] as ApprovalWorkflowId_WorkflowName,
ApprovalWorkflowId.[IsDefaultApprovalWorkflow] as ApprovalWorkflowId_IsDefaultApprovalWorkflow,
ApprovalWorkflowId.[RequiredApprovers] as ApprovalWorkflowId_RequiredApprovers,
ApprovalWorkflowId.[RequiredGroups] as ApprovalWorkflowId_RequiredGroups,
ApprovalWorkflowId.[IsActive] as ApprovalWorkflowId_IsActive,
ApprovalWorkflowId.[CreatedByUserId] as ApprovalWorkflowId_CreatedByUserId,
ApprovalWorkflowId.[UpdatedByUserId] as ApprovalWorkflowId_UpdatedByUserId,
ApprovalWorkflowId.[DeletedByUserId] as ApprovalWorkflowId_DeletedByUserId,
ApprovalWorkflowId.[IsDeleted] as ApprovalWorkflowId_IsDeleted,
ApprovalWorkflowId.[Tags] as ApprovalWorkflowId_Tags,
ApprovalWorkflowId.[Caption] as ApprovalWorkflowId_Caption,
ApprovalGroupId.[ApprovalWorkflowId] as ApprovalGroupId_ApprovalWorkflowId,
ApprovalGroupId.[Name] as ApprovalGroupId_Name,
ApprovalGroupId.[IsActive] as ApprovalGroupId_IsActive,
ApprovalGroupId.[CreatedByUserId] as ApprovalGroupId_CreatedByUserId,
ApprovalGroupId.[UpdatedByUserId] as ApprovalGroupId_UpdatedByUserId,
ApprovalGroupId.[DeletedByUserId] as ApprovalGroupId_DeletedByUserId,
ApprovalGroupId.[IsDeleted] as ApprovalGroupId_IsDeleted,
ApprovalGroupId.[Tags] as ApprovalGroupId_Tags,
ApprovalGroupId.[Caption] as ApprovalGroupId_Caption

 from Security.[ApprovalGroupWorkflow] ApprovalGroupWorkflow

 left join Security.ApprovalWorkflow ApprovalWorkflowId on ApprovalWorkflowId.Id=ApprovalGroupWorkflow.ApprovalWorkflowId
 left join Security.ApprovalGroup ApprovalGroupId on ApprovalGroupId.Id=ApprovalGroupWorkflow.ApprovalGroupId;
GO
/****** Object:  View [Security].[ApprovalLogMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalLogMasterView source

CREATE   VIEW [Security].[ApprovalLogMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalLog.[Id]) AS RowNumber,
ApprovalLog.[Id],
 ApprovalLog.[ApprovalId],
 ApprovalLog.[Sequence],
 ApprovalLog.[ApprovalGroupId],
 ApprovalLog.[ApprovedByUserId],
 ApprovalLog.[DateApproved],
 ApprovalLog.[Status],
 ApprovalLog.[Comment],
 ApprovalLog.[Description],
 ApprovalLog.[IsActive],
 ApprovalLog.[CreatedByUserId],
 ApprovalLog.[DateCreated],
 ApprovalLog.[UpdatedByUserId],
 ApprovalLog.[DateUpdated],
 ApprovalLog.[DeletedByUserId],
 ApprovalLog.[IsDeleted],
 ApprovalLog.[DateDeleted],
 ApprovalLog.[RowVersion],
 ApprovalLog.[FullText],
 ApprovalLog.[Tags],
 ApprovalLog.[Caption],
ApprovalId.[Module] as ApprovalId_Module,
ApprovalId.[ApprovalType] as ApprovalId_ApprovalType,
ApprovalId.[Status] as ApprovalId_Status,
ApprovalId.[CurrentSequence] as ApprovalId_CurrentSequence,
ApprovalId.[ApprovalWorkflowId] as ApprovalId_ApprovalWorkflowId,
ApprovalId.[Payload] as ApprovalId_Payload,
ApprovalId.[IsApprovalCompleted] as ApprovalId_IsApprovalCompleted,
ApprovalId.[Comment] as ApprovalId_Comment,
ApprovalId.[EntityId] as ApprovalId_EntityId,
ApprovalId.[TriedCount] as ApprovalId_TriedCount,
ApprovalId.[IsActive] as ApprovalId_IsActive,
ApprovalId.[CreatedByUserId] as ApprovalId_CreatedByUserId,
ApprovalId.[UpdatedByUserId] as ApprovalId_UpdatedByUserId,
ApprovalId.[DeletedByUserId] as ApprovalId_DeletedByUserId,
ApprovalId.[IsDeleted] as ApprovalId_IsDeleted,
ApprovalId.[Tags] as ApprovalId_Tags,
ApprovalId.[Caption] as ApprovalId_Caption,
ApprovalGroupId.[ApprovalWorkflowId] as ApprovalGroupId_ApprovalWorkflowId,
ApprovalGroupId.[Name] as ApprovalGroupId_Name,
ApprovalGroupId.[IsActive] as ApprovalGroupId_IsActive,
ApprovalGroupId.[CreatedByUserId] as ApprovalGroupId_CreatedByUserId,
ApprovalGroupId.[UpdatedByUserId] as ApprovalGroupId_UpdatedByUserId,
ApprovalGroupId.[DeletedByUserId] as ApprovalGroupId_DeletedByUserId,
ApprovalGroupId.[IsDeleted] as ApprovalGroupId_IsDeleted,
ApprovalGroupId.[Tags] as ApprovalGroupId_Tags,
ApprovalGroupId.[Caption] as ApprovalGroupId_Caption,
ApprovedByUserId.[AdObjectId] as ApprovedByUserId_AdObjectId,
ApprovedByUserId.[IsAdmin] as ApprovedByUserId_IsAdmin,
ApprovedByUserId.[SecondaryPhone] as ApprovedByUserId_SecondaryPhone,
ApprovedByUserId.[SecondaryPhoneConfirmed] as ApprovedByUserId_SecondaryPhoneConfirmed,
ApprovedByUserId.[UserName] as ApprovedByUserId_UserName,
ApprovedByUserId.[NormalizedUserName] as ApprovedByUserId_NormalizedUserName,
ApprovedByUserId.[Email] as ApprovedByUserId_Email,
ApprovedByUserId.[NormalizedEmail] as ApprovedByUserId_NormalizedEmail,
ApprovedByUserId.[EmailConfirmed] as ApprovedByUserId_EmailConfirmed,
ApprovedByUserId.[PasswordHash] as ApprovedByUserId_PasswordHash,
ApprovedByUserId.[SecurityStamp] as ApprovedByUserId_SecurityStamp,
ApprovedByUserId.[ConcurrencyStamp] as ApprovedByUserId_ConcurrencyStamp,
ApprovedByUserId.[PhoneNumber] as ApprovedByUserId_PhoneNumber,
ApprovedByUserId.[PhoneNumberConfirmed] as ApprovedByUserId_PhoneNumberConfirmed,
ApprovedByUserId.[TwoFactorEnabled] as ApprovedByUserId_TwoFactorEnabled,
ApprovedByUserId.[LockoutEnd] as ApprovedByUserId_LockoutEnd,
ApprovedByUserId.[LockoutEnabled] as ApprovedByUserId_LockoutEnabled,
ApprovedByUserId.[AccessFailedCount] as ApprovedByUserId_AccessFailedCount

 from Security.[ApprovalLog] ApprovalLog

 left join Security.Approval ApprovalId on ApprovalId.Id=ApprovalLog.ApprovalId
 left join Security.ApprovalGroup ApprovalGroupId on ApprovalGroupId.Id=ApprovalLog.ApprovalGroupId
 left join Security.ApplicationUser ApprovedByUserId on ApprovedByUserId.Id=ApprovalLog.ApprovedByUserId
GO
/****** Object:  View [Security].[ApprovalMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalMasterView source

CREATE   VIEW [Security].[ApprovalMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY Approval.[Id]) AS RowNumber,
Approval.[Id],
 Approval.[Module],
 Approval.[ApprovalType],
 Approval.[Status],
 Approval.[CurrentSequence],
 Approval.[ApprovalWorkflowId],
 Approval.[Payload],
 Approval.[IsApprovalCompleted],
 Approval.[Comment],
 Approval.[EntityId],
 Approval.[TriedCount],
 Approval.[Description],
 Approval.[IsActive],
 Approval.[CreatedByUserId],
 Approval.[DateCreated],
 Approval.[UpdatedByUserId],
 Approval.[DateUpdated],
 Approval.[DeletedByUserId],
 Approval.[IsDeleted],
 Approval.[DateDeleted],
 Approval.[RowVersion],
 Approval.[FullText],
 Approval.[Tags],
 Approval.[Caption],
ApprovalWorkflowId.[WorkflowName] as ApprovalWorkflowId_WorkflowName,
ApprovalWorkflowId.[IsDefaultApprovalWorkflow] as ApprovalWorkflowId_IsDefaultApprovalWorkflow,
ApprovalWorkflowId.[RequiredApprovers] as ApprovalWorkflowId_RequiredApprovers,
ApprovalWorkflowId.[RequiredGroups] as ApprovalWorkflowId_RequiredGroups,
ApprovalWorkflowId.[IsActive] as ApprovalWorkflowId_IsActive,
ApprovalWorkflowId.[CreatedByUserId] as ApprovalWorkflowId_CreatedByUserId,
ApprovalWorkflowId.[UpdatedByUserId] as ApprovalWorkflowId_UpdatedByUserId,
ApprovalWorkflowId.[DeletedByUserId] as ApprovalWorkflowId_DeletedByUserId,
ApprovalWorkflowId.[IsDeleted] as ApprovalWorkflowId_IsDeleted,
ApprovalWorkflowId.[Tags] as ApprovalWorkflowId_Tags,
ApprovalWorkflowId.[Caption] as ApprovalWorkflowId_Caption

 from Security.[Approval] Approval

 left join Security.ApprovalWorkflow ApprovalWorkflowId on ApprovalWorkflowId.Id=Approval.ApprovalWorkflowId
GO
/****** Object:  View [Security].[ApprovalRoleMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalRoleMasterView source

CREATE   VIEW [Security].[ApprovalRoleMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalRole.[Id]) AS RowNumber,
ApprovalRole.[Id],
 ApprovalRole.[EventGlobalCodeId],
 ApprovalRole.[RoleId],
 ApprovalRole.[Order],
 ApprovalRole.[Description],
 ApprovalRole.[IsActive],
 ApprovalRole.[CreatedByUserId],
 ApprovalRole.[DateCreated],
 ApprovalRole.[UpdatedByUserId],
 ApprovalRole.[DateUpdated],
 ApprovalRole.[DeletedByUserId],
 ApprovalRole.[IsDeleted],
 ApprovalRole.[DateDeleted],
 ApprovalRole.[RowVersion],
 ApprovalRole.[FullText],
 ApprovalRole.[Tags],
 ApprovalRole.[Caption],
EventGlobalCodeId.[CodeType] as EventGlobalCodeId_CodeType,
EventGlobalCodeId.[Code] as EventGlobalCodeId_Code,
EventGlobalCodeId.[Name] as EventGlobalCodeId_Name,
EventGlobalCodeId.[SystemFlag] as EventGlobalCodeId_SystemFlag,
EventGlobalCodeId.[IsActive] as EventGlobalCodeId_IsActive,
EventGlobalCodeId.[CreatedByUserId] as EventGlobalCodeId_CreatedByUserId,
EventGlobalCodeId.[UpdatedByUserId] as EventGlobalCodeId_UpdatedByUserId,
EventGlobalCodeId.[DeletedByUserId] as EventGlobalCodeId_DeletedByUserId,
EventGlobalCodeId.[IsDeleted] as EventGlobalCodeId_IsDeleted,
EventGlobalCodeId.[Tags] as EventGlobalCodeId_Tags,
EventGlobalCodeId.[Caption] as EventGlobalCodeId_Caption,
RoleId.[IsSystemRole] as RoleId_IsSystemRole,
RoleId.[Code] as RoleId_Code,
RoleId.[Name] as RoleId_Name,
RoleId.[NormalizedName] as RoleId_NormalizedName,
RoleId.[ConcurrencyStamp] as RoleId_ConcurrencyStamp

 from Security.[ApprovalRole] ApprovalRole

 left join MasterData.GlobalCode EventGlobalCodeId on EventGlobalCodeId.Id=ApprovalRole.EventGlobalCodeId
 left join Security.ApplicationRole RoleId on RoleId.Id=ApprovalRole.RoleId
GO
/****** Object:  View [Security].[ApprovalStatsMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalStatsMasterView source

-- [Security].ApprovalStatsMasterView source

CREATE   VIEW [Security].[ApprovalStatsMasterView]
as select

   ROW_NUMBER() OVER (ORDER BY approval.[ApprovalType]) AS RowNumber,
   approval.[ApprovalType],
   COUNT(1) as Size
FROM [Security].Approval approval
WHERE approval.Status = 'INITIATED'
GROUP BY approval.ApprovalType
GO
/****** Object:  View [Security].[ApprovalWorkflowMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].ApprovalWorkflowMasterView source

CREATE   VIEW [Security].[ApprovalWorkflowMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY ApprovalWorkflow.[Id]) AS RowNumber,
ApprovalWorkflow.[Id],
 ApprovalWorkflow.[WorkflowName],
 ApprovalWorkflow.[IsDefaultApprovalWorkflow],
 ApprovalWorkflow.[RequiredApprovers],
 ApprovalWorkflow.[RequiredGroups],
 ApprovalWorkflow.[Description],
 ApprovalWorkflow.[IsActive],
 ApprovalWorkflow.[CreatedByUserId],
 ApprovalWorkflow.[DateCreated],
 ApprovalWorkflow.[UpdatedByUserId],
 ApprovalWorkflow.[DateUpdated],
 ApprovalWorkflow.[DeletedByUserId],
 ApprovalWorkflow.[IsDeleted],
 ApprovalWorkflow.[DateDeleted],
 ApprovalWorkflow.[RowVersion],
 ApprovalWorkflow.[FullText],
 ApprovalWorkflow.[Tags],
 ApprovalWorkflow.[Caption]

 from Security.[ApprovalWorkflow] ApprovalWorkflow
GO
/****** Object:  View [Security].[AuditTrailMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].AuditTrailMasterView source

CREATE   VIEW [Security].[AuditTrailMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY AuditTrail.[Id]) AS RowNumber,
AuditTrail.[Id],
 AuditTrail.[ApplicationUserId],
 AuditTrail.[EventGlobalCodeId],
 AuditTrail.[UserName],
 AuditTrail.[Timestamp],
 AuditTrail.[EventType],
 AuditTrail.[TableName],
 AuditTrail.[PrimaryKey],
 AuditTrail.[OldValues],
 AuditTrail.[NewValues],
 AuditTrail.[AuditJson],
 AuditTrail.[Module],
 AuditTrail.[Payload],
 AuditTrail.[Action],
 AuditTrail.[IPAddress],
 AuditTrail.[Description],
 AuditTrail.[IsActive],
 AuditTrail.[CreatedByUserId],
 AuditTrail.[DateCreated],
 AuditTrail.[UpdatedByUserId],
 AuditTrail.[DateUpdated],
 AuditTrail.[DeletedByUserId],
 AuditTrail.[IsDeleted],
 AuditTrail.[DateDeleted],
 AuditTrail.[RowVersion],
 AuditTrail.[FullText],
 AuditTrail.[Tags],
 AuditTrail.[Caption],
ApplicationUserId.[AdObjectId] as ApplicationUserId_AdObjectId,
ApplicationUserId.[IsAdmin] as ApplicationUserId_IsAdmin,
ApplicationUserId.[SecondaryPhone] as ApplicationUserId_SecondaryPhone,
ApplicationUserId.[SecondaryPhoneConfirmed] as ApplicationUserId_SecondaryPhoneConfirmed,
ApplicationUserId.[UserName] as ApplicationUserId_UserName,
ApplicationUserId.[NormalizedUserName] as ApplicationUserId_NormalizedUserName,
ApplicationUserId.[Email] as ApplicationUserId_Email,
ApplicationUserId.[NormalizedEmail] as ApplicationUserId_NormalizedEmail,
ApplicationUserId.[EmailConfirmed] as ApplicationUserId_EmailConfirmed,
ApplicationUserId.[PasswordHash] as ApplicationUserId_PasswordHash,
ApplicationUserId.[SecurityStamp] as ApplicationUserId_SecurityStamp,
ApplicationUserId.[ConcurrencyStamp] as ApplicationUserId_ConcurrencyStamp,
ApplicationUserId.[PhoneNumber] as ApplicationUserId_PhoneNumber,
ApplicationUserId.[PhoneNumberConfirmed] as ApplicationUserId_PhoneNumberConfirmed,
ApplicationUserId.[TwoFactorEnabled] as ApplicationUserId_TwoFactorEnabled,
ApplicationUserId.[LockoutEnd] as ApplicationUserId_LockoutEnd,
ApplicationUserId.[LockoutEnabled] as ApplicationUserId_LockoutEnabled,
ApplicationUserId.[AccessFailedCount] as ApplicationUserId_AccessFailedCount,
EventGlobalCodeId.[CodeType] as EventGlobalCodeId_CodeType,
EventGlobalCodeId.[Code] as EventGlobalCodeId_Code,
EventGlobalCodeId.[Name] as EventGlobalCodeId_Name,
EventGlobalCodeId.[SystemFlag] as EventGlobalCodeId_SystemFlag,
EventGlobalCodeId.[IsActive] as EventGlobalCodeId_IsActive,
EventGlobalCodeId.[CreatedByUserId] as EventGlobalCodeId_CreatedByUserId,
EventGlobalCodeId.[UpdatedByUserId] as EventGlobalCodeId_UpdatedByUserId,
EventGlobalCodeId.[DeletedByUserId] as EventGlobalCodeId_DeletedByUserId,
EventGlobalCodeId.[IsDeleted] as EventGlobalCodeId_IsDeleted,
EventGlobalCodeId.[Tags] as EventGlobalCodeId_Tags,
EventGlobalCodeId.[Caption] as EventGlobalCodeId_Caption

 from Security.[AuditTrail] AuditTrail

 left join Security.ApplicationUser ApplicationUserId on ApplicationUserId.Id=AuditTrail.ApplicationUserId
 left join MasterData.GlobalCode EventGlobalCodeId on EventGlobalCodeId.Id=AuditTrail.EventGlobalCodeId
GO
/****** Object:  View [Security].[EnrollmentPaymentInfoMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].EnrollmentPaymentInfoMasterView source

CREATE   VIEW [Security].[EnrollmentPaymentInfoMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY EnrollmentPaymentInfo.[Id]) AS RowNumber,
EnrollmentPaymentInfo.[Id],
 EnrollmentPaymentInfo.[ProfileId],
 EnrollmentPaymentInfo.[Evidence],
 EnrollmentPaymentInfo.[MimeType],
 EnrollmentPaymentInfo.[FileName],
 EnrollmentPaymentInfo.[FileSize],
 EnrollmentPaymentInfo.[Description],
 EnrollmentPaymentInfo.[IsActive],
 EnrollmentPaymentInfo.[CreatedByUserId],
 EnrollmentPaymentInfo.[DateCreated],
 EnrollmentPaymentInfo.[UpdatedByUserId],
 EnrollmentPaymentInfo.[DateUpdated],
 EnrollmentPaymentInfo.[DeletedByUserId],
 EnrollmentPaymentInfo.[IsDeleted],
 EnrollmentPaymentInfo.[DateDeleted],
 EnrollmentPaymentInfo.[RowVersion],
 EnrollmentPaymentInfo.[FullText],
 EnrollmentPaymentInfo.[Tags],
 EnrollmentPaymentInfo.[Caption],
ProfileId.[ApplicationUserId] as ProfileId_ApplicationUserId,
ProfileId.[YearsOfService] as ProfileId_YearsOfService,
ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
ProfileId.[KycStartDate] as ProfileId_KycStartDate,
ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
ProfileId.[Status] as ProfileId_Status,
ProfileId.[Gender] as ProfileId_Gender,
ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
ProfileId.[PassportUrl] as ProfileId_PassportUrl,
ProfileId.[IdentificationType] as ProfileId_IdentificationType,
ProfileId.[IdentificationNumber] as ProfileId_IdentificationNumber,
ProfileId.[IdentificationUrl] as ProfileId_IdentificationUrl,
ProfileId.[KycSubmitted] as ProfileId_KycSubmitted,
ProfileId.[KycSubmittedOn] as ProfileId_KycSubmittedOn,
ProfileId.[KycApproved] as ProfileId_KycApproved,
ProfileId.[KycApprovedOn] as ProfileId_KycApprovedOn,
ProfileId.[KycApprovedBy] as ProfileId_KycApprovedBy,
ProfileId.[FirstName] as ProfileId_FirstName,
ProfileId.[LastName] as ProfileId_LastName,
ProfileId.[MiddleName] as ProfileId_MiddleName,
ProfileId.[DepartmentId] as ProfileId_DepartmentId,
ProfileId.[MembershipId] as ProfileId_MembershipId,
ProfileId.[CAI] as ProfileId_CAI,
ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
ProfileId.[StateOfOrigin] as ProfileId_StateOfOrigin,
ProfileId.[PrimaryEmail] as ProfileId_PrimaryEmail,
ProfileId.[SecondaryEmail] as ProfileId_SecondaryEmail,
ProfileId.[PrimaryPhone] as ProfileId_PrimaryPhone,
ProfileId.[SecondaryPhone] as ProfileId_SecondaryPhone,
ProfileId.[ResidentialAddress] as ProfileId_ResidentialAddress,
ProfileId.[OfficeAddress] as ProfileId_OfficeAddress,
ProfileId.[Rank] as ProfileId_Rank,
ProfileId.[JobRole] as ProfileId_JobRole,
ProfileId.[DOB] as ProfileId_DOB,
ProfileId.[Address] as ProfileId_Address,
ProfileId.[Country] as ProfileId_Country,
ProfileId.[State] as ProfileId_State,
ProfileId.[IsActive] as ProfileId_IsActive,
ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
ProfileId.[IsDeleted] as ProfileId_IsDeleted,
ProfileId.[Tags] as ProfileId_Tags,
ProfileId.[Caption] as ProfileId_Caption,
ProfileId.[MemberType] as ProfileId_MemberType

 from Security.[EnrollmentPaymentInfo] EnrollmentPaymentInfo

 left join Security.MemberProfile ProfileId on ProfileId.Id=EnrollmentPaymentInfo.ProfileId
GO
/****** Object:  View [Security].[MemberBankAccountMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].MemberBankAccountMasterView source

-- Customer.MemberBankAccountMasterView source

CREATE   VIEW [Security].[MemberBankAccountMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY MemberBankAccount.[Id]) AS RowNumber,
MemberBankAccount.[Id],
 MemberBankAccount.[ProfileId],
 MemberBankAccount.[BankId],
 MemberBankAccount.[AccountName],
 MemberBankAccount.[AccountNumber],
 MemberBankAccount.[BVN],
 MemberBankAccount.[Branch],
 MemberBankAccount.[Description],
 MemberBankAccount.[IsActive],
 MemberBankAccount.[CreatedByUserId],
 MemberBankAccount.[DateCreated],
 MemberBankAccount.[UpdatedByUserId],
 MemberBankAccount.[DateUpdated],
 MemberBankAccount.[DeletedByUserId],
 MemberBankAccount.[IsDeleted],
 MemberBankAccount.[DateDeleted],
 MemberBankAccount.[RowVersion],
 MemberBankAccount.[FullText],
 MemberBankAccount.[Tags],
 MemberBankAccount.[Caption],
MemberId.[ApplicationUserId] as MemberId_ApplicationUserId,
MemberId.[YearsOfService] as MemberId_YearsOfService,
MemberId.[IsKycStarted] as MemberId_IsKycStarted,
MemberId.[IsKycCompleted] as MemberId_IsKycCompleted,
MemberId.[KycStartDate] as MemberId_KycStartDate,
MemberId.[KycCompletedDate] as MemberId_KycCompletedDate,
MemberId.[Status] as MemberId_Status,
MemberId.[MemberType] as MemberId_MemberType,
MemberId.[Gender] as MemberId_Gender,
MemberId.[ProfileImageUrl] as MemberId_ProfileImageUrl,
MemberId.[PassportUrl] as MemberId_PassportUrl,
MemberId.[IdentificationType] as MemberId_IdentificationType,
MemberId.[IdentificationNumber] as MemberId_IdentificationNumber,
MemberId.[IdentificationUrl] as MemberId_IdentificationUrl,
MemberId.[KycSubmitted] as MemberId_KycSubmitted,
MemberId.[KycSubmittedOn] as MemberId_KycSubmittedOn,
MemberId.[KycApproved] as MemberId_KycApproved,
MemberId.[KycApprovedOn] as MemberId_KycApprovedOn,
MemberId.[KycApprovedBy] as MemberId_KycApprovedBy,
MemberId.[FirstName] as MemberId_FirstName,
MemberId.[LastName] as MemberId_LastName,
MemberId.[MiddleName] as MemberId_MiddleName,
MemberId.[DepartmentId] as MemberId_DepartmentId,
MemberId.[CAI] as MemberId_CAI,
MemberId.[RetireeNumber] as MemberId_RetireeNumber,
MemberId.[StateOfOrigin] as MemberId_StateOfOrigin,
MemberId.[PrimaryEmail] as MemberId_PrimaryEmail,
MemberId.[SecondaryEmail] as MemberId_SecondaryEmail,
MemberId.[PrimaryPhone] as MemberId_PrimaryPhone,
MemberId.[SecondaryPhone] as MemberId_SecondaryPhone,
MemberId.[ResidentialAddress] as MemberId_ResidentialAddress,
MemberId.[OfficeAddress] as MemberId_OfficeAddress,
MemberId.[Rank] as MemberId_Rank,
MemberId.[JobRole] as MemberId_JobRole,
MemberId.[DOB] as MemberId_DOB,
MemberId.[Address] as MemberId_Address,
MemberId.[Country] as MemberId_Country,
MemberId.[State] as MemberId_State,
MemberId.[IsActive] as MemberId_IsActive,
MemberId.[CreatedByUserId] as MemberId_CreatedByUserId,
MemberId.[UpdatedByUserId] as MemberId_UpdatedByUserId,
MemberId.[DeletedByUserId] as MemberId_DeletedByUserId,
MemberId.[IsDeleted] as MemberId_IsDeleted,
MemberId.[Tags] as MemberId_Tags,
MemberId.[Caption] as MemberId_Caption,
BankId.[Code] as BankId_Code,
BankId.[SortCode] as BankId_SortCode,
BankId.[Name] as BankId_Name,
BankId.[Address] as BankId_Address,
BankId.[ContactName] as BankId_ContactName,
BankId.[ContactDetails] as BankId_ContactDetails,
BankId.[IsActive] as BankId_IsActive,
BankId.[CreatedByUserId] as BankId_CreatedByUserId,
BankId.[UpdatedByUserId] as BankId_UpdatedByUserId,
BankId.[DeletedByUserId] as BankId_DeletedByUserId,
BankId.[IsDeleted] as BankId_IsDeleted,
BankId.[Tags] as BankId_Tags,
BankId.[Caption] as BankId_Caption

 from [Security].[MemberBankAccount] MemberBankAccount

 left join Security.MemberProfile MemberId on MemberId.Id=MemberBankAccount.ProfileId
 left join MasterData.Bank BankId on BankId.Id=MemberBankAccount.BankId
GO
/****** Object:  View [Security].[MemberBeneficiaryMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].MemberBeneficiaryMasterView source

CREATE   VIEW [Security].[MemberBeneficiaryMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY MemberBeneficiary.[Id]) AS RowNumber,
MemberBeneficiary.[Id],
 MemberBeneficiary.[ProfileId],
 MemberBeneficiary.[FirstName],
 MemberBeneficiary.[LastName],
 MemberBeneficiary.[Email],
 MemberBeneficiary.[Phone],
 MemberBeneficiary.[Address],
 MemberBeneficiary.[Description],
 MemberBeneficiary.[IsActive],
 MemberBeneficiary.[CreatedByUserId],
 MemberBeneficiary.[DateCreated],
 MemberBeneficiary.[UpdatedByUserId],
 MemberBeneficiary.[DateUpdated],
 MemberBeneficiary.[DeletedByUserId],
 MemberBeneficiary.[IsDeleted],
 MemberBeneficiary.[DateDeleted],
 MemberBeneficiary.[RowVersion],
 MemberBeneficiary.[FullText],
 MemberBeneficiary.[Tags],
 MemberBeneficiary.[Caption],
ProfileId.[ApplicationUserId] as ProfileId_ApplicationUserId,
ProfileId.[YearsOfService] as ProfileId_YearsOfService,
ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
ProfileId.[KycStartDate] as ProfileId_KycStartDate,
ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
ProfileId.[Status] as ProfileId_Status,
ProfileId.[Gender] as ProfileId_Gender,
ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
ProfileId.[PassportUrl] as ProfileId_PassportUrl,
ProfileId.[IdentificationType] as ProfileId_IdentificationType,
ProfileId.[IdentificationNumber] as ProfileId_IdentificationNumber,
ProfileId.[IdentificationUrl] as ProfileId_IdentificationUrl,
ProfileId.[KycSubmitted] as ProfileId_KycSubmitted,
ProfileId.[KycSubmittedOn] as ProfileId_KycSubmittedOn,
ProfileId.[KycApproved] as ProfileId_KycApproved,
ProfileId.[KycApprovedOn] as ProfileId_KycApprovedOn,
ProfileId.[KycApprovedBy] as ProfileId_KycApprovedBy,
ProfileId.[FirstName] as ProfileId_FirstName,
ProfileId.[LastName] as ProfileId_LastName,
ProfileId.[MiddleName] as ProfileId_MiddleName,
ProfileId.[DepartmentId] as ProfileId_DepartmentId,
ProfileId.[MembershipId] as ProfileId_MembershipId,
ProfileId.[CAI] as ProfileId_CAI,
ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
ProfileId.[StateOfOrigin] as ProfileId_StateOfOrigin,
ProfileId.[PrimaryEmail] as ProfileId_PrimaryEmail,
ProfileId.[SecondaryEmail] as ProfileId_SecondaryEmail,
ProfileId.[PrimaryPhone] as ProfileId_PrimaryPhone,
ProfileId.[SecondaryPhone] as ProfileId_SecondaryPhone,
ProfileId.[ResidentialAddress] as ProfileId_ResidentialAddress,
ProfileId.[OfficeAddress] as ProfileId_OfficeAddress,
ProfileId.[Rank] as ProfileId_Rank,
ProfileId.[JobRole] as ProfileId_JobRole,
ProfileId.[DOB] as ProfileId_DOB,
ProfileId.[Address] as ProfileId_Address,
ProfileId.[Country] as ProfileId_Country,
ProfileId.[State] as ProfileId_State,
ProfileId.[IsActive] as ProfileId_IsActive,
ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
ProfileId.[IsDeleted] as ProfileId_IsDeleted,
ProfileId.[Tags] as ProfileId_Tags,
ProfileId.[Caption] as ProfileId_Caption,
ProfileId.[MemberType] as ProfileId_MemberType

 from Security.[MemberBeneficiary] MemberBeneficiary

 left join Security.MemberProfile ProfileId on ProfileId.Id=MemberBeneficiary.ProfileId
GO
/****** Object:  View [Security].[MemberBulkUploadSessionMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].MemberBulkUploadSessionMasterView source

CREATE   VIEW [Security].[MemberBulkUploadSessionMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY MemberBulkUploadSession.[Id]) AS RowNumber,
MemberBulkUploadSession.[Id],
 MemberBulkUploadSession.[ApprovedByUserId],
 MemberBulkUploadSession.[Size],
 MemberBulkUploadSession.[Status],
 MemberBulkUploadSession.[Description],
 MemberBulkUploadSession.[IsActive],
 MemberBulkUploadSession.[CreatedByUserId],
 MemberBulkUploadSession.[DateCreated],
 MemberBulkUploadSession.[UpdatedByUserId],
 MemberBulkUploadSession.[DateUpdated],
 MemberBulkUploadSession.[DeletedByUserId],
 MemberBulkUploadSession.[IsDeleted],
 MemberBulkUploadSession.[DateDeleted],
 MemberBulkUploadSession.[RowVersion],
 MemberBulkUploadSession.[FullText],
 MemberBulkUploadSession.[Tags],
 MemberBulkUploadSession.[Caption]

 from Security.[MemberBulkUploadSession] MemberBulkUploadSession
GO
/****** Object:  View [Security].[MemberBulkUploadTempMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].MemberBulkUploadTempMasterView source

CREATE   VIEW [Security].[MemberBulkUploadTempMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY MemberBulkUploadTemp.[Id]) AS RowNumber,
MemberBulkUploadTemp.[Id],
 MemberBulkUploadTemp.[RecordId],
 MemberBulkUploadTemp.[LastName],
 MemberBulkUploadTemp.[FirstName],
 MemberBulkUploadTemp.[Gender],
 MemberBulkUploadTemp.[Email],
 MemberBulkUploadTemp.[PhoneNumber],
 MemberBulkUploadTemp.[MembershipNumber],
 MemberBulkUploadTemp.[UserRole],
 MemberBulkUploadTemp.[Country],
 MemberBulkUploadTemp.[Status],
 MemberBulkUploadTemp.[IsValid],
 MemberBulkUploadTemp.[UploadedByUserId],
 MemberBulkUploadTemp.[MemberBulkUploadSessionId],
 MemberBulkUploadTemp.[ApprovalId],
 MemberBulkUploadTemp.[IsSuccessfullyRegistered],
 MemberBulkUploadTemp.[Description],
 MemberBulkUploadTemp.[IsActive],
 MemberBulkUploadTemp.[CreatedByUserId],
 MemberBulkUploadTemp.[DateCreated],
 MemberBulkUploadTemp.[UpdatedByUserId],
 MemberBulkUploadTemp.[DateUpdated],
 MemberBulkUploadTemp.[DeletedByUserId],
 MemberBulkUploadTemp.[IsDeleted],
 MemberBulkUploadTemp.[DateDeleted],
 MemberBulkUploadTemp.[RowVersion],
 MemberBulkUploadTemp.[FullText],
 MemberBulkUploadTemp.[Tags],
 MemberBulkUploadTemp.[Caption]

 from Security.[MemberBulkUploadTemp] MemberBulkUploadTemp
GO
/****** Object:  View [Security].[MemberNextOfKinMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].MemberNextOfKinMasterView source

CREATE   VIEW [Security].[MemberNextOfKinMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY MemberNextOfKin.[Id]) AS RowNumber,
MemberNextOfKin.[Id],
 MemberNextOfKin.[ProfileId],
 MemberNextOfKin.[FirstName],
 MemberNextOfKin.[LastName],
 MemberNextOfKin.[Email],
 MemberNextOfKin.[Phone],
 MemberNextOfKin.[Relationship],
 MemberNextOfKin.[Address],
 MemberNextOfKin.[Description],
 MemberNextOfKin.[IsActive],
 MemberNextOfKin.[CreatedByUserId],
 MemberNextOfKin.[DateCreated],
 MemberNextOfKin.[UpdatedByUserId],
 MemberNextOfKin.[DateUpdated],
 MemberNextOfKin.[DeletedByUserId],
 MemberNextOfKin.[IsDeleted],
 MemberNextOfKin.[DateDeleted],
 MemberNextOfKin.[RowVersion],
 MemberNextOfKin.[FullText],
 MemberNextOfKin.[Tags],
 MemberNextOfKin.[Caption],
ProfileId.[ApplicationUserId] as ProfileId_ApplicationUserId,
ProfileId.[YearsOfService] as ProfileId_YearsOfService,
ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
ProfileId.[KycStartDate] as ProfileId_KycStartDate,
ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
ProfileId.[Status] as ProfileId_Status,
ProfileId.[Gender] as ProfileId_Gender,
ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
ProfileId.[PassportUrl] as ProfileId_PassportUrl,
ProfileId.[IdentificationType] as ProfileId_IdentificationType,
ProfileId.[IdentificationNumber] as ProfileId_IdentificationNumber,
ProfileId.[IdentificationUrl] as ProfileId_IdentificationUrl,
ProfileId.[KycSubmitted] as ProfileId_KycSubmitted,
ProfileId.[KycSubmittedOn] as ProfileId_KycSubmittedOn,
ProfileId.[KycApproved] as ProfileId_KycApproved,
ProfileId.[KycApprovedOn] as ProfileId_KycApprovedOn,
ProfileId.[KycApprovedBy] as ProfileId_KycApprovedBy,
ProfileId.[FirstName] as ProfileId_FirstName,
ProfileId.[LastName] as ProfileId_LastName,
ProfileId.[MiddleName] as ProfileId_MiddleName,
ProfileId.[DepartmentId] as ProfileId_DepartmentId,
ProfileId.[MembershipId] as ProfileId_MembershipId,
ProfileId.[CAI] as ProfileId_CAI,
ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
ProfileId.[StateOfOrigin] as ProfileId_StateOfOrigin,
ProfileId.[PrimaryEmail] as ProfileId_PrimaryEmail,
ProfileId.[SecondaryEmail] as ProfileId_SecondaryEmail,
ProfileId.[PrimaryPhone] as ProfileId_PrimaryPhone,
ProfileId.[SecondaryPhone] as ProfileId_SecondaryPhone,
ProfileId.[ResidentialAddress] as ProfileId_ResidentialAddress,
ProfileId.[OfficeAddress] as ProfileId_OfficeAddress,
ProfileId.[Rank] as ProfileId_Rank,
ProfileId.[JobRole] as ProfileId_JobRole,
ProfileId.[DOB] as ProfileId_DOB,
ProfileId.[Address] as ProfileId_Address,
ProfileId.[Country] as ProfileId_Country,
ProfileId.[State] as ProfileId_State,
ProfileId.[IsActive] as ProfileId_IsActive,
ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
ProfileId.[IsDeleted] as ProfileId_IsDeleted,
ProfileId.[Tags] as ProfileId_Tags,
ProfileId.[Caption] as ProfileId_Caption,
ProfileId.[MemberType] as ProfileId_MemberType

 from Security.[MemberNextOfKin] MemberNextOfKin

 left join Security.MemberProfile ProfileId on ProfileId.Id=MemberNextOfKin.ProfileId
GO
/****** Object:  View [Security].[MemberProfileMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].MemberProfileMasterView source

CREATE   VIEW [Security].[MemberProfileMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY MemberProfile.[Id]) AS RowNumber,
MemberProfile.[Id],
 MemberProfile.[ApplicationUserId],
 MemberProfile.[YearsOfService],
 MemberProfile.[IsKycStarted],
 MemberProfile.[IsKycCompleted],
 MemberProfile.[KycStartDate],
 MemberProfile.[KycCompletedDate],
 MemberProfile.[Status],
 MemberProfile.[Gender],
 MemberProfile.[ProfileImageUrl],
 MemberProfile.[PassportUrl],
 MemberProfile.[IdentificationType],
 MemberProfile.[IdentificationNumber],
 MemberProfile.[IdentificationUrl],
 MemberProfile.[KycSubmitted],
 MemberProfile.[KycSubmittedOn],
 MemberProfile.[KycApproved],
 MemberProfile.[KycApprovedOn],
 MemberProfile.[KycApprovedBy],
 MemberProfile.[FirstName],
 MemberProfile.[LastName],
 MemberProfile.[MiddleName],
 MemberProfile.[DepartmentId],
 MemberProfile.[MembershipId],
 MemberProfile.[CAI],
 MemberProfile.[RetireeNumber],
 MemberProfile.[StateOfOrigin],
 MemberProfile.[PrimaryEmail],
 MemberProfile.[SecondaryEmail],
 MemberProfile.[PrimaryPhone],
 MemberProfile.[SecondaryPhone],
 MemberProfile.[ResidentialAddress],
 MemberProfile.[OfficeAddress],
 MemberProfile.[Rank],
 MemberProfile.[JobRole],
 MemberProfile.[DOB],
 MemberProfile.[Address],
 MemberProfile.[Country],
 MemberProfile.[State],
 MemberProfile.[Description],
 MemberProfile.[IsActive],
 MemberProfile.[CreatedByUserId],
 MemberProfile.[DateCreated],
 MemberProfile.[UpdatedByUserId],
 MemberProfile.[DateUpdated],
 MemberProfile.[DeletedByUserId],
 MemberProfile.[IsDeleted],
 MemberProfile.[DateDeleted],
 MemberProfile.[RowVersion],
 MemberProfile.[FullText],
 MemberProfile.[Tags],
 MemberProfile.[Caption],
 MemberProfile.[MemberType],
ApplicationUserId.[AdObjectId] as ApplicationUserId_AdObjectId,
ApplicationUserId.[IsAdmin] as ApplicationUserId_IsAdmin,
ApplicationUserId.[SecondaryPhone] as ApplicationUserId_SecondaryPhone,
ApplicationUserId.[SecondaryPhoneConfirmed] as ApplicationUserId_SecondaryPhoneConfirmed,
ApplicationUserId.[UserName] as ApplicationUserId_UserName,
ApplicationUserId.[NormalizedUserName] as ApplicationUserId_NormalizedUserName,
ApplicationUserId.[Email] as ApplicationUserId_Email,
ApplicationUserId.[NormalizedEmail] as ApplicationUserId_NormalizedEmail,
ApplicationUserId.[EmailConfirmed] as ApplicationUserId_EmailConfirmed,
ApplicationUserId.[PasswordHash] as ApplicationUserId_PasswordHash,
ApplicationUserId.[SecurityStamp] as ApplicationUserId_SecurityStamp,
ApplicationUserId.[ConcurrencyStamp] as ApplicationUserId_ConcurrencyStamp,
ApplicationUserId.[PhoneNumber] as ApplicationUserId_PhoneNumber,
ApplicationUserId.[PhoneNumberConfirmed] as ApplicationUserId_PhoneNumberConfirmed,
ApplicationUserId.[TwoFactorEnabled] as ApplicationUserId_TwoFactorEnabled,
ApplicationUserId.[LockoutEnd] as ApplicationUserId_LockoutEnd,
ApplicationUserId.[LockoutEnabled] as ApplicationUserId_LockoutEnabled,
ApplicationUserId.[AccessFailedCount] as ApplicationUserId_AccessFailedCount,
DepartmentId.[Name] as DepartmentId_Name,
DepartmentId.[IsActive] as DepartmentId_IsActive,
DepartmentId.[CreatedByUserId] as DepartmentId_CreatedByUserId,
DepartmentId.[UpdatedByUserId] as DepartmentId_UpdatedByUserId,
DepartmentId.[DeletedByUserId] as DepartmentId_DeletedByUserId,
DepartmentId.[IsDeleted] as DepartmentId_IsDeleted,
DepartmentId.[Tags] as DepartmentId_Tags,
DepartmentId.[Caption] as DepartmentId_Caption

 from Security.[MemberProfile] MemberProfile

 left join Security.ApplicationUser ApplicationUserId on ApplicationUserId.Id=MemberProfile.ApplicationUserId
 left join MasterData.Department DepartmentId on DepartmentId.Id=MemberProfile.DepartmentId
GO
/****** Object:  View [Security].[PermissionMasterView]    Script Date: 7/6/2023 6:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [Security].PermissionMasterView source

CREATE   VIEW [Security].[PermissionMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY Permission.[Id]) AS RowNumber,
Permission.[Id],
 Permission.[Code],
 Permission.[Name],
 Permission.[Group],
 Permission.[Category],
 Permission.[Module],
 Permission.[Owner],
 Permission.[Description],
 Permission.[IsActive],
 Permission.[CreatedByUserId],
 Permission.[DateCreated],
 Permission.[UpdatedByUserId],
 Permission.[DateUpdated],
 Permission.[DeletedByUserId],
 Permission.[IsDeleted],
 Permission.[DateDeleted],
 Permission.[RowVersion],
 Permission.[FullText],
 Permission.[Tags],
 Permission.[Caption]

 from Security.[Permission] Permission
GO
