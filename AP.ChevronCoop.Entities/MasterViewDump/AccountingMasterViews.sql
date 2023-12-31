/****** Object:  View [Accounting].[AccountingPeriodMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.AccountingPeriodMasterView source

CREATE   VIEW [Accounting].[AccountingPeriodMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY AccountingPeriod.[Id]) AS RowNumber,
AccountingPeriod.[Id],
 AccountingPeriod.[CalendarId],
 AccountingPeriod.[Name],
 AccountingPeriod.[StartDate],
 AccountingPeriod.[EndDate],
 AccountingPeriod.[IsCurrent],
 AccountingPeriod.[IsClosed],
 AccountingPeriod.[ClosedByUserName],
 AccountingPeriod.[DateClosed],
 AccountingPeriod.[FinancialCalendarId],
 AccountingPeriod.[Description],
 AccountingPeriod.[IsActive],
 AccountingPeriod.[CreatedByUserId],
 AccountingPeriod.[DateCreated],
 AccountingPeriod.[UpdatedByUserId],
 AccountingPeriod.[DateUpdated],
 AccountingPeriod.[DeletedByUserId],
 AccountingPeriod.[IsDeleted],
 AccountingPeriod.[DateDeleted],
 AccountingPeriod.[RowVersion],
 AccountingPeriod.[FullText],
 AccountingPeriod.[Tags],
 AccountingPeriod.[Caption],
CalendarId.[Code] as CalendarId_Code,
CalendarId.[Name] as CalendarId_Name,
CalendarId.[StartDate] as CalendarId_StartDate,
CalendarId.[EndDate] as CalendarId_EndDate,
CalendarId.[IsCurrent] as CalendarId_IsCurrent,
CalendarId.[IsClosed] as CalendarId_IsClosed,
CalendarId.[ClosedByUserName] as CalendarId_ClosedByUserName,
CalendarId.[DateClosed] as CalendarId_DateClosed,
CalendarId.[IsActive] as CalendarId_IsActive,
CalendarId.[CreatedByUserId] as CalendarId_CreatedByUserId,
CalendarId.[UpdatedByUserId] as CalendarId_UpdatedByUserId,
CalendarId.[DeletedByUserId] as CalendarId_DeletedByUserId,
CalendarId.[IsDeleted] as CalendarId_IsDeleted,
CalendarId.[Tags] as CalendarId_Tags,
CalendarId.[Caption] as CalendarId_Caption,
FinancialCalendarId.[Code] as FinancialCalendarId_Code,
FinancialCalendarId.[Name] as FinancialCalendarId_Name,
FinancialCalendarId.[StartDate] as FinancialCalendarId_StartDate,
FinancialCalendarId.[EndDate] as FinancialCalendarId_EndDate,
FinancialCalendarId.[IsCurrent] as FinancialCalendarId_IsCurrent,
FinancialCalendarId.[IsClosed] as FinancialCalendarId_IsClosed,
FinancialCalendarId.[ClosedByUserName] as FinancialCalendarId_ClosedByUserName,
FinancialCalendarId.[DateClosed] as FinancialCalendarId_DateClosed,
FinancialCalendarId.[IsActive] as FinancialCalendarId_IsActive,
FinancialCalendarId.[CreatedByUserId] as FinancialCalendarId_CreatedByUserId,
FinancialCalendarId.[UpdatedByUserId] as FinancialCalendarId_UpdatedByUserId,
FinancialCalendarId.[DeletedByUserId] as FinancialCalendarId_DeletedByUserId,
FinancialCalendarId.[IsDeleted] as FinancialCalendarId_IsDeleted,
FinancialCalendarId.[Tags] as FinancialCalendarId_Tags,
FinancialCalendarId.[Caption] as FinancialCalendarId_Caption

 from Accounting.[AccountingPeriod] AccountingPeriod

 left join Accounting.FinancialCalendar CalendarId on CalendarId.Id=AccountingPeriod.CalendarId
 left join Accounting.FinancialCalendar FinancialCalendarId on FinancialCalendarId.Id=AccountingPeriod.FinancialCalendarId
GO
/****** Object:  View [Accounting].[CompanyBankAccountMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.CompanyBankAccountMasterView source

CREATE   VIEW [Accounting].[CompanyBankAccountMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY CompanyBankAccount.[Id]) AS RowNumber,
CompanyBankAccount.[Id],
 CompanyBankAccount.[LedgerAccountId],
 CompanyBankAccount.[BankId],
 CompanyBankAccount.[BranchName],
 CompanyBankAccount.[BranchAddress],
 CompanyBankAccount.[CurrencyId],
 CompanyBankAccount.[AccountName],
 CompanyBankAccount.[AccountNumber],
 CompanyBankAccount.[BVN],
 CompanyBankAccount.[Description],
 CompanyBankAccount.[IsActive],
 CompanyBankAccount.[CreatedByUserId],
 CompanyBankAccount.[DateCreated],
 CompanyBankAccount.[UpdatedByUserId],
 CompanyBankAccount.[DateUpdated],
 CompanyBankAccount.[DeletedByUserId],
 CompanyBankAccount.[IsDeleted],
 CompanyBankAccount.[DateDeleted],
 CompanyBankAccount.[RowVersion],
 CompanyBankAccount.[FullText],
 CompanyBankAccount.[Tags],
 CompanyBankAccount.[Caption],
LedgerAccountId.[AccountType] as LedgerAccountId_AccountType,
LedgerAccountId.[UOM] as LedgerAccountId_UOM,
LedgerAccountId.[CurrencyId] as LedgerAccountId_CurrencyId,
LedgerAccountId.[Code] as LedgerAccountId_Code,
LedgerAccountId.[Name] as LedgerAccountId_Name,
LedgerAccountId.[ParentId] as LedgerAccountId_ParentId,
LedgerAccountId.[ClearedBalance] as LedgerAccountId_ClearedBalance,
LedgerAccountId.[UnclearedBalance] as LedgerAccountId_UnclearedBalance,
LedgerAccountId.[LedgerBalance] as LedgerAccountId_LedgerBalance,
LedgerAccountId.[AvailableBalance] as LedgerAccountId_AvailableBalance,
LedgerAccountId.[IsOfficeAccount] as LedgerAccountId_IsOfficeAccount,
LedgerAccountId.[AllowManualEntry] as LedgerAccountId_AllowManualEntry,
LedgerAccountId.[IsClosed] as LedgerAccountId_IsClosed,
LedgerAccountId.[DateClosed] as LedgerAccountId_DateClosed,
LedgerAccountId.[ClosedByUserName] as LedgerAccountId_ClosedByUserName,
LedgerAccountId.[IsActive] as LedgerAccountId_IsActive,
LedgerAccountId.[CreatedByUserId] as LedgerAccountId_CreatedByUserId,
LedgerAccountId.[UpdatedByUserId] as LedgerAccountId_UpdatedByUserId,
LedgerAccountId.[DeletedByUserId] as LedgerAccountId_DeletedByUserId,
LedgerAccountId.[IsDeleted] as LedgerAccountId_IsDeleted,
LedgerAccountId.[Tags] as LedgerAccountId_Tags,
LedgerAccountId.[Caption] as LedgerAccountId_Caption,
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
BankId.[Caption] as BankId_Caption,
CurrencyId.[Code] as CurrencyId_Code,
CurrencyId.[Name] as CurrencyId_Name,
CurrencyId.[Symbol] as CurrencyId_Symbol,
CurrencyId.[IsoSymbol] as CurrencyId_IsoSymbol,
CurrencyId.[DecimalPlaces] as CurrencyId_DecimalPlaces,
CurrencyId.[Format] as CurrencyId_Format,
CurrencyId.[IsActive] as CurrencyId_IsActive,
CurrencyId.[CreatedByUserId] as CurrencyId_CreatedByUserId,
CurrencyId.[UpdatedByUserId] as CurrencyId_UpdatedByUserId,
CurrencyId.[DeletedByUserId] as CurrencyId_DeletedByUserId,
CurrencyId.[IsDeleted] as CurrencyId_IsDeleted,
CurrencyId.[Tags] as CurrencyId_Tags,
CurrencyId.[Caption] as CurrencyId_Caption

 from Accounting.[CompanyBankAccount] CompanyBankAccount

 left join Accounting.LedgerAccount LedgerAccountId on LedgerAccountId.Id=CompanyBankAccount.LedgerAccountId
 left join MasterData.Bank BankId on BankId.Id=CompanyBankAccount.BankId
 left join MasterData.Currency CurrencyId on CurrencyId.Id=CompanyBankAccount.CurrencyId
GO
/****** Object:  View [Accounting].[FinancialCalendarMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.FinancialCalendarMasterView source

CREATE   VIEW [Accounting].[FinancialCalendarMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY FinancialCalendar.[Id]) AS RowNumber,
FinancialCalendar.[Id],
 FinancialCalendar.[Code],
 FinancialCalendar.[Name],
 FinancialCalendar.[StartDate],
 FinancialCalendar.[EndDate],
 FinancialCalendar.[IsCurrent],
 FinancialCalendar.[IsClosed],
 FinancialCalendar.[ClosedByUserName],
 FinancialCalendar.[DateClosed],
 FinancialCalendar.[Description],
 FinancialCalendar.[IsActive],
 FinancialCalendar.[CreatedByUserId],
 FinancialCalendar.[DateCreated],
 FinancialCalendar.[UpdatedByUserId],
 FinancialCalendar.[DateUpdated],
 FinancialCalendar.[DeletedByUserId],
 FinancialCalendar.[IsDeleted],
 FinancialCalendar.[DateDeleted],
 FinancialCalendar.[RowVersion],
 FinancialCalendar.[FullText],
 FinancialCalendar.[Tags],
 FinancialCalendar.[Caption]

 from Accounting.[FinancialCalendar] FinancialCalendar
GO
/****** Object:  View [Accounting].[JournalEntryMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.JournalEntryMasterView source

CREATE   VIEW [Accounting].[JournalEntryMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY JournalEntry.[Id]) AS RowNumber,
JournalEntry.[Id],
 JournalEntry.[TransactionEntryNo],
 JournalEntry.[TransactionJournalId],
 JournalEntry.[AccountId],
 JournalEntry.[EntryType],
 JournalEntry.[DecimalPlaces],
 JournalEntry.[Debit],
 JournalEntry.[Credit],
 JournalEntry.[TransactionDate],
 JournalEntry.[IsPosted],
 JournalEntry.[PostedByUserName],
 JournalEntry.[DatePosted],
 JournalEntry.[IsReversed],
 JournalEntry.[ReversedByUserName],
 JournalEntry.[ReversalDate],
 JournalEntry.[Memo],
 JournalEntry.[Description],
 JournalEntry.[IsActive],
 JournalEntry.[CreatedByUserId],
 JournalEntry.[DateCreated],
 JournalEntry.[UpdatedByUserId],
 JournalEntry.[DateUpdated],
 JournalEntry.[DeletedByUserId],
 JournalEntry.[IsDeleted],
 JournalEntry.[DateDeleted],
 JournalEntry.[RowVersion],
 JournalEntry.[FullText],
 JournalEntry.[Tags],
 JournalEntry.[Caption],
TransactionJournalId.[TransactionNo] as TransactionJournalId_TransactionNo,
TransactionJournalId.[Title] as TransactionJournalId_Title,
TransactionJournalId.[DocumentRef] as TransactionJournalId_DocumentRef,
TransactionJournalId.[DocumentRefId] as TransactionJournalId_DocumentRefId,
TransactionJournalId.[PostingRef] as TransactionJournalId_PostingRef,
TransactionJournalId.[PostingRefId] as TransactionJournalId_PostingRefId,
TransactionJournalId.[EntityRef] as TransactionJournalId_EntityRef,
TransactionJournalId.[EntityRefId] as TransactionJournalId_EntityRefId,
TransactionJournalId.[TransactionDate] as TransactionJournalId_TransactionDate,
TransactionJournalId.[IsPosted] as TransactionJournalId_IsPosted,
TransactionJournalId.[PostedByUserName] as TransactionJournalId_PostedByUserName,
TransactionJournalId.[DatePosted] as TransactionJournalId_DatePosted,
TransactionJournalId.[IsReversed] as TransactionJournalId_IsReversed,
TransactionJournalId.[ReversedByUserName] as TransactionJournalId_ReversedByUserName,
TransactionJournalId.[ReversalDate] as TransactionJournalId_ReversalDate,
TransactionJournalId.[Memo] as TransactionJournalId_Memo,
TransactionJournalId.[IsActive] as TransactionJournalId_IsActive,
TransactionJournalId.[CreatedByUserId] as TransactionJournalId_CreatedByUserId,
TransactionJournalId.[UpdatedByUserId] as TransactionJournalId_UpdatedByUserId,
TransactionJournalId.[DeletedByUserId] as TransactionJournalId_DeletedByUserId,
TransactionJournalId.[IsDeleted] as TransactionJournalId_IsDeleted,
TransactionJournalId.[Tags] as TransactionJournalId_Tags,
TransactionJournalId.[Caption] as TransactionJournalId_Caption,
AccountId.[AccountType] as AccountId_AccountType,
AccountId.[UOM] as AccountId_UOM,
AccountId.[CurrencyId] as AccountId_CurrencyId,
AccountId.[Code] as AccountId_Code,
AccountId.[Name] as AccountId_Name,
AccountId.[ParentId] as AccountId_ParentId,
AccountId.[ClearedBalance] as AccountId_ClearedBalance,
AccountId.[UnclearedBalance] as AccountId_UnclearedBalance,
AccountId.[LedgerBalance] as AccountId_LedgerBalance,
AccountId.[AvailableBalance] as AccountId_AvailableBalance,
AccountId.[IsOfficeAccount] as AccountId_IsOfficeAccount,
AccountId.[AllowManualEntry] as AccountId_AllowManualEntry,
AccountId.[IsClosed] as AccountId_IsClosed,
AccountId.[DateClosed] as AccountId_DateClosed,
AccountId.[ClosedByUserName] as AccountId_ClosedByUserName,
AccountId.[IsActive] as AccountId_IsActive,
AccountId.[CreatedByUserId] as AccountId_CreatedByUserId,
AccountId.[UpdatedByUserId] as AccountId_UpdatedByUserId,
AccountId.[DeletedByUserId] as AccountId_DeletedByUserId,
AccountId.[IsDeleted] as AccountId_IsDeleted,
AccountId.[Tags] as AccountId_Tags,
AccountId.[Caption] as AccountId_Caption

 from Accounting.[JournalEntry] JournalEntry

 left join Accounting.TransactionJournal TransactionJournalId on TransactionJournalId.Id=JournalEntry.TransactionJournalId
 left join Accounting.LedgerAccount AccountId on AccountId.Id=JournalEntry.AccountId
GO
/****** Object:  View [Accounting].[LedgerAccountMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.LedgerAccountMasterView source

CREATE   VIEW [Accounting].[LedgerAccountMasterView]
  as


  with children(Id,ChildCount)
as (
SELECT    p.Id, Count(c.ParentID) as ChildCount
FROM      Accounting.LedgerAccount  p
LEFT JOIN Accounting.LedgerAccount  c ON p.Id = c.ParentId
GROUP BY  p.Id  )


select
 ROW_NUMBER() OVER (ORDER BY LedgerAccount.[Id]) AS RowNumber,

  LedgerAccount.[Id],
 LedgerAccount.[AccountType],
 LedgerAccount.[UOM],
 LedgerAccount.[CurrencyId],
 LedgerAccount.[Code],
 LedgerAccount.[Name],
 LedgerAccount.[ParentId],
 LedgerAccount.[ClearedBalance],
 LedgerAccount.[UnclearedBalance],
 LedgerAccount.[LedgerBalance],
 LedgerAccount.[AvailableBalance],
 LedgerAccount.[IsOfficeAccount],
 LedgerAccount.[AllowManualEntry],
 LedgerAccount.[IsClosed],
 LedgerAccount.[DateClosed],
 LedgerAccount.[ClosedByUserName],
 LedgerAccount.[Description],
 LedgerAccount.[IsActive],
 LedgerAccount.[CreatedByUserId],
 LedgerAccount.[DateCreated],
 LedgerAccount.[UpdatedByUserId],
 LedgerAccount.[DateUpdated],
 LedgerAccount.[DeletedByUserId],
 LedgerAccount.[IsDeleted],
 LedgerAccount.[DateDeleted],
 LedgerAccount.[RowVersion],
 LedgerAccount.[FullText],
 LedgerAccount.[Tags],
 LedgerAccount.[Caption],
CurrencyId.[Code] as CurrencyId_Code,
CurrencyId.[Name] as CurrencyId_Name,
CurrencyId.[Symbol] as CurrencyId_Symbol,
CurrencyId.[IsoSymbol] as CurrencyId_IsoSymbol,
CurrencyId.[DecimalPlaces] as CurrencyId_DecimalPlaces,
CurrencyId.[Format] as CurrencyId_Format,
CurrencyId.[IsActive] as CurrencyId_IsActive,
CurrencyId.[CreatedByUserId] as CurrencyId_CreatedByUserId,
CurrencyId.[UpdatedByUserId] as CurrencyId_UpdatedByUserId,
CurrencyId.[DeletedByUserId] as CurrencyId_DeletedByUserId,
CurrencyId.[IsDeleted] as CurrencyId_IsDeleted,
CurrencyId.[Tags] as CurrencyId_Tags,
CurrencyId.[Caption] as CurrencyId_Caption,
Header.[AccountType] as Header_AccountType,
Header.[UOM] as Header_UOM,
Header.[CurrencyId] as Header_CurrencyId,
Header.[Code] as Header_Code,
Header.[Name] as Header_Name,
Header.[ParentId] as Header_ParentId,
Header.[ClearedBalance] as Header_ClearedBalance,
Header.[UnclearedBalance] as Header_UnclearedBalance,
Header.[LedgerBalance] as Header_LedgerBalance,
Header.[AvailableBalance] as Header_AvailableBalance,
Header.[IsOfficeAccount] as Header_IsOfficeAccount,
Header.[AllowManualEntry] as Header_AllowManualEntry,
Header.[IsClosed] as Header_IsClosed,
Header.[DateClosed] as Header_DateClosed,
Header.[ClosedByUserName] as Header_ClosedByUserName,
Header.[IsActive] as Header_IsActive,
Header.[CreatedByUserId] as Header_CreatedByUserId,
Header.[UpdatedByUserId] as Header_UpdatedByUserId,
Header.[DeletedByUserId] as Header_DeletedByUserId,
Header.[IsDeleted] as Header_IsDeleted,
Header.[Tags] as Header_Tags,
Header.[Caption] as Header_Caption

,
subs.ChildCount,
 case
 when subs.ChildCount > 0 then cast(1 as bit)
        else cast(0 as bit)
        end as HasChildren

 from Accounting.[LedgerAccount] LedgerAccount
 left join children subs on LedgerAccount.Id=subs.Id
 left join MasterData.Currency CurrencyId on CurrencyId.Id=LedgerAccount.CurrencyId
 left join Accounting.LedgerAccount Header on Header.Id=LedgerAccount.ParentId
GO
/****** Object:  View [Accounting].[LienTypeMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.LienTypeMasterView source

CREATE   VIEW [Accounting].[LienTypeMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY LienType.[Id]) AS RowNumber,
LienType.[Id],
 LienType.[Code],
 LienType.[Name],
 LienType.[Description],
 LienType.[IsActive],
 LienType.[CreatedByUserId],
 LienType.[DateCreated],
 LienType.[UpdatedByUserId],
 LienType.[DateUpdated],
 LienType.[DeletedByUserId],
 LienType.[IsDeleted],
 LienType.[DateDeleted],
 LienType.[RowVersion],
 LienType.[FullText],
 LienType.[Tags],
 LienType.[Caption]

 from Accounting.[LienType] LienType
GO
/****** Object:  View [Accounting].[PaymentModeMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.PaymentModeMasterView source

CREATE   VIEW [Accounting].[PaymentModeMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY PaymentMode.[Id]) AS RowNumber,
PaymentMode.[Id],
 PaymentMode.[Name],
 PaymentMode.[Channel],
 PaymentMode.[Description],
 PaymentMode.[IsActive],
 PaymentMode.[CreatedByUserId],
 PaymentMode.[DateCreated],
 PaymentMode.[UpdatedByUserId],
 PaymentMode.[DateUpdated],
 PaymentMode.[DeletedByUserId],
 PaymentMode.[IsDeleted],
 PaymentMode.[DateDeleted],
 PaymentMode.[RowVersion],
 PaymentMode.[FullText],
 PaymentMode.[Tags],
 PaymentMode.[Caption]

 from Accounting.[PaymentMode] PaymentMode
GO
/****** Object:  View [Accounting].[TransactionDocumentMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.TransactionDocumentMasterView source

CREATE   VIEW [Accounting].[TransactionDocumentMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY TransactionDocument.[Id]) AS RowNumber,
TransactionDocument.[Id],
 TransactionDocument.[DocumentNo],
 TransactionDocument.[TransactionJournalId],
 TransactionDocument.[DocumentTypeId],
 TransactionDocument.[Name],
 TransactionDocument.[Document],
 TransactionDocument.[DocumentUrl],
 TransactionDocument.[TransactionJournalId1],
 TransactionDocument.[Description],
 TransactionDocument.[IsActive],
 TransactionDocument.[CreatedByUserId],
 TransactionDocument.[DateCreated],
 TransactionDocument.[UpdatedByUserId],
 TransactionDocument.[DateUpdated],
 TransactionDocument.[DeletedByUserId],
 TransactionDocument.[IsDeleted],
 TransactionDocument.[DateDeleted],
 TransactionDocument.[RowVersion],
 TransactionDocument.[FullText],
 TransactionDocument.[Tags],
 TransactionDocument.[Caption],
TransactionJournalId.[TransactionNo] as TransactionJournalId_TransactionNo,
TransactionJournalId.[Title] as TransactionJournalId_Title,
TransactionJournalId.[DocumentRef] as TransactionJournalId_DocumentRef,
TransactionJournalId.[DocumentRefId] as TransactionJournalId_DocumentRefId,
TransactionJournalId.[PostingRef] as TransactionJournalId_PostingRef,
TransactionJournalId.[PostingRefId] as TransactionJournalId_PostingRefId,
TransactionJournalId.[EntityRef] as TransactionJournalId_EntityRef,
TransactionJournalId.[EntityRefId] as TransactionJournalId_EntityRefId,
TransactionJournalId.[TransactionDate] as TransactionJournalId_TransactionDate,
TransactionJournalId.[IsPosted] as TransactionJournalId_IsPosted,
TransactionJournalId.[PostedByUserName] as TransactionJournalId_PostedByUserName,
TransactionJournalId.[DatePosted] as TransactionJournalId_DatePosted,
TransactionJournalId.[IsReversed] as TransactionJournalId_IsReversed,
TransactionJournalId.[ReversedByUserName] as TransactionJournalId_ReversedByUserName,
TransactionJournalId.[ReversalDate] as TransactionJournalId_ReversalDate,
TransactionJournalId.[Memo] as TransactionJournalId_Memo,
TransactionJournalId.[IsActive] as TransactionJournalId_IsActive,
TransactionJournalId.[CreatedByUserId] as TransactionJournalId_CreatedByUserId,
TransactionJournalId.[UpdatedByUserId] as TransactionJournalId_UpdatedByUserId,
TransactionJournalId.[DeletedByUserId] as TransactionJournalId_DeletedByUserId,
TransactionJournalId.[IsDeleted] as TransactionJournalId_IsDeleted,
TransactionJournalId.[Tags] as TransactionJournalId_Tags,
TransactionJournalId.[Caption] as TransactionJournalId_Caption,
DocumentTypeId.[Name] as DocumentTypeId_Name,
DocumentTypeId.[SystemFlag] as DocumentTypeId_SystemFlag,
DocumentTypeId.[IsActive] as DocumentTypeId_IsActive,
DocumentTypeId.[CreatedByUserId] as DocumentTypeId_CreatedByUserId,
DocumentTypeId.[UpdatedByUserId] as DocumentTypeId_UpdatedByUserId,
DocumentTypeId.[DeletedByUserId] as DocumentTypeId_DeletedByUserId,
DocumentTypeId.[IsDeleted] as DocumentTypeId_IsDeleted,
DocumentTypeId.[Tags] as DocumentTypeId_Tags,
DocumentTypeId.[Caption] as DocumentTypeId_Caption,
TransactionJournalId1.[TransactionNo] as TransactionJournalId1_TransactionNo,
TransactionJournalId1.[Title] as TransactionJournalId1_Title,
TransactionJournalId1.[DocumentRef] as TransactionJournalId1_DocumentRef,
TransactionJournalId1.[DocumentRefId] as TransactionJournalId1_DocumentRefId,
TransactionJournalId1.[PostingRef] as TransactionJournalId1_PostingRef,
TransactionJournalId1.[PostingRefId] as TransactionJournalId1_PostingRefId,
TransactionJournalId1.[EntityRef] as TransactionJournalId1_EntityRef,
TransactionJournalId1.[EntityRefId] as TransactionJournalId1_EntityRefId,
TransactionJournalId1.[TransactionDate] as TransactionJournalId1_TransactionDate,
TransactionJournalId1.[IsPosted] as TransactionJournalId1_IsPosted,
TransactionJournalId1.[PostedByUserName] as TransactionJournalId1_PostedByUserName,
TransactionJournalId1.[DatePosted] as TransactionJournalId1_DatePosted,
TransactionJournalId1.[IsReversed] as TransactionJournalId1_IsReversed,
TransactionJournalId1.[ReversedByUserName] as TransactionJournalId1_ReversedByUserName,
TransactionJournalId1.[ReversalDate] as TransactionJournalId1_ReversalDate,
TransactionJournalId1.[Memo] as TransactionJournalId1_Memo,
TransactionJournalId1.[IsActive] as TransactionJournalId1_IsActive,
TransactionJournalId1.[CreatedByUserId] as TransactionJournalId1_CreatedByUserId,
TransactionJournalId1.[UpdatedByUserId] as TransactionJournalId1_UpdatedByUserId,
TransactionJournalId1.[DeletedByUserId] as TransactionJournalId1_DeletedByUserId,
TransactionJournalId1.[IsDeleted] as TransactionJournalId1_IsDeleted,
TransactionJournalId1.[Tags] as TransactionJournalId1_Tags,
TransactionJournalId1.[Caption] as TransactionJournalId1_Caption

 from Accounting.[TransactionDocument] TransactionDocument

 left join Accounting.TransactionJournal TransactionJournalId on TransactionJournalId.Id=TransactionDocument.TransactionJournalId
 left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=TransactionDocument.DocumentTypeId
 left join Accounting.TransactionJournal TransactionJournalId1 on TransactionJournalId1.Id=TransactionDocument.TransactionJournalId1
GO
/****** Object:  View [Accounting].[TransactionJournalMasterView]    Script Date: 7/6/2023 5:45:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Accounting.TransactionJournalMasterView source

CREATE   VIEW [Accounting].[TransactionJournalMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY TransactionJournal.[Id]) AS RowNumber,
TransactionJournal.[Id],
 TransactionJournal.[TransactionNo],
 TransactionJournal.[Title],
 TransactionJournal.[DocumentRef],
 TransactionJournal.[DocumentRefId],
 TransactionJournal.[PostingRef],
 TransactionJournal.[PostingRefId],
 TransactionJournal.[EntityRef],
 TransactionJournal.[EntityRefId],
 TransactionJournal.[TransactionDate],
 TransactionJournal.[IsPosted],
 TransactionJournal.[PostedByUserName],
 TransactionJournal.[DatePosted],
 TransactionJournal.[IsReversed],
 TransactionJournal.[ReversedByUserName],
 TransactionJournal.[ReversalDate],
 TransactionJournal.[Memo],
 TransactionJournal.[Description],
 TransactionJournal.[IsActive],
 TransactionJournal.[CreatedByUserId],
 TransactionJournal.[DateCreated],
 TransactionJournal.[UpdatedByUserId],
 TransactionJournal.[DateUpdated],
 TransactionJournal.[DeletedByUserId],
 TransactionJournal.[IsDeleted],
 TransactionJournal.[DateDeleted],
 TransactionJournal.[RowVersion],
 TransactionJournal.[FullText],
 TransactionJournal.[Tags],
 TransactionJournal.[Caption]

 from Accounting.[TransactionJournal] TransactionJournal
GO
