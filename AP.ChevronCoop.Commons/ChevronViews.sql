-- Accounting.AccountingPeriodMasterView source

CREATE OR ALTER VIEW Accounting.AccountingPeriodMasterView
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

   from Accounting.AccountingPeriod AccountingPeriod

            left join Accounting.FinancialCalendar CalendarId on CalendarId.Id=AccountingPeriod.CalendarId
            left join Accounting.FinancialCalendar FinancialCalendarId on FinancialCalendarId.Id=AccountingPeriod.FinancialCalendarId;


-- Accounting.ChargeMasterView source

CREATE OR ALTER VIEW Accounting.ChargeMasterView
as select

       ROW_NUMBER() OVER (ORDER BY Charge.[Id]) AS RowNumber,
           Charge.[Id],
       Charge.[Code],
       Charge.[Name],
       Charge.[Method],
       Charge.[Target],
       Charge.[CalculationMethod],
       Charge.[CurrencyId],
       Charge.[ChargeValue],
       Charge.[MaximumCharge],
       Charge.[MinimimumCharge],
       Charge.[Description],
       Charge.[IsActive],
       Charge.[CreatedByUserId],
       Charge.[DateCreated],
       Charge.[UpdatedByUserId],
       Charge.[DateUpdated],
       Charge.[DeletedByUserId],
       Charge.[IsDeleted],
       Charge.[DateDeleted],
       Charge.[RowVersion],
       Charge.[FullText],
       Charge.[Tags],
       Charge.[Caption],
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

   from Accounting.Charge Charge

            left join MasterData.Currency CurrencyId on CurrencyId.Id=Charge.CurrencyId;


-- Accounting.CompanyBankAccountMasterView source

CREATE OR ALTER VIEW Accounting.CompanyBankAccountMasterView
as select

       ROW_NUMBER() OVER (ORDER BY CompanyBankAccount.[Id]) AS RowNumber,
           CompanyBankAccount.[Id],
       CompanyBankAccount.[LedgerAccountId],
       CompanyBankAccount.[BankId],
       CompanyBankAccount.[BranchName],
       CompanyBankAccount.[BranchAddress],
       CompanyBankAccount.[CurrencyId],
       CompanyBankAccount.[AccountName],
       CompanyBankAccount.[AccountNo],
       CompanyBankAccount.[AccountBVN],
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
       BankId.[Code] as BankId_Code,
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
       CurrencyId.[Caption] as CurrencyId_Caption,
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
       LedgerAccountId.[Caption] as LedgerAccountId_Caption

   from Accounting.CompanyBankAccount CompanyBankAccount

            left join MasterData.Bank BankId on BankId.Id=CompanyBankAccount.BankId
            left join MasterData.Currency CurrencyId on CurrencyId.Id=CompanyBankAccount.CurrencyId
            left join Accounting.LedgerAccount LedgerAccountId on LedgerAccountId.Id=CompanyBankAccount.LedgerAccountId;


-- Accounting.FinancialCalendarMasterView source

CREATE OR ALTER VIEW Accounting.FinancialCalendarMasterView
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

   from Accounting.FinancialCalendar FinancialCalendar;


-- Accounting.JournalEntryMasterView source

CREATE OR ALTER VIEW Accounting.JournalEntryMasterView
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

   from Accounting.JournalEntry JournalEntry

            left join Accounting.TransactionJournal TransactionJournalId on TransactionJournalId.Id=JournalEntry.TransactionJournalId
            left join Accounting.LedgerAccount AccountId on AccountId.Id=JournalEntry.AccountId;


-- Accounting.LedgerAccountMasterView source

CREATE OR ALTER VIEW Accounting.LedgerAccountMasterView
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

from Accounting.LedgerAccount LedgerAccount
         left join children subs on LedgerAccount.Id=subs.Id
         left join MasterData.Currency CurrencyId on CurrencyId.Id=LedgerAccount.CurrencyId
         left join Accounting.LedgerAccount Header on Header.Id=LedgerAccount.ParentId;


-- Accounting.LienTypeMasterView source

CREATE OR ALTER VIEW Accounting.LienTypeMasterView
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

   from Accounting.LienType LienType;


-- Accounting.PaymentModeMasterView source

CREATE OR ALTER VIEW Accounting.PaymentModeMasterView
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

   from Accounting.PaymentMode PaymentMode;


-- Accounting.TransactionDocumentMasterView source

CREATE OR ALTER VIEW Accounting.TransactionDocumentMasterView
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
       DocumentTypeId.[Name] as DocumentTypeId_Name,
       DocumentTypeId.[SystemFlag] as DocumentTypeId_SystemFlag,
       DocumentTypeId.[IsActive] as DocumentTypeId_IsActive,
       DocumentTypeId.[CreatedByUserId] as DocumentTypeId_CreatedByUserId,
       DocumentTypeId.[UpdatedByUserId] as DocumentTypeId_UpdatedByUserId,
       DocumentTypeId.[DeletedByUserId] as DocumentTypeId_DeletedByUserId,
       DocumentTypeId.[IsDeleted] as DocumentTypeId_IsDeleted,
       DocumentTypeId.[Tags] as DocumentTypeId_Tags,
       DocumentTypeId.[Caption] as DocumentTypeId_Caption,
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

   from Accounting.TransactionDocument TransactionDocument

            left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=TransactionDocument.DocumentTypeId
            left join Accounting.TransactionJournal TransactionJournalId on TransactionJournalId.Id=TransactionDocument.TransactionJournalId
            left join Accounting.TransactionJournal TransactionJournalId1 on TransactionJournalId1.Id=TransactionDocument.TransactionJournalId1;


-- Accounting.TransactionJournalMasterView source

CREATE OR ALTER VIEW Accounting.TransactionJournalMasterView
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

   from Accounting.TransactionJournal TransactionJournal;


-- Customer.CustomerMasterView source

CREATE OR ALTER VIEW Customer.CustomerMasterView
as select

       ROW_NUMBER() OVER (ORDER BY Customer.[Id]) AS RowNumber,
           Customer.[Id],
       Customer.[CustomerNo],
       Customer.[LastName],
       Customer.[MiddleName],
       Customer.[FirstName],
       Customer.[Dob],
       Customer.[Gender],
       Customer.[ProfileImageUrl],
       Customer.[RegistrationDate],
       Customer.[DepartmentId],
       Customer.[ProfileId],
       Customer.[Description],
       Customer.[IsActive],
       Customer.[CreatedByUserId],
       Customer.[DateCreated],
       Customer.[UpdatedByUserId],
       Customer.[DateUpdated],
       Customer.[DeletedByUserId],
       Customer.[IsDeleted],
       Customer.[DateDeleted],
       Customer.[RowVersion],
       Customer.[FullText],
       Customer.[Tags],
       Customer.[Caption],
       DepartmentId.[Name] as DepartmentId_Name,
       DepartmentId.[IsActive] as DepartmentId_IsActive,
       DepartmentId.[CreatedByUserId] as DepartmentId_CreatedByUserId,
       DepartmentId.[UpdatedByUserId] as DepartmentId_UpdatedByUserId,
       DepartmentId.[DeletedByUserId] as DepartmentId_DeletedByUserId,
       DepartmentId.[IsDeleted] as DepartmentId_IsDeleted,
       DepartmentId.[Tags] as DepartmentId_Tags,
       DepartmentId.[Caption] as DepartmentId_Caption,
       ProfileId.[ApplicationUserId] as ProfileId_ApplicationUserId,
       ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
       ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
       ProfileId.[KycStartDate] as ProfileId_KycStartDate,
       ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
       ProfileId.[Status] as ProfileId_Status,
       ProfileId.[Gender] as ProfileId_Gender,
       ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
       ProfileId.[FirstName] as ProfileId_FirstName,
       ProfileId.[LastName] as ProfileId_LastName,
       ProfileId.[MiddleName] as ProfileId_MiddleName,
       ProfileId.[CAI] as ProfileId_CAI,
       ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
       ProfileId.[Address] as ProfileId_Address,
       ProfileId.[Country] as ProfileId_Country,
       ProfileId.[State] as ProfileId_State,
       ProfileId.[DepartmentId] as ProfileId_DepartmentId,
       ProfileId.[IsActive] as ProfileId_IsActive,
       ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
       ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
       ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
       ProfileId.[IsDeleted] as ProfileId_IsDeleted,
       ProfileId.[Tags] as ProfileId_Tags,
       ProfileId.[Caption] as ProfileId_Caption

   from Customer.Customer Customer

            left join MasterData.Department DepartmentId on DepartmentId.Id=Customer.DepartmentId
            left join Security.MemberProfile ProfileId on ProfileId.Id=Customer.ProfileId;


-- Deposits.DepositProductChargeMasterView source

CREATE OR ALTER VIEW Deposits.DepositProductChargeMasterView
as select

       ROW_NUMBER() OVER (ORDER BY DepositProductCharge.[Id]) AS RowNumber,
           DepositProductCharge.[Id],
       DepositProductCharge.[ProductId],
       DepositProductCharge.[ChargeId],
       DepositProductCharge.[Description],
       DepositProductCharge.[IsActive],
       DepositProductCharge.[CreatedByUserId],
       DepositProductCharge.[DateCreated],
       DepositProductCharge.[UpdatedByUserId],
       DepositProductCharge.[DateUpdated],
       DepositProductCharge.[DeletedByUserId],
       DepositProductCharge.[IsDeleted],
       DepositProductCharge.[DateDeleted],
       DepositProductCharge.[RowVersion],
       DepositProductCharge.[FullText],
       DepositProductCharge.[Tags],
       DepositProductCharge.[Caption],
       ProductId.[Code] as ProductId_Code,
       ProductId.[Name] as ProductId_Name,
       ProductId.[ShortName] as ProductId_ShortName,
       ProductId.[MinimumAge] as ProductId_MinimumAge,
       ProductId.[MaximumAge] as ProductId_MaximumAge,
       ProductId.[DefaultCurrencyId] as ProductId_DefaultCurrencyId,
       ProductId.[BankDepositAccountId] as ProductId_BankDepositAccountId,
       ProductId.[ProductDepositAccountId] as ProductId_ProductDepositAccountId,
       ProductId.[ChargesIncomeAccountId] as ProductId_ChargesIncomeAccountId,
       ProductId.[ChargesAccrualAccountId] as ProductId_ChargesAccrualAccountId,
       ProductId.[InterestPayableAccountId] as ProductId_InterestPayableAccountId,
       ProductId.[InterestPayoutAccountId] as ProductId_InterestPayoutAccountId,
       ProductId.[IsInterestEnabled] as ProductId_IsInterestEnabled,
       ProductId.[IsActive] as ProductId_IsActive,
       ProductId.[CreatedByUserId] as ProductId_CreatedByUserId,
       ProductId.[UpdatedByUserId] as ProductId_UpdatedByUserId,
       ProductId.[DeletedByUserId] as ProductId_DeletedByUserId,
       ProductId.[IsDeleted] as ProductId_IsDeleted,
       ProductId.[Tags] as ProductId_Tags,
       ProductId.[Caption] as ProductId_Caption,
       ProductId.[Tenure] as ProductId_Tenure,
       ProductId.[TenureValue] as ProductId_TenureValue,
       ChargeId.[Code] as ChargeId_Code,
       ChargeId.[Name] as ChargeId_Name,
       ChargeId.[Method] as ChargeId_Method,
       ChargeId.[Target] as ChargeId_Target,
       ChargeId.[CalculationMethod] as ChargeId_CalculationMethod,
       ChargeId.[CurrencyId] as ChargeId_CurrencyId,
       ChargeId.[ChargeValue] as ChargeId_ChargeValue,
       ChargeId.[MaximumCharge] as ChargeId_MaximumCharge,
       ChargeId.[MinimimumCharge] as ChargeId_MinimimumCharge,
       ChargeId.[IsActive] as ChargeId_IsActive,
       ChargeId.[CreatedByUserId] as ChargeId_CreatedByUserId,
       ChargeId.[UpdatedByUserId] as ChargeId_UpdatedByUserId,
       ChargeId.[DeletedByUserId] as ChargeId_DeletedByUserId,
       ChargeId.[IsDeleted] as ChargeId_IsDeleted,
       ChargeId.[Tags] as ChargeId_Tags,
       ChargeId.[Caption] as ChargeId_Caption

   from Deposits.DepositProductCharge DepositProductCharge

            left join Deposits.DepositProduct ProductId on ProductId.Id=DepositProductCharge.ProductId
            left join Accounting.Charge ChargeId on ChargeId.Id=DepositProductCharge.ChargeId;


-- Deposits.DepositProductInterestRangeMasterView source

CREATE OR ALTER VIEW Deposits.DepositProductInterestRangeMasterView
as select

       ROW_NUMBER() OVER (ORDER BY DepositProductInterestRange.[Id]) AS RowNumber,
           DepositProductInterestRange.[Id],
       DepositProductInterestRange.[ProductId],
       DepositProductInterestRange.[LowerLimit],
       DepositProductInterestRange.[UpperLimit],
       DepositProductInterestRange.[InterestRate],
       DepositProductInterestRange.[Description],
       DepositProductInterestRange.[IsActive],
       DepositProductInterestRange.[CreatedByUserId],
       DepositProductInterestRange.[DateCreated],
       DepositProductInterestRange.[UpdatedByUserId],
       DepositProductInterestRange.[DateUpdated],
       DepositProductInterestRange.[DeletedByUserId],
       DepositProductInterestRange.[IsDeleted],
       DepositProductInterestRange.[DateDeleted],
       DepositProductInterestRange.[RowVersion],
       DepositProductInterestRange.[FullText],
       DepositProductInterestRange.[Tags],
       DepositProductInterestRange.[Caption],
       ProductId.[Code] as ProductId_Code,
       ProductId.[Name] as ProductId_Name,
       ProductId.[ShortName] as ProductId_ShortName,
       ProductId.[MinimumAge] as ProductId_MinimumAge,
       ProductId.[MaximumAge] as ProductId_MaximumAge,
       ProductId.[DefaultCurrencyId] as ProductId_DefaultCurrencyId,
       ProductId.[BankDepositAccountId] as ProductId_BankDepositAccountId,
       ProductId.[ProductDepositAccountId] as ProductId_ProductDepositAccountId,
       ProductId.[ChargesIncomeAccountId] as ProductId_ChargesIncomeAccountId,
       ProductId.[ChargesAccrualAccountId] as ProductId_ChargesAccrualAccountId,
       ProductId.[InterestPayableAccountId] as ProductId_InterestPayableAccountId,
       ProductId.[InterestPayoutAccountId] as ProductId_InterestPayoutAccountId,
       ProductId.[IsInterestEnabled] as ProductId_IsInterestEnabled,
       ProductId.[IsActive] as ProductId_IsActive,
       ProductId.[CreatedByUserId] as ProductId_CreatedByUserId,
       ProductId.[UpdatedByUserId] as ProductId_UpdatedByUserId,
       ProductId.[DeletedByUserId] as ProductId_DeletedByUserId,
       ProductId.[IsDeleted] as ProductId_IsDeleted,
       ProductId.[Tags] as ProductId_Tags,
       ProductId.[Caption] as ProductId_Caption,
       ProductId.[Tenure] as ProductId_Tenure,
       ProductId.[TenureValue] as ProductId_TenureValue

   from Deposits.DepositProductInterestRange DepositProductInterestRange

            left join Deposits.DepositProduct ProductId on ProductId.Id=DepositProductInterestRange.ProductId;


-- Deposits.DepositProductMasterView source

-- Deposits.DepositProductMasterView source

CREATE OR ALTER VIEW [Deposits].[DepositProductMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY DepositProduct.[Id]) AS RowNumber,
           DepositProduct.[Id],
       DepositProduct.[Code],
       DepositProduct.[Name],
       DepositProduct.[ShortName],
       DepositProduct.[MinimumAge],
       DepositProduct.[MaximumAge],
       DepositProduct.[DefaultCurrencyId],
       DepositProduct.[BankDepositAccountId],
       DepositProduct.[ProductDepositAccountId],
       DepositProduct.[ChargesIncomeAccountId],
       DepositProduct.[ChargesAccrualAccountId],
       DepositProduct.[InterestPayableAccountId],
       DepositProduct.[InterestPayoutAccountId],
       DepositProduct.[IsInterestEnabled],
       DepositProduct.[Description],
       DepositProduct.[IsActive],
       DepositProduct.[CreatedByUserId],
       DepositProduct.[DateCreated],
       DepositProduct.[UpdatedByUserId],
       DepositProduct.[DateUpdated],
       DepositProduct.[DeletedByUserId],
       DepositProduct.[IsDeleted],
       DepositProduct.[DateDeleted],
       DepositProduct.[RowVersion],
       DepositProduct.[FullText],
       DepositProduct.[Tags],
       DepositProduct.[Caption],
       DepositProduct.[Tenure],
       DepositProduct.[TenureValue],
       DepositProduct.[Status],
       DefaultCurrencyId.[Code] as DefaultCurrencyId_Code,
       DefaultCurrencyId.[Name] as DefaultCurrencyId_Name,
       DefaultCurrencyId.[Symbol] as DefaultCurrencyId_Symbol,
       DefaultCurrencyId.[IsoSymbol] as DefaultCurrencyId_IsoSymbol,
       DefaultCurrencyId.[DecimalPlaces] as DefaultCurrencyId_DecimalPlaces,
       DefaultCurrencyId.[Format] as DefaultCurrencyId_Format,
       DefaultCurrencyId.[IsActive] as DefaultCurrencyId_IsActive,
       DefaultCurrencyId.[CreatedByUserId] as DefaultCurrencyId_CreatedByUserId,
       DefaultCurrencyId.[UpdatedByUserId] as DefaultCurrencyId_UpdatedByUserId,
       DefaultCurrencyId.[DeletedByUserId] as DefaultCurrencyId_DeletedByUserId,
       DefaultCurrencyId.[IsDeleted] as DefaultCurrencyId_IsDeleted,
       DefaultCurrencyId.[Tags] as DefaultCurrencyId_Tags,
       DefaultCurrencyId.[Caption] as DefaultCurrencyId_Caption,
       ChargesAccrualAccountId.[AccountType] as ChargesAccrualAccountId_AccountType,
       ChargesAccrualAccountId.[UOM] as ChargesAccrualAccountId_UOM,
       ChargesAccrualAccountId.[CurrencyId] as ChargesAccrualAccountId_CurrencyId,
       ChargesAccrualAccountId.[Code] as ChargesAccrualAccountId_Code,
       ChargesAccrualAccountId.[Name] as ChargesAccrualAccountId_Name,
       ChargesAccrualAccountId.[ParentId] as ChargesAccrualAccountId_ParentId,
       ChargesAccrualAccountId.[ClearedBalance] as ChargesAccrualAccountId_ClearedBalance,
       ChargesAccrualAccountId.[UnclearedBalance] as ChargesAccrualAccountId_UnclearedBalance,
       ChargesAccrualAccountId.[LedgerBalance] as ChargesAccrualAccountId_LedgerBalance,
       ChargesAccrualAccountId.[AvailableBalance] as ChargesAccrualAccountId_AvailableBalance,
       ChargesAccrualAccountId.[IsOfficeAccount] as ChargesAccrualAccountId_IsOfficeAccount,
       ChargesAccrualAccountId.[AllowManualEntry] as ChargesAccrualAccountId_AllowManualEntry,
       ChargesAccrualAccountId.[IsClosed] as ChargesAccrualAccountId_IsClosed,
       ChargesAccrualAccountId.[DateClosed] as ChargesAccrualAccountId_DateClosed,
       ChargesAccrualAccountId.[ClosedByUserName] as ChargesAccrualAccountId_ClosedByUserName,
       ChargesAccrualAccountId.[IsActive] as ChargesAccrualAccountId_IsActive,
       ChargesAccrualAccountId.[CreatedByUserId] as ChargesAccrualAccountId_CreatedByUserId,
       ChargesAccrualAccountId.[UpdatedByUserId] as ChargesAccrualAccountId_UpdatedByUserId,
       ChargesAccrualAccountId.[DeletedByUserId] as ChargesAccrualAccountId_DeletedByUserId,
       ChargesAccrualAccountId.[IsDeleted] as ChargesAccrualAccountId_IsDeleted,
       ChargesAccrualAccountId.[Tags] as ChargesAccrualAccountId_Tags,
       ChargesAccrualAccountId.[Caption] as ChargesAccrualAccountId_Caption,
       ChargesIncomeAccountId.[AccountType] as ChargesIncomeAccountId_AccountType,
       ChargesIncomeAccountId.[UOM] as ChargesIncomeAccountId_UOM,
       ChargesIncomeAccountId.[CurrencyId] as ChargesIncomeAccountId_CurrencyId,
       ChargesIncomeAccountId.[Code] as ChargesIncomeAccountId_Code,
       ChargesIncomeAccountId.[Name] as ChargesIncomeAccountId_Name,
       ChargesIncomeAccountId.[ParentId] as ChargesIncomeAccountId_ParentId,
       ChargesIncomeAccountId.[ClearedBalance] as ChargesIncomeAccountId_ClearedBalance,
       ChargesIncomeAccountId.[UnclearedBalance] as ChargesIncomeAccountId_UnclearedBalance,
       ChargesIncomeAccountId.[LedgerBalance] as ChargesIncomeAccountId_LedgerBalance,
       ChargesIncomeAccountId.[AvailableBalance] as ChargesIncomeAccountId_AvailableBalance,
       ChargesIncomeAccountId.[IsOfficeAccount] as ChargesIncomeAccountId_IsOfficeAccount,
       ChargesIncomeAccountId.[AllowManualEntry] as ChargesIncomeAccountId_AllowManualEntry,
       ChargesIncomeAccountId.[IsClosed] as ChargesIncomeAccountId_IsClosed,
       ChargesIncomeAccountId.[DateClosed] as ChargesIncomeAccountId_DateClosed,
       ChargesIncomeAccountId.[ClosedByUserName] as ChargesIncomeAccountId_ClosedByUserName,
       ChargesIncomeAccountId.[IsActive] as ChargesIncomeAccountId_IsActive,
       ChargesIncomeAccountId.[CreatedByUserId] as ChargesIncomeAccountId_CreatedByUserId,
       ChargesIncomeAccountId.[UpdatedByUserId] as ChargesIncomeAccountId_UpdatedByUserId,
       ChargesIncomeAccountId.[DeletedByUserId] as ChargesIncomeAccountId_DeletedByUserId,
       ChargesIncomeAccountId.[IsDeleted] as ChargesIncomeAccountId_IsDeleted,
       ChargesIncomeAccountId.[Tags] as ChargesIncomeAccountId_Tags,
       ChargesIncomeAccountId.[Caption] as ChargesIncomeAccountId_Caption,
       InterestPayableAccountId.[AccountType] as InterestPayableAccountId_AccountType,
       InterestPayableAccountId.[UOM] as InterestPayableAccountId_UOM,
       InterestPayableAccountId.[CurrencyId] as InterestPayableAccountId_CurrencyId,
       InterestPayableAccountId.[Code] as InterestPayableAccountId_Code,
       InterestPayableAccountId.[Name] as InterestPayableAccountId_Name,
       InterestPayableAccountId.[ParentId] as InterestPayableAccountId_ParentId,
       InterestPayableAccountId.[ClearedBalance] as InterestPayableAccountId_ClearedBalance,
       InterestPayableAccountId.[UnclearedBalance] as InterestPayableAccountId_UnclearedBalance,
       InterestPayableAccountId.[LedgerBalance] as InterestPayableAccountId_LedgerBalance,
       InterestPayableAccountId.[AvailableBalance] as InterestPayableAccountId_AvailableBalance,
       InterestPayableAccountId.[IsOfficeAccount] as InterestPayableAccountId_IsOfficeAccount,
       InterestPayableAccountId.[AllowManualEntry] as InterestPayableAccountId_AllowManualEntry,
       InterestPayableAccountId.[IsClosed] as InterestPayableAccountId_IsClosed,
       InterestPayableAccountId.[DateClosed] as InterestPayableAccountId_DateClosed,
       InterestPayableAccountId.[ClosedByUserName] as InterestPayableAccountId_ClosedByUserName,
       InterestPayableAccountId.[IsActive] as InterestPayableAccountId_IsActive,
       InterestPayableAccountId.[CreatedByUserId] as InterestPayableAccountId_CreatedByUserId,
       InterestPayableAccountId.[UpdatedByUserId] as InterestPayableAccountId_UpdatedByUserId,
       InterestPayableAccountId.[DeletedByUserId] as InterestPayableAccountId_DeletedByUserId,
       InterestPayableAccountId.[IsDeleted] as InterestPayableAccountId_IsDeleted,
       InterestPayableAccountId.[Tags] as InterestPayableAccountId_Tags,
       InterestPayableAccountId.[Caption] as InterestPayableAccountId_Caption,
       InterestPayoutAccountId.[AccountType] as InterestPayoutAccountId_AccountType,
       InterestPayoutAccountId.[UOM] as InterestPayoutAccountId_UOM,
       InterestPayoutAccountId.[CurrencyId] as InterestPayoutAccountId_CurrencyId,
       InterestPayoutAccountId.[Code] as InterestPayoutAccountId_Code,
       InterestPayoutAccountId.[Name] as InterestPayoutAccountId_Name,
       InterestPayoutAccountId.[ParentId] as InterestPayoutAccountId_ParentId,
       InterestPayoutAccountId.[ClearedBalance] as InterestPayoutAccountId_ClearedBalance,
       InterestPayoutAccountId.[UnclearedBalance] as InterestPayoutAccountId_UnclearedBalance,
       InterestPayoutAccountId.[LedgerBalance] as InterestPayoutAccountId_LedgerBalance,
       InterestPayoutAccountId.[AvailableBalance] as InterestPayoutAccountId_AvailableBalance,
       InterestPayoutAccountId.[IsOfficeAccount] as InterestPayoutAccountId_IsOfficeAccount,
       InterestPayoutAccountId.[AllowManualEntry] as InterestPayoutAccountId_AllowManualEntry,
       InterestPayoutAccountId.[IsClosed] as InterestPayoutAccountId_IsClosed,
       InterestPayoutAccountId.[DateClosed] as InterestPayoutAccountId_DateClosed,
       InterestPayoutAccountId.[ClosedByUserName] as InterestPayoutAccountId_ClosedByUserName,
       InterestPayoutAccountId.[IsActive] as InterestPayoutAccountId_IsActive,
       InterestPayoutAccountId.[CreatedByUserId] as InterestPayoutAccountId_CreatedByUserId,
       InterestPayoutAccountId.[UpdatedByUserId] as InterestPayoutAccountId_UpdatedByUserId,
       InterestPayoutAccountId.[DeletedByUserId] as InterestPayoutAccountId_DeletedByUserId,
       InterestPayoutAccountId.[IsDeleted] as InterestPayoutAccountId_IsDeleted,
       InterestPayoutAccountId.[Tags] as InterestPayoutAccountId_Tags,
       InterestPayoutAccountId.[Caption] as InterestPayoutAccountId_Caption,
       ProductDepositAccountId.[AccountType] as ProductDepositAccountId_AccountType,
       ProductDepositAccountId.[UOM] as ProductDepositAccountId_UOM,
       ProductDepositAccountId.[CurrencyId] as ProductDepositAccountId_CurrencyId,
       ProductDepositAccountId.[Code] as ProductDepositAccountId_Code,
       ProductDepositAccountId.[Name] as ProductDepositAccountId_Name,
       ProductDepositAccountId.[ParentId] as ProductDepositAccountId_ParentId,
       ProductDepositAccountId.[ClearedBalance] as ProductDepositAccountId_ClearedBalance,
       ProductDepositAccountId.[UnclearedBalance] as ProductDepositAccountId_UnclearedBalance,
       ProductDepositAccountId.[LedgerBalance] as ProductDepositAccountId_LedgerBalance,
       ProductDepositAccountId.[AvailableBalance] as ProductDepositAccountId_AvailableBalance,
       ProductDepositAccountId.[IsOfficeAccount] as ProductDepositAccountId_IsOfficeAccount,
       ProductDepositAccountId.[AllowManualEntry] as ProductDepositAccountId_AllowManualEntry,
       ProductDepositAccountId.[IsClosed] as ProductDepositAccountId_IsClosed,
       ProductDepositAccountId.[DateClosed] as ProductDepositAccountId_DateClosed,
       ProductDepositAccountId.[ClosedByUserName] as ProductDepositAccountId_ClosedByUserName,
       ProductDepositAccountId.[IsActive] as ProductDepositAccountId_IsActive,
       ProductDepositAccountId.[CreatedByUserId] as ProductDepositAccountId_CreatedByUserId,
       ProductDepositAccountId.[UpdatedByUserId] as ProductDepositAccountId_UpdatedByUserId,
       ProductDepositAccountId.[DeletedByUserId] as ProductDepositAccountId_DeletedByUserId,
       ProductDepositAccountId.[IsDeleted] as ProductDepositAccountId_IsDeleted,
       ProductDepositAccountId.[Tags] as ProductDepositAccountId_Tags,
       ProductDepositAccountId.[Caption] as ProductDepositAccountId_Caption,
       BankDepositAccountId.[LedgerAccountId] as BankDepositAccountId_LedgerAccountId,
       BankDepositAccountId.[BankId] as BankDepositAccountId_BankId,
       BankDepositAccountId.[BranchName] as BankDepositAccountId_BranchName,
       BankDepositAccountId.[BranchAddress] as BankDepositAccountId_BranchAddress,
       BankDepositAccountId.[CurrencyId] as BankDepositAccountId_CurrencyId,
       BankDepositAccountId.[AccountName] as BankDepositAccountId_AccountName,
       BankDepositAccountId.[AccountNo] as BankDepositAccountId_AccountNo,
       BankDepositAccountId.[AccountBVN] as BankDepositAccountId_AccountBVN,
       BankDepositAccountId.[IsActive] as BankDepositAccountId_IsActive,
       BankDepositAccountId.[CreatedByUserId] as BankDepositAccountId_CreatedByUserId,
       BankDepositAccountId.[UpdatedByUserId] as BankDepositAccountId_UpdatedByUserId,
       BankDepositAccountId.[DeletedByUserId] as BankDepositAccountId_DeletedByUserId,
       BankDepositAccountId.[IsDeleted] as BankDepositAccountId_IsDeleted,
       BankDepositAccountId.[Tags] as BankDepositAccountId_Tags,
       BankDepositAccountId.[Caption] as BankDepositAccountId_Caption

   from Deposits.DepositProduct DepositProduct

            left join MasterData.Currency DefaultCurrencyId on DefaultCurrencyId.Id=DepositProduct.DefaultCurrencyId
            left join Accounting.LedgerAccount ChargesAccrualAccountId on ChargesAccrualAccountId.Id=DepositProduct.ChargesAccrualAccountId
            left join Accounting.LedgerAccount ChargesIncomeAccountId on ChargesIncomeAccountId.Id=DepositProduct.ChargesIncomeAccountId
            left join Accounting.LedgerAccount InterestPayableAccountId on InterestPayableAccountId.Id=DepositProduct.InterestPayableAccountId
            left join Accounting.LedgerAccount InterestPayoutAccountId on InterestPayoutAccountId.Id=DepositProduct.InterestPayoutAccountId
            left join Accounting.LedgerAccount ProductDepositAccountId on ProductDepositAccountId.Id=DepositProduct.ProductDepositAccountId
            left join Accounting.CompanyBankAccount BankDepositAccountId on BankDepositAccountId.Id=DepositProduct.BankDepositAccountId;


-- Docs.DocumentTypeMasterView source

CREATE OR ALTER VIEW Docs.DocumentTypeMasterView
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

   from Docs.DocumentType DocumentType;


-- Docs.OfficeDocumentMasterView source

CREATE OR ALTER VIEW Docs.OfficeDocumentMasterView
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

   from Docs.OfficeDocument OfficeDocument

            left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=OfficeDocument.DocumentTypeId;



-- Docs.OfficePhotoMasterView source

CREATE OR ALTER VIEW Docs.OfficePhotoMasterView
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

   from Docs.OfficePhoto OfficePhoto

            left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=OfficePhoto.DocumentTypeId;


-- Docs.OfficeSheetMasterView source

CREATE OR ALTER VIEW Docs.OfficeSheetMasterView
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

   from Docs.OfficeSheet OfficeSheet

            left join Docs.DocumentType DocumentTypeId on DocumentTypeId.Id=OfficeSheet.DocumentTypeId;


-- HR.EmployeeMasterView source

CREATE OR ALTER VIEW HR.EmployeeMasterView
as select

       ROW_NUMBER() OVER (ORDER BY Employee.[Id]) AS RowNumber,
           Employee.[Id],
       Employee.[EmployeeNo],
       Employee.[LastName],
       Employee.[MiddleName],
       Employee.[FirstName],
       Employee.[Dob],
       Employee.[Gender],
       Employee.[ProfileImageUrl],
       Employee.[EmploymentDate],
       Employee.[DepartmentId],
       Employee.[ProfileId],
       Employee.[Description],
       Employee.[IsActive],
       Employee.[CreatedByUserId],
       Employee.[DateCreated],
       Employee.[UpdatedByUserId],
       Employee.[DateUpdated],
       Employee.[DeletedByUserId],
       Employee.[IsDeleted],
       Employee.[DateDeleted],
       Employee.[RowVersion],
       Employee.[FullText],
       Employee.[Tags],
       Employee.[Caption],
       DepartmentId.[Name] as DepartmentId_Name,
       DepartmentId.[IsActive] as DepartmentId_IsActive,
       DepartmentId.[CreatedByUserId] as DepartmentId_CreatedByUserId,
       DepartmentId.[UpdatedByUserId] as DepartmentId_UpdatedByUserId,
       DepartmentId.[DeletedByUserId] as DepartmentId_DeletedByUserId,
       DepartmentId.[IsDeleted] as DepartmentId_IsDeleted,
       DepartmentId.[Tags] as DepartmentId_Tags,
       DepartmentId.[Caption] as DepartmentId_Caption,
       ProfileId.[ApplicationUserId] as ProfileId_ApplicationUserId,
       ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
       ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
       ProfileId.[KycStartDate] as ProfileId_KycStartDate,
       ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
       ProfileId.[Status] as ProfileId_Status,
       ProfileId.[Gender] as ProfileId_Gender,
       ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
       ProfileId.[FirstName] as ProfileId_FirstName,
       ProfileId.[LastName] as ProfileId_LastName,
       ProfileId.[MiddleName] as ProfileId_MiddleName,
       ProfileId.[CAI] as ProfileId_CAI,
       ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
       ProfileId.[Address] as ProfileId_Address,
       ProfileId.[Country] as ProfileId_Country,
       ProfileId.[State] as ProfileId_State,
       ProfileId.[DepartmentId] as ProfileId_DepartmentId,
       ProfileId.[IsActive] as ProfileId_IsActive,
       ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
       ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
       ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
       ProfileId.[IsDeleted] as ProfileId_IsDeleted,
       ProfileId.[Tags] as ProfileId_Tags,
       ProfileId.[Caption] as ProfileId_Caption

   from HR.Employee Employee

            left join MasterData.Department DepartmentId on DepartmentId.Id=Employee.DepartmentId
            left join Security.MemberProfile ProfileId on ProfileId.Id=Employee.ProfileId;


-- Loans.LoanProductChargeMasterView source

-- Loans.LoanProductChargeMasterView source

CREATE OR ALTER VIEW Loans.LoanProductChargeMasterView
as select

       ROW_NUMBER() OVER (ORDER BY LoanProductCharge.[Id]) AS RowNumber,
           LoanProductCharge.[Id],
       LoanProductCharge.[ProductId],
       LoanProductCharge.[ChargeId],
       LoanProductCharge.[ChargeType],
       LoanProductCharge.[Description],
       LoanProductCharge.[IsActive],
       LoanProductCharge.[CreatedByUserId],
       LoanProductCharge.[DateCreated],
       LoanProductCharge.[UpdatedByUserId],
       LoanProductCharge.[DateUpdated],
       LoanProductCharge.[DeletedByUserId],
       LoanProductCharge.[IsDeleted],
       LoanProductCharge.[DateDeleted],
       LoanProductCharge.[RowVersion],
       LoanProductCharge.[FullText],
       LoanProductCharge.[Tags],
       LoanProductCharge.[Caption],
       ProductId.[Code] as ProductId_Code,
       ProductId.[Name] as ProductId_Name,
       ProductId.[ShortName] as ProductId_ShortName,
       ProductId.[MinimumAge] as ProductId_MinimumAge,
       ProductId.[MaximumAge] as ProductId_MaximumAge,
       ProductId.[DefaultCurrencyId] as ProductId_DefaultCurrencyId,
       ProductId.[BankDepositAccountId] as ProductId_BankDepositAccountId,
       ProductId.[DisbursementAccountId] as ProductId_DisbursementAccountId,
       ProductId.[PrincipalReceivableAccountId] as ProductId_PrincipalReceivableAccountId,
       ProductId.[PrincipalLossAccountId] as ProductId_PrincipalLossAccountId,
       ProductId.[InterestReceivableAccountId] as ProductId_InterestReceivableAccountId,
       ProductId.[InterestLossAccountId] as ProductId_InterestLossAccountId,
       ProductId.[PenalInterestReceivableAccountId] as ProductId_PenalInterestReceivableAccountId,
       ProductId.[ChargesReceivableAccountId] as ProductId_ChargesReceivableAccountId,
       ProductId.[ChargesAccrualAccountId] as ProductId_ChargesAccrualAccountId,
       ProductId.[IsActive] as ProductId_IsActive,
       ProductId.[CreatedByUserId] as ProductId_CreatedByUserId,
       ProductId.[UpdatedByUserId] as ProductId_UpdatedByUserId,
       ProductId.[DeletedByUserId] as ProductId_DeletedByUserId,
       ProductId.[IsDeleted] as ProductId_IsDeleted,
       ProductId.[Tags] as ProductId_Tags,
       ProductId.[Caption] as ProductId_Caption,
       ChargeId.[Code] as ChargeId_Code,
       ChargeId.[Name] as ChargeId_Name,
       ChargeId.[Method] as ChargeId_Method,
       ChargeId.[Target] as ChargeId_Target,
       ChargeId.[CalculationMethod] as ChargeId_CalculationMethod,
       ChargeId.[CurrencyId] as ChargeId_CurrencyId,
       ChargeId.[IsActive] as ChargeId_IsActive,
       ChargeId.[CreatedByUserId] as ChargeId_CreatedByUserId,
       ChargeId.[UpdatedByUserId] as ChargeId_UpdatedByUserId,
       ChargeId.[DeletedByUserId] as ChargeId_DeletedByUserId,
       ChargeId.[IsDeleted] as ChargeId_IsDeleted,
       ChargeId.[Tags] as ChargeId_Tags,
       ChargeId.[Caption] as ChargeId_Caption

   from Loans.LoanProductCharge LoanProductCharge

            left join Loans.LoanProduct ProductId on ProductId.Id=LoanProductCharge.ProductId
            left join Accounting.Charge ChargeId on ChargeId.Id=LoanProductCharge.ChargeId;


-- Loans.LoanProductMasterView source

-- Loans.LoanProductMasterView source

-- Loans.LoanProductMasterView source

CREATE OR ALTER VIEW Loans.LoanProductMasterView
as select

       ROW_NUMBER() OVER (ORDER BY LoanProduct.[Id]) AS RowNumber,
           LoanProduct.[Id],
       LoanProduct.[Code],
       LoanProduct.[Name],
       LoanProduct.[ShortName],
       LoanProduct.[PrincipalLimitType],
       LoanProduct.[PrincipalMultiple],
       LoanProduct.[PrincipalMinLimit],
       LoanProduct.[PrincipalMaxLimit],
       LoanProduct.[TenureUnit],
       LoanProduct.[MinTenureValue],
       LoanProduct.[MaxTenureValue],
       LoanProduct.[InterestMethod],
       LoanProduct.[InterestRate],
       LoanProduct.[HasAdminCharges],
       LoanProduct.[IsTargetLoan],
       LoanProduct.[BenefitCode],
       LoanProduct.[AllowedOffsetType],
       LoanProduct.[OffsetPeriodUnit],
       LoanProduct.[OffsetPeriodValue],
       LoanProduct.[EnableSavingsOffset],
       LoanProduct.[EnableChargeWaiver],
       LoanProduct.[EnableTopUp],
       LoanProduct.[EnableTopUpCharges],
       LoanProduct.[EnableWaitingPeriod],
       LoanProduct.[WaitingPeriodUnit],
       LoanProduct.[WaitingPeriodValue],
       LoanProduct.[QualificationTargetProduct],
       LoanProduct.[QualificationMinBalancePercentage],
       LoanProduct.[EnableWaitingPeriodCharge],
       LoanProduct.[IsGuarantorRequired],
       LoanProduct.[GuarantorMinYear],
       LoanProduct.[GuarantorAmountUnit],
       LoanProduct.[EmployeeGuarantorCount],
       LoanProduct.[NonEmployeeGuarantorCount],
       LoanProduct.[SavingsOffSetJson],
       LoanProduct.[MemberTypeJson],
       LoanProduct.[Status],
       LoanProduct.[MinimumAge],
       LoanProduct.[MaximumAge],
       LoanProduct.[DefaultCurrencyId],
       LoanProduct.[BankDepositAccountId],
       LoanProduct.[DisbursementAccountId],
       LoanProduct.[PrincipalReceivableAccountId],
       LoanProduct.[PrincipalLossAccountId],
       LoanProduct.[InterestReceivableAccountId],
       LoanProduct.[InterestLossAccountId],
       LoanProduct.[PenalInterestReceivableAccountId],
       LoanProduct.[ChargesReceivableAccountId],
       LoanProduct.[ChargesAccrualAccountId],
       LoanProduct.[Description],
       LoanProduct.[IsActive],
       LoanProduct.[CreatedByUserId],
       LoanProduct.[DateCreated],
       LoanProduct.[UpdatedByUserId],
       LoanProduct.[DateUpdated],
       LoanProduct.[DeletedByUserId],
       LoanProduct.[IsDeleted],
       LoanProduct.[DateDeleted],
       LoanProduct.[RowVersion],
       LoanProduct.[FullText],
       LoanProduct.[Tags],
       LoanProduct.[Caption],
       DefaultCurrencyId.[Code] as DefaultCurrencyId_Code,
       DefaultCurrencyId.[Name] as DefaultCurrencyId_Name,
       DefaultCurrencyId.[Symbol] as DefaultCurrencyId_Symbol,
       DefaultCurrencyId.[IsoSymbol] as DefaultCurrencyId_IsoSymbol,
       DefaultCurrencyId.[DecimalPlaces] as DefaultCurrencyId_DecimalPlaces,
       DefaultCurrencyId.[Format] as DefaultCurrencyId_Format,
       DefaultCurrencyId.[IsActive] as DefaultCurrencyId_IsActive,
       DefaultCurrencyId.[CreatedByUserId] as DefaultCurrencyId_CreatedByUserId,
       DefaultCurrencyId.[UpdatedByUserId] as DefaultCurrencyId_UpdatedByUserId,
       DefaultCurrencyId.[DeletedByUserId] as DefaultCurrencyId_DeletedByUserId,
       DefaultCurrencyId.[IsDeleted] as DefaultCurrencyId_IsDeleted,
       DefaultCurrencyId.[Tags] as DefaultCurrencyId_Tags,
       DefaultCurrencyId.[Caption] as DefaultCurrencyId_Caption,
       ChargesAccrualAccountId.[AccountType] as ChargesAccrualAccountId_AccountType,
       ChargesAccrualAccountId.[UOM] as ChargesAccrualAccountId_UOM,
       ChargesAccrualAccountId.[CurrencyId] as ChargesAccrualAccountId_CurrencyId,
       ChargesAccrualAccountId.[Code] as ChargesAccrualAccountId_Code,
       ChargesAccrualAccountId.[Name] as ChargesAccrualAccountId_Name,
       ChargesAccrualAccountId.[ParentId] as ChargesAccrualAccountId_ParentId,
       ChargesAccrualAccountId.[ClearedBalance] as ChargesAccrualAccountId_ClearedBalance,
       ChargesAccrualAccountId.[UnclearedBalance] as ChargesAccrualAccountId_UnclearedBalance,
       ChargesAccrualAccountId.[LedgerBalance] as ChargesAccrualAccountId_LedgerBalance,
       ChargesAccrualAccountId.[AvailableBalance] as ChargesAccrualAccountId_AvailableBalance,
       ChargesAccrualAccountId.[IsOfficeAccount] as ChargesAccrualAccountId_IsOfficeAccount,
       ChargesAccrualAccountId.[AllowManualEntry] as ChargesAccrualAccountId_AllowManualEntry,
       ChargesAccrualAccountId.[IsClosed] as ChargesAccrualAccountId_IsClosed,
       ChargesAccrualAccountId.[DateClosed] as ChargesAccrualAccountId_DateClosed,
       ChargesAccrualAccountId.[ClosedByUserName] as ChargesAccrualAccountId_ClosedByUserName,
       ChargesAccrualAccountId.[IsActive] as ChargesAccrualAccountId_IsActive,
       ChargesAccrualAccountId.[CreatedByUserId] as ChargesAccrualAccountId_CreatedByUserId,
       ChargesAccrualAccountId.[UpdatedByUserId] as ChargesAccrualAccountId_UpdatedByUserId,
       ChargesAccrualAccountId.[DeletedByUserId] as ChargesAccrualAccountId_DeletedByUserId,
       ChargesAccrualAccountId.[IsDeleted] as ChargesAccrualAccountId_IsDeleted,
       ChargesAccrualAccountId.[Tags] as ChargesAccrualAccountId_Tags,
       ChargesAccrualAccountId.[Caption] as ChargesAccrualAccountId_Caption,
       ChargesReceivableAccountId.[AccountType] as ChargesReceivableAccountId_AccountType,
       ChargesReceivableAccountId.[UOM] as ChargesReceivableAccountId_UOM,
       ChargesReceivableAccountId.[CurrencyId] as ChargesReceivableAccountId_CurrencyId,
       ChargesReceivableAccountId.[Code] as ChargesReceivableAccountId_Code,
       ChargesReceivableAccountId.[Name] as ChargesReceivableAccountId_Name,
       ChargesReceivableAccountId.[ParentId] as ChargesReceivableAccountId_ParentId,
       ChargesReceivableAccountId.[ClearedBalance] as ChargesReceivableAccountId_ClearedBalance,
       ChargesReceivableAccountId.[UnclearedBalance] as ChargesReceivableAccountId_UnclearedBalance,
       ChargesReceivableAccountId.[LedgerBalance] as ChargesReceivableAccountId_LedgerBalance,
       ChargesReceivableAccountId.[AvailableBalance] as ChargesReceivableAccountId_AvailableBalance,
       ChargesReceivableAccountId.[IsOfficeAccount] as ChargesReceivableAccountId_IsOfficeAccount,
       ChargesReceivableAccountId.[AllowManualEntry] as ChargesReceivableAccountId_AllowManualEntry,
       ChargesReceivableAccountId.[IsClosed] as ChargesReceivableAccountId_IsClosed,
       ChargesReceivableAccountId.[DateClosed] as ChargesReceivableAccountId_DateClosed,
       ChargesReceivableAccountId.[ClosedByUserName] as ChargesReceivableAccountId_ClosedByUserName,
       ChargesReceivableAccountId.[IsActive] as ChargesReceivableAccountId_IsActive,
       ChargesReceivableAccountId.[CreatedByUserId] as ChargesReceivableAccountId_CreatedByUserId,
       ChargesReceivableAccountId.[UpdatedByUserId] as ChargesReceivableAccountId_UpdatedByUserId,
       ChargesReceivableAccountId.[DeletedByUserId] as ChargesReceivableAccountId_DeletedByUserId,
       ChargesReceivableAccountId.[IsDeleted] as ChargesReceivableAccountId_IsDeleted,
       ChargesReceivableAccountId.[Tags] as ChargesReceivableAccountId_Tags,
       ChargesReceivableAccountId.[Caption] as ChargesReceivableAccountId_Caption,
       InterestLossAccountId.[AccountType] as InterestLossAccountId_AccountType,
       InterestLossAccountId.[UOM] as InterestLossAccountId_UOM,
       InterestLossAccountId.[CurrencyId] as InterestLossAccountId_CurrencyId,
       InterestLossAccountId.[Code] as InterestLossAccountId_Code,
       InterestLossAccountId.[Name] as InterestLossAccountId_Name,
       InterestLossAccountId.[ParentId] as InterestLossAccountId_ParentId,
       InterestLossAccountId.[ClearedBalance] as InterestLossAccountId_ClearedBalance,
       InterestLossAccountId.[UnclearedBalance] as InterestLossAccountId_UnclearedBalance,
       InterestLossAccountId.[LedgerBalance] as InterestLossAccountId_LedgerBalance,
       InterestLossAccountId.[AvailableBalance] as InterestLossAccountId_AvailableBalance,
       InterestLossAccountId.[IsOfficeAccount] as InterestLossAccountId_IsOfficeAccount,
       InterestLossAccountId.[AllowManualEntry] as InterestLossAccountId_AllowManualEntry,
       InterestLossAccountId.[IsClosed] as InterestLossAccountId_IsClosed,
       InterestLossAccountId.[DateClosed] as InterestLossAccountId_DateClosed,
       InterestLossAccountId.[ClosedByUserName] as InterestLossAccountId_ClosedByUserName,
       InterestLossAccountId.[IsActive] as InterestLossAccountId_IsActive,
       InterestLossAccountId.[CreatedByUserId] as InterestLossAccountId_CreatedByUserId,
       InterestLossAccountId.[UpdatedByUserId] as InterestLossAccountId_UpdatedByUserId,
       InterestLossAccountId.[DeletedByUserId] as InterestLossAccountId_DeletedByUserId,
       InterestLossAccountId.[IsDeleted] as InterestLossAccountId_IsDeleted,
       InterestLossAccountId.[Tags] as InterestLossAccountId_Tags,
       InterestLossAccountId.[Caption] as InterestLossAccountId_Caption,
       InterestReceivableAccountId.[AccountType] as InterestReceivableAccountId_AccountType,
       InterestReceivableAccountId.[UOM] as InterestReceivableAccountId_UOM,
       InterestReceivableAccountId.[CurrencyId] as InterestReceivableAccountId_CurrencyId,
       InterestReceivableAccountId.[Code] as InterestReceivableAccountId_Code,
       InterestReceivableAccountId.[Name] as InterestReceivableAccountId_Name,
       InterestReceivableAccountId.[ParentId] as InterestReceivableAccountId_ParentId,
       InterestReceivableAccountId.[ClearedBalance] as InterestReceivableAccountId_ClearedBalance,
       InterestReceivableAccountId.[UnclearedBalance] as InterestReceivableAccountId_UnclearedBalance,
       InterestReceivableAccountId.[LedgerBalance] as InterestReceivableAccountId_LedgerBalance,
       InterestReceivableAccountId.[AvailableBalance] as InterestReceivableAccountId_AvailableBalance,
       InterestReceivableAccountId.[IsOfficeAccount] as InterestReceivableAccountId_IsOfficeAccount,
       InterestReceivableAccountId.[AllowManualEntry] as InterestReceivableAccountId_AllowManualEntry,
       InterestReceivableAccountId.[IsClosed] as InterestReceivableAccountId_IsClosed,
       InterestReceivableAccountId.[DateClosed] as InterestReceivableAccountId_DateClosed,
       InterestReceivableAccountId.[ClosedByUserName] as InterestReceivableAccountId_ClosedByUserName,
       InterestReceivableAccountId.[IsActive] as InterestReceivableAccountId_IsActive,
       InterestReceivableAccountId.[CreatedByUserId] as InterestReceivableAccountId_CreatedByUserId,
       InterestReceivableAccountId.[UpdatedByUserId] as InterestReceivableAccountId_UpdatedByUserId,
       InterestReceivableAccountId.[DeletedByUserId] as InterestReceivableAccountId_DeletedByUserId,
       InterestReceivableAccountId.[IsDeleted] as InterestReceivableAccountId_IsDeleted,
       InterestReceivableAccountId.[Tags] as InterestReceivableAccountId_Tags,
       InterestReceivableAccountId.[Caption] as InterestReceivableAccountId_Caption,
       PenalInterestReceivableAccountId.[AccountType] as PenalInterestReceivableAccountId_AccountType,
       PenalInterestReceivableAccountId.[UOM] as PenalInterestReceivableAccountId_UOM,
       PenalInterestReceivableAccountId.[CurrencyId] as PenalInterestReceivableAccountId_CurrencyId,
       PenalInterestReceivableAccountId.[Code] as PenalInterestReceivableAccountId_Code,
       PenalInterestReceivableAccountId.[Name] as PenalInterestReceivableAccountId_Name,
       PenalInterestReceivableAccountId.[ParentId] as PenalInterestReceivableAccountId_ParentId,
       PenalInterestReceivableAccountId.[ClearedBalance] as PenalInterestReceivableAccountId_ClearedBalance,
       PenalInterestReceivableAccountId.[UnclearedBalance] as PenalInterestReceivableAccountId_UnclearedBalance,
       PenalInterestReceivableAccountId.[LedgerBalance] as PenalInterestReceivableAccountId_LedgerBalance,
       PenalInterestReceivableAccountId.[AvailableBalance] as PenalInterestReceivableAccountId_AvailableBalance,
       PenalInterestReceivableAccountId.[IsOfficeAccount] as PenalInterestReceivableAccountId_IsOfficeAccount,
       PenalInterestReceivableAccountId.[AllowManualEntry] as PenalInterestReceivableAccountId_AllowManualEntry,
       PenalInterestReceivableAccountId.[IsClosed] as PenalInterestReceivableAccountId_IsClosed,
       PenalInterestReceivableAccountId.[DateClosed] as PenalInterestReceivableAccountId_DateClosed,
       PenalInterestReceivableAccountId.[ClosedByUserName] as PenalInterestReceivableAccountId_ClosedByUserName,
       PenalInterestReceivableAccountId.[IsActive] as PenalInterestReceivableAccountId_IsActive,
       PenalInterestReceivableAccountId.[CreatedByUserId] as PenalInterestReceivableAccountId_CreatedByUserId,
       PenalInterestReceivableAccountId.[UpdatedByUserId] as PenalInterestReceivableAccountId_UpdatedByUserId,
       PenalInterestReceivableAccountId.[DeletedByUserId] as PenalInterestReceivableAccountId_DeletedByUserId,
       PenalInterestReceivableAccountId.[IsDeleted] as PenalInterestReceivableAccountId_IsDeleted,
       PenalInterestReceivableAccountId.[Tags] as PenalInterestReceivableAccountId_Tags,
       PenalInterestReceivableAccountId.[Caption] as PenalInterestReceivableAccountId_Caption,
       PrincipalLossAccountId.[AccountType] as PrincipalLossAccountId_AccountType,
       PrincipalLossAccountId.[UOM] as PrincipalLossAccountId_UOM,
       PrincipalLossAccountId.[CurrencyId] as PrincipalLossAccountId_CurrencyId,
       PrincipalLossAccountId.[Code] as PrincipalLossAccountId_Code,
       PrincipalLossAccountId.[Name] as PrincipalLossAccountId_Name,
       PrincipalLossAccountId.[ParentId] as PrincipalLossAccountId_ParentId,
       PrincipalLossAccountId.[ClearedBalance] as PrincipalLossAccountId_ClearedBalance,
       PrincipalLossAccountId.[UnclearedBalance] as PrincipalLossAccountId_UnclearedBalance,
       PrincipalLossAccountId.[LedgerBalance] as PrincipalLossAccountId_LedgerBalance,
       PrincipalLossAccountId.[AvailableBalance] as PrincipalLossAccountId_AvailableBalance,
       PrincipalLossAccountId.[IsOfficeAccount] as PrincipalLossAccountId_IsOfficeAccount,
       PrincipalLossAccountId.[AllowManualEntry] as PrincipalLossAccountId_AllowManualEntry,
       PrincipalLossAccountId.[IsClosed] as PrincipalLossAccountId_IsClosed,
       PrincipalLossAccountId.[DateClosed] as PrincipalLossAccountId_DateClosed,
       PrincipalLossAccountId.[ClosedByUserName] as PrincipalLossAccountId_ClosedByUserName,
       PrincipalLossAccountId.[IsActive] as PrincipalLossAccountId_IsActive,
       PrincipalLossAccountId.[CreatedByUserId] as PrincipalLossAccountId_CreatedByUserId,
       PrincipalLossAccountId.[UpdatedByUserId] as PrincipalLossAccountId_UpdatedByUserId,
       PrincipalLossAccountId.[DeletedByUserId] as PrincipalLossAccountId_DeletedByUserId,
       PrincipalLossAccountId.[IsDeleted] as PrincipalLossAccountId_IsDeleted,
       PrincipalLossAccountId.[Tags] as PrincipalLossAccountId_Tags,
       PrincipalLossAccountId.[Caption] as PrincipalLossAccountId_Caption,
       PrincipalReceivableAccountId.[AccountType] as PrincipalReceivableAccountId_AccountType,
       PrincipalReceivableAccountId.[UOM] as PrincipalReceivableAccountId_UOM,
       PrincipalReceivableAccountId.[CurrencyId] as PrincipalReceivableAccountId_CurrencyId,
       PrincipalReceivableAccountId.[Code] as PrincipalReceivableAccountId_Code,
       PrincipalReceivableAccountId.[Name] as PrincipalReceivableAccountId_Name,
       PrincipalReceivableAccountId.[ParentId] as PrincipalReceivableAccountId_ParentId,
       PrincipalReceivableAccountId.[ClearedBalance] as PrincipalReceivableAccountId_ClearedBalance,
       PrincipalReceivableAccountId.[UnclearedBalance] as PrincipalReceivableAccountId_UnclearedBalance,
       PrincipalReceivableAccountId.[LedgerBalance] as PrincipalReceivableAccountId_LedgerBalance,
       PrincipalReceivableAccountId.[AvailableBalance] as PrincipalReceivableAccountId_AvailableBalance,
       PrincipalReceivableAccountId.[IsOfficeAccount] as PrincipalReceivableAccountId_IsOfficeAccount,
       PrincipalReceivableAccountId.[AllowManualEntry] as PrincipalReceivableAccountId_AllowManualEntry,
       PrincipalReceivableAccountId.[IsClosed] as PrincipalReceivableAccountId_IsClosed,
       PrincipalReceivableAccountId.[DateClosed] as PrincipalReceivableAccountId_DateClosed,
       PrincipalReceivableAccountId.[ClosedByUserName] as PrincipalReceivableAccountId_ClosedByUserName,
       PrincipalReceivableAccountId.[IsActive] as PrincipalReceivableAccountId_IsActive,
       PrincipalReceivableAccountId.[CreatedByUserId] as PrincipalReceivableAccountId_CreatedByUserId,
       PrincipalReceivableAccountId.[UpdatedByUserId] as PrincipalReceivableAccountId_UpdatedByUserId,
       PrincipalReceivableAccountId.[DeletedByUserId] as PrincipalReceivableAccountId_DeletedByUserId,
       PrincipalReceivableAccountId.[IsDeleted] as PrincipalReceivableAccountId_IsDeleted,
       PrincipalReceivableAccountId.[Tags] as PrincipalReceivableAccountId_Tags,
       PrincipalReceivableAccountId.[Caption] as PrincipalReceivableAccountId_Caption,
       BankDepositAccountId.[LedgerAccountId] as BankDepositAccountId_LedgerAccountId,
       BankDepositAccountId.[BankId] as BankDepositAccountId_BankId,
       BankDepositAccountId.[BranchName] as BankDepositAccountId_BranchName,
       BankDepositAccountId.[BranchAddress] as BankDepositAccountId_BranchAddress,
       BankDepositAccountId.[CurrencyId] as BankDepositAccountId_CurrencyId,
       BankDepositAccountId.[AccountName] as BankDepositAccountId_AccountName,
       BankDepositAccountId.[AccountNo] as BankDepositAccountId_AccountNo,
       BankDepositAccountId.[AccountBVN] as BankDepositAccountId_AccountBVN,
       BankDepositAccountId.[IsActive] as BankDepositAccountId_IsActive,
       BankDepositAccountId.[CreatedByUserId] as BankDepositAccountId_CreatedByUserId,
       BankDepositAccountId.[UpdatedByUserId] as BankDepositAccountId_UpdatedByUserId,
       BankDepositAccountId.[DeletedByUserId] as BankDepositAccountId_DeletedByUserId,
       BankDepositAccountId.[IsDeleted] as BankDepositAccountId_IsDeleted,
       BankDepositAccountId.[Tags] as BankDepositAccountId_Tags,
       BankDepositAccountId.[Caption] as BankDepositAccountId_Caption,
       DisbursementAccountId.[LedgerAccountId] as DisbursementAccountId_LedgerAccountId,
       DisbursementAccountId.[BankId] as DisbursementAccountId_BankId,
       DisbursementAccountId.[BranchName] as DisbursementAccountId_BranchName,
       DisbursementAccountId.[BranchAddress] as DisbursementAccountId_BranchAddress,
       DisbursementAccountId.[CurrencyId] as DisbursementAccountId_CurrencyId,
       DisbursementAccountId.[AccountName] as DisbursementAccountId_AccountName,
       DisbursementAccountId.[AccountNo] as DisbursementAccountId_AccountNo,
       DisbursementAccountId.[AccountBVN] as DisbursementAccountId_AccountBVN,
       DisbursementAccountId.[IsActive] as DisbursementAccountId_IsActive,
       DisbursementAccountId.[CreatedByUserId] as DisbursementAccountId_CreatedByUserId,
       DisbursementAccountId.[UpdatedByUserId] as DisbursementAccountId_UpdatedByUserId,
       DisbursementAccountId.[DeletedByUserId] as DisbursementAccountId_DeletedByUserId,
       DisbursementAccountId.[IsDeleted] as DisbursementAccountId_IsDeleted,
       DisbursementAccountId.[Tags] as DisbursementAccountId_Tags,
       DisbursementAccountId.[Caption] as DisbursementAccountId_Caption

   from Loans.LoanProduct LoanProduct

            left join MasterData.Currency DefaultCurrencyId on DefaultCurrencyId.Id=LoanProduct.DefaultCurrencyId
            left join Accounting.LedgerAccount ChargesAccrualAccountId on ChargesAccrualAccountId.Id=LoanProduct.ChargesAccrualAccountId
            left join Accounting.LedgerAccount ChargesReceivableAccountId on ChargesReceivableAccountId.Id=LoanProduct.ChargesReceivableAccountId
            left join Accounting.LedgerAccount InterestLossAccountId on InterestLossAccountId.Id=LoanProduct.InterestLossAccountId
            left join Accounting.LedgerAccount InterestReceivableAccountId on InterestReceivableAccountId.Id=LoanProduct.InterestReceivableAccountId
            left join Accounting.LedgerAccount PenalInterestReceivableAccountId on PenalInterestReceivableAccountId.Id=LoanProduct.PenalInterestReceivableAccountId
            left join Accounting.LedgerAccount PrincipalLossAccountId on PrincipalLossAccountId.Id=LoanProduct.PrincipalLossAccountId
            left join Accounting.LedgerAccount PrincipalReceivableAccountId on PrincipalReceivableAccountId.Id=LoanProduct.PrincipalReceivableAccountId
            left join Accounting.CompanyBankAccount BankDepositAccountId on BankDepositAccountId.Id=LoanProduct.BankDepositAccountId
            left join Accounting.CompanyBankAccount DisbursementAccountId on DisbursementAccountId.Id=LoanProduct.DisbursementAccountId;


-- MasterData.BankMasterView source

CREATE OR ALTER VIEW MasterData.BankMasterView
as select

       ROW_NUMBER() OVER (ORDER BY Bank.[Id]) AS RowNumber,
           Bank.[Id],
       Bank.[Code],
       Bank.[Name],
       Bank.[Address],
       Bank.[ContactName],
       Bank.[ContactDetails],
       Bank.[Description],
       Bank.[IsActive],
       Bank.[CreatedByUserId],
       Bank.[DateCreated],
       Bank.[UpdatedByUserId],
       Bank.[DateUpdated],
       Bank.[DeletedByUserId],
       Bank.[IsDeleted],
       Bank.[DateDeleted],
       Bank.[RowVersion],
       Bank.[FullText],
       Bank.[Tags],
       Bank.[Caption]

   from MasterData.Bank Bank;


-- MasterData.CurrencyMasterView source

CREATE OR ALTER VIEW MasterData.CurrencyMasterView
as select

       ROW_NUMBER() OVER (ORDER BY Currency.[Id]) AS RowNumber,
           Currency.[Id],
       Currency.[Code],
       Currency.[Name],
       Currency.[Symbol],
       Currency.[IsoSymbol],
       Currency.[DecimalPlaces],
       Currency.[Format],
       Currency.[Description],
       Currency.[IsActive],
       Currency.[CreatedByUserId],
       Currency.[DateCreated],
       Currency.[UpdatedByUserId],
       Currency.[DateUpdated],
       Currency.[DeletedByUserId],
       Currency.[IsDeleted],
       Currency.[DateDeleted],
       Currency.[RowVersion],
       Currency.[FullText],
       Currency.[Tags],
       Currency.[Caption]

   from MasterData.Currency Currency;


-- MasterData.DepartmentMasterView source

CREATE OR ALTER VIEW MasterData.DepartmentMasterView
as select

       ROW_NUMBER() OVER (ORDER BY Department.[Id]) AS RowNumber,
           Department.[Id],
       Department.[Name],
       Department.[Description],
       Department.[IsActive],
       Department.[CreatedByUserId],
       Department.[DateCreated],
       Department.[UpdatedByUserId],
       Department.[DateUpdated],
       Department.[DeletedByUserId],
       Department.[IsDeleted],
       Department.[DateDeleted],
       Department.[RowVersion],
       Department.[FullText],
       Department.[Tags],
       Department.[Caption]

   from MasterData.Department Department;


-- MasterData.GlobalCodeMasterView source

CREATE OR ALTER VIEW MasterData.GlobalCodeMasterView
as select

       ROW_NUMBER() OVER (ORDER BY GlobalCode.[Id]) AS RowNumber,
           GlobalCode.[Id],
       GlobalCode.[CodeType],
       GlobalCode.[Code],
       GlobalCode.[Name],
       GlobalCode.[SystemFlag],
       GlobalCode.[Description],
       GlobalCode.[IsActive],
       GlobalCode.[CreatedByUserId],
       GlobalCode.[DateCreated],
       GlobalCode.[UpdatedByUserId],
       GlobalCode.[DateUpdated],
       GlobalCode.[DeletedByUserId],
       GlobalCode.[IsDeleted],
       GlobalCode.[DateDeleted],
       GlobalCode.[RowVersion],
       GlobalCode.[FullText],
       GlobalCode.[Tags],
       GlobalCode.[Caption]

   from MasterData.GlobalCode GlobalCode;


-- MasterData.LocationMasterView source

CREATE OR ALTER VIEW MasterData.LocationMasterView
as


with children(Id,ChildCount)
         as (
        SELECT    p.Id, Count(c.ParentID) as ChildCount
        FROM      MasterData.Location  p
                      LEFT JOIN MasterData.Location  c ON p.Id = c.ParentId
        GROUP BY  p.Id  )


select
    ROW_NUMBER() OVER (ORDER BY Location.[Id]) AS RowNumber,

        Location.[Id],
    Location.[LocationType],
    Location.[ParentId],
    Location.[Code],
    Location.[Name],
    Location.[SystemFlag],
    Location.[Description],
    Location.[IsActive],
    Location.[CreatedByUserId],
    Location.[DateCreated],
    Location.[UpdatedByUserId],
    Location.[DateUpdated],
    Location.[DeletedByUserId],
    Location.[IsDeleted],
    Location.[DateDeleted],
    Location.[RowVersion],
    Location.[FullText],
    Location.[Tags],
    Location.[Caption],
    Header.[LocationType] as Header_LocationType,
    Header.[ParentId] as Header_ParentId,
    Header.[Code] as Header_Code,
    Header.[Name] as Header_Name,
    Header.[SystemFlag] as Header_SystemFlag,
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

from MasterData.Location Location
         left join children subs on Location.Id=subs.Id
         left join MasterData.Location Header on Header.Id=Location.ParentId;


-- [Security].ApplicationRoleClaimMasterView source

CREATE OR ALTER VIEW Security.ApplicationRoleClaimMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApplicationRoleClaim.[Id]) AS RowNumber,
           ApplicationRoleClaim.[Id],
       ApplicationRoleClaim.[PermissionId],
       ApplicationRoleClaim.[RoleId],
       ApplicationRoleClaim.[ClaimType],
       ApplicationRoleClaim.[ClaimValue],
       RoleId.[IsSystemRole] as RoleId_IsSystemRole,
       RoleId.[Name] as RoleId_Name,
       RoleId.[NormalizedName] as RoleId_NormalizedName,
       RoleId.[ConcurrencyStamp] as RoleId_ConcurrencyStamp,
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
        PermissionId.[Caption] as PermissionId_Caption

        from Security.ApplicationRoleClaim ApplicationRoleClaim

        left join Security.ApplicationRole RoleId on RoleId.Id=ApplicationRoleClaim.RoleId
        left join Security.Permission PermissionId on PermissionId.Id=ApplicationRoleClaim.PermissionId;



-- [Security].ApplicationRoleMasterView source

CREATE OR ALTER VIEW Security.ApplicationRoleMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApplicationRole.[Id]) AS RowNumber,
           ApplicationRole.[Id],
       ApplicationRole.[IsSystemRole],
       ApplicationRole.[Name],
       ApplicationRole.[Code],
       ApplicationRole.[NormalizedName],
       ApplicationRole.[ConcurrencyStamp]

   from Security.ApplicationRole ApplicationRole;


-- [Security].ApplicationUserClaimMasterView source

CREATE OR ALTER VIEW Security.ApplicationUserClaimMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApplicationUserClaim.[Id]) AS RowNumber,
           ApplicationUserClaim.[Id],
       ApplicationUserClaim.[PermissionId],
       ApplicationUserClaim.[UserId],
       ApplicationUserClaim.[ClaimType],
       ApplicationUserClaim.[ClaimValue],
       UserId.[AdObjectId] as UserId_AdObjectId,
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
        PermissionId.[Caption] as PermissionId_Caption

        from Security.ApplicationUserClaim ApplicationUserClaim

        left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserClaim.UserId
        left join Security.Permission PermissionId on PermissionId.Id=ApplicationUserClaim.PermissionId;


-- [Security].ApplicationUserLoginMasterView source

CREATE OR ALTER VIEW Security.ApplicationUserLoginMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApplicationUserLogin.UserId) AS RowNumber,
           ApplicationUserLogin.[LoginProvider],
       ApplicationUserLogin.[ProviderKey],
       ApplicationUserLogin.[ProviderDisplayName],
       ApplicationUserLogin.[UserId],
       UserId.[AdObjectId] as UserId_AdObjectId,
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

   from Security.ApplicationUserLogin ApplicationUserLogin

            left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserLogin.UserId;


-- [Security].ApplicationUserMasterView source

-- [Security].ApplicationUserMasterView source

CREATE OR ALTER VIEW Security.ApplicationUserMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApplicationUser.[Id]) AS RowNumber,
           ApplicationUser.[Id],
       ApplicationUser.[AdObjectId],
       ApplicationUser.[SecondaryPhone],
       ApplicationUser.[SecondaryPhoneConfirmed],
       ApplicationUser.[UserName],
       ApplicationUser.[NormalizedUserName],
       ApplicationUser.[Email],
       ApplicationUser.[IsAdmin],
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

   from Security.ApplicationUser ApplicationUser;


-- [Security].ApplicationUserRoleMasterView source

CREATE OR ALTER VIEW Security.ApplicationUserRoleMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApplicationUserRole.UserId) AS RowNumber,
           ApplicationUserRole.[UserId],
       ApplicationUserRole.[RoleId],
       RoleId.[IsSystemRole] as RoleId_IsSystemRole,
       RoleId.[Name] as RoleId_Name,
       RoleId.[NormalizedName] as RoleId_NormalizedName,
       RoleId.[ConcurrencyStamp] as RoleId_ConcurrencyStamp,
       UserId.[AdObjectId] as UserId_AdObjectId,
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

   from Security.ApplicationUserRole ApplicationUserRole

            left join Security.ApplicationRole RoleId on RoleId.Id=ApplicationUserRole.RoleId
            left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserRole.UserId;


-- [Security].ApplicationUserTokenMasterView source

CREATE OR ALTER VIEW Security.ApplicationUserTokenMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApplicationUserToken.UserId) AS RowNumber,
           ApplicationUserToken.[UserId],
       ApplicationUserToken.[LoginProvider],
       ApplicationUserToken.[Name],
       ApplicationUserToken.[Value],
       UserId.[AdObjectId] as UserId_AdObjectId,
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

   from Security.ApplicationUserToken ApplicationUserToken

            left join Security.ApplicationUser UserId on UserId.Id=ApplicationUserToken.UserId;


-- [Security].ApprovalConsolidationMasterView source

CREATE OR ALTER VIEW [Security].[ApprovalConsolidationMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY ApprovalConsolidation.[Id]) AS RowNumber,
           ApprovalConsolidation.[Id],
       ApprovalConsolidation.[RequestName],
       ApprovalConsolidation.[Description],
       ApprovalConsolidation.[Size],
       ApprovalConsolidation.[AprrovalProcessingPage],
       ApprovalConsolidation.[DateCreated],
       ApprovalConsolidation.[UpdatedByUserId],
       ApprovalConsolidation.[DateUpdated]
   from [Security].[ApprovalConsolidation] ApprovalConsolidation;


-- [Security].ApprovalDocumentMasterView source

CREATE OR ALTER VIEW Security.ApprovalDocumentMasterView
as select

       ROW_NUMBER() OVER (ORDER BY ApprovalDocument.[Id]) AS RowNumber,
           ApprovalDocument.[Id],
       ApprovalDocument.[ApprovalId],
       ApprovalDocument.[Evidence],
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
       ApprovalId.[EventGlobalCodeId] as ApprovalId_EventGlobalCodeId,
       ApprovalId.[Module] as ApprovalId_Module,
        ApprovalId.[Payload] as ApprovalId_Payload,
        ApprovalId.[EntityPageUrl] as ApprovalId_EntityPageUrl,
        ApprovalId.[EntityType] as ApprovalId_EntityType,
        ApprovalId.[EntityId] as ApprovalId_EntityId,
        ApprovalId.[TableName] as ApprovalId_TableName,
        ApprovalId.[IsApproved] as ApprovalId_IsApproved,
        ApprovalId.[RequestedByUserId] as ApprovalId_RequestedByUserId,
        ApprovalId.[RequestDate] as ApprovalId_RequestDate,
        ApprovalId.[ProcessedByUserId] as ApprovalId_ProcessedByUserId,
        ApprovalId.[ProcessedDate] as ApprovalId_ProcessedDate,
        ApprovalId.[IsActive] as ApprovalId_IsActive,
        ApprovalId.[CreatedByUserId] as ApprovalId_CreatedByUserId,
        ApprovalId.[UpdatedByUserId] as ApprovalId_UpdatedByUserId,
        ApprovalId.[DeletedByUserId] as ApprovalId_DeletedByUserId,
        ApprovalId.[IsDeleted] as ApprovalId_IsDeleted,
        ApprovalId.[Tags] as ApprovalId_Tags,
        ApprovalId.[Caption] as ApprovalId_Caption

        from Security.ApprovalDocument ApprovalDocument

        left join Security.Approval ApprovalId on ApprovalId.Id=ApprovalDocument.ApprovalId;


-- [Security].ApprovalMasterView source

CREATE OR ALTER VIEW [Security].[ApprovalMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY Approval.[Id]) AS RowNumber,
           Approval.[Id],
       Approval.[Module],
       Approval.[ApprovalWorkflowId],
       Approval.[CurrentApprovalState],
       Approval.[Comment],
       Approval.[IsApprovalCompleted],
       Approval.[EmailTitle],
       Approval.[RequestEntityId],
       Approval.[Payload],
       Approval.[EntityPageUrl],
       Approval.[EntityType],
       Approval.[EntityId],
       Approval.[TableName],
       Approval.[RequestedByUserId],
       Approval.[RequestDate],
       Approval.[ProcessedByUserId],
       Approval.[ProcessedDate],
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
       Approval.[Caption]
-- EventGlobalCodeId.[CodeType] as EventGlobalCodeId_CodeType,
-- EventGlobalCodeId.[Code] as EventGlobalCodeId_Code,
-- EventGlobalCodeId.[Name] as EventGlobalCodeId_Name,
-- EventGlobalCodeId.[SystemFlag] as EventGlobalCodeId_SystemFlag,
-- EventGlobalCodeId.[IsActive] as EventGlobalCodeId_IsActive,
-- EventGlobalCodeId.[CreatedByUserId] as EventGlobalCodeId_CreatedByUserId,
-- EventGlobalCodeId.[UpdatedByUserId] as EventGlobalCodeId_UpdatedByUserId,
-- EventGlobalCodeId.[DeletedByUserId] as EventGlobalCodeId_DeletedByUserId,
-- EventGlobalCodeId.[IsDeleted] as EventGlobalCodeId_IsDeleted,
-- EventGlobalCodeId.[Tags] as EventGlobalCodeId_Tags,
-- EventGlobalCodeId.[Caption] as EventGlobalCodeId_Caption

   from Security.Approval Approval

--left join MasterData.GlobalCode EventGlobalCodeId on EventGlobalCodeId.Id=Approval.EventGlobalCodeId;



-- [Security].ApprovalRoleMasterView source

CREATE OR ALTER VIEW Security.ApprovalRoleMasterView
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
        RoleId.[IsSystemRole] as RoleId_IsSystemRole,
        RoleId.[Name] as RoleId_Name,
        RoleId.[NormalizedName] as RoleId_NormalizedName,
        RoleId.[ConcurrencyStamp] as RoleId_ConcurrencyStamp,
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

        from Security.ApprovalRole ApprovalRole

        left join Security.ApplicationRole RoleId on RoleId.Id=ApprovalRole.RoleId
        left join MasterData.GlobalCode EventGlobalCodeId on EventGlobalCodeId.Id=ApprovalRole.EventGlobalCodeId;


-- [Security].ApprovalWorkflowMasterView source

CREATE OR ALTER VIEW [Security].[ApprovalWorkflowMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY ApprovalWorkflow.[Id]) AS RowNumber,
           ApprovalWorkflow.[Id],
       ApprovalWorkflow.[Description],
       ApprovalWorkflow.[WorkflowName],
       ApprovalWorkflow.[Module],
       ApprovalWorkflow.[ApprovalWorkflowType],
       ApprovalWorkflow.[SLAConfigurationId],
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
       ApprovalWorkflow.[Caption],
       ApprovalWorkflow.[AreCommentsRequired],
       ApprovalWorkflow.[AreAttachmentsRequired]
   from Security.ApprovalWorkflow ApprovalWorkflow;


-- [Security].AuditTrailMasterView source

-- [Security].AuditTrailMasterView source

-- [Security].AuditTrailMasterView source

-- [Security].AuditTrailMasterView source

-- [Security].AuditTrailMasterView source

CREATE OR ALTER VIEW [Security].[AuditTrailMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY AuditTrail.[Id]) AS RowNumber,
           AuditTrail.[Id],
       AuditTrail.[ApplicationUserId],
       AuditTrail.[EventGlobalCodeId],
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
       ApplicationUserId.[SecondaryPhone] as ApplicationUserId_SecondaryPhone,
       ApplicationUserId.[SecondaryPhoneConfirmed] as ApplicationUserId_SecondaryPhoneConfirmed,
       AuditTrail.[UserName] as ApplicationUserId_UserName,
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
       ApplicationUserId.[AccessFailedCount] as ApplicationUserId_AccessFailedCount

   from Security.AuditTrail AuditTrail

       left join Security.ApplicationUser ApplicationUserId on ApplicationUserId.Id=AuditTrail.ApplicationUserId;



-- [Security].CustomerBankAccountMasterView source

-- [Security].CustomerBankAccountMasterView source

-- [Security].CustomerBankAccountMasterView source

CREATE OR ALTER VIEW Security.CustomerBankAccountMasterView
as select

       ROW_NUMBER() OVER (ORDER BY CustomerBankAccount.[Id]) AS RowNumber,
           CustomerBankAccount.[Id],
       CustomerBankAccount.[ProfileId],
       CustomerBankAccount.[MemberProfileId],
       CustomerBankAccount.[BankId],
       CustomerBankAccount.[BVN],
       CustomerBankAccount.[Branch],
       CustomerBankAccount.[SortCode],
       CustomerBankAccount.[AccountNumber],
       CustomerBankAccount.[AccountName],
       CustomerBankAccount.[Description],
       CustomerBankAccount.[IsActive],
       CustomerBankAccount.[CreatedByUserId],
       CustomerBankAccount.[DateCreated],
       CustomerBankAccount.[UpdatedByUserId],
       CustomerBankAccount.[DateUpdated],
       CustomerBankAccount.[DeletedByUserId],
       CustomerBankAccount.[IsDeleted],
       CustomerBankAccount.[DateDeleted],
       CustomerBankAccount.[RowVersion],
       CustomerBankAccount.[FullText],
       CustomerBankAccount.[Tags],
       CustomerBankAccount.[Caption],
       BankId.[Code] as BankId_Code,
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
       MemberProfileId.[ApplicationUserId] as MemberProfileId_ApplicationUserId,
       MemberProfileId.[IsKycStarted] as MemberProfileId_IsKycStarted,
       MemberProfileId.[IsKycCompleted] as MemberProfileId_IsKycCompleted,
       MemberProfileId.[KycStartDate] as MemberProfileId_KycStartDate,
       MemberProfileId.[KycCompletedDate] as MemberProfileId_KycCompletedDate,
       MemberProfileId.[Status] as MemberProfileId_Status,
       MemberProfileId.[Gender] as MemberProfileId_Gender,
       MemberProfileId.[ProfileImageUrl] as MemberProfileId_ProfileImageUrl,
       MemberProfileId.[FirstName] as MemberProfileId_FirstName,
       MemberProfileId.[LastName] as MemberProfileId_LastName,
       MemberProfileId.[MiddleName] as MemberProfileId_MiddleName,
       MemberProfileId.[CAI] as MemberProfileId_CAI,
       MemberProfileId.[RetireeNumber] as MemberProfileId_RetireeNumber,
       MemberProfileId.[Address] as MemberProfileId_Address,
       MemberProfileId.[Country] as MemberProfileId_Country,
       MemberProfileId.[State] as MemberProfileId_State,
       MemberProfileId.[DepartmentId] as MemberProfileId_DepartmentId,
       MemberProfileId.[IsActive] as MemberProfileId_IsActive,
       MemberProfileId.[CreatedByUserId] as MemberProfileId_CreatedByUserId,
       MemberProfileId.[UpdatedByUserId] as MemberProfileId_UpdatedByUserId,
       MemberProfileId.[DeletedByUserId] as MemberProfileId_DeletedByUserId,
       MemberProfileId.[IsDeleted] as MemberProfileId_IsDeleted,
       MemberProfileId.[Tags] as MemberProfileId_Tags,
       MemberProfileId.[Caption] as MemberProfileId_Caption

   from Security.CustomerBankAccount CustomerBankAccount

            left join MasterData.Bank BankId on BankId.Id=CustomerBankAccount.BankId
            left join Security.MemberProfile MemberProfileId on MemberProfileId.Id=CustomerBankAccount.MemberProfileId;



-- [Security].EnrollmentPaymentInfoMasterView source

CREATE OR ALTER VIEW Security.EnrollmentPaymentInfoMasterView
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
       ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
       ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
       ProfileId.[KycStartDate] as ProfileId_KycStartDate,
       ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
       ProfileId.[Status] as ProfileId_Status,
       ProfileId.[Gender] as ProfileId_Gender,
       ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
       ProfileId.[FirstName] as ProfileId_FirstName,
       ProfileId.[LastName] as ProfileId_LastName,
       ProfileId.[MiddleName] as ProfileId_MiddleName,
       ProfileId.[CAI] as ProfileId_CAI,
       ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
       ProfileId.[Address] as ProfileId_Address,
       ProfileId.[Country] as ProfileId_Country,
       ProfileId.[State] as ProfileId_State,
       ProfileId.[DepartmentId] as ProfileId_DepartmentId,
       ProfileId.[IsActive] as ProfileId_IsActive,
       ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
       ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
       ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
       ProfileId.[IsDeleted] as ProfileId_IsDeleted,
       ProfileId.[Tags] as ProfileId_Tags,
       ProfileId.[Caption] as ProfileId_Caption

   from Security.EnrollmentPaymentInfo EnrollmentPaymentInfo

            left join Security.MemberProfile ProfileId on ProfileId.Id=EnrollmentPaymentInfo.ProfileId;



-- [Security].MemberBeneficiaryMasterView source

-- [Security].MemberBeneficiaryMasterView source

CREATE OR ALTER VIEW Security.MemberBeneficiaryMasterView
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
       ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
       ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
       ProfileId.[KycStartDate] as ProfileId_KycStartDate,
       ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
       ProfileId.[Status] as ProfileId_Status,
       ProfileId.[Gender] as ProfileId_Gender,
       ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
       ProfileId.[FirstName] as ProfileId_FirstName,
       ProfileId.[LastName] as ProfileId_LastName,
       ProfileId.[MiddleName] as ProfileId_MiddleName,
       ProfileId.[CAI] as ProfileId_CAI,
       ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
       ProfileId.[Address] as ProfileId_Address,
       ProfileId.[Country] as ProfileId_Country,
       ProfileId.[State] as ProfileId_State,
       ProfileId.[DepartmentId] as ProfileId_DepartmentId,
       ProfileId.[IsActive] as ProfileId_IsActive,
       ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
       ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
       ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
       ProfileId.[IsDeleted] as ProfileId_IsDeleted,
       ProfileId.[Tags] as ProfileId_Tags,
       ProfileId.[Caption] as ProfileId_Caption

   from Security.MemberBeneficiary MemberBeneficiary

            left join Security.MemberProfile ProfileId on ProfileId.Id=MemberBeneficiary.ProfileId;



-- [Security].MemberBulkUploadSessionMasterView source

CREATE OR ALTER VIEW [Security].[MemberBulkUploadSessionMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY MemberBulkUploadSession.[Id]) AS RowNumber,
           MemberBulkUploadSession.[Id],
       MemberBulkUploadSession.[Id] as MemberBulkUploadSessionId,
       MemberBulkUploadSession.[ApprovedByUserId],
       MemberBulkUploadSession.[Status],
       MemberBulkUploadSession.[ApprovalId],
       MemberBulkUploadSession.[ApprovalWorkflowId],
       MemberBulkUploadSession.[Size],
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
       MemberBulkUploadSession.[Caption],
       UserId.[UserName] as UserId_UserName,
       UserId.[Email] as UserId_Email,
       ApprovalId.[Id] as ApprovalId_Id,
       ApprovalId.[IsApprovalCompleted] as ApprovalId_ApprovalStatus

   from Security.MemberBulkUploadSession MemberBulkUploadSession

       left join Security.ApplicationUser UserId on UserId.Id=MemberBulkUploadSession.CreatedByUserId
       left join Security.Approval ApprovalId on ApprovalId.Id=MemberBulkUploadSession.ApprovalId;



-- [Security].MemberNextOfKinMasterView source

-- [Security].MemberNextOfKinMasterView source

CREATE OR ALTER VIEW Security.MemberNextOfKinMasterView
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
       ProfileId.[IsKycStarted] as ProfileId_IsKycStarted,
       ProfileId.[IsKycCompleted] as ProfileId_IsKycCompleted,
       ProfileId.[KycStartDate] as ProfileId_KycStartDate,
       ProfileId.[KycCompletedDate] as ProfileId_KycCompletedDate,
       ProfileId.[Status] as ProfileId_Status,
       ProfileId.[Gender] as ProfileId_Gender,
       ProfileId.[ProfileImageUrl] as ProfileId_ProfileImageUrl,
       ProfileId.[FirstName] as ProfileId_FirstName,
       ProfileId.[LastName] as ProfileId_LastName,
       ProfileId.[MiddleName] as ProfileId_MiddleName,
       ProfileId.[CAI] as ProfileId_CAI,
       ProfileId.[RetireeNumber] as ProfileId_RetireeNumber,
       ProfileId.[Address] as ProfileId_Address,
       ProfileId.[Country] as ProfileId_Country,
       ProfileId.[State] as ProfileId_State,
       ProfileId.[DepartmentId] as ProfileId_DepartmentId,
       ProfileId.[IsActive] as ProfileId_IsActive,
       ProfileId.[CreatedByUserId] as ProfileId_CreatedByUserId,
       ProfileId.[UpdatedByUserId] as ProfileId_UpdatedByUserId,
       ProfileId.[DeletedByUserId] as ProfileId_DeletedByUserId,
       ProfileId.[IsDeleted] as ProfileId_IsDeleted,
       ProfileId.[Tags] as ProfileId_Tags,
       ProfileId.[Caption] as ProfileId_Caption

   from Security.MemberNextOfKin MemberNextOfKin

            left join Security.MemberProfile ProfileId on ProfileId.Id=MemberNextOfKin.ProfileId;



-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

-- [Security].MemberProfileMasterView source

CREATE OR ALTER VIEW Security.MemberProfileMasterView
as select

       ROW_NUMBER() OVER (ORDER BY MemberProfile.[Id]) AS RowNumber,
           MemberProfile.[Id],
       MemberProfile.[ApplicationUserId],
       MemberProfile.[IsKycStarted],
       MemberProfile.[IsKycCompleted],
       MemberProfile.[KycStartDate],
       MemberProfile.[KycCompletedDate],
       MemberProfile.[Status],
       MemberProfile.[Gender],
       MemberProfile.[ProfileImageUrl],
       MemberProfile.[PassportUrl],
       MemberProfile.[FirstName],
       MemberProfile.[LastName],
       MemberProfile.[MiddleName],
       MemberProfile.[CAI],
       MemberProfile.[RetireeNumber],
       MemberProfile.[Address],
       MemberProfile.[Country],
       MemberProfile.[State],
       MemberProfile.[DepartmentId],
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
       MemberProfile.[JobRole],
       MemberProfile.[OfficeAddress],
       MemberProfile.[PrimaryEmail],
       MemberProfile.[PrimaryPhone],
       MemberProfile.[Rank],
       MemberProfile.[ResidentialAddress],
       MemberProfile.[SecondaryEmail],
       MemberProfile.[SecondaryPhone],
       MemberProfile.[DOB],
       MemberProfile.[StateOfOrigin],
       MemberProfile.[MembershipId],
       MemberProfile.[IdentificationType],
       MemberProfile.[IdentificationNumber],
       MemberProfile.[IdentificationUrl],
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

   from Security.MemberProfile MemberProfile

            left join Security.ApplicationUser ApplicationUserId on ApplicationUserId.Id=MemberProfile.ApplicationUserId
            left join MasterData.Department DepartmentId on DepartmentId.Id=MemberProfile.DepartmentId;



-- [Security].MemberProfileViaUploadMasterView source

CREATE OR ALTER VIEW [Security].[MemberProfileViaUploadMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY MemberProfile.[Id]) AS RowNumber,
           MemberProfile.[Id],
       MemberProfile.[ApplicationUserId],
       MemberProfile.[IsKycStarted],
       MemberProfile.[IsKycCompleted],
       MemberProfile.[KycStartDate],
       MemberProfile.[KycCompletedDate],
       MemberProfile.[Status],
       MemberProfile.[Gender],
       MemberProfile.[ProfileImageUrl],
       MemberProfile.[PassportUrl],
       MemberProfile.[FirstName],
       MemberProfile.[LastName],
       MemberProfile.[MiddleName],
       MemberProfile.[CAI],
       MemberProfile.[RetireeNumber],
       MemberProfile.[Address],
       MemberProfile.[Country],
       MemberProfile.[State],
       MemberProfile.[DepartmentId],
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
       MemberProfile.[JobRole],
       MemberProfile.[OfficeAddress],
       MemberProfile.[PrimaryEmail],
       MemberProfile.[PrimaryPhone],
       MemberProfile.[Rank],
       MemberProfile.[ResidentialAddress],
       MemberProfile.[SecondaryEmail],
       MemberProfile.[SecondaryPhone],
       MemberProfile.[StateOfOrigin],
       MemberProfile.[MembershipId],

       MemberBulkUploadTempId.[MembershipNumber] as MemberBulkUploadTempId_MembershipNumber,
       MemberBulkUploadTempId.[UserRole] as MemberBulkUploadTempId_UserRole,
       MemberBulkUploadTempId.[UploadedByUserId] as MemberBulkUploadTempId_UploadedByUserId,
       MemberBulkUploadTempId.[MemberBulkUploadSessionId] as MemberBulkUploadTempId_MemberBulkUploadSessionId,
       MemberBulkUploadSessionId.[ApprovedByUserId] as MemberBulkUploadSessionId_ApprovedByUserId,
       MemberBulkUploadSessionId.[Status] as MemberBulkUploadSessionId_ApprovedStatus

   from Security.MemberProfile MemberProfile
            left join [Security].MemberBulkUploadTemp MemberBulkUploadTempId on MemberBulkUploadTempId.MembershipNumber=MemberProfile.MembershipId
       left join [Security].MemberBulkUploadSession MemberBulkUploadSessionId on MemberBulkUploadSessionId.Id=MemberBulkUploadTempId.MemberBulkUploadSessionId

   Where MemberBulkUploadSessionId.[Status] = 'APPROVED';




-- [Security].PermissionMasterView source

CREATE OR ALTER VIEW Security.PermissionMasterView
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

        from Security.Permission Permission;



-- [Security].RetireeSwitchMasterView source

-- [Security].RetireeSwitchMasterView source

CREATE OR ALTER VIEW [Security].[RetireeSwitchMasterView]
as select

       ROW_NUMBER() OVER (ORDER BY RetireeSwitch.[Id]) AS RowNumber,
           RetireeSwitch.[Id],
       RetireeSwitch.[Description],
       RetireeSwitch.[Status],
       RetireeSwitch.[MemberProfileId],
       RetireeSwitch.[IsActive],
       RetireeSwitch.[CreatedByUserId],
       RetireeSwitch.[DateCreated],
       RetireeSwitch.[UpdatedByUserId],
       RetireeSwitch.[DateUpdated],
       RetireeSwitch.[DeletedByUserId],
       RetireeSwitch.[IsDeleted],
       RetireeSwitch.[DateDeleted],
       RetireeSwitch.[RowVersion],
       RetireeSwitch.[FullText],
       RetireeSwitch.[Tags],
       RetireeSwitch.[Caption],
       ApplicationUserId.[Id] as InitiatedBy_UserId,
       ApplicationUserId.[UserName] as InitiatedBy_UserName,
       MemberProfileId.[ApplicationUserId] as MemberProfileId_ApplicationUserId,
       MemberProfileId.[Gender] as MemberProfileId_Gender,
       MemberProfileId.[ProfileImageUrl] as MemberProfileId_ProfileImageUrl,
       MemberProfileId.[FirstName] as MemberProfileId_FirstName,
       MemberProfileId.[LastName] as MemberProfileId_LastName,
       MemberProfileId.[MiddleName] as MemberProfileId_MiddleName,
       MemberProfileId.[Address] as MemberProfileId_Address,
       MemberProfileId.[Country] as MemberProfileId_Country,
       MemberProfileId.[State] as MemberProfileId_State,
       MemberProfileId.[MembershipId] as MemberProfileId_MembershipId,
       MemberProfileId.[CAI] as MemberProfileId_EmployeeNumber,
       MemberProfileId.[RetireeNumber] as MemberProfileId_RetireeNumber,
       MemberProfileId.[PrimaryEmail] as MemberProfileId_Email,
       MemberProfileId.[PrimaryPhone] as MemberProfileId_PhoneNumber,
       MemberProfileId.[IsActive] as MemberProfileId_IsActive

   from Security.RetireeSwitch RetireeSwitch

            left join Security.ApplicationUser ApplicationUserId on ApplicationUserId.Id=RetireeSwitch.InitiatedBy
            left join Security.MemberProfile MemberProfileId on MemberProfileId.Id=RetireeSwitch.MemberProfileId;
 