/****** Object:  View [Docs].[CustomerPaymentDocumentMasterView]    Script Date: 7/6/2023 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Docs.CustomerPaymentDocumentMasterView source

CREATE   VIEW [Docs].[CustomerPaymentDocumentMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY CustomerPaymentDocument.[Id]) AS RowNumber,
CustomerPaymentDocument.[Id],
 CustomerPaymentDocument.[CustomerId],
 CustomerPaymentDocument.[DocumentData],
 CustomerPaymentDocument.[MimeType],
 CustomerPaymentDocument.[FileName],
 CustomerPaymentDocument.[FileSize],
 CustomerPaymentDocument.[DocumentType],
 CustomerPaymentDocument.[Description],
 CustomerPaymentDocument.[IsActive],
 CustomerPaymentDocument.[CreatedByUserId],
 CustomerPaymentDocument.[DateCreated],
 CustomerPaymentDocument.[UpdatedByUserId],
 CustomerPaymentDocument.[DateUpdated],
 CustomerPaymentDocument.[DeletedByUserId],
 CustomerPaymentDocument.[IsDeleted],
 CustomerPaymentDocument.[DateDeleted],
 CustomerPaymentDocument.[RowVersion],
 CustomerPaymentDocument.[FullText],
 CustomerPaymentDocument.[Tags],
 CustomerPaymentDocument.[Caption],
CustomerId.[CustomerNo] as CustomerId_CustomerNo,
CustomerId.[ApplicationUserId] as CustomerId_ApplicationUserId,
CustomerId.[YearsOfService] as CustomerId_YearsOfService,
CustomerId.[IsKycStarted] as CustomerId_IsKycStarted,
CustomerId.[IsKycCompleted] as CustomerId_IsKycCompleted,
CustomerId.[KycStartDate] as CustomerId_KycStartDate,
CustomerId.[KycCompletedDate] as CustomerId_KycCompletedDate,
CustomerId.[Status] as CustomerId_Status,
CustomerId.[MemberType] as CustomerId_MemberType,
CustomerId.[Gender] as CustomerId_Gender,
CustomerId.[ProfileImageUrl] as CustomerId_ProfileImageUrl,
CustomerId.[PassportUrl] as CustomerId_PassportUrl,
CustomerId.[IdentificationType] as CustomerId_IdentificationType,
CustomerId.[IdentificationNumber] as CustomerId_IdentificationNumber,
CustomerId.[IdentificationUrl] as CustomerId_IdentificationUrl,
CustomerId.[KycSubmitted] as CustomerId_KycSubmitted,
CustomerId.[KycSubmittedOn] as CustomerId_KycSubmittedOn,
CustomerId.[KycApproved] as CustomerId_KycApproved,
CustomerId.[KycApprovedOn] as CustomerId_KycApprovedOn,
CustomerId.[KycApprovedBy] as CustomerId_KycApprovedBy,
CustomerId.[FirstName] as CustomerId_FirstName,
CustomerId.[LastName] as CustomerId_LastName,
CustomerId.[MiddleName] as CustomerId_MiddleName,
CustomerId.[DepartmentId] as CustomerId_DepartmentId,
CustomerId.[MemberId] as CustomerId_MemberId,
CustomerId.[CAI] as CustomerId_CAI,
CustomerId.[RetireeNumber] as CustomerId_RetireeNumber,
CustomerId.[StateOfOrigin] as CustomerId_StateOfOrigin,
CustomerId.[PrimaryEmail] as CustomerId_PrimaryEmail,
CustomerId.[SecondaryEmail] as CustomerId_SecondaryEmail,
CustomerId.[PrimaryPhone] as CustomerId_PrimaryPhone,
CustomerId.[SecondaryPhone] as CustomerId_SecondaryPhone,
CustomerId.[ResidentialAddress] as CustomerId_ResidentialAddress,
CustomerId.[OfficeAddress] as CustomerId_OfficeAddress,
CustomerId.[Rank] as CustomerId_Rank,
CustomerId.[JobRole] as CustomerId_JobRole,
CustomerId.[DOB] as CustomerId_DOB,
CustomerId.[Address] as CustomerId_Address,
CustomerId.[Country] as CustomerId_Country,
CustomerId.[State] as CustomerId_State,
CustomerId.[IsActive] as CustomerId_IsActive,
CustomerId.[CreatedByUserId] as CustomerId_CreatedByUserId,
CustomerId.[UpdatedByUserId] as CustomerId_UpdatedByUserId,
CustomerId.[DeletedByUserId] as CustomerId_DeletedByUserId,
CustomerId.[IsDeleted] as CustomerId_IsDeleted,
CustomerId.[Tags] as CustomerId_Tags,
CustomerId.[Caption] as CustomerId_Caption

 from Docs.[CustomerPaymentDocument] CustomerPaymentDocument

 left join Customer.Customer CustomerId on CustomerId.Id=CustomerPaymentDocument.CustomerId
GO
/****** Object:  View [Docs].[DocumentTypeMasterView]    Script Date: 7/6/2023 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Docs.DocumentTypeMasterView source

CREATE   VIEW [Docs].[DocumentTypeMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY DocumentType.[Id]) AS RowNumber,
DocumentType.[Id],
 DocumentType.[Name],
 DocumentType.[SystemFlag],
 DocumentType.[Description],
 DocumentType.[IsActive],
 DocumentType.[CreatedByUserId],
 DocumentType.[DateCreated],
 DocumentType.[UpdatedByUserId],
 DocumentType.[DateUpdated],
 DocumentType.[DeletedByUserId],
 DocumentType.[IsDeleted],
 DocumentType.[DateDeleted],
 DocumentType.[RowVersion],
 DocumentType.[FullText],
 DocumentType.[Tags],
 DocumentType.[Caption]

 from Docs.[DocumentType] DocumentType
GO
/****** Object:  View [Docs].[OfficeDocumentMasterView]    Script Date: 7/6/2023 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Docs.OfficeDocumentMasterView source

CREATE   VIEW [Docs].[OfficeDocumentMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY OfficeDocument.[Id]) AS RowNumber,
OfficeDocument.[Id],
 OfficeDocument.[DocumentNo],
 OfficeDocument.[DocumentTypeId],
 OfficeDocument.[Name],
 OfficeDocument.[DocumentData],
 OfficeDocument.[MimeType],
 OfficeDocument.[FilePath],
 OfficeDocument.[Description],
 OfficeDocument.[IsActive],
 OfficeDocument.[CreatedByUserId],
 OfficeDocument.[DateCreated],
 OfficeDocument.[UpdatedByUserId],
 OfficeDocument.[DateUpdated],
 OfficeDocument.[DeletedByUserId],
 OfficeDocument.[IsDeleted],
 OfficeDocument.[DateDeleted],
 OfficeDocument.[RowVersion],
 OfficeDocument.[FullText],
 OfficeDocument.[Tags],
 OfficeDocument.[Caption],
DocumentTypeId.[Name] as DocumentTypeId_Name,
DocumentTypeId.[SystemFlag] as DocumentTypeId_SystemFlag,
DocumentTypeId.[IsActive] as DocumentTypeId_IsActive,
DocumentTypeId.[CreatedByUserId] as DocumentTypeId_CreatedByUserId,
DocumentTypeId.[UpdatedByUserId] as DocumentTypeId_UpdatedByUserId,
DocumentTypeId.[DeletedByUserId] as DocumentTypeId_DeletedByUserId,
DocumentTypeId.[IsDeleted] as DocumentTypeId_IsDeleted,
DocumentTypeId.[Tags] as DocumentTypeId_Tags,
DocumentTypeId.[Caption] as DocumentTypeId_Caption

 from Docs.[OfficeDocument] OfficeDocument

 left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=OfficeDocument.DocumentTypeId
GO
/****** Object:  View [Docs].[OfficePhotoMasterView]    Script Date: 7/6/2023 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Docs.OfficePhotoMasterView source

CREATE   VIEW [Docs].[OfficePhotoMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY OfficePhoto.[Id]) AS RowNumber,
OfficePhoto.[Id],
 OfficePhoto.[DocumentNo],
 OfficePhoto.[DocumentTypeId],
 OfficePhoto.[Name],
 OfficePhoto.[DocumentData],
 OfficePhoto.[MimeType],
 OfficePhoto.[FilePath],
 OfficePhoto.[Description],
 OfficePhoto.[IsActive],
 OfficePhoto.[CreatedByUserId],
 OfficePhoto.[DateCreated],
 OfficePhoto.[UpdatedByUserId],
 OfficePhoto.[DateUpdated],
 OfficePhoto.[DeletedByUserId],
 OfficePhoto.[IsDeleted],
 OfficePhoto.[DateDeleted],
 OfficePhoto.[RowVersion],
 OfficePhoto.[FullText],
 OfficePhoto.[Tags],
 OfficePhoto.[Caption],
DocumentTypeId.[Name] as DocumentTypeId_Name,
DocumentTypeId.[SystemFlag] as DocumentTypeId_SystemFlag,
DocumentTypeId.[IsActive] as DocumentTypeId_IsActive,
DocumentTypeId.[CreatedByUserId] as DocumentTypeId_CreatedByUserId,
DocumentTypeId.[UpdatedByUserId] as DocumentTypeId_UpdatedByUserId,
DocumentTypeId.[DeletedByUserId] as DocumentTypeId_DeletedByUserId,
DocumentTypeId.[IsDeleted] as DocumentTypeId_IsDeleted,
DocumentTypeId.[Tags] as DocumentTypeId_Tags,
DocumentTypeId.[Caption] as DocumentTypeId_Caption

 from Docs.[OfficePhoto] OfficePhoto

 left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=OfficePhoto.DocumentTypeId
GO
/****** Object:  View [Docs].[OfficeSheetMasterView]    Script Date: 7/6/2023 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Docs.OfficeSheetMasterView source

CREATE   VIEW [Docs].[OfficeSheetMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY OfficeSheet.[Id]) AS RowNumber,
OfficeSheet.[Id],
 OfficeSheet.[DocumentNo],
 OfficeSheet.[DocumentTypeId],
 OfficeSheet.[Name],
 OfficeSheet.[DocumentData],
 OfficeSheet.[MimeType],
 OfficeSheet.[FilePath],
 OfficeSheet.[Description],
 OfficeSheet.[IsActive],
 OfficeSheet.[CreatedByUserId],
 OfficeSheet.[DateCreated],
 OfficeSheet.[UpdatedByUserId],
 OfficeSheet.[DateUpdated],
 OfficeSheet.[DeletedByUserId],
 OfficeSheet.[IsDeleted],
 OfficeSheet.[DateDeleted],
 OfficeSheet.[RowVersion],
 OfficeSheet.[FullText],
 OfficeSheet.[Tags],
 OfficeSheet.[Caption],
DocumentTypeId.[Name] as DocumentTypeId_Name,
DocumentTypeId.[SystemFlag] as DocumentTypeId_SystemFlag,
DocumentTypeId.[IsActive] as DocumentTypeId_IsActive,
DocumentTypeId.[CreatedByUserId] as DocumentTypeId_CreatedByUserId,
DocumentTypeId.[UpdatedByUserId] as DocumentTypeId_UpdatedByUserId,
DocumentTypeId.[DeletedByUserId] as DocumentTypeId_DeletedByUserId,
DocumentTypeId.[IsDeleted] as DocumentTypeId_IsDeleted,
DocumentTypeId.[Tags] as DocumentTypeId_Tags,
DocumentTypeId.[Caption] as DocumentTypeId_Caption

 from Docs.[OfficeSheet] OfficeSheet

 left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=OfficeSheet.DocumentTypeId
GO
/****** Object:  View [Docs].[PayrollDeductionDocumentMasterView]    Script Date: 7/6/2023 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Docs.PayrollDeductionDocumentMasterView source

CREATE   VIEW [Docs].[PayrollDeductionDocumentMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY PayrollDeductionDocument.[Id]) AS RowNumber,
PayrollDeductionDocument.[Id],
 PayrollDeductionDocument.[PayrollDeductionScheduleId],
 PayrollDeductionDocument.[ProcessSequence],
 PayrollDeductionDocument.[IsProcessed],
 PayrollDeductionDocument.[ProcessedDate],
 PayrollDeductionDocument.[DocumentData],
 PayrollDeductionDocument.[MimeType],
 PayrollDeductionDocument.[FileName],
 PayrollDeductionDocument.[FileSize],
 PayrollDeductionDocument.[Description],
 PayrollDeductionDocument.[IsActive],
 PayrollDeductionDocument.[CreatedByUserId],
 PayrollDeductionDocument.[DateCreated],
 PayrollDeductionDocument.[UpdatedByUserId],
 PayrollDeductionDocument.[DateUpdated],
 PayrollDeductionDocument.[DeletedByUserId],
 PayrollDeductionDocument.[IsDeleted],
 PayrollDeductionDocument.[DateDeleted],
 PayrollDeductionDocument.[RowVersion],
 PayrollDeductionDocument.[FullText],
 PayrollDeductionDocument.[Tags],
 PayrollDeductionDocument.[Caption],
PayrollDeductionScheduleId.[ScheduleName] as PayrollDeductionScheduleId_ScheduleName,
PayrollDeductionScheduleId.[PayrollDeductionAccounId] as PayrollDeductionScheduleId_PayrollDeductionAccounId,
PayrollDeductionScheduleId.[PayrollSuspenseAccountId] as PayrollDeductionScheduleId_PayrollSuspenseAccountId,
PayrollDeductionScheduleId.[BankAccountId] as PayrollDeductionScheduleId_BankAccountId,
PayrollDeductionScheduleId.[TotalDeductions] as PayrollDeductionScheduleId_TotalDeductions,
PayrollDeductionScheduleId.[MinDecimalPlace] as PayrollDeductionScheduleId_MinDecimalPlace,
PayrollDeductionScheduleId.[MaxDecimalPlace] as PayrollDeductionScheduleId_MaxDecimalPlace,
PayrollDeductionScheduleId.[AdviseDate] as PayrollDeductionScheduleId_AdviseDate,
PayrollDeductionScheduleId.[ExpectedDate] as PayrollDeductionScheduleId_ExpectedDate,
PayrollDeductionScheduleId.[IsPosted] as PayrollDeductionScheduleId_IsPosted,
PayrollDeductionScheduleId.[PayrollDate] as PayrollDeductionScheduleId_PayrollDate,
PayrollDeductionScheduleId.[IsUploaded] as PayrollDeductionScheduleId_IsUploaded,
PayrollDeductionScheduleId.[LastUploadedDate] as PayrollDeductionScheduleId_LastUploadedDate,
PayrollDeductionScheduleId.[IsProcessed] as PayrollDeductionScheduleId_IsProcessed,
PayrollDeductionScheduleId.[ProcessedDate] as PayrollDeductionScheduleId_ProcessedDate,
PayrollDeductionScheduleId.[GenerateDeductionCronJobStatus] as PayrollDeductionScheduleId_GenerateDeductionCronJobStatus,
PayrollDeductionScheduleId.[GenerateDeductionCronJobStartedDate] as PayrollDeductionScheduleId_GenerateDeductionCronJobStartedDate,
PayrollDeductionScheduleId.[GenerateDeductionCronJobCompletedDate] as PayrollDeductionScheduleId_GenerateDeductionCronJobCompletedDate,
PayrollDeductionScheduleId.[ProcessDeductionCronJobStatus] as PayrollDeductionScheduleId_ProcessDeductionCronJobStatus,
PayrollDeductionScheduleId.[ProcessDeductionCronJobStartedDate] as PayrollDeductionScheduleId_ProcessDeductionCronJobStartedDate,
PayrollDeductionScheduleId.[ProcessDeductionCronJobCompletedDate] as PayrollDeductionScheduleId_ProcessDeductionCronJobCompletedDate,
PayrollDeductionScheduleId.[IsActive] as PayrollDeductionScheduleId_IsActive,
PayrollDeductionScheduleId.[CreatedByUserId] as PayrollDeductionScheduleId_CreatedByUserId,
PayrollDeductionScheduleId.[UpdatedByUserId] as PayrollDeductionScheduleId_UpdatedByUserId,
PayrollDeductionScheduleId.[DeletedByUserId] as PayrollDeductionScheduleId_DeletedByUserId,
PayrollDeductionScheduleId.[IsDeleted] as PayrollDeductionScheduleId_IsDeleted,
PayrollDeductionScheduleId.[Tags] as PayrollDeductionScheduleId_Tags,
PayrollDeductionScheduleId.[Caption] as PayrollDeductionScheduleId_Caption

 from Docs.[PayrollDeductionDocument] PayrollDeductionDocument

 left join Payroll.PayrollDeductionSchedule PayrollDeductionScheduleId on PayrollDeductionScheduleId.Id=PayrollDeductionDocument.PayrollDeductionScheduleId
GO
