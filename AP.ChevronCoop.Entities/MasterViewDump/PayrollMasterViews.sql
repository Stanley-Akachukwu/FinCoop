/****** Object:  View [Payroll].[PayrollCronJobConfigMasterView]    Script Date: 7/6/2023 6:07:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [Payroll].[PayrollCronJobConfigMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY PayrollCronJobConfig.[Id]) AS RowNumber,
PayrollCronJobConfig.[Id],
 PayrollCronJobConfig.[CronJobType],
 PayrollCronJobConfig.[JobName],
 PayrollCronJobConfig.[JobDate],
 PayrollCronJobConfig.[JobStatus],
 PayrollCronJobConfig.[ComputationStartDate],
 PayrollCronJobConfig.[ComputationEndDate],
 PayrollCronJobConfig.[RecordsProcessed],
 PayrollCronJobConfig.[Description],
 PayrollCronJobConfig.[IsActive],
 PayrollCronJobConfig.[CreatedByUserId],
 PayrollCronJobConfig.[DateCreated],
 PayrollCronJobConfig.[UpdatedByUserId],
 PayrollCronJobConfig.[DateUpdated],
 PayrollCronJobConfig.[DeletedByUserId],
 PayrollCronJobConfig.[IsDeleted],
 PayrollCronJobConfig.[DateDeleted],
 PayrollCronJobConfig.[RowVersion],
 PayrollCronJobConfig.[FullText],
 PayrollCronJobConfig.[Tags],
 PayrollCronJobConfig.[Caption]

 from Payroll.[PayrollCronJobConfig] PayrollCronJobConfig
GO
/****** Object:  View [Payroll].[PayrollDeductionItemMasterView]    Script Date: 7/6/2023 6:07:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [Payroll].[PayrollDeductionItemMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY PayrollDeductionItem.[Id]) AS RowNumber,
PayrollDeductionItem.[Id],
 PayrollDeductionItem.[PayrollDeductionScheduleId],
 PayrollDeductionItem.[BatchRefNo],
 PayrollDeductionItem.[MemberId],
 PayrollDeductionItem.[EmployeeNo],
 PayrollDeductionItem.[MemberName],
 PayrollDeductionItem.[AccountNo],
 PayrollDeductionItem.[Amount],
 PayrollDeductionItem.[PayrollCode],
 PayrollDeductionItem.[Narration],
 PayrollDeductionItem.[PayrollDate],
 PayrollDeductionItem.[CurrentStatus],
 PayrollDeductionItem.[AccountDueDate],
 PayrollDeductionItem.[DeductionType],
 PayrollDeductionItem.[TotalDeduction]


 from Payroll.[PayrollDeductionItem] PayrollDeductionItem

 left join Payroll.PayrollDeductionSchedule PayrollDeductionScheduleId on PayrollDeductionScheduleId.Id=PayrollDeductionItem.PayrollDeductionScheduleId
GO
/****** Object:  View [Payroll].[PayrollDeductionScheduleItemMasterView]    Script Date: 7/6/2023 6:07:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [Payroll].[PayrollDeductionScheduleItemMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY PayrollDeductionScheduleItem.[Id]) AS RowNumber,
PayrollDeductionScheduleItem.[Id],
 PayrollDeductionScheduleItem.[PayrollDeductionScheduleId],
 PayrollDeductionScheduleItem.[BatchRefNo],
 PayrollDeductionScheduleItem.[MemberId],
 PayrollDeductionScheduleItem.[MemberName],
 PayrollDeductionScheduleItem.[AccountNo],
 PayrollDeductionScheduleItem.[Amount],
 PayrollDeductionScheduleItem.[PayrollCode],
 PayrollDeductionScheduleItem.[Narration],
 PayrollDeductionScheduleItem.[PayrollDate],
 PayrollDeductionScheduleItem.[AccountDueDate],
 PayrollDeductionScheduleItem.[CurrentStatus],
 PayrollDeductionScheduleItem.[DeductionType],
 PayrollDeductionScheduleItem.[PayrollDeductionScheduleId1],
 PayrollDeductionScheduleItem.[Description],
 PayrollDeductionScheduleItem.[IsActive],
 PayrollDeductionScheduleItem.[CreatedByUserId],
 PayrollDeductionScheduleItem.[DateCreated],
 PayrollDeductionScheduleItem.[UpdatedByUserId],
 PayrollDeductionScheduleItem.[DateUpdated],
 PayrollDeductionScheduleItem.[DeletedByUserId],
 PayrollDeductionScheduleItem.[IsDeleted],
 PayrollDeductionScheduleItem.[DateDeleted],
 PayrollDeductionScheduleItem.[RowVersion],
 PayrollDeductionScheduleItem.[FullText],
 PayrollDeductionScheduleItem.[Tags],
 PayrollDeductionScheduleItem.[Caption],
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
PayrollDeductionScheduleId.[Caption] as PayrollDeductionScheduleId_Caption,
PayrollDeductionScheduleId1.[ScheduleName] as PayrollDeductionScheduleId1_ScheduleName,
PayrollDeductionScheduleId1.[PayrollDeductionAccounId] as PayrollDeductionScheduleId1_PayrollDeductionAccounId,
PayrollDeductionScheduleId1.[PayrollSuspenseAccountId] as PayrollDeductionScheduleId1_PayrollSuspenseAccountId,
PayrollDeductionScheduleId1.[BankAccountId] as PayrollDeductionScheduleId1_BankAccountId,
PayrollDeductionScheduleId1.[TotalDeductions] as PayrollDeductionScheduleId1_TotalDeductions,
PayrollDeductionScheduleId1.[MinDecimalPlace] as PayrollDeductionScheduleId1_MinDecimalPlace,
PayrollDeductionScheduleId1.[MaxDecimalPlace] as PayrollDeductionScheduleId1_MaxDecimalPlace,
PayrollDeductionScheduleId1.[AdviseDate] as PayrollDeductionScheduleId1_AdviseDate,
PayrollDeductionScheduleId1.[ExpectedDate] as PayrollDeductionScheduleId1_ExpectedDate,
PayrollDeductionScheduleId1.[IsPosted] as PayrollDeductionScheduleId1_IsPosted,
PayrollDeductionScheduleId1.[PayrollDate] as PayrollDeductionScheduleId1_PayrollDate,
PayrollDeductionScheduleId1.[IsUploaded] as PayrollDeductionScheduleId1_IsUploaded,
PayrollDeductionScheduleId1.[LastUploadedDate] as PayrollDeductionScheduleId1_LastUploadedDate,
PayrollDeductionScheduleId1.[IsProcessed] as PayrollDeductionScheduleId1_IsProcessed,
PayrollDeductionScheduleId1.[ProcessedDate] as PayrollDeductionScheduleId1_ProcessedDate,
PayrollDeductionScheduleId1.[GenerateDeductionCronJobStatus] as PayrollDeductionScheduleId1_GenerateDeductionCronJobStatus,
PayrollDeductionScheduleId1.[GenerateDeductionCronJobStartedDate] as PayrollDeductionScheduleId1_GenerateDeductionCronJobStartedDate,
PayrollDeductionScheduleId1.[GenerateDeductionCronJobCompletedDate] as PayrollDeductionScheduleId1_GenerateDeductionCronJobCompletedDate,
PayrollDeductionScheduleId1.[ProcessDeductionCronJobStatus] as PayrollDeductionScheduleId1_ProcessDeductionCronJobStatus,
PayrollDeductionScheduleId1.[ProcessDeductionCronJobStartedDate] as PayrollDeductionScheduleId1_ProcessDeductionCronJobStartedDate,
PayrollDeductionScheduleId1.[ProcessDeductionCronJobCompletedDate] as PayrollDeductionScheduleId1_ProcessDeductionCronJobCompletedDate,
PayrollDeductionScheduleId1.[IsActive] as PayrollDeductionScheduleId1_IsActive,
PayrollDeductionScheduleId1.[CreatedByUserId] as PayrollDeductionScheduleId1_CreatedByUserId,
PayrollDeductionScheduleId1.[UpdatedByUserId] as PayrollDeductionScheduleId1_UpdatedByUserId,
PayrollDeductionScheduleId1.[DeletedByUserId] as PayrollDeductionScheduleId1_DeletedByUserId,
PayrollDeductionScheduleId1.[IsDeleted] as PayrollDeductionScheduleId1_IsDeleted,
PayrollDeductionScheduleId1.[Tags] as PayrollDeductionScheduleId1_Tags,
PayrollDeductionScheduleId1.[Caption] as PayrollDeductionScheduleId1_Caption

 from Payroll.[PayrollDeductionScheduleItem] PayrollDeductionScheduleItem

 left join Payroll.PayrollDeductionSchedule PayrollDeductionScheduleId on PayrollDeductionScheduleId.Id=PayrollDeductionScheduleItem.PayrollDeductionScheduleId
 left join Payroll.PayrollDeductionSchedule PayrollDeductionScheduleId1 on PayrollDeductionScheduleId1.Id=PayrollDeductionScheduleItem.PayrollDeductionScheduleId1
GO
/****** Object:  View [Payroll].[PayrollDeductionScheduleMasterView]    Script Date: 7/6/2023 6:07:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [Payroll].[PayrollDeductionScheduleMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY PayrollDeductionSchedule.[Id]) AS RowNumber,
PayrollDeductionSchedule.[Id],
 PayrollDeductionSchedule.[ScheduleName],
 PayrollDeductionSchedule.[PayrollDeductionAccounId],
 PayrollDeductionSchedule.[PayrollSuspenseAccountId],
 PayrollDeductionSchedule.[BankAccountId],
 PayrollDeductionSchedule.[TotalDeductions],
 PayrollDeductionSchedule.[MinDecimalPlace],
 PayrollDeductionSchedule.[MaxDecimalPlace],
 PayrollDeductionSchedule.[AdviseDate],
 PayrollDeductionSchedule.[ExpectedDate],
 PayrollDeductionSchedule.[IsPosted],
 PayrollDeductionSchedule.[PayrollDate],
 PayrollDeductionSchedule.[IsUploaded],
 PayrollDeductionSchedule.[LastUploadedDate],
 PayrollDeductionSchedule.[IsProcessed],
 PayrollDeductionSchedule.[ProcessedDate],
 PayrollDeductionSchedule.[GenerateDeductionCronJobStatus],
 PayrollDeductionSchedule.[GenerateDeductionCronJobStartedDate],
 PayrollDeductionSchedule.[GenerateDeductionCronJobCompletedDate],
 PayrollDeductionSchedule.[ProcessDeductionCronJobStatus],
 PayrollDeductionSchedule.[ProcessDeductionCronJobStartedDate],
 PayrollDeductionSchedule.[ProcessDeductionCronJobCompletedDate],
 PayrollDeductionSchedule.[Description],
 PayrollDeductionSchedule.[IsActive],
 PayrollDeductionSchedule.[CreatedByUserId],
 PayrollDeductionSchedule.[DateCreated],
 PayrollDeductionSchedule.[UpdatedByUserId],
 PayrollDeductionSchedule.[DateUpdated],
 PayrollDeductionSchedule.[DeletedByUserId],
 PayrollDeductionSchedule.[IsDeleted],
 PayrollDeductionSchedule.[DateDeleted],
 PayrollDeductionSchedule.[RowVersion],
 PayrollDeductionSchedule.[FullText],
 PayrollDeductionSchedule.[Tags],
 PayrollDeductionSchedule.[Caption],
PayrollDeductionAccounId.[AccountType] as PayrollDeductionAccounId_AccountType,
PayrollDeductionAccounId.[UOM] as PayrollDeductionAccounId_UOM,
PayrollDeductionAccounId.[CurrencyId] as PayrollDeductionAccounId_CurrencyId,
PayrollDeductionAccounId.[Code] as PayrollDeductionAccounId_Code,
PayrollDeductionAccounId.[Name] as PayrollDeductionAccounId_Name,
PayrollDeductionAccounId.[ParentId] as PayrollDeductionAccounId_ParentId,
PayrollDeductionAccounId.[ClearedBalance] as PayrollDeductionAccounId_ClearedBalance,
PayrollDeductionAccounId.[UnclearedBalance] as PayrollDeductionAccounId_UnclearedBalance,
PayrollDeductionAccounId.[LedgerBalance] as PayrollDeductionAccounId_LedgerBalance,
PayrollDeductionAccounId.[AvailableBalance] as PayrollDeductionAccounId_AvailableBalance,
PayrollDeductionAccounId.[IsOfficeAccount] as PayrollDeductionAccounId_IsOfficeAccount,
PayrollDeductionAccounId.[AllowManualEntry] as PayrollDeductionAccounId_AllowManualEntry,
PayrollDeductionAccounId.[IsClosed] as PayrollDeductionAccounId_IsClosed,
PayrollDeductionAccounId.[DateClosed] as PayrollDeductionAccounId_DateClosed,
PayrollDeductionAccounId.[ClosedByUserName] as PayrollDeductionAccounId_ClosedByUserName,
PayrollDeductionAccounId.[IsActive] as PayrollDeductionAccounId_IsActive,
PayrollDeductionAccounId.[CreatedByUserId] as PayrollDeductionAccounId_CreatedByUserId,
PayrollDeductionAccounId.[UpdatedByUserId] as PayrollDeductionAccounId_UpdatedByUserId,
PayrollDeductionAccounId.[DeletedByUserId] as PayrollDeductionAccounId_DeletedByUserId,
PayrollDeductionAccounId.[IsDeleted] as PayrollDeductionAccounId_IsDeleted,
PayrollDeductionAccounId.[Tags] as PayrollDeductionAccounId_Tags,
PayrollDeductionAccounId.[Caption] as PayrollDeductionAccounId_Caption,
PayrollSuspenseAccountId.[AccountType] as PayrollSuspenseAccountId_AccountType,
PayrollSuspenseAccountId.[UOM] as PayrollSuspenseAccountId_UOM,
PayrollSuspenseAccountId.[CurrencyId] as PayrollSuspenseAccountId_CurrencyId,
PayrollSuspenseAccountId.[Code] as PayrollSuspenseAccountId_Code,
PayrollSuspenseAccountId.[Name] as PayrollSuspenseAccountId_Name,
PayrollSuspenseAccountId.[ParentId] as PayrollSuspenseAccountId_ParentId,
PayrollSuspenseAccountId.[ClearedBalance] as PayrollSuspenseAccountId_ClearedBalance,
PayrollSuspenseAccountId.[UnclearedBalance] as PayrollSuspenseAccountId_UnclearedBalance,
PayrollSuspenseAccountId.[LedgerBalance] as PayrollSuspenseAccountId_LedgerBalance,
PayrollSuspenseAccountId.[AvailableBalance] as PayrollSuspenseAccountId_AvailableBalance,
PayrollSuspenseAccountId.[IsOfficeAccount] as PayrollSuspenseAccountId_IsOfficeAccount,
PayrollSuspenseAccountId.[AllowManualEntry] as PayrollSuspenseAccountId_AllowManualEntry,
PayrollSuspenseAccountId.[IsClosed] as PayrollSuspenseAccountId_IsClosed,
PayrollSuspenseAccountId.[DateClosed] as PayrollSuspenseAccountId_DateClosed,
PayrollSuspenseAccountId.[ClosedByUserName] as PayrollSuspenseAccountId_ClosedByUserName,
PayrollSuspenseAccountId.[IsActive] as PayrollSuspenseAccountId_IsActive,
PayrollSuspenseAccountId.[CreatedByUserId] as PayrollSuspenseAccountId_CreatedByUserId,
PayrollSuspenseAccountId.[UpdatedByUserId] as PayrollSuspenseAccountId_UpdatedByUserId,
PayrollSuspenseAccountId.[DeletedByUserId] as PayrollSuspenseAccountId_DeletedByUserId,
PayrollSuspenseAccountId.[IsDeleted] as PayrollSuspenseAccountId_IsDeleted,
PayrollSuspenseAccountId.[Tags] as PayrollSuspenseAccountId_Tags,
PayrollSuspenseAccountId.[Caption] as PayrollSuspenseAccountId_Caption,
BankAccountId.[LedgerAccountId] as BankAccountId_LedgerAccountId,
BankAccountId.[BankId] as BankAccountId_BankId,
BankAccountId.[BranchName] as BankAccountId_BranchName,
BankAccountId.[BranchAddress] as BankAccountId_BranchAddress,
BankAccountId.[CurrencyId] as BankAccountId_CurrencyId,
BankAccountId.[AccountName] as BankAccountId_AccountName,
BankAccountId.[AccountNumber] as BankAccountId_AccountNumber,
BankAccountId.[BVN] as BankAccountId_BVN,
BankAccountId.[IsActive] as BankAccountId_IsActive,
BankAccountId.[CreatedByUserId] as BankAccountId_CreatedByUserId,
BankAccountId.[UpdatedByUserId] as BankAccountId_UpdatedByUserId,
BankAccountId.[DeletedByUserId] as BankAccountId_DeletedByUserId,
BankAccountId.[IsDeleted] as BankAccountId_IsDeleted,
BankAccountId.[Tags] as BankAccountId_Tags,
BankAccountId.[Caption] as BankAccountId_Caption

 from Payroll.[PayrollDeductionSchedule] PayrollDeductionSchedule

 left join Accounting.LedgerAccount PayrollDeductionAccounId on PayrollDeductionAccounId.Id=PayrollDeductionSchedule.PayrollDeductionAccounId
 left join Accounting.LedgerAccount PayrollSuspenseAccountId on PayrollSuspenseAccountId.Id=PayrollDeductionSchedule.PayrollSuspenseAccountId
 left join Accounting.CompanyBankAccount BankAccountId on BankAccountId.Id=PayrollDeductionSchedule.BankAccountId
GO
