IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Accounting') IS NULL EXEC(N'CREATE SCHEMA [Accounting];');
GO

IF SCHEMA_ID(N'Security') IS NULL EXEC(N'CREATE SCHEMA [Security];');
GO

IF SCHEMA_ID(N'MasterData') IS NULL EXEC(N'CREATE SCHEMA [MasterData];');
GO

IF SCHEMA_ID(N'Customer') IS NULL EXEC(N'CREATE SCHEMA [Customer];');
GO

IF SCHEMA_ID(N'Deposits') IS NULL EXEC(N'CREATE SCHEMA [Deposits];');
GO

IF SCHEMA_ID(N'Loans') IS NULL EXEC(N'CREATE SCHEMA [Loans];');
GO

IF SCHEMA_ID(N'Docs') IS NULL EXEC(N'CREATE SCHEMA [Docs];');
GO

IF SCHEMA_ID(N'HR') IS NULL EXEC(N'CREATE SCHEMA [HR];');
GO

IF SCHEMA_ID(N'Payroll') IS NULL EXEC(N'CREATE SCHEMA [Payroll];');
GO

IF SCHEMA_ID(N'Core') IS NULL EXEC(N'CREATE SCHEMA [Core];');
GO

CREATE SEQUENCE [Core].[Asset] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[COGS] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Deposits] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Equity] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Expense] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[General] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Income] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Liability] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Loans] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Repayments] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [Core].[Withdrawals] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [Security].[ApplicationRole] (
    [Id] nvarchar(450) NOT NULL,
    [IsSystemRole] bit NOT NULL,
    [Code] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_ApplicationRole] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Security].[ApplicationUser] (
    [Id] nvarchar(450) NOT NULL,
    [AdObjectId] nvarchar(max) NULL,
    [IsAdmin] bit NOT NULL,
    [SecondaryPhone] nvarchar(max) NULL,
    [SecondaryPhoneConfirmed] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_ApplicationUser] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Security].[ApprovalEmailAlert] (
    [Id] nvarchar(40) NOT NULL,
    [ApprovalId] nvarchar(40) NOT NULL,
    [ApprovalWorkflowId] nvarchar(max) NULL,
    [EmailTitle] nvarchar(80) NOT NULL,
    [EmailBody] nvarchar(800) NOT NULL,
    [TaskCompleted] bit NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalEmailAlert] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Security].[ApprovalWorkflow] (
    [Id] nvarchar(40) NOT NULL,
    [WorkflowName] nvarchar(80) NOT NULL,
    [IsDefaultApprovalWorkflow] bit NOT NULL,
    [RequiredApprovers] int NOT NULL,
    [RequiredGroups] int NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalWorkflow] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MasterData].[Bank] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(16) NOT NULL,
    [SortCode] nvarchar(max) NULL,
    [Name] nvarchar(64) NOT NULL,
    [Address] nvarchar(128) NULL,
    [ContactName] nvarchar(128) NULL,
    [ContactDetails] nvarchar(128) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Bank] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MasterData].[Currency] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(8) NOT NULL,
    [Name] nvarchar(64) NOT NULL,
    [Symbol] nvarchar(8) NOT NULL,
    [IsoSymbol] nvarchar(10) NULL,
    [DecimalPlaces] int NOT NULL,
    [Format] nvarchar(16) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MasterData].[Department] (
    [Id] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Docs].[DocumentType] (
    [Id] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [SystemFlag] bit NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_DocumentType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Accounting].[FinancialCalendar] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(20) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [IsCurrent] bit NOT NULL,
    [IsClosed] bit NOT NULL,
    [ClosedByUserName] nvarchar(64) NULL,
    [DateClosed] datetime2 NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FinancialCalendar] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MasterData].[GlobalCode] (
    [Id] nvarchar(40) NOT NULL,
    [CodeType] nvarchar(100) NOT NULL,
    [Code] nvarchar(64) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [SystemFlag] bit NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_GlobalCode] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Accounting].[LienType] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LienType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MasterData].[Location] (
    [Id] nvarchar(40) NOT NULL,
    [LocationType] nvarchar(100) NOT NULL,
    [ParentId] nvarchar(40) NULL,
    [Code] nvarchar(64) NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [SystemFlag] bit NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Location_Location_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [MasterData].[Location] ([Id])
);
GO

CREATE TABLE [Security].[MemberBulkUploadSession] (
    [Id] nvarchar(40) NOT NULL,
    [ApprovedByUserId] nvarchar(max) NULL,
    [Size] int NOT NULL,
    [Status] nvarchar(max) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_MemberBulkUploadSession] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Security].[MemberBulkUploadTemp] (
    [Id] nvarchar(40) NOT NULL,
    [RecordId] int NOT NULL,
    [LastName] nvarchar(128) NULL,
    [FirstName] nvarchar(128) NULL,
    [Gender] nvarchar(32) NULL,
    [Email] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [MembershipNumber] nvarchar(128) NULL,
    [UserRole] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [Status] nvarchar(32) NULL,
    [IsValid] bit NOT NULL,
    [UploadedByUserId] nvarchar(max) NULL,
    [MemberBulkUploadSessionId] nvarchar(max) NULL,
    [ApprovalId] nvarchar(max) NULL,
    [IsSuccessfullyRegistered] bit NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_MemberBulkUploadTemp] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Accounting].[PaymentMode] (
    [Id] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Channel] nvarchar(100) NOT NULL DEFAULT N'CASH',
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_PaymentMode] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Security].[Permission] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(128) NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Group] nvarchar(256) NULL,
    [Category] nvarchar(256) NULL,
    [Module] nvarchar(256) NULL,
    [Owner] nvarchar(256) NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Security].[ApplicationUserLogin] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_ApplicationUserLogin] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_ApplicationUserLogin_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[ApplicationUser] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[ApplicationUserRole] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_ApplicationUserRole] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_ApplicationUserRole_ApplicationRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[ApplicationRole] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApplicationUserRole_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[ApplicationUser] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[ApplicationUserToken] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_ApplicationUserToken] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_ApplicationUserToken_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[ApplicationUser] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[Approval] (
    [Id] nvarchar(40) NOT NULL,
    [Module] nvarchar(256) NOT NULL,
    [ApprovalType] nvarchar(200) NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [CurrentSequence] int NOT NULL,
    [ApprovalWorkflowId] nvarchar(40) NOT NULL,
    [Payload] nvarchar(max) NOT NULL,
    [IsApprovalCompleted] bit NOT NULL,
    [Comment] nvarchar(128) NULL,
    [EntityId] nvarchar(100) NULL,
    [TriedCount] int NOT NULL DEFAULT 0,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Approval] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Approval_ApprovalWorkflow_ApprovalWorkflowId] FOREIGN KEY ([ApprovalWorkflowId]) REFERENCES [Security].[ApprovalWorkflow] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[ApprovalGroup] (
    [Id] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [ApprovalWorkflowId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalGroup] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalGroup_ApprovalWorkflow_ApprovalWorkflowId] FOREIGN KEY ([ApprovalWorkflowId]) REFERENCES [Security].[ApprovalWorkflow] ([Id])
);
GO

CREATE TABLE [Security].[ApprovalNotification] (
    [Id] nvarchar(40) NOT NULL,
    [ApprovalWorkflowId] nvarchar(40) NULL,
    [Reminder] nvarchar(max) NOT NULL,
    [Escalation] nvarchar(max) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalNotification] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalNotification_ApprovalWorkflow_ApprovalWorkflowId] FOREIGN KEY ([ApprovalWorkflowId]) REFERENCES [Security].[ApprovalWorkflow] ([Id])
);
GO

CREATE TABLE [MasterData].[Charge] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(32) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Method] nvarchar(100) NOT NULL DEFAULT N'FLAT',
    [Target] nvarchar(max) NOT NULL DEFAULT N'VALUE',
    [CalculationMethod] nvarchar(100) NOT NULL DEFAULT N'ADD',
    [CurrencyId] nvarchar(40) NOT NULL,
    [ChargeValue] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MaximumCharge] decimal(18,2) NULL,
    [MinimimumCharge] decimal(18,2) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Charge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Charge_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [MasterData].[Currency] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounting].[LedgerAccount] (
    [Id] nvarchar(40) NOT NULL,
    [AccountType] nvarchar(100) NOT NULL,
    [UOM] nvarchar(100) NOT NULL,
    [CurrencyId] nvarchar(40) NOT NULL,
    [Code] nvarchar(100) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [ParentId] nvarchar(40) NULL,
    [ClearedBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [UnclearedBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [LedgerBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [AvailableBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [IsOfficeAccount] bit NOT NULL DEFAULT CAST(0 AS bit),
    [AllowManualEntry] bit NOT NULL DEFAULT CAST(1 AS bit),
    [IsClosed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [DateClosed] datetime2 NULL,
    [ClosedByUserName] nvarchar(128) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LedgerAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LedgerAccount_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [MasterData].[Currency] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LedgerAccount_LedgerAccount_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Accounting].[LedgerAccount] ([Id])
);
GO

CREATE TABLE [Security].[MemberProfile] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationUserId] nvarchar(450) NOT NULL,
    [YearsOfService] int NOT NULL,
    [IsKycStarted] bit NOT NULL,
    [IsKycCompleted] bit NOT NULL,
    [KycStartDate] datetime2 NULL,
    [KycCompletedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [MemberType] nvarchar(100) NOT NULL,
    [Gender] nvarchar(32) NOT NULL,
    [ProfileImageUrl] nvarchar(max) NULL,
    [PassportUrl] nvarchar(max) NULL,
    [IdentificationType] nvarchar(max) NULL,
    [IdentificationNumber] nvarchar(max) NULL,
    [IdentificationUrl] nvarchar(max) NULL,
    [KycSubmitted] bit NOT NULL,
    [KycSubmittedOn] datetime2 NULL,
    [KycApproved] bit NOT NULL,
    [KycApprovedOn] datetime2 NULL,
    [KycApprovedBy] bit NOT NULL,
    [FirstName] nvarchar(128) NULL,
    [LastName] nvarchar(128) NULL,
    [MiddleName] nvarchar(128) NULL,
    [DepartmentId] nvarchar(40) NULL,
    [MembershipId] nvarchar(max) NULL,
    [CAI] nvarchar(max) NULL,
    [RetireeNumber] nvarchar(max) NULL,
    [StateOfOrigin] nvarchar(max) NULL,
    [PrimaryEmail] nvarchar(max) NULL,
    [SecondaryEmail] nvarchar(max) NULL,
    [PrimaryPhone] nvarchar(max) NULL,
    [SecondaryPhone] nvarchar(max) NULL,
    [ResidentialAddress] nvarchar(max) NULL,
    [OfficeAddress] nvarchar(max) NULL,
    [Rank] nvarchar(max) NULL,
    [SwitchToRetireeRequested] bit NOT NULL DEFAULT CAST(0 AS bit),
    [JobRole] nvarchar(max) NULL,
    [DOB] datetimeoffset NULL,
    [Address] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_MemberProfile] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MemberProfile_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Security].[ApplicationUser] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MemberProfile_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [MasterData].[Department] ([Id])
);
GO

CREATE TABLE [Docs].[OfficeDocument] (
    [Id] nvarchar(40) NOT NULL,
    [DocumentNo] nvarchar(32) NOT NULL,
    [DocumentTypeId] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [DocumentData] varbinary(max) NULL,
    [MimeType] nvarchar(32) NULL,
    [FilePath] nvarchar(200) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_OfficeDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OfficeDocument_DocumentType_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [Docs].[DocumentType] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Docs].[OfficePhoto] (
    [Id] nvarchar(40) NOT NULL,
    [DocumentNo] nvarchar(32) NOT NULL,
    [DocumentTypeId] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [DocumentData] varbinary(max) NULL,
    [MimeType] nvarchar(32) NULL,
    [FilePath] nvarchar(200) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_OfficePhoto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OfficePhoto_DocumentType_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [Docs].[DocumentType] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Docs].[OfficeSheet] (
    [Id] nvarchar(40) NOT NULL,
    [DocumentNo] nvarchar(32) NOT NULL,
    [DocumentTypeId] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [DocumentData] varbinary(max) NULL,
    [MimeType] nvarchar(32) NULL,
    [FilePath] nvarchar(200) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_OfficeSheet] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OfficeSheet_DocumentType_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [Docs].[DocumentType] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounting].[AccountingPeriod] (
    [Id] nvarchar(40) NOT NULL,
    [CalendarId] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [IsCurrent] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsClosed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ClosedByUserName] nvarchar(128) NULL,
    [DateClosed] datetime2 NULL,
    [FinancialCalendarId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_AccountingPeriod] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountingPeriod_FinancialCalendar_CalendarId] FOREIGN KEY ([CalendarId]) REFERENCES [Accounting].[FinancialCalendar] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AccountingPeriod_FinancialCalendar_FinancialCalendarId] FOREIGN KEY ([FinancialCalendarId]) REFERENCES [Accounting].[FinancialCalendar] ([Id])
);
GO

CREATE TABLE [Security].[ApprovalRole] (
    [Id] nvarchar(40) NOT NULL,
    [EventGlobalCodeId] nvarchar(40) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    [Order] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalRole] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalRole_ApplicationRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[ApplicationRole] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApprovalRole_GlobalCode_EventGlobalCodeId] FOREIGN KEY ([EventGlobalCodeId]) REFERENCES [MasterData].[GlobalCode] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[AuditTrail] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    [EventGlobalCodeId] nvarchar(40) NULL,
    [UserName] nvarchar(max) NULL,
    [Timestamp] datetime2 NOT NULL,
    [EventType] nvarchar(max) NULL,
    [TableName] nvarchar(max) NULL,
    [PrimaryKey] nvarchar(max) NULL,
    [OldValues] nvarchar(max) NULL,
    [NewValues] nvarchar(max) NULL,
    [AuditJson] nvarchar(max) NULL,
    [Module] nvarchar(128) NULL,
    [Payload] nvarchar(max) NULL,
    [Action] nvarchar(max) NULL,
    [IPAddress] nvarchar(max) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_AuditTrail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AuditTrail_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_AuditTrail_GlobalCode_EventGlobalCodeId] FOREIGN KEY ([EventGlobalCodeId]) REFERENCES [MasterData].[GlobalCode] ([Id])
);
GO

CREATE TABLE [Security].[ApplicationRoleClaim] (
    [Id] int NOT NULL IDENTITY,
    [PermissionId] nvarchar(40) NULL,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_ApplicationRoleClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApplicationRoleClaim_ApplicationRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[ApplicationRole] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApplicationRoleClaim_Permission_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Security].[Permission] ([Id])
);
GO

CREATE TABLE [Security].[ApplicationUserClaim] (
    [Id] int NOT NULL IDENTITY,
    [PermissionId] nvarchar(40) NULL,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_ApplicationUserClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApplicationUserClaim_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[ApplicationUser] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApplicationUserClaim_Permission_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Security].[Permission] ([Id])
);
GO

CREATE TABLE [Security].[ApprovalDocument] (
    [Id] nvarchar(40) NOT NULL,
    [ApprovalId] nvarchar(40) NOT NULL,
    [Document] varbinary(max) NOT NULL,
    [MimeType] nvarchar(64) NOT NULL,
    [FileName] nvarchar(256) NOT NULL,
    [FileSize] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalDocument_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounting].[TransactionJournal] (
    [Id] nvarchar(40) NOT NULL,
    [TransactionNo] nvarchar(50) NOT NULL,
    [Title] nvarchar(256) NOT NULL,
    [TransactionType] nvarchar(100) NOT NULL DEFAULT N'GENERAL',
    [DocumentRef] nvarchar(128) NULL,
    [DocumentRefId] nvarchar(128) NULL,
    [PostingRef] nvarchar(128) NULL,
    [PostingRefId] nvarchar(128) NULL,
    [EntityRef] nvarchar(128) NULL,
    [EntityRefId] nvarchar(128) NULL,
    [TransactionDate] datetime2 NOT NULL,
    [IsPosted] bit NOT NULL,
    [PostedByUserId] nvarchar(450) NULL,
    [DatePosted] datetime2 NULL,
    [IsReversed] bit NOT NULL,
    [ReversedByUserId] nvarchar(450) NULL,
    [ReversalDate] datetime2 NULL,
    [ApprovalId] nvarchar(40) NULL,
    [ApprovalDate] datetime2 NULL,
    [Memo] nvarchar(512) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_TransactionJournal] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransactionJournal_ApplicationUser_PostedByUserId] FOREIGN KEY ([PostedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_TransactionJournal_ApplicationUser_ReversedByUserId] FOREIGN KEY ([ReversedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_TransactionJournal_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id])
);
GO

CREATE TABLE [Security].[ApprovalGroupMember] (
    [Id] nvarchar(40) NOT NULL,
    [ApprovalGroupId] nvarchar(40) NOT NULL,
    [ApprovalSequence] int NOT NULL,
    [ApplicationUserId] nvarchar(450) NOT NULL,
    [ApprovalGroupId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalGroupMember] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalGroupMember_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Security].[ApplicationUser] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApprovalGroupMember_ApprovalGroup_ApprovalGroupId] FOREIGN KEY ([ApprovalGroupId]) REFERENCES [Security].[ApprovalGroup] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApprovalGroupMember_ApprovalGroup_ApprovalGroupId1] FOREIGN KEY ([ApprovalGroupId1]) REFERENCES [Security].[ApprovalGroup] ([Id])
);
GO

CREATE TABLE [Security].[ApprovalGroupWorkflow] (
    [Id] nvarchar(40) NOT NULL,
    [ApprovalWorkflowId] nvarchar(40) NOT NULL,
    [ApprovalGroupId] nvarchar(40) NOT NULL,
    [Sequence] int NOT NULL,
    [RequiredApprovers] int NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalGroupWorkflow] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalGroupWorkflow_ApprovalGroup_ApprovalGroupId] FOREIGN KEY ([ApprovalGroupId]) REFERENCES [Security].[ApprovalGroup] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApprovalGroupWorkflow_ApprovalWorkflow_ApprovalWorkflowId] FOREIGN KEY ([ApprovalWorkflowId]) REFERENCES [Security].[ApprovalWorkflow] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[ApprovalLog] (
    [Id] nvarchar(40) NOT NULL,
    [ApprovalId] nvarchar(40) NOT NULL,
    [Sequence] int NOT NULL,
    [ApprovalGroupId] nvarchar(40) NULL,
    [ApprovedByUserId] nvarchar(450) NOT NULL,
    [DateApproved] datetime2 NOT NULL,
    [Status] nvarchar(32) NOT NULL,
    [Comment] nvarchar(1024) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_ApprovalLog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApprovalLog_ApplicationUser_ApprovedByUserId] FOREIGN KEY ([ApprovedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ApprovalLog_ApprovalGroup_ApprovalGroupId] FOREIGN KEY ([ApprovalGroupId]) REFERENCES [Security].[ApprovalGroup] ([Id]),
    CONSTRAINT [FK_ApprovalLog_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounting].[CompanyBankAccount] (
    [Id] nvarchar(40) NOT NULL,
    [LedgerAccountId] nvarchar(40) NOT NULL,
    [BankId] nvarchar(40) NOT NULL,
    [BranchName] nvarchar(128) NULL,
    [BranchAddress] nvarchar(128) NULL,
    [CurrencyId] nvarchar(40) NOT NULL,
    [AccountName] nvarchar(128) NOT NULL,
    [AccountNumber] nvarchar(32) NOT NULL,
    [BVN] nvarchar(32) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_CompanyBankAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CompanyBankAccount_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [MasterData].[Bank] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CompanyBankAccount_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [MasterData].[Currency] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CompanyBankAccount_LedgerAccount_LedgerAccountId] FOREIGN KEY ([LedgerAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Customer].[Customer] (
    [Id] nvarchar(40) NOT NULL,
    [CustomerNo] nvarchar(max) NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    [CashAccountId] nvarchar(40) NOT NULL,
    [YearsOfService] int NOT NULL,
    [IsKycStarted] bit NOT NULL,
    [IsKycCompleted] bit NOT NULL,
    [KycStartDate] datetime2 NULL,
    [KycCompletedDate] datetime2 NULL,
    [Status] nvarchar(32) NOT NULL,
    [MemberType] nvarchar(32) NOT NULL,
    [Gender] nvarchar(32) NOT NULL,
    [ProfileImageUrl] nvarchar(max) NULL,
    [PassportUrl] nvarchar(max) NULL,
    [IdentificationType] nvarchar(max) NULL,
    [IdentificationNumber] nvarchar(max) NULL,
    [IdentificationUrl] nvarchar(max) NULL,
    [KycSubmitted] bit NOT NULL,
    [KycSubmittedOn] datetime2 NULL,
    [KycApproved] bit NOT NULL,
    [KycApprovedOn] datetime2 NULL,
    [KycApprovedBy] bit NOT NULL,
    [FirstName] nvarchar(128) NULL,
    [LastName] nvarchar(128) NULL,
    [MiddleName] nvarchar(128) NULL,
    [DepartmentId] nvarchar(40) NULL,
    [MemberId] nvarchar(max) NULL,
    [CAI] nvarchar(max) NULL,
    [RetireeNumber] nvarchar(max) NULL,
    [StateOfOrigin] nvarchar(max) NULL,
    [PrimaryEmail] nvarchar(max) NULL,
    [SecondaryEmail] nvarchar(max) NULL,
    [PrimaryPhone] nvarchar(max) NULL,
    [SecondaryPhone] nvarchar(max) NULL,
    [ResidentialAddress] nvarchar(max) NULL,
    [OfficeAddress] nvarchar(max) NULL,
    [Rank] nvarchar(max) NULL,
    [JobRole] nvarchar(max) NULL,
    [DOB] datetimeoffset NULL,
    [Address] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customer_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Customer_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [MasterData].[Department] ([Id]),
    CONSTRAINT [FK_Customer_LedgerAccount_CashAccountId] FOREIGN KEY ([CashAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Security].[EnrollmentPaymentInfo] (
    [Id] nvarchar(450) NOT NULL,
    [ProfileId] nvarchar(max) NULL,
    [MemberProfileId] nvarchar(40) NULL,
    [Evidence] varbinary(max) NOT NULL,
    [MimeType] nvarchar(64) NOT NULL,
    [FileName] nvarchar(256) NOT NULL,
    [FileSize] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_EnrollmentPaymentInfo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EnrollmentPaymentInfo_MemberProfile_MemberProfileId] FOREIGN KEY ([MemberProfileId]) REFERENCES [Security].[MemberProfile] ([Id])
);
GO

CREATE TABLE [Security].[MemberBankAccount] (
    [Id] nvarchar(40) NOT NULL,
    [ProfileId] nvarchar(40) NULL,
    [BankId] nvarchar(40) NULL,
    [AccountName] nvarchar(128) NOT NULL,
    [AccountNumber] nvarchar(50) NOT NULL,
    [BVN] nvarchar(32) NULL,
    [Branch] nvarchar(64) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_MemberBankAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MemberBankAccount_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [MasterData].[Bank] ([Id]),
    CONSTRAINT [FK_MemberBankAccount_MemberProfile_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Security].[MemberProfile] ([Id])
);
GO

CREATE TABLE [Security].[MemberBeneficiary] (
    [Id] nvarchar(40) NOT NULL,
    [ProfileId] nvarchar(40) NULL,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [Email] nvarchar(128) NULL,
    [Phone] nvarchar(32) NOT NULL,
    [Address] nvarchar(512) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_MemberBeneficiary] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MemberBeneficiary_MemberProfile_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Security].[MemberProfile] ([Id])
);
GO

CREATE TABLE [Security].[MemberNextOfKin] (
    [Id] nvarchar(40) NOT NULL,
    [ProfileId] nvarchar(40) NULL,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [Email] nvarchar(128) NULL,
    [Phone] nvarchar(32) NOT NULL,
    [Relationship] nvarchar(128) NOT NULL,
    [Address] nvarchar(512) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_MemberNextOfKin] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MemberNextOfKin_MemberProfile_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Security].[MemberProfile] ([Id])
);
GO

CREATE TABLE [Accounting].[JournalEntry] (
    [Id] nvarchar(40) NOT NULL,
    [TransactionEntryNo] nvarchar(32) NOT NULL,
    [TransactionJournalId] nvarchar(40) NOT NULL,
    [AccountId] nvarchar(40) NOT NULL,
    [EntryType] nvarchar(100) NOT NULL,
    [DecimalPlaces] int NOT NULL,
    [Debit] decimal(18,2) NOT NULL,
    [Credit] decimal(18,2) NOT NULL,
    [TransactionDate] datetime2 NOT NULL,
    [IsPosted] bit NOT NULL,
    [PostedByUserName] nvarchar(max) NULL,
    [DatePosted] datetime2 NULL,
    [IsReversed] bit NOT NULL,
    [ReversedByUserName] nvarchar(max) NULL,
    [ReversalDate] datetime2 NULL,
    [Memo] nvarchar(512) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_JournalEntry] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_JournalEntry_LedgerAccount_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_JournalEntry_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounting].[TransactionDocument] (
    [Id] nvarchar(40) NOT NULL,
    [DocumentNo] nvarchar(32) NOT NULL,
    [TransactionJournalId] nvarchar(40) NOT NULL,
    [DocumentTypeId] nvarchar(40) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Document] varbinary(max) NULL,
    [DocumentUrl] nvarchar(2000) NULL,
    [TransactionJournalId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_TransactionDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransactionDocument_DocumentType_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [Docs].[DocumentType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransactionDocument_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransactionDocument_TransactionJournal_TransactionJournalId1] FOREIGN KEY ([TransactionJournalId1]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[DepositProduct] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(32) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [ShortName] nvarchar(128) NOT NULL,
    [ApprovalWorkflowId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [MinimumAge] int NOT NULL,
    [MaximumAge] int NOT NULL,
    [Tenure] nvarchar(100) NOT NULL DEFAULT N'NONE',
    [TenureValue] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Status] nvarchar(100) NOT NULL DEFAULT N'PENDING_APPROVAL',
    [PublicationType] nvarchar(100) NOT NULL DEFAULT N'ALL',
    [PublishedByUserId] nvarchar(450) NULL,
    [DefaultCurrencyId] nvarchar(40) NOT NULL,
    [BankDepositAccountId] nvarchar(40) NULL,
    [ProductDepositAccountId] nvarchar(40) NOT NULL,
    [ChargesIncomeAccountId] nvarchar(40) NOT NULL,
    [ChargesAccrualAccountId] nvarchar(40) NOT NULL,
    [ChargesWaivedAccountId] nvarchar(40) NOT NULL,
    [InterestPayableAccountId] nvarchar(40) NOT NULL,
    [InterestPayoutAccountId] nvarchar(40) NOT NULL,
    [IsInterestEnabled] bit NOT NULL,
    [MinimumContributionRegular] decimal(18,2) NULL,
    [MinimumContributionRetiree] decimal(18,2) NULL,
    [ProductType] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_DepositProduct] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DepositProduct_ApplicationUser_PublishedByUserId] FOREIGN KEY ([PublishedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_DepositProduct_ApprovalWorkflow_ApprovalWorkflowId] FOREIGN KEY ([ApprovalWorkflowId]) REFERENCES [Security].[ApprovalWorkflow] ([Id]),
    CONSTRAINT [FK_DepositProduct_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_DepositProduct_CompanyBankAccount_BankDepositAccountId] FOREIGN KEY ([BankDepositAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProduct_Currency_DefaultCurrencyId] FOREIGN KEY ([DefaultCurrencyId]) REFERENCES [MasterData].[Currency] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProduct_LedgerAccount_ChargesAccrualAccountId] FOREIGN KEY ([ChargesAccrualAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProduct_LedgerAccount_ChargesIncomeAccountId] FOREIGN KEY ([ChargesIncomeAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProduct_LedgerAccount_ChargesWaivedAccountId] FOREIGN KEY ([ChargesWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProduct_LedgerAccount_InterestPayableAccountId] FOREIGN KEY ([InterestPayableAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProduct_LedgerAccount_InterestPayoutAccountId] FOREIGN KEY ([InterestPayoutAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProduct_LedgerAccount_ProductDepositAccountId] FOREIGN KEY ([ProductDepositAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[LoanProduct] (
    [Id] nvarchar(40) NOT NULL,
    [Code] nvarchar(32) NOT NULL,
    [PayrollCode] nvarchar(max) NULL,
    [Name] nvarchar(128) NOT NULL,
    [ShortName] nvarchar(128) NOT NULL,
    [ApprovalWorkflowId] nvarchar(40) NOT NULL,
    [ApprovalId] nvarchar(40) NULL,
    [PrincipalMultiple] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PrincipalMinLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PrincipalMaxLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TenureUnit] nvarchar(100) NOT NULL,
    [MinTenureValue] decimal(18,2) NOT NULL,
    [MaxTenureValue] decimal(18,2) NOT NULL,
    [RepaymentPeriod] nvarchar(100) NOT NULL DEFAULT N'MONTHLY',
    [InterestMethod] nvarchar(100) NOT NULL,
    [InterestCalculationMethod] nvarchar(100) NOT NULL DEFAULT N'FLAT_RATE',
    [DaysInYear] decimal(18,2) NOT NULL DEFAULT 365.0,
    [InterestRate] decimal(18,2) NOT NULL DEFAULT 0.0,
    [HasAdminCharges] bit NOT NULL,
    [IsTargetLoan] bit NOT NULL,
    [BenefitCode] nvarchar(32) NULL,
    [AllowedOffsetType] nvarchar(100) NOT NULL,
    [OffsetPeriodUnit] nvarchar(100) NOT NULL,
    [OffsetPeriodValue] decimal(18,2) NOT NULL,
    [EnableSavingsOffset] bit NOT NULL,
    [EnableChargeWaiver] bit NOT NULL,
    [EnableTopUp] bit NOT NULL,
    [EnableTopUpCharges] bit NOT NULL,
    [EnableAdminOffsetCharge] bit NOT NULL,
    [EnableWaitingPeriod] bit NOT NULL,
    [WaitingPeriodUnit] nvarchar(100) NOT NULL,
    [WaitingPeriodValue] decimal(18,2) NOT NULL,
    [EnableWaitingPeriodCharge] bit NOT NULL,
    [IsGuarantorRequired] bit NOT NULL,
    [GuarantorMinYear] int NOT NULL,
    [GuarantorAmountLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [EmployeeGuarantorCount] int NOT NULL,
    [NonEmployeeGuarantorCount] int NOT NULL,
    [QualificationTargetProduct] nvarchar(100) NOT NULL,
    [QualificationMinBalancePercentage] decimal(18,2) NOT NULL DEFAULT 0.0,
    [SavingsOffSetProductIdList] nvarchar(max) NULL,
    [MemberTypeIdList] nvarchar(max) NULL,
    [MinimumAge] int NULL DEFAULT 0,
    [MaximumAge] int NULL DEFAULT 0,
    [DefaultCurrencyId] nvarchar(40) NULL,
    [LoanProductType] nvarchar(100) NOT NULL,
    [BankDepositAccountId] nvarchar(40) NULL,
    [DisbursementAccountId] nvarchar(40) NULL,
    [PrincipalAccountId] nvarchar(40) NOT NULL,
    [PrincipalLossAccountId] nvarchar(40) NOT NULL,
    [InterestIncomeAccountId] nvarchar(40) NOT NULL,
    [UnearnedInterestAccountId] nvarchar(40) NOT NULL,
    [InterestLossAccountId] nvarchar(40) NOT NULL,
    [PenalInterestReceivableAccountId] nvarchar(40) NOT NULL,
    [InterestWaivedAccountId] nvarchar(40) NOT NULL,
    [ChargesIncomeAccountId] nvarchar(40) NOT NULL,
    [ChargesAccrualAccountId] nvarchar(40) NOT NULL,
    [ChargesWaivedAccountId] nvarchar(40) NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [PublicationType] int NOT NULL,
    [PublishedByUserId] nvarchar(450) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanProduct] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanProduct_ApplicationUser_PublishedByUserId] FOREIGN KEY ([PublishedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_LoanProduct_ApprovalWorkflow_ApprovalWorkflowId] FOREIGN KEY ([ApprovalWorkflowId]) REFERENCES [Security].[ApprovalWorkflow] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_LoanProduct_CompanyBankAccount_BankDepositAccountId] FOREIGN KEY ([BankDepositAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id]),
    CONSTRAINT [FK_LoanProduct_CompanyBankAccount_DisbursementAccountId] FOREIGN KEY ([DisbursementAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id]),
    CONSTRAINT [FK_LoanProduct_Currency_DefaultCurrencyId] FOREIGN KEY ([DefaultCurrencyId]) REFERENCES [MasterData].[Currency] ([Id]),
    CONSTRAINT [FK_LoanProduct_LedgerAccount_ChargesAccrualAccountId] FOREIGN KEY ([ChargesAccrualAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_ChargesIncomeAccountId] FOREIGN KEY ([ChargesIncomeAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_ChargesWaivedAccountId] FOREIGN KEY ([ChargesWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_InterestIncomeAccountId] FOREIGN KEY ([InterestIncomeAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_InterestLossAccountId] FOREIGN KEY ([InterestLossAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_InterestWaivedAccountId] FOREIGN KEY ([InterestWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_PenalInterestReceivableAccountId] FOREIGN KEY ([PenalInterestReceivableAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_PrincipalAccountId] FOREIGN KEY ([PrincipalAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_PrincipalLossAccountId] FOREIGN KEY ([PrincipalLossAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProduct_LedgerAccount_UnearnedInterestAccountId] FOREIGN KEY ([UnearnedInterestAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Payroll].[PayrollDeductionSchedule] (
    [Id] nvarchar(40) NOT NULL,
    [ScheduleName] nvarchar(256) NOT NULL,
    [ScheduleType] nvarchar(100) NOT NULL,
    [BankAccountId] nvarchar(40) NULL,
    [SpecialDepositBankAccountId] nvarchar(40) NULL,
    [FixedDepositBankAccountId] nvarchar(40) NULL,
    [DeductionsCount] int NOT NULL,
    [TotalDeductions] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MinDecimalPlace] int NOT NULL DEFAULT 1,
    [MaxDecimalPlace] int NOT NULL DEFAULT 1,
    [AdviseDate] datetime2 NOT NULL,
    [ExpectedDate] datetime2 NOT NULL,
    [IsPosted] bit NOT NULL,
    [PayrollDate] datetime2 NOT NULL,
    [IsUploaded] bit NOT NULL,
    [LastUploadedDate] datetime2 NOT NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NOT NULL,
    [GenerateDeductionCronJobStatus] int NOT NULL,
    [GenerateDeductionCronJobStartedDate] datetime2 NULL,
    [GenerateDeductionCronJobCompletedDate] datetime2 NULL,
    [ProcessDeductionCronJobStatus] int NOT NULL,
    [ProcessDeductionCronJobStartedDate] datetime2 NULL,
    [ProcessDeductionCronJobCompletedDate] datetime2 NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_PayrollDeductionSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PayrollDeductionSchedule_CompanyBankAccount_BankAccountId] FOREIGN KEY ([BankAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id]),
    CONSTRAINT [FK_PayrollDeductionSchedule_CompanyBankAccount_FixedDepositBankAccountId] FOREIGN KEY ([FixedDepositBankAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id]),
    CONSTRAINT [FK_PayrollDeductionSchedule_CompanyBankAccount_SpecialDepositBankAccountId] FOREIGN KEY ([SpecialDepositBankAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id])
);
GO

CREATE TABLE [Customer].[CustomerBankAccount] (
    [Id] nvarchar(40) NOT NULL,
    [LedgerAccountId] nvarchar(40) NULL,
    [CustomerId] nvarchar(40) NULL,
    [BankId] nvarchar(40) NULL,
    [AccountName] nvarchar(128) NOT NULL,
    [AccountNumber] nvarchar(50) NOT NULL,
    [BVN] nvarchar(32) NULL,
    [Branch] nvarchar(64) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_CustomerBankAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerBankAccount_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [MasterData].[Bank] ([Id]),
    CONSTRAINT [FK_CustomerBankAccount_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]),
    CONSTRAINT [FK_CustomerBankAccount_LedgerAccount_LedgerAccountId] FOREIGN KEY ([LedgerAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id])
);
GO

CREATE TABLE [Customer].[CustomerBeneficiary] (
    [Id] nvarchar(40) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [Email] nvarchar(128) NULL,
    [Phone] nvarchar(32) NOT NULL,
    [Address] nvarchar(512) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_CustomerBeneficiary] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerBeneficiary_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Customer].[CustomerNextOfKin] (
    [Id] nvarchar(40) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [Email] nvarchar(128) NULL,
    [Phone] nvarchar(32) NOT NULL,
    [Relationship] nvarchar(128) NOT NULL,
    [Address] nvarchar(512) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_CustomerNextOfKin] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerNextOfKin_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Docs].[CustomerPaymentDocument] (
    [Id] nvarchar(40) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [DocumentData] nvarchar(max) NULL,
    [MimeType] nvarchar(max) NULL,
    [FileName] nvarchar(50) NULL,
    [FileSize] int NOT NULL,
    [DocumentType] nvarchar(32) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_CustomerPaymentDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerPaymentDocument_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [HR].[Employee] (
    [Id] nvarchar(40) NOT NULL,
    [EmployeeNo] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [MiddleName] nvarchar(50) NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [Dob] datetime2 NULL,
    [Gender] nvarchar(32) NOT NULL,
    [ProfileImageUrl] nvarchar(256) NULL,
    [EmploymentDate] datetime2 NULL,
    [DepartmentId] nvarchar(40) NULL,
    [CustomerId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employee_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]),
    CONSTRAINT [FK_Employee_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [MasterData].[Department] ([Id])
);
GO

CREATE TABLE [Deposits].[CustomerDepositProductPublication] (
    [Id] nvarchar(40) NOT NULL,
    [PublicationType] nvarchar(100) NOT NULL,
    [ProductId] nvarchar(40) NOT NULL,
    [CustomerId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_CustomerDepositProductPublication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerDepositProductPublication_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]),
    CONSTRAINT [FK_CustomerDepositProductPublication_DepositProduct_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[DepartmentDepositProductPublication] (
    [Id] nvarchar(40) NOT NULL,
    [PublicationType] nvarchar(100) NOT NULL,
    [ProductId] nvarchar(40) NOT NULL,
    [DepartmentId] nvarchar(40) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_DepartmentDepositProductPublication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DepartmentDepositProductPublication_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [MasterData].[Department] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepartmentDepositProductPublication_DepositProduct_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[DepositProductCharge] (
    [Id] nvarchar(40) NOT NULL,
    [ProductId] nvarchar(40) NOT NULL,
    [ChargeId] nvarchar(40) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_DepositProductCharge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DepositProductCharge_Charge_ChargeId] FOREIGN KEY ([ChargeId]) REFERENCES [MasterData].[Charge] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepositProductCharge_DepositProduct_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[DepositProductInterestRange] (
    [Id] nvarchar(40) NOT NULL,
    [ProductId] nvarchar(40) NOT NULL,
    [LowerLimit] decimal(30,6) NOT NULL,
    [UpperLimit] decimal(30,6) NOT NULL,
    [InterestRate] decimal(30,6) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_DepositProductInterestRange] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DepositProductInterestRange_DepositProduct_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[SavingsAccountApplication] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationNo] nvarchar(100) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [DepositProductId] nvarchar(40) NOT NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SavingsAccountApplication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SavingsAccountApplication_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SavingsAccountApplication_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SavingsAccountApplication_DepositProduct_DepositProductId] FOREIGN KEY ([DepositProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[CustomerLoanProductPublication] (
    [Id] nvarchar(40) NOT NULL,
    [PublicationType] nvarchar(100) NOT NULL,
    [ProductId] nvarchar(40) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_CustomerLoanProductPublication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerLoanProductPublication_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerLoanProductPublication_LoanProduct_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Loans].[LoanProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[DepartmentLoanProductPublication] (
    [Id] nvarchar(40) NOT NULL,
    [PublicationType] nvarchar(100) NOT NULL,
    [ProductId] nvarchar(40) NOT NULL,
    [DepartmentId] nvarchar(40) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_DepartmentLoanProductPublication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DepartmentLoanProductPublication_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [MasterData].[Department] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DepartmentLoanProductPublication_LoanProduct_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Loans].[LoanProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[LoanProductCharge] (
    [Id] nvarchar(40) NOT NULL,
    [ChargeType] nvarchar(100) NOT NULL,
    [ProductId] nvarchar(40) NOT NULL,
    [ChargeId] nvarchar(40) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanProductCharge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanProductCharge_Charge_ChargeId] FOREIGN KEY ([ChargeId]) REFERENCES [MasterData].[Charge] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanProductCharge_LoanProduct_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Loans].[LoanProduct] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Payroll].[PayrollCronJobConfig] (
    [Id] nvarchar(40) NOT NULL,
    [DeductionScheduleId] nvarchar(40) NULL,
    [CronJobType] nvarchar(100) NOT NULL,
    [JobName] nvarchar(max) NULL,
    [JobDate] datetime2 NOT NULL,
    [JobStatus] nvarchar(100) NOT NULL,
    [ComputationStartDate] datetime2 NOT NULL,
    [ComputationEndDate] datetime2 NOT NULL,
    [RecordsProcessed] int NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_PayrollCronJobConfig] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PayrollCronJobConfig_PayrollDeductionSchedule_DeductionScheduleId] FOREIGN KEY ([DeductionScheduleId]) REFERENCES [Payroll].[PayrollDeductionSchedule] ([Id])
);
GO

CREATE TABLE [Docs].[PayrollDeductionDocument] (
    [Id] nvarchar(40) NOT NULL,
    [PayrollDeductionScheduleId] nvarchar(40) NULL,
    [ProcessSequence] int NOT NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NOT NULL,
    [DocumentData] nvarchar NULL,
    [MimeType] nvarchar(32) NULL,
    [FileName] nvarchar(128) NOT NULL,
    [FileSize] int NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_PayrollDeductionDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PayrollDeductionDocument_PayrollDeductionSchedule_PayrollDeductionScheduleId] FOREIGN KEY ([PayrollDeductionScheduleId]) REFERENCES [Payroll].[PayrollDeductionSchedule] ([Id])
);
GO

CREATE TABLE [Payroll].[PayrollDeductionItem] (
    [Id] nvarchar(40) NOT NULL,
    [PayrollDeductionScheduleId] nvarchar(40) NOT NULL,
    [BatchRefNo] nvarchar(max) NULL,
    [MemberId] nvarchar(40) NOT NULL,
    [EmployeeNo] nvarchar(max) NULL,
    [MemberName] nvarchar(120) NOT NULL,
    [AccountNo] nvarchar(32) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [PayrollCode] nvarchar(255) NOT NULL,
    [Narration] nvarchar(255) NULL,
    [PayrollDate] datetime2 NOT NULL,
    [CurrentStatus] nvarchar(100) NULL,
    [AccountDueDate] datetime2 NOT NULL,
    [DeductionType] nvarchar(100) NOT NULL,
    [TotalDeduction] decimal(18,2) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_PayrollDeductionItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PayrollDeductionItem_PayrollDeductionSchedule_PayrollDeductionScheduleId] FOREIGN KEY ([PayrollDeductionScheduleId]) REFERENCES [Payroll].[PayrollDeductionSchedule] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[SpecialDepositAccountApplication] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationNo] nvarchar(50) NULL,
    [CustomerId] nvarchar(40) NULL,
    [DepositProductId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestRate] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ModeOfPayment] nvarchar(100) NOT NULL,
    [PaymentDocumentId] nvarchar(40) NULL,
    [PaymentAccountNumber] nvarchar(50) NULL,
    [PaymentBankName] nvarchar(100) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositAccountApplication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositAccountApplication_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccountApplication_CustomerPaymentDocument_PaymentDocumentId] FOREIGN KEY ([PaymentDocumentId]) REFERENCES [Docs].[CustomerPaymentDocument] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccountApplication_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccountApplication_DepositProduct_DepositProductId] FOREIGN KEY ([DepositProductId]) REFERENCES [Deposits].[DepositProduct] ([Id])
);
GO

CREATE TABLE [Deposits].[SavingsAccount] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationId] nvarchar(40) NULL,
    [AccountNo] nvarchar(50) NOT NULL,
    [CustomerId] nvarchar(40) NULL,
    [DepositProductId] nvarchar(40) NULL,
    [LedgerDepositAccountId] nvarchar(40) NULL,
    [ChargesPayableAccountId] nvarchar(40) NULL,
    [ChargesAccruedAccountId] nvarchar(40) NOT NULL,
    [ChargesWaivedAccountId] nvarchar(40) NOT NULL,
    [ChargesIncomeAccountId] nvarchar(40) NOT NULL,
    [PayrollAmount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [IsClosed] bit NOT NULL,
    [DateClosed] datetime2 NULL,
    [ClosedByUserId] nvarchar(450) NULL,
    [MaximumBalanceLimit] decimal(18,2) NOT NULL,
    [MinimumBalanceLimit] decimal(18,2) NOT NULL,
    [SingleWithdrawalLimit] decimal(18,2) NOT NULL,
    [DailyWithdrawalLimit] decimal(18,2) NOT NULL,
    [WeeklyWithdrawalLimit] decimal(18,2) NOT NULL,
    [MonthlyWithdrawalLimit] decimal(18,2) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SavingsAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SavingsAccount_ApplicationUser_ClosedByUserId] FOREIGN KEY ([ClosedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_SavingsAccount_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]),
    CONSTRAINT [FK_SavingsAccount_DepositProduct_DepositProductId] FOREIGN KEY ([DepositProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]),
    CONSTRAINT [FK_SavingsAccount_LedgerAccount_ChargesAccruedAccountId] FOREIGN KEY ([ChargesAccruedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SavingsAccount_LedgerAccount_ChargesIncomeAccountId] FOREIGN KEY ([ChargesIncomeAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SavingsAccount_LedgerAccount_ChargesPayableAccountId] FOREIGN KEY ([ChargesPayableAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]),
    CONSTRAINT [FK_SavingsAccount_LedgerAccount_ChargesWaivedAccountId] FOREIGN KEY ([ChargesWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SavingsAccount_LedgerAccount_LedgerDepositAccountId] FOREIGN KEY ([LedgerDepositAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]),
    CONSTRAINT [FK_SavingsAccount_SavingsAccountApplication_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [Deposits].[SavingsAccountApplication] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositInterestSchedule] (
    [Id] nvarchar(40) NOT NULL,
    [CronJobConfigId] nvarchar(40) NULL,
    [ScheduleName] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositInterestSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositInterestSchedule_PayrollCronJobConfig_CronJobConfigId] FOREIGN KEY ([CronJobConfigId]) REFERENCES [Payroll].[PayrollCronJobConfig] ([Id])
);
GO

CREATE TABLE [Deposits].[SpecialDepositInterestSchedule] (
    [Id] nvarchar(40) NOT NULL,
    [CronJobConfigId] nvarchar(40) NULL,
    [ScheduleName] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositInterestSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositInterestSchedule_PayrollCronJobConfig_CronJobConfigId] FOREIGN KEY ([CronJobConfigId]) REFERENCES [Payroll].[PayrollCronJobConfig] ([Id])
);
GO

CREATE TABLE [Deposits].[SpecialDepositAccount] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationId] nvarchar(40) NULL,
    [AccountNo] nvarchar(50) NULL,
    [CustomerId] nvarchar(40) NULL,
    [DepositProductId] nvarchar(40) NULL,
    [DepositAccountId] nvarchar(40) NULL,
    [ChargesAccruedAccountId] nvarchar(40) NOT NULL,
    [ChargesIncomeAccountId] nvarchar(40) NOT NULL,
    [ChargesWaivedAccountId] nvarchar(40) NOT NULL,
    [InterestEarnedAccountId] nvarchar(40) NULL,
    [InterestPayoutAccountId] nvarchar(40) NULL,
    [FundingAmount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestRate] decimal(18,2) NOT NULL DEFAULT 0.0,
    [LastInterestComputationDate] datetime2 NULL,
    [MaximumBalanceLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MinimumBalanceLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [SingleWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [DailyWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [WeeklyWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MonthlyWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [IsClosed] bit NOT NULL,
    [DateClosed] datetime2 NULL,
    [ClosedByUserId] nvarchar(450) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositAccount_ApplicationUser_ClosedByUserId] FOREIGN KEY ([ClosedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccount_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccount_DepositProduct_DepositProductId] FOREIGN KEY ([DepositProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccount_LedgerAccount_ChargesAccruedAccountId] FOREIGN KEY ([ChargesAccruedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialDepositAccount_LedgerAccount_ChargesIncomeAccountId] FOREIGN KEY ([ChargesIncomeAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialDepositAccount_LedgerAccount_ChargesWaivedAccountId] FOREIGN KEY ([ChargesWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialDepositAccount_LedgerAccount_DepositAccountId] FOREIGN KEY ([DepositAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccount_LedgerAccount_InterestEarnedAccountId] FOREIGN KEY ([InterestEarnedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccount_LedgerAccount_InterestPayoutAccountId] FOREIGN KEY ([InterestPayoutAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositAccount_SpecialDepositAccountApplication_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [Deposits].[SpecialDepositAccountApplication] ([Id])
);
GO

CREATE TABLE [Deposits].[SavingsAccountDeductionSchedule] (
    [Id] nvarchar(40) NOT NULL,
    [SavingsAccountId] nvarchar(40) NOT NULL,
    [BatchRefNo] nvarchar(50) NULL,
    [MemberId] nvarchar(60) NULL,
    [EmployeeNo] nvarchar(60) NULL,
    [MemberName] nvarchar(100) NULL,
    [AccountNo] nvarchar(50) NULL,
    [Amount] decimal(18,3) NOT NULL,
    [PayrollCode] nvarchar(60) NULL,
    [Narration] nvarchar(100) NULL,
    [DueDate] datetime2 NOT NULL,
    [CurrentStatus] nvarchar(20) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SavingsAccountDeductionSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SavingsAccountDeductionSchedule_SavingsAccount_SavingsAccountId] FOREIGN KEY ([SavingsAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[SavingsCashAddition] (
    [Id] nvarchar(40) NOT NULL,
    [SavingsAccountId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ModeOfPayment] nvarchar(100) NOT NULL,
    [CustomerPaymentDocumentId] nvarchar(40) NULL,
    [BatchRefNo] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SavingsCashAddition] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SavingsCashAddition_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SavingsCashAddition_CustomerPaymentDocument_CustomerPaymentDocumentId] FOREIGN KEY ([CustomerPaymentDocumentId]) REFERENCES [Docs].[CustomerPaymentDocument] ([Id]),
    CONSTRAINT [FK_SavingsCashAddition_SavingsAccount_SavingsAccountId] FOREIGN KEY ([SavingsAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]),
    CONSTRAINT [FK_SavingsCashAddition_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[SavingsIncreaseDecrease] (
    [Id] nvarchar(40) NOT NULL,
    [SavingsAccountId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ContributionChangeRequest] nvarchar(100) NOT NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SavingsIncreaseDecrease] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SavingsIncreaseDecrease_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SavingsIncreaseDecrease_SavingsAccount_SavingsAccountId] FOREIGN KEY ([SavingsAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositAccountApplication] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationNo] nvarchar(100) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [DepositProductId] nvarchar(40) NOT NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TenureUnit] nvarchar(100) NOT NULL DEFAULT N'NONE',
    [TenureValue] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestRate] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MaturityInstructionType] nvarchar(100) NOT NULL,
    [LiquidationAccountType] nvarchar(100) NOT NULL,
    [SavingsLiquidationAccountId] nvarchar(40) NULL,
    [SpecialDepositLiquidationAccountId] nvarchar(40) NULL,
    [CustomerBankLiquidationAccountId] nvarchar(40) NULL,
    [ModeOfPayment] nvarchar(100) NOT NULL,
    [SpecialDepositFundingSourceAccountId] nvarchar(40) NULL,
    [CustomerBankFundingSourceAccountId] nvarchar(40) NULL,
    [PaymentDocumentId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositAccountApplication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositAccountApplication_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_FixedDepositAccountApplication_CustomerBankAccount_CustomerBankFundingSourceAccountId] FOREIGN KEY ([CustomerBankFundingSourceAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositAccountApplication_CustomerBankAccount_CustomerBankLiquidationAccountId] FOREIGN KEY ([CustomerBankLiquidationAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositAccountApplication_CustomerPaymentDocument_PaymentDocumentId] FOREIGN KEY ([PaymentDocumentId]) REFERENCES [Docs].[CustomerPaymentDocument] ([Id]),
    CONSTRAINT [FK_FixedDepositAccountApplication_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccountApplication_DepositProduct_DepositProductId] FOREIGN KEY ([DepositProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccountApplication_SavingsAccount_SavingsLiquidationAccountId] FOREIGN KEY ([SavingsLiquidationAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositAccountApplication_SpecialDepositAccount_SpecialDepositFundingSourceAccountId] FOREIGN KEY ([SpecialDepositFundingSourceAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositAccountApplication_SpecialDepositAccount_SpecialDepositLiquidationAccountId] FOREIGN KEY ([SpecialDepositLiquidationAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id])
);
GO

CREATE TABLE [Loans].[LoanApplication] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationNo] nvarchar(64) NOT NULL,
    [AccountNo] nvarchar(64) NULL,
    [LoanProductId] nvarchar(40) NOT NULL,
    [ApprovalId] nvarchar(40) NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [Principal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TenureUnit] nvarchar(100) NOT NULL,
    [TenureValue] decimal(18,2) NOT NULL,
    [RepaymentCommencementDate] datetimeoffset NOT NULL,
    [UseSpecialDeposit] bit NOT NULL,
    [SpecialDepositId] nvarchar(40) NULL,
    [CustomerDisbursementAccountId] nvarchar(40) NULL,
    [QualificationTargetProductId] nvarchar(40) NULL,
    [Status] nvarchar(100) NOT NULL,
    [QualificationTargetProductType] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanApplication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanApplication_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_LoanApplication_CustomerBankAccount_CustomerDisbursementAccountId] FOREIGN KEY ([CustomerDisbursementAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_LoanApplication_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanApplication_LoanProduct_LoanProductId] FOREIGN KEY ([LoanProductId]) REFERENCES [Loans].[LoanProduct] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanApplication_SpecialDepositAccount_SpecialDepositId] FOREIGN KEY ([SpecialDepositId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id])
);
GO

CREATE TABLE [Deposits].[SpecialDepositAccountDeductionSchedule] (
    [Id] nvarchar(40) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NOT NULL,
    [BatchRefNo] nvarchar(50) NULL,
    [MemberId] nvarchar(60) NULL,
    [EmployeeNo] nvarchar(60) NULL,
    [MemberName] nvarchar(100) NULL,
    [AccountNo] nvarchar(50) NULL,
    [Amount] decimal(18,3) NOT NULL,
    [PayrollCode] nvarchar(60) NULL,
    [Narration] nvarchar(100) NULL,
    [DueDate] datetime2 NOT NULL,
    [CurrentStatus] nvarchar(20) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositAccountDeductionSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositAccountDeductionSchedule_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[SpecialDepositCashAddition] (
    [Id] nvarchar(40) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [CustomerPaymentDocumentId] nvarchar(40) NULL,
    [ModeOfPayment] nvarchar(100) NOT NULL,
    [BatchRefNo] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositCashAddition] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositCashAddition_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SpecialDepositCashAddition_CustomerPaymentDocument_CustomerPaymentDocumentId] FOREIGN KEY ([CustomerPaymentDocumentId]) REFERENCES [Docs].[CustomerPaymentDocument] ([Id]),
    CONSTRAINT [FK_SpecialDepositCashAddition_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositCashAddition_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[SpecialDepositInterestScheduleItem] (
    [Id] nvarchar(40) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [SpecialDepositInterestScheduleId] nvarchar(40) NULL,
    [OldBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PeriodCashAddition] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestRate] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestEarned] decimal(18,2) NOT NULL DEFAULT 0.0,
    [NewBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [SpecialDepositInterestScheduleId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositInterestScheduleItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositInterestScheduleItem_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositInterestScheduleItem_SpecialDepositInterestSchedule_SpecialDepositInterestScheduleId] FOREIGN KEY ([SpecialDepositInterestScheduleId]) REFERENCES [Deposits].[SpecialDepositInterestSchedule] ([Id]),
    CONSTRAINT [FK_SpecialDepositInterestScheduleItem_SpecialDepositInterestSchedule_SpecialDepositInterestScheduleId1] FOREIGN KEY ([SpecialDepositInterestScheduleId1]) REFERENCES [Deposits].[SpecialDepositInterestSchedule] ([Id])
);
GO

CREATE TABLE [Deposits].[SpecialDepositWithdrawal] (
    [Id] nvarchar(40) NOT NULL,
    [SpecialDepositSourceAccountId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [WithdrawalDestinationType] nvarchar(100) NOT NULL,
    [CustomerDestinationBankAccountId] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositWithdrawal] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositWithdrawal_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SpecialDepositWithdrawal_CustomerBankAccount_CustomerDestinationBankAccountId] FOREIGN KEY ([CustomerDestinationBankAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositWithdrawal_SpecialDepositAccount_SpecialDepositSourceAccountId] FOREIGN KEY ([SpecialDepositSourceAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositWithdrawal_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositAccount] (
    [Id] nvarchar(40) NOT NULL,
    [ApplicationId] nvarchar(40) NULL,
    [AccountNo] nvarchar(50) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [DepositProductId] nvarchar(40) NOT NULL,
    [DepositAccountId] nvarchar(40) NOT NULL,
    [ChargesAccruedAccountId] nvarchar(40) NOT NULL,
    [ChargesIncomeAccountId] nvarchar(40) NOT NULL,
    [InterestEarnedAccountId] nvarchar(40) NOT NULL,
    [InterestPayoutAccountId] nvarchar(40) NOT NULL,
    [ChargesWaivedAccountId] nvarchar(40) NOT NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TenureUnit] nvarchar(100) NOT NULL DEFAULT N'NONE',
    [TenureValue] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestRate] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MaturityInstructionType] nvarchar(100) NOT NULL,
    [LiquidationAccountType] nvarchar(100) NOT NULL,
    [SavingsLiquidationAccountId] nvarchar(40) NULL,
    [SpecialDepositLiquidationAccountId] nvarchar(40) NULL,
    [CustomerBankLiquidationAccountId] nvarchar(40) NULL,
    [LastInterestComputationDate] datetime2 NULL,
    [HasMature] bit NOT NULL,
    [IsClosed] bit NOT NULL,
    [DateClosed] datetime2 NULL,
    [ClosedByUserId] nvarchar(450) NULL,
    [MaximumBalanceLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MinimumBalanceLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [SingleWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [DailyWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [WeeklyWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MonthlyWithdrawalLimit] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositAccount_ApplicationUser_ClosedByUserId] FOREIGN KEY ([ClosedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_FixedDepositAccount_CustomerBankAccount_CustomerBankLiquidationAccountId] FOREIGN KEY ([CustomerBankLiquidationAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositAccount_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_DepositProduct_DepositProductId] FOREIGN KEY ([DepositProductId]) REFERENCES [Deposits].[DepositProduct] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_FixedDepositAccountApplication_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [Deposits].[FixedDepositAccountApplication] ([Id]),
    CONSTRAINT [FK_FixedDepositAccount_LedgerAccount_ChargesAccruedAccountId] FOREIGN KEY ([ChargesAccruedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_LedgerAccount_ChargesIncomeAccountId] FOREIGN KEY ([ChargesIncomeAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_LedgerAccount_ChargesWaivedAccountId] FOREIGN KEY ([ChargesWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_LedgerAccount_DepositAccountId] FOREIGN KEY ([DepositAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_LedgerAccount_InterestEarnedAccountId] FOREIGN KEY ([InterestEarnedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_LedgerAccount_InterestPayoutAccountId] FOREIGN KEY ([InterestPayoutAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositAccount_SavingsAccount_SavingsLiquidationAccountId] FOREIGN KEY ([SavingsLiquidationAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositAccount_SpecialDepositAccount_SpecialDepositLiquidationAccountId] FOREIGN KEY ([SpecialDepositLiquidationAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id])
);
GO

CREATE TABLE [Loans].[LoanApplicationApproval] (
    [Id] nvarchar(40) NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [LoanApplicationId] nvarchar(40) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanApplicationApproval] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanApplicationApproval_LoanApplication_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [Loans].[LoanApplication] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[LoanApplicationGuarantor] (
    [Id] nvarchar(40) NOT NULL,
    [LoanApplicationId] nvarchar(40) NOT NULL,
    [GuarantorType] nvarchar(100) NOT NULL,
    [GuarantorId] nvarchar(40) NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [ApprovedOn] datetime2 NULL,
    [Comment] nvarchar(2048) NULL,
    [GuarantorApprovalType] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanApplicationGuarantor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanApplicationGuarantor_Customer_GuarantorId] FOREIGN KEY ([GuarantorId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanApplicationGuarantor_LoanApplication_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [Loans].[LoanApplication] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[LoanApplicationItem] (
    [Id] nvarchar(40) NOT NULL,
    [LoanApplicationId] nvarchar(40) NOT NULL,
    [ItemType] nvarchar(100) NOT NULL,
    [Name] nvarchar(32) NOT NULL,
    [BrandName] nvarchar(32) NULL,
    [Model] nvarchar(32) NOT NULL,
    [Color] nvarchar(16) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanApplicationItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanApplicationItem_LoanApplication_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [Loans].[LoanApplication] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[LoanApplicationSchedule] (
    [Id] nvarchar(40) NOT NULL,
    [LoanApplicationId] nvarchar(40) NOT NULL,
    [RepaymentNo] int NOT NULL,
    [TenureUnit] nvarchar(100) NOT NULL,
    [TenureValue] decimal(18,2) NOT NULL,
    [PeriodStartDate] datetime2 NULL,
    [DueDate] datetime2 NULL,
    [DaysInPeriod] int NULL,
    [PeriodPayment] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CumulativeTotal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TotalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PeriodPrincipal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CumulativePrincipal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PrincipalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PeriodInterest] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CumulativeInterest] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanApplicationSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanApplicationSchedule_LoanApplication_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [Loans].[LoanApplication] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Deposits].[SpecialDepositInterestAddition] (
    [Id] nvarchar(40) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [InterestScheduleItemId] nvarchar(40) NULL,
    [InterestEarned] decimal(18,2) NOT NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositInterestAddition] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositInterestAddition_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositInterestAddition_SpecialDepositInterestScheduleItem_InterestScheduleItemId] FOREIGN KEY ([InterestScheduleItemId]) REFERENCES [Deposits].[SpecialDepositInterestScheduleItem] ([Id]),
    CONSTRAINT [FK_SpecialDepositInterestAddition_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositChangeInMaturity] (
    [Id] nvarchar(40) NOT NULL,
    [MaturityInstructionType] nvarchar(100) NOT NULL,
    [FixedDepositAccountId] nvarchar(40) NOT NULL,
    [LiquidationAccountType] nvarchar(100) NOT NULL,
    [SavingsLiquidationAccountId] nvarchar(40) NULL,
    [SpecialDepositLiquidationAccountId] nvarchar(40) NULL,
    [CustomerBankLiquidationAccountId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositChangeInMaturity] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositChangeInMaturity_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_FixedDepositChangeInMaturity_CustomerBankAccount_CustomerBankLiquidationAccountId] FOREIGN KEY ([CustomerBankLiquidationAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositChangeInMaturity_FixedDepositAccount_FixedDepositAccountId] FOREIGN KEY ([FixedDepositAccountId]) REFERENCES [Deposits].[FixedDepositAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositChangeInMaturity_SavingsAccount_SavingsLiquidationAccountId] FOREIGN KEY ([SavingsLiquidationAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositChangeInMaturity_SpecialDepositAccount_SpecialDepositLiquidationAccountId] FOREIGN KEY ([SpecialDepositLiquidationAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositInterestScheduleItem] (
    [Id] nvarchar(40) NOT NULL,
    [FixedDepositAccountId] nvarchar(40) NULL,
    [FixedDepositInterestScheduleId] nvarchar(40) NULL,
    [OldBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PeriodCashAddition] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestRate] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestEarned] decimal(18,2) NOT NULL DEFAULT 0.0,
    [NewBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [FixedDepositInterestScheduleId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositInterestScheduleItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositInterestScheduleItem_FixedDepositAccount_FixedDepositAccountId] FOREIGN KEY ([FixedDepositAccountId]) REFERENCES [Deposits].[FixedDepositAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositInterestScheduleItem_FixedDepositInterestSchedule_FixedDepositInterestScheduleId] FOREIGN KEY ([FixedDepositInterestScheduleId]) REFERENCES [Deposits].[FixedDepositInterestSchedule] ([Id]),
    CONSTRAINT [FK_FixedDepositInterestScheduleItem_FixedDepositInterestSchedule_FixedDepositInterestScheduleId1] FOREIGN KEY ([FixedDepositInterestScheduleId1]) REFERENCES [Deposits].[FixedDepositInterestSchedule] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositLiquidation] (
    [Id] nvarchar(40) NOT NULL,
    [FixedDepositAccountId] nvarchar(40) NOT NULL,
    [LiquidationAccountType] nvarchar(100) NULL,
    [SavingsLiquidationAccountId] nvarchar(40) NULL,
    [SpecialDepositLiquidationAccountId] nvarchar(40) NULL,
    [CustomerBankLiquidationAccountId] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositLiquidation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositLiquidation_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_FixedDepositLiquidation_CustomerBankAccount_CustomerBankLiquidationAccountId] FOREIGN KEY ([CustomerBankLiquidationAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositLiquidation_FixedDepositAccount_FixedDepositAccountId] FOREIGN KEY ([FixedDepositAccountId]) REFERENCES [Deposits].[FixedDepositAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FixedDepositLiquidation_SavingsAccount_SavingsLiquidationAccountId] FOREIGN KEY ([SavingsLiquidationAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositLiquidation_SpecialDepositAccount_SpecialDepositLiquidationAccountId] FOREIGN KEY ([SpecialDepositLiquidationAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositLiquidation_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[SpecialDepositFundTransfer] (
    [Id] nvarchar(40) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [DestinationAccountType] nvarchar(100) NOT NULL,
    [SavingsDestinationAccountId] nvarchar(40) NULL,
    [FixedDepositDestinationAccountId] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositFundTransfer] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositFundTransfer_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SpecialDepositFundTransfer_FixedDepositAccount_FixedDepositDestinationAccountId] FOREIGN KEY ([FixedDepositDestinationAccountId]) REFERENCES [Deposits].[FixedDepositAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositFundTransfer_SavingsAccount_SavingsDestinationAccountId] FOREIGN KEY ([SavingsDestinationAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositFundTransfer_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_SpecialDepositFundTransfer_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositInterestAddition] (
    [Id] nvarchar(40) NOT NULL,
    [FixedDepositAccountId] nvarchar(40) NULL,
    [InterestScheduleItemId] nvarchar(40) NULL,
    [InterestEarned] decimal(18,2) NOT NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositInterestAddition] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositInterestAddition_FixedDepositAccount_FixedDepositAccountId] FOREIGN KEY ([FixedDepositAccountId]) REFERENCES [Deposits].[FixedDepositAccount] ([Id]),
    CONSTRAINT [FK_FixedDepositInterestAddition_FixedDepositInterestScheduleItem_InterestScheduleItemId] FOREIGN KEY ([InterestScheduleItemId]) REFERENCES [Deposits].[FixedDepositInterestScheduleItem] ([Id]),
    CONSTRAINT [FK_FixedDepositInterestAddition_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Deposits].[FixedDepositLiquidationCharge] (
    [Id] nvarchar(40) NOT NULL,
    [FixedDepositLiquidationId] nvarchar(40) NULL,
    [ChargeType] nvarchar(100) NOT NULL DEFAULT N'FIXED_DEPOSIT_LIQUIDATION_CHARGE',
    [LiquidationCharge] decimal(18,2) NOT NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_FixedDepositLiquidationCharge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixedDepositLiquidationCharge_FixedDepositLiquidation_FixedDepositLiquidationId] FOREIGN KEY ([FixedDepositLiquidationId]) REFERENCES [Deposits].[FixedDepositLiquidation] ([Id]),
    CONSTRAINT [FK_FixedDepositLiquidationCharge_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Loans].[LoanAccount] (
    [Id] nvarchar(40) NOT NULL,
    [AccountNo] nvarchar(64) NOT NULL,
    [LoanApplicationId] nvarchar(40) NOT NULL,
    [CustomerId] nvarchar(40) NOT NULL,
    [PrincipalBalanceAccountId] nvarchar(40) NOT NULL,
    [PrincipalLossAccountId] nvarchar(40) NOT NULL,
    [EarnedInterestAccountId] nvarchar(40) NOT NULL,
    [InterestBalanceAccountId] nvarchar(40) NOT NULL,
    [UnearnedInterestAccountId] nvarchar(40) NOT NULL,
    [InterestLossAccountId] nvarchar(40) NOT NULL,
    [InterestWaivedAccountId] nvarchar(40) NOT NULL,
    [ChargesAccruedAccountId] nvarchar(40) NOT NULL,
    [ChargesIncomeAccountId] nvarchar(40) NOT NULL,
    [ChargesWaivedAccountId] nvarchar(40) NOT NULL,
    [Principal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TenureUnit] nvarchar(100) NOT NULL,
    [TenureValue] decimal(18,2) NOT NULL,
    [RepaymentCommencementDate] datetimeoffset NOT NULL,
    [UseSpecialDeposit] bit NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [DestinationAccountId] nvarchar(40) NULL,
    [IsClosed] bit NOT NULL,
    [DateClosed] datetime2 NULL,
    [ClosedByUserId] nvarchar(450) NULL,
    [LoanTopupId] nvarchar(max) NULL,
    [LoanTopupId1] nvarchar(40) NULL,
    [InterestEarnedAccountId] nvarchar(40) NOT NULL,
    [InterestPayoutAccountId] nvarchar(40) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanAccount_ApplicationUser_ClosedByUserId] FOREIGN KEY ([ClosedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_LoanAccount_CustomerBankAccount_DestinationAccountId] FOREIGN KEY ([DestinationAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_LoanAccount_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_ChargesAccruedAccountId] FOREIGN KEY ([ChargesAccruedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_ChargesIncomeAccountId] FOREIGN KEY ([ChargesIncomeAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_ChargesWaivedAccountId] FOREIGN KEY ([ChargesWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_EarnedInterestAccountId] FOREIGN KEY ([EarnedInterestAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_InterestBalanceAccountId] FOREIGN KEY ([InterestBalanceAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_InterestEarnedAccountId] FOREIGN KEY ([InterestEarnedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_InterestLossAccountId] FOREIGN KEY ([InterestLossAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_InterestPayoutAccountId] FOREIGN KEY ([InterestPayoutAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_InterestWaivedAccountId] FOREIGN KEY ([InterestWaivedAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_PrincipalBalanceAccountId] FOREIGN KEY ([PrincipalBalanceAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_PrincipalLossAccountId] FOREIGN KEY ([PrincipalLossAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LedgerAccount_UnearnedInterestAccountId] FOREIGN KEY ([UnearnedInterestAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_LoanApplication_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [Loans].[LoanApplication] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanAccount_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id])
);
GO

CREATE TABLE [Loans].[LoanDisbursement] (
    [Id] nvarchar(40) NOT NULL,
    [LoanAccountId] nvarchar(40) NOT NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ApprovedByUserId] nvarchar(450) NULL,
    [DisbursedByUserId] nvarchar(450) NULL,
    [DisbursementStatus] nvarchar(100) NOT NULL,
    [DisbursementAccountId] nvarchar(40) NULL,
    [DisbursementDate] datetimeoffset NULL,
    [DisbursementMode] nvarchar(100) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [CustomerBankAccountId] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [ApprovalDate] datetimeoffset NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [LoanAccountId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanDisbursement] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanDisbursement_ApplicationUser_ApprovedByUserId] FOREIGN KEY ([ApprovedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_LoanDisbursement_ApplicationUser_DisbursedByUserId] FOREIGN KEY ([DisbursedByUserId]) REFERENCES [Security].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_LoanDisbursement_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_LoanDisbursement_CompanyBankAccount_DisbursementAccountId] FOREIGN KEY ([DisbursementAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id]),
    CONSTRAINT [FK_LoanDisbursement_CustomerBankAccount_CustomerBankAccountId] FOREIGN KEY ([CustomerBankAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_LoanDisbursement_LoanAccount_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [Loans].[LoanAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanDisbursement_LoanAccount_LoanAccountId1] FOREIGN KEY ([LoanAccountId1]) REFERENCES [Loans].[LoanAccount] ([Id]),
    CONSTRAINT [FK_LoanDisbursement_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_LoanDisbursement_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Loans].[LoanOffset] (
    [Id] nvarchar(40) NOT NULL,
    [LoanAccountId] nvarchar(40) NOT NULL,
    [OffsetAmount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [OldPrincipalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [NewPrincipalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [OldInterestBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [NewInterestBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TotalOffsetCharges] decimal(18,2) NOT NULL DEFAULT 0.0,
    [IsLiquidated] bit NOT NULL,
    [AllowedOffsetType] nvarchar(100) NOT NULL,
    [LoanRepaymentMode] nvarchar(100) NOT NULL,
    [OffSetRepaymentDate] datetimeoffset NOT NULL,
    [SavingsAccountId] nvarchar(40) NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [CustomerBankAccountId] nvarchar(40) NULL,
    [ModeOfPayment] int NOT NULL,
    [CustomerPaymentDocumentId] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanOffset] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanOffset_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_LoanOffset_CustomerBankAccount_CustomerBankAccountId] FOREIGN KEY ([CustomerBankAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_LoanOffset_CustomerPaymentDocument_CustomerPaymentDocumentId] FOREIGN KEY ([CustomerPaymentDocumentId]) REFERENCES [Docs].[CustomerPaymentDocument] ([Id]),
    CONSTRAINT [FK_LoanOffset_LoanAccount_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [Loans].[LoanAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanOffset_SavingsAccount_SavingsAccountId] FOREIGN KEY ([SavingsAccountId]) REFERENCES [Deposits].[SavingsAccount] ([Id]),
    CONSTRAINT [FK_LoanOffset_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_LoanOffset_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Loans].[LoanRepaymentSchedule] (
    [Id] nvarchar(40) NOT NULL,
    [LoanAccountId] nvarchar(40) NOT NULL,
    [RepaymentNo] int NOT NULL,
    [BatchRefNo] nvarchar(max) NOT NULL,
    [TenureUnit] nvarchar(100) NOT NULL,
    [TenureValue] decimal(18,2) NOT NULL,
    [PeriodStartDate] datetime2 NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [DaysInPeriod] int NULL,
    [PeriodPayment] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CumulativeTotal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TotalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PeriodPrincipal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CumulativePrincipal] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PrincipalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PeriodInterest] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CumulativeInterest] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InterestBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [IsPaid] bit NOT NULL,
    [IsPrincipalAllocated] bit NOT NULL,
    [IsInterestAllocated] bit NOT NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanRepaymentSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanRepaymentSchedule_LoanAccount_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [Loans].[LoanAccount] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Loans].[LoanTopup] (
    [Id] nvarchar(40) NOT NULL,
    [LoanAccountId] nvarchar(40) NOT NULL,
    [TopupAmount] decimal(18,2) NOT NULL,
    [DestinationType] nvarchar(100) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [CustomerBankAccountId] nvarchar(40) NULL,
    [OldPrincipalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [NewPrincipalBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [OldInterestBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [NewInterestBalance] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TotalTopupCharges] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TopupDate] datetimeoffset NOT NULL,
    [CommencementDate] datetimeoffset NOT NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanTopup] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanTopup_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_LoanTopup_CustomerBankAccount_CustomerBankAccountId] FOREIGN KEY ([CustomerBankAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_LoanTopup_LoanAccount_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [Loans].[LoanAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanTopup_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id]),
    CONSTRAINT [FK_LoanTopup_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Loans].[LoanDisbursementCharge] (
    [Id] nvarchar(40) NOT NULL,
    [LoanDisbursementId] nvarchar(40) NOT NULL,
    [DisbursementChargeId] nvarchar(40) NOT NULL,
    [ChargeType] nvarchar(100) NOT NULL,
    [TotalCharge] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TransactionJournalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanDisbursementCharge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanDisbursementCharge_Charge_DisbursementChargeId] FOREIGN KEY ([DisbursementChargeId]) REFERENCES [MasterData].[Charge] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanDisbursementCharge_LoanDisbursement_LoanDisbursementId] FOREIGN KEY ([LoanDisbursementId]) REFERENCES [Loans].[LoanDisbursement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanDisbursementCharge_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Loans].[LoanOffSetCharge] (
    [Id] nvarchar(40) NOT NULL,
    [LoanOffsetId] nvarchar(40) NOT NULL,
    [OffsetChargeId] nvarchar(40) NOT NULL,
    [ChargeType] nvarchar(100) NOT NULL,
    [TotalCharge] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TransactionJournalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [LoanOffsetId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanOffSetCharge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanOffSetCharge_Charge_OffsetChargeId] FOREIGN KEY ([OffsetChargeId]) REFERENCES [MasterData].[Charge] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanOffSetCharge_LoanOffset_LoanOffsetId] FOREIGN KEY ([LoanOffsetId]) REFERENCES [Loans].[LoanOffset] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanOffSetCharge_LoanOffset_LoanOffsetId1] FOREIGN KEY ([LoanOffsetId1]) REFERENCES [Loans].[LoanOffset] ([Id]),
    CONSTRAINT [FK_LoanOffSetCharge_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Payroll].[PayrollDeductionScheduleItem] (
    [Id] nvarchar(40) NOT NULL,
    [PayrollDeductionScheduleId] nvarchar(40) NULL,
    [BatchRefNo] nvarchar(max) NOT NULL,
    [MemberId] nvarchar(max) NOT NULL,
    [MemberName] nvarchar(max) NOT NULL,
    [AccountNo] nvarchar(max) NOT NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [PayrollCode] nvarchar(32) NOT NULL,
    [Narration] nvarchar(240) NOT NULL,
    [PayrollDate] datetime2 NOT NULL,
    [AccountDueDate] datetime2 NOT NULL,
    [CurrentStatus] nvarchar(120) NOT NULL,
    [DeductionType] nvarchar(100) NOT NULL,
    [LoanRepaymentScheduleId] nvarchar(40) NULL,
    [SavingsAccountDeductionScheduleId] nvarchar(40) NULL,
    [PayrollCronJobConfigId] nvarchar(40) NULL,
    [SpecialDepositAccountDeductionScheduleId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_PayrollDeductionScheduleItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PayrollDeductionScheduleItem_LoanRepaymentSchedule_LoanRepaymentScheduleId] FOREIGN KEY ([LoanRepaymentScheduleId]) REFERENCES [Loans].[LoanRepaymentSchedule] ([Id]),
    CONSTRAINT [FK_PayrollDeductionScheduleItem_PayrollCronJobConfig_PayrollCronJobConfigId] FOREIGN KEY ([PayrollCronJobConfigId]) REFERENCES [Payroll].[PayrollCronJobConfig] ([Id]),
    CONSTRAINT [FK_PayrollDeductionScheduleItem_PayrollDeductionSchedule_PayrollDeductionScheduleId] FOREIGN KEY ([PayrollDeductionScheduleId]) REFERENCES [Payroll].[PayrollDeductionSchedule] ([Id]),
    CONSTRAINT [FK_PayrollDeductionScheduleItem_SavingsAccountDeductionSchedule_SavingsAccountDeductionScheduleId] FOREIGN KEY ([SavingsAccountDeductionScheduleId]) REFERENCES [Deposits].[SavingsAccountDeductionSchedule] ([Id]),
    CONSTRAINT [FK_PayrollDeductionScheduleItem_SpecialDepositAccountDeductionSchedule_SpecialDepositAccountDeductionScheduleId] FOREIGN KEY ([SpecialDepositAccountDeductionScheduleId]) REFERENCES [Deposits].[SpecialDepositAccountDeductionSchedule] ([Id])
);
GO

CREATE TABLE [Loans].[LoanTopupCharge] (
    [Id] nvarchar(40) NOT NULL,
    [LoanTopupId] nvarchar(40) NOT NULL,
    [TopupChargeId] nvarchar(40) NOT NULL,
    [ChargeType] nvarchar(100) NOT NULL,
    [TotalCharge] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TransactionJournalId] nvarchar(40) NULL,
    [LoanTopupId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanTopupCharge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanTopupCharge_Charge_TopupChargeId] FOREIGN KEY ([TopupChargeId]) REFERENCES [MasterData].[Charge] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanTopupCharge_LoanTopup_LoanTopupId] FOREIGN KEY ([LoanTopupId]) REFERENCES [Loans].[LoanTopup] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanTopupCharge_LoanTopup_LoanTopupId1] FOREIGN KEY ([LoanTopupId1]) REFERENCES [Loans].[LoanTopup] ([Id]),
    CONSTRAINT [FK_LoanTopupCharge_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Loans].[LoanRepayment] (
    [Id] nvarchar(40) NOT NULL,
    [RepaymentMode] nvarchar(100) NOT NULL,
    [LoanAccountId] nvarchar(40) NOT NULL,
    [LoanRepaymentScheduleId] nvarchar(40) NULL,
    [PayrollDeductionScheduleItemId] nvarchar(40) NULL,
    [LoanOffsetId] nvarchar(40) NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Principal] decimal(18,2) NOT NULL,
    [Interest] decimal(18,2) NOT NULL,
    [PeriodStartDate] datetimeoffset NULL,
    [RepaymentDate] datetimeoffset NULL,
    [PaymentAccountId] nvarchar(40) NULL,
    [CustomerBankAccountId] nvarchar(40) NULL,
    [TransactionJournalId] nvarchar(40) NULL,
    [IsProcessed] bit NOT NULL,
    [ProcessedDate] datetime2 NULL,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanRepayment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanRepayment_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_LoanRepayment_CompanyBankAccount_PaymentAccountId] FOREIGN KEY ([PaymentAccountId]) REFERENCES [Accounting].[CompanyBankAccount] ([Id]),
    CONSTRAINT [FK_LoanRepayment_CustomerBankAccount_CustomerBankAccountId] FOREIGN KEY ([CustomerBankAccountId]) REFERENCES [Customer].[CustomerBankAccount] ([Id]),
    CONSTRAINT [FK_LoanRepayment_LoanAccount_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [Loans].[LoanAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanRepayment_LoanOffset_LoanOffsetId] FOREIGN KEY ([LoanOffsetId]) REFERENCES [Loans].[LoanOffset] ([Id]),
    CONSTRAINT [FK_LoanRepayment_LoanRepaymentSchedule_LoanRepaymentScheduleId] FOREIGN KEY ([LoanRepaymentScheduleId]) REFERENCES [Loans].[LoanRepaymentSchedule] ([Id]),
    CONSTRAINT [FK_LoanRepayment_PayrollDeductionScheduleItem_PayrollDeductionScheduleItemId] FOREIGN KEY ([PayrollDeductionScheduleItemId]) REFERENCES [Payroll].[PayrollDeductionScheduleItem] ([Id]),
    CONSTRAINT [FK_LoanRepayment_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE TABLE [Loans].[LoanRepaymentCharge] (
    [Id] nvarchar(40) NOT NULL,
    [LoanRepaymentId] nvarchar(40) NOT NULL,
    [RepaymentChargeId] nvarchar(40) NOT NULL,
    [ChargeType] nvarchar(100) NOT NULL,
    [TotalCharge] decimal(18,2) NOT NULL DEFAULT 0.0,
    [TransactionJournalId] nvarchar(40) NULL,
    [LoanRepaymentId1] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_LoanRepaymentCharge] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanRepaymentCharge_Charge_RepaymentChargeId] FOREIGN KEY ([RepaymentChargeId]) REFERENCES [MasterData].[Charge] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanRepaymentCharge_LoanRepayment_LoanRepaymentId] FOREIGN KEY ([LoanRepaymentId]) REFERENCES [Loans].[LoanRepayment] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LoanRepaymentCharge_LoanRepayment_LoanRepaymentId1] FOREIGN KEY ([LoanRepaymentId1]) REFERENCES [Loans].[LoanRepayment] ([Id]),
    CONSTRAINT [FK_LoanRepaymentCharge_TransactionJournal_TransactionJournalId] FOREIGN KEY ([TransactionJournalId]) REFERENCES [Accounting].[TransactionJournal] ([Id])
);
GO

CREATE INDEX [IX_AccountingPeriod_CalendarId] ON [Accounting].[AccountingPeriod] ([CalendarId]);
GO

CREATE INDEX [IX_AccountingPeriod_FinancialCalendarId] ON [Accounting].[AccountingPeriod] ([FinancialCalendarId]);
GO

CREATE UNIQUE INDEX [IX_AccountingPeriod_Name] ON [Accounting].[AccountingPeriod] ([Name]) WHERE [Name] IS NOT NULL;
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Security].[ApplicationRole] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_ApplicationRoleClaim_PermissionId] ON [Security].[ApplicationRoleClaim] ([PermissionId]);
GO

CREATE INDEX [IX_ApplicationRoleClaim_RoleId] ON [Security].[ApplicationRoleClaim] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Security].[ApplicationUser] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Security].[ApplicationUser] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_ApplicationUserClaim_PermissionId] ON [Security].[ApplicationUserClaim] ([PermissionId]);
GO

CREATE INDEX [IX_ApplicationUserClaim_UserId] ON [Security].[ApplicationUserClaim] ([UserId]);
GO

CREATE INDEX [IX_ApplicationUserLogin_UserId] ON [Security].[ApplicationUserLogin] ([UserId]);
GO

CREATE INDEX [IX_ApplicationUserRole_RoleId] ON [Security].[ApplicationUserRole] ([RoleId]);
GO

CREATE INDEX [IX_Approval_ApprovalWorkflowId] ON [Security].[Approval] ([ApprovalWorkflowId]);
GO

CREATE INDEX [IX_ApprovalDocument_ApprovalId] ON [Security].[ApprovalDocument] ([ApprovalId]);
GO

CREATE INDEX [IX_ApprovalGroup_ApprovalWorkflowId] ON [Security].[ApprovalGroup] ([ApprovalWorkflowId]);
GO

CREATE INDEX [IX_ApprovalGroupMember_ApplicationUserId] ON [Security].[ApprovalGroupMember] ([ApplicationUserId]);
GO

CREATE INDEX [IX_ApprovalGroupMember_ApprovalGroupId] ON [Security].[ApprovalGroupMember] ([ApprovalGroupId]);
GO

CREATE INDEX [IX_ApprovalGroupMember_ApprovalGroupId1] ON [Security].[ApprovalGroupMember] ([ApprovalGroupId1]);
GO

CREATE INDEX [IX_ApprovalGroupWorkflow_ApprovalGroupId] ON [Security].[ApprovalGroupWorkflow] ([ApprovalGroupId]);
GO

CREATE INDEX [IX_ApprovalGroupWorkflow_ApprovalWorkflowId] ON [Security].[ApprovalGroupWorkflow] ([ApprovalWorkflowId]);
GO

CREATE INDEX [IX_ApprovalLog_ApprovalGroupId] ON [Security].[ApprovalLog] ([ApprovalGroupId]);
GO

CREATE INDEX [IX_ApprovalLog_ApprovalId] ON [Security].[ApprovalLog] ([ApprovalId]);
GO

CREATE INDEX [IX_ApprovalLog_ApprovedByUserId] ON [Security].[ApprovalLog] ([ApprovedByUserId]);
GO

CREATE INDEX [IX_ApprovalNotification_ApprovalWorkflowId] ON [Security].[ApprovalNotification] ([ApprovalWorkflowId]);
GO

CREATE UNIQUE INDEX [IX_ApprovalRole_EventGlobalCodeId_RoleId] ON [Security].[ApprovalRole] ([EventGlobalCodeId], [RoleId]);
GO

CREATE UNIQUE INDEX [IX_ApprovalRole_EventGlobalCodeId_RoleId_Order] ON [Security].[ApprovalRole] ([EventGlobalCodeId], [RoleId], [Order]);
GO

CREATE INDEX [IX_ApprovalRole_RoleId] ON [Security].[ApprovalRole] ([RoleId]);
GO

CREATE INDEX [IX_AuditTrail_ApplicationUserId] ON [Security].[AuditTrail] ([ApplicationUserId]);
GO

CREATE INDEX [IX_AuditTrail_EventGlobalCodeId] ON [Security].[AuditTrail] ([EventGlobalCodeId]);
GO

CREATE UNIQUE INDEX [IX_Bank_Name] ON [MasterData].[Bank] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Bank_Name_Code] ON [MasterData].[Bank] ([Name], [Code]);
GO

CREATE UNIQUE INDEX [IX_Charge_Code] ON [MasterData].[Charge] ([Code]);
GO

CREATE UNIQUE INDEX [IX_Charge_Code_Name] ON [MasterData].[Charge] ([Code], [Name]);
GO

CREATE INDEX [IX_Charge_CurrencyId] ON [MasterData].[Charge] ([CurrencyId]);
GO

CREATE UNIQUE INDEX [IX_CompanyBankAccount_BankId] ON [Accounting].[CompanyBankAccount] ([BankId]);
GO

CREATE UNIQUE INDEX [IX_CompanyBankAccount_BankId_LedgerAccountId] ON [Accounting].[CompanyBankAccount] ([BankId], [LedgerAccountId]);
GO

CREATE INDEX [IX_CompanyBankAccount_CurrencyId] ON [Accounting].[CompanyBankAccount] ([CurrencyId]);
GO

CREATE INDEX [IX_CompanyBankAccount_LedgerAccountId] ON [Accounting].[CompanyBankAccount] ([LedgerAccountId]);
GO

CREATE UNIQUE INDEX [IX_Customer_ApplicationUserId] ON [Customer].[Customer] ([ApplicationUserId]) WHERE [ApplicationUserId] IS NOT NULL;
GO

CREATE INDEX [IX_Customer_CashAccountId] ON [Customer].[Customer] ([CashAccountId]);
GO

CREATE INDEX [IX_Customer_DepartmentId] ON [Customer].[Customer] ([DepartmentId]);
GO

CREATE INDEX [IX_CustomerBankAccount_BankId] ON [Customer].[CustomerBankAccount] ([BankId]);
GO

CREATE INDEX [IX_CustomerBankAccount_CustomerId] ON [Customer].[CustomerBankAccount] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerBankAccount_LedgerAccountId] ON [Customer].[CustomerBankAccount] ([LedgerAccountId]);
GO

CREATE INDEX [IX_CustomerBeneficiary_CustomerId] ON [Customer].[CustomerBeneficiary] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerDepositProductPublication_CustomerId] ON [Deposits].[CustomerDepositProductPublication] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerDepositProductPublication_ProductId] ON [Deposits].[CustomerDepositProductPublication] ([ProductId]);
GO

CREATE INDEX [IX_CustomerLoanProductPublication_CustomerId] ON [Loans].[CustomerLoanProductPublication] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerLoanProductPublication_ProductId] ON [Loans].[CustomerLoanProductPublication] ([ProductId]);
GO

CREATE INDEX [IX_CustomerNextOfKin_CustomerId] ON [Customer].[CustomerNextOfKin] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerPaymentDocument_CustomerId] ON [Docs].[CustomerPaymentDocument] ([CustomerId]);
GO

CREATE UNIQUE INDEX [IX_Department_Name] ON [MasterData].[Department] ([Name]);
GO

CREATE INDEX [IX_DepartmentDepositProductPublication_DepartmentId] ON [Deposits].[DepartmentDepositProductPublication] ([DepartmentId]);
GO

CREATE INDEX [IX_DepartmentDepositProductPublication_ProductId] ON [Deposits].[DepartmentDepositProductPublication] ([ProductId]);
GO

CREATE INDEX [IX_DepartmentLoanProductPublication_DepartmentId] ON [Loans].[DepartmentLoanProductPublication] ([DepartmentId]);
GO

CREATE INDEX [IX_DepartmentLoanProductPublication_ProductId] ON [Loans].[DepartmentLoanProductPublication] ([ProductId]);
GO

CREATE INDEX [IX_DepositProduct_ApprovalId] ON [Deposits].[DepositProduct] ([ApprovalId]);
GO

CREATE INDEX [IX_DepositProduct_ApprovalWorkflowId] ON [Deposits].[DepositProduct] ([ApprovalWorkflowId]);
GO

CREATE INDEX [IX_DepositProduct_BankDepositAccountId] ON [Deposits].[DepositProduct] ([BankDepositAccountId]);
GO

CREATE INDEX [IX_DepositProduct_ChargesAccrualAccountId] ON [Deposits].[DepositProduct] ([ChargesAccrualAccountId]);
GO

CREATE INDEX [IX_DepositProduct_ChargesIncomeAccountId] ON [Deposits].[DepositProduct] ([ChargesIncomeAccountId]);
GO

CREATE INDEX [IX_DepositProduct_ChargesWaivedAccountId] ON [Deposits].[DepositProduct] ([ChargesWaivedAccountId]);
GO

CREATE INDEX [IX_DepositProduct_DefaultCurrencyId] ON [Deposits].[DepositProduct] ([DefaultCurrencyId]);
GO

CREATE INDEX [IX_DepositProduct_InterestPayableAccountId] ON [Deposits].[DepositProduct] ([InterestPayableAccountId]);
GO

CREATE INDEX [IX_DepositProduct_InterestPayoutAccountId] ON [Deposits].[DepositProduct] ([InterestPayoutAccountId]);
GO

CREATE UNIQUE INDEX [IX_DepositProduct_Name] ON [Deposits].[DepositProduct] ([Name]);
GO

CREATE UNIQUE INDEX [IX_DepositProduct_Name_Code] ON [Deposits].[DepositProduct] ([Name], [Code]);
GO

CREATE INDEX [IX_DepositProduct_ProductDepositAccountId] ON [Deposits].[DepositProduct] ([ProductDepositAccountId]);
GO

CREATE INDEX [IX_DepositProduct_PublishedByUserId] ON [Deposits].[DepositProduct] ([PublishedByUserId]);
GO

CREATE INDEX [IX_DepositProductCharge_ChargeId] ON [Deposits].[DepositProductCharge] ([ChargeId]);
GO

CREATE UNIQUE INDEX [IX_DepositProductCharge_ProductId_ChargeId] ON [Deposits].[DepositProductCharge] ([ProductId], [ChargeId]);
GO

CREATE UNIQUE INDEX [IX_DepositProductInterestRange_ProductId_LowerLimit_UpperLimit] ON [Deposits].[DepositProductInterestRange] ([ProductId], [LowerLimit], [UpperLimit]);
GO

CREATE UNIQUE INDEX [IX_DocumentType_Name] ON [Docs].[DocumentType] ([Name]);
GO

CREATE INDEX [IX_Employee_CustomerId] ON [HR].[Employee] ([CustomerId]);
GO

CREATE INDEX [IX_Employee_DepartmentId] ON [HR].[Employee] ([DepartmentId]);
GO

CREATE UNIQUE INDEX [IX_Employee_EmployeeNo] ON [HR].[Employee] ([EmployeeNo]);
GO

CREATE INDEX [IX_EnrollmentPaymentInfo_MemberProfileId] ON [Security].[EnrollmentPaymentInfo] ([MemberProfileId]);
GO

CREATE UNIQUE INDEX [IX_FinancialCalendar_Name] ON [Accounting].[FinancialCalendar] ([Name]);
GO

CREATE UNIQUE INDEX [IX_FinancialCalendar_Name_Code] ON [Accounting].[FinancialCalendar] ([Name], [Code]);
GO

CREATE UNIQUE INDEX [IX_FixedDepositAccount_AccountNo] ON [Deposits].[FixedDepositAccount] ([AccountNo]);
GO

CREATE INDEX [IX_FixedDepositAccount_ApplicationId] ON [Deposits].[FixedDepositAccount] ([ApplicationId]);
GO

CREATE INDEX [IX_FixedDepositAccount_ChargesAccruedAccountId] ON [Deposits].[FixedDepositAccount] ([ChargesAccruedAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_ChargesIncomeAccountId] ON [Deposits].[FixedDepositAccount] ([ChargesIncomeAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_ChargesWaivedAccountId] ON [Deposits].[FixedDepositAccount] ([ChargesWaivedAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_ClosedByUserId] ON [Deposits].[FixedDepositAccount] ([ClosedByUserId]);
GO

CREATE INDEX [IX_FixedDepositAccount_CustomerBankLiquidationAccountId] ON [Deposits].[FixedDepositAccount] ([CustomerBankLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_CustomerId] ON [Deposits].[FixedDepositAccount] ([CustomerId]);
GO

CREATE INDEX [IX_FixedDepositAccount_DepositAccountId] ON [Deposits].[FixedDepositAccount] ([DepositAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_DepositProductId] ON [Deposits].[FixedDepositAccount] ([DepositProductId]);
GO

CREATE INDEX [IX_FixedDepositAccount_InterestEarnedAccountId] ON [Deposits].[FixedDepositAccount] ([InterestEarnedAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_InterestPayoutAccountId] ON [Deposits].[FixedDepositAccount] ([InterestPayoutAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_SavingsLiquidationAccountId] ON [Deposits].[FixedDepositAccount] ([SavingsLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_SpecialDepositLiquidationAccountId] ON [Deposits].[FixedDepositAccount] ([SpecialDepositLiquidationAccountId]);
GO

CREATE UNIQUE INDEX [IX_FixedDepositAccountApplication_ApplicationNo] ON [Deposits].[FixedDepositAccountApplication] ([ApplicationNo]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_ApprovalId] ON [Deposits].[FixedDepositAccountApplication] ([ApprovalId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_CustomerBankFundingSourceAccountId] ON [Deposits].[FixedDepositAccountApplication] ([CustomerBankFundingSourceAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_CustomerBankLiquidationAccountId] ON [Deposits].[FixedDepositAccountApplication] ([CustomerBankLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_CustomerId] ON [Deposits].[FixedDepositAccountApplication] ([CustomerId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_DepositProductId] ON [Deposits].[FixedDepositAccountApplication] ([DepositProductId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_PaymentDocumentId] ON [Deposits].[FixedDepositAccountApplication] ([PaymentDocumentId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_SavingsLiquidationAccountId] ON [Deposits].[FixedDepositAccountApplication] ([SavingsLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_SpecialDepositFundingSourceAccountId] ON [Deposits].[FixedDepositAccountApplication] ([SpecialDepositFundingSourceAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccountApplication_SpecialDepositLiquidationAccountId] ON [Deposits].[FixedDepositAccountApplication] ([SpecialDepositLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositChangeInMaturity_ApprovalId] ON [Deposits].[FixedDepositChangeInMaturity] ([ApprovalId]);
GO

CREATE INDEX [IX_FixedDepositChangeInMaturity_CustomerBankLiquidationAccountId] ON [Deposits].[FixedDepositChangeInMaturity] ([CustomerBankLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositChangeInMaturity_FixedDepositAccountId] ON [Deposits].[FixedDepositChangeInMaturity] ([FixedDepositAccountId]);
GO

CREATE INDEX [IX_FixedDepositChangeInMaturity_SavingsLiquidationAccountId] ON [Deposits].[FixedDepositChangeInMaturity] ([SavingsLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositChangeInMaturity_SpecialDepositLiquidationAccountId] ON [Deposits].[FixedDepositChangeInMaturity] ([SpecialDepositLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositInterestAddition_FixedDepositAccountId] ON [Deposits].[FixedDepositInterestAddition] ([FixedDepositAccountId]);
GO

CREATE INDEX [IX_FixedDepositInterestAddition_InterestScheduleItemId] ON [Deposits].[FixedDepositInterestAddition] ([InterestScheduleItemId]);
GO

CREATE INDEX [IX_FixedDepositInterestAddition_TransactionJournalId] ON [Deposits].[FixedDepositInterestAddition] ([TransactionJournalId]);
GO

CREATE INDEX [IX_FixedDepositInterestSchedule_CronJobConfigId] ON [Deposits].[FixedDepositInterestSchedule] ([CronJobConfigId]);
GO

CREATE INDEX [IX_FixedDepositInterestScheduleItem_FixedDepositAccountId] ON [Deposits].[FixedDepositInterestScheduleItem] ([FixedDepositAccountId]);
GO

CREATE INDEX [IX_FixedDepositInterestScheduleItem_FixedDepositInterestScheduleId] ON [Deposits].[FixedDepositInterestScheduleItem] ([FixedDepositInterestScheduleId]);
GO

CREATE INDEX [IX_FixedDepositInterestScheduleItem_FixedDepositInterestScheduleId1] ON [Deposits].[FixedDepositInterestScheduleItem] ([FixedDepositInterestScheduleId1]);
GO

CREATE INDEX [IX_FixedDepositLiquidation_ApprovalId] ON [Deposits].[FixedDepositLiquidation] ([ApprovalId]);
GO

CREATE INDEX [IX_FixedDepositLiquidation_CustomerBankLiquidationAccountId] ON [Deposits].[FixedDepositLiquidation] ([CustomerBankLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositLiquidation_FixedDepositAccountId] ON [Deposits].[FixedDepositLiquidation] ([FixedDepositAccountId]);
GO

CREATE INDEX [IX_FixedDepositLiquidation_SavingsLiquidationAccountId] ON [Deposits].[FixedDepositLiquidation] ([SavingsLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositLiquidation_SpecialDepositLiquidationAccountId] ON [Deposits].[FixedDepositLiquidation] ([SpecialDepositLiquidationAccountId]);
GO

CREATE INDEX [IX_FixedDepositLiquidation_TransactionJournalId] ON [Deposits].[FixedDepositLiquidation] ([TransactionJournalId]);
GO

CREATE INDEX [IX_FixedDepositLiquidationCharge_FixedDepositLiquidationId] ON [Deposits].[FixedDepositLiquidationCharge] ([FixedDepositLiquidationId]);
GO

CREATE INDEX [IX_FixedDepositLiquidationCharge_TransactionJournalId] ON [Deposits].[FixedDepositLiquidationCharge] ([TransactionJournalId]);
GO

CREATE UNIQUE INDEX [IX_GlobalCode_Name_Code_CodeType] ON [MasterData].[GlobalCode] ([Name], [Code], [CodeType]);
GO

CREATE INDEX [IX_JournalEntry_AccountId] ON [Accounting].[JournalEntry] ([AccountId]);
GO

CREATE UNIQUE INDEX [IX_JournalEntry_TransactionEntryNo] ON [Accounting].[JournalEntry] ([TransactionEntryNo]);
GO

CREATE UNIQUE INDEX [IX_JournalEntry_TransactionEntryNo_AccountId] ON [Accounting].[JournalEntry] ([TransactionEntryNo], [AccountId]);
GO

CREATE INDEX [IX_JournalEntry_TransactionJournalId] ON [Accounting].[JournalEntry] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LedgerAccount_CurrencyId] ON [Accounting].[LedgerAccount] ([CurrencyId]);
GO

CREATE UNIQUE INDEX [IX_LedgerAccount_Name] ON [Accounting].[LedgerAccount] ([Name]);
GO

CREATE UNIQUE INDEX [IX_LedgerAccount_Name_Code] ON [Accounting].[LedgerAccount] ([Name], [Code]);
GO

CREATE INDEX [IX_LedgerAccount_ParentId] ON [Accounting].[LedgerAccount] ([ParentId]);
GO

CREATE UNIQUE INDEX [IX_LienType_Name] ON [Accounting].[LienType] ([Name]);
GO

CREATE UNIQUE INDEX [IX_LoanAccount_AccountNo] ON [Loans].[LoanAccount] ([AccountNo]);
GO

CREATE INDEX [IX_LoanAccount_ChargesAccruedAccountId] ON [Loans].[LoanAccount] ([ChargesAccruedAccountId]);
GO

CREATE INDEX [IX_LoanAccount_ChargesIncomeAccountId] ON [Loans].[LoanAccount] ([ChargesIncomeAccountId]);
GO

CREATE INDEX [IX_LoanAccount_ChargesWaivedAccountId] ON [Loans].[LoanAccount] ([ChargesWaivedAccountId]);
GO

CREATE INDEX [IX_LoanAccount_ClosedByUserId] ON [Loans].[LoanAccount] ([ClosedByUserId]);
GO

CREATE INDEX [IX_LoanAccount_CustomerId] ON [Loans].[LoanAccount] ([CustomerId]);
GO

CREATE INDEX [IX_LoanAccount_DestinationAccountId] ON [Loans].[LoanAccount] ([DestinationAccountId]);
GO

CREATE INDEX [IX_LoanAccount_EarnedInterestAccountId] ON [Loans].[LoanAccount] ([EarnedInterestAccountId]);
GO

CREATE INDEX [IX_LoanAccount_InterestBalanceAccountId] ON [Loans].[LoanAccount] ([InterestBalanceAccountId]);
GO

CREATE INDEX [IX_LoanAccount_InterestEarnedAccountId] ON [Loans].[LoanAccount] ([InterestEarnedAccountId]);
GO

CREATE INDEX [IX_LoanAccount_InterestLossAccountId] ON [Loans].[LoanAccount] ([InterestLossAccountId]);
GO

CREATE INDEX [IX_LoanAccount_InterestPayoutAccountId] ON [Loans].[LoanAccount] ([InterestPayoutAccountId]);
GO

CREATE INDEX [IX_LoanAccount_InterestWaivedAccountId] ON [Loans].[LoanAccount] ([InterestWaivedAccountId]);
GO

CREATE INDEX [IX_LoanAccount_LoanApplicationId] ON [Loans].[LoanAccount] ([LoanApplicationId]);
GO

CREATE INDEX [IX_LoanAccount_LoanTopupId1] ON [Loans].[LoanAccount] ([LoanTopupId1]);
GO

CREATE INDEX [IX_LoanAccount_PrincipalBalanceAccountId] ON [Loans].[LoanAccount] ([PrincipalBalanceAccountId]);
GO

CREATE INDEX [IX_LoanAccount_PrincipalLossAccountId] ON [Loans].[LoanAccount] ([PrincipalLossAccountId]);
GO

CREATE INDEX [IX_LoanAccount_SpecialDepositAccountId] ON [Loans].[LoanAccount] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_LoanAccount_UnearnedInterestAccountId] ON [Loans].[LoanAccount] ([UnearnedInterestAccountId]);
GO

CREATE UNIQUE INDEX [IX_LoanApplication_ApplicationNo] ON [Loans].[LoanApplication] ([ApplicationNo]);
GO

CREATE INDEX [IX_LoanApplication_ApprovalId] ON [Loans].[LoanApplication] ([ApprovalId]);
GO

CREATE INDEX [IX_LoanApplication_CustomerDisbursementAccountId] ON [Loans].[LoanApplication] ([CustomerDisbursementAccountId]);
GO

CREATE INDEX [IX_LoanApplication_CustomerId] ON [Loans].[LoanApplication] ([CustomerId]);
GO

CREATE INDEX [IX_LoanApplication_LoanProductId] ON [Loans].[LoanApplication] ([LoanProductId]);
GO

CREATE INDEX [IX_LoanApplication_SpecialDepositId] ON [Loans].[LoanApplication] ([SpecialDepositId]);
GO

CREATE INDEX [IX_LoanApplicationApproval_LoanApplicationId] ON [Loans].[LoanApplicationApproval] ([LoanApplicationId]);
GO

CREATE INDEX [IX_LoanApplicationGuarantor_GuarantorId] ON [Loans].[LoanApplicationGuarantor] ([GuarantorId]);
GO

CREATE INDEX [IX_LoanApplicationGuarantor_LoanApplicationId] ON [Loans].[LoanApplicationGuarantor] ([LoanApplicationId]);
GO

CREATE INDEX [IX_LoanApplicationItem_LoanApplicationId] ON [Loans].[LoanApplicationItem] ([LoanApplicationId]);
GO

CREATE INDEX [IX_LoanApplicationItem_Name_Model_BrandName_Color] ON [Loans].[LoanApplicationItem] ([Name], [Model], [BrandName], [Color]);
GO

CREATE INDEX [IX_LoanApplicationSchedule_LoanApplicationId] ON [Loans].[LoanApplicationSchedule] ([LoanApplicationId]);
GO

CREATE UNIQUE INDEX [IX_LoanApplicationSchedule_LoanApplicationId_RepaymentNo] ON [Loans].[LoanApplicationSchedule] ([LoanApplicationId], [RepaymentNo]);
GO

CREATE INDEX [IX_LoanDisbursement_ApprovalId] ON [Loans].[LoanDisbursement] ([ApprovalId]);
GO

CREATE INDEX [IX_LoanDisbursement_ApprovedByUserId] ON [Loans].[LoanDisbursement] ([ApprovedByUserId]);
GO

CREATE INDEX [IX_LoanDisbursement_CustomerBankAccountId] ON [Loans].[LoanDisbursement] ([CustomerBankAccountId]);
GO

CREATE INDEX [IX_LoanDisbursement_DisbursedByUserId] ON [Loans].[LoanDisbursement] ([DisbursedByUserId]);
GO

CREATE INDEX [IX_LoanDisbursement_DisbursementAccountId] ON [Loans].[LoanDisbursement] ([DisbursementAccountId]);
GO

CREATE INDEX [IX_LoanDisbursement_LoanAccountId] ON [Loans].[LoanDisbursement] ([LoanAccountId]);
GO

CREATE INDEX [IX_LoanDisbursement_LoanAccountId1] ON [Loans].[LoanDisbursement] ([LoanAccountId1]);
GO

CREATE INDEX [IX_LoanDisbursement_SpecialDepositAccountId] ON [Loans].[LoanDisbursement] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_LoanDisbursement_TransactionJournalId] ON [Loans].[LoanDisbursement] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LoanDisbursementCharge_DisbursementChargeId] ON [Loans].[LoanDisbursementCharge] ([DisbursementChargeId]);
GO

CREATE INDEX [IX_LoanDisbursementCharge_LoanDisbursementId] ON [Loans].[LoanDisbursementCharge] ([LoanDisbursementId]);
GO

CREATE INDEX [IX_LoanDisbursementCharge_TransactionJournalId] ON [Loans].[LoanDisbursementCharge] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LoanOffset_ApprovalId] ON [Loans].[LoanOffset] ([ApprovalId]);
GO

CREATE INDEX [IX_LoanOffset_CustomerBankAccountId] ON [Loans].[LoanOffset] ([CustomerBankAccountId]);
GO

CREATE INDEX [IX_LoanOffset_CustomerPaymentDocumentId] ON [Loans].[LoanOffset] ([CustomerPaymentDocumentId]);
GO

CREATE INDEX [IX_LoanOffset_LoanAccountId] ON [Loans].[LoanOffset] ([LoanAccountId]);
GO

CREATE INDEX [IX_LoanOffset_SavingsAccountId] ON [Loans].[LoanOffset] ([SavingsAccountId]);
GO

CREATE INDEX [IX_LoanOffset_SpecialDepositAccountId] ON [Loans].[LoanOffset] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_LoanOffset_TransactionJournalId] ON [Loans].[LoanOffset] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LoanOffSetCharge_LoanOffsetId] ON [Loans].[LoanOffSetCharge] ([LoanOffsetId]);
GO

CREATE INDEX [IX_LoanOffSetCharge_LoanOffsetId1] ON [Loans].[LoanOffSetCharge] ([LoanOffsetId1]);
GO

CREATE INDEX [IX_LoanOffSetCharge_OffsetChargeId] ON [Loans].[LoanOffSetCharge] ([OffsetChargeId]);
GO

CREATE INDEX [IX_LoanOffSetCharge_TransactionJournalId] ON [Loans].[LoanOffSetCharge] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LoanProduct_ApprovalId] ON [Loans].[LoanProduct] ([ApprovalId]);
GO

CREATE INDEX [IX_LoanProduct_ApprovalWorkflowId] ON [Loans].[LoanProduct] ([ApprovalWorkflowId]);
GO

CREATE INDEX [IX_LoanProduct_BankDepositAccountId] ON [Loans].[LoanProduct] ([BankDepositAccountId]);
GO

CREATE INDEX [IX_LoanProduct_ChargesAccrualAccountId] ON [Loans].[LoanProduct] ([ChargesAccrualAccountId]);
GO

CREATE INDEX [IX_LoanProduct_ChargesIncomeAccountId] ON [Loans].[LoanProduct] ([ChargesIncomeAccountId]);
GO

CREATE INDEX [IX_LoanProduct_ChargesWaivedAccountId] ON [Loans].[LoanProduct] ([ChargesWaivedAccountId]);
GO

CREATE INDEX [IX_LoanProduct_DefaultCurrencyId] ON [Loans].[LoanProduct] ([DefaultCurrencyId]);
GO

CREATE INDEX [IX_LoanProduct_DisbursementAccountId] ON [Loans].[LoanProduct] ([DisbursementAccountId]);
GO

CREATE INDEX [IX_LoanProduct_InterestIncomeAccountId] ON [Loans].[LoanProduct] ([InterestIncomeAccountId]);
GO

CREATE INDEX [IX_LoanProduct_InterestLossAccountId] ON [Loans].[LoanProduct] ([InterestLossAccountId]);
GO

CREATE INDEX [IX_LoanProduct_InterestWaivedAccountId] ON [Loans].[LoanProduct] ([InterestWaivedAccountId]);
GO

CREATE UNIQUE INDEX [IX_LoanProduct_Name] ON [Loans].[LoanProduct] ([Name]);
GO

CREATE UNIQUE INDEX [IX_LoanProduct_Name_Code] ON [Loans].[LoanProduct] ([Name], [Code]);
GO

CREATE INDEX [IX_LoanProduct_PenalInterestReceivableAccountId] ON [Loans].[LoanProduct] ([PenalInterestReceivableAccountId]);
GO

CREATE INDEX [IX_LoanProduct_PrincipalAccountId] ON [Loans].[LoanProduct] ([PrincipalAccountId]);
GO

CREATE INDEX [IX_LoanProduct_PrincipalLossAccountId] ON [Loans].[LoanProduct] ([PrincipalLossAccountId]);
GO

CREATE INDEX [IX_LoanProduct_PublishedByUserId] ON [Loans].[LoanProduct] ([PublishedByUserId]);
GO

CREATE INDEX [IX_LoanProduct_UnearnedInterestAccountId] ON [Loans].[LoanProduct] ([UnearnedInterestAccountId]);
GO

CREATE INDEX [IX_LoanProductCharge_ChargeId] ON [Loans].[LoanProductCharge] ([ChargeId]);
GO

CREATE INDEX [IX_LoanProductCharge_ProductId] ON [Loans].[LoanProductCharge] ([ProductId]);
GO

CREATE INDEX [IX_LoanRepayment_ApprovalId] ON [Loans].[LoanRepayment] ([ApprovalId]);
GO

CREATE INDEX [IX_LoanRepayment_CustomerBankAccountId] ON [Loans].[LoanRepayment] ([CustomerBankAccountId]);
GO

CREATE INDEX [IX_LoanRepayment_LoanAccountId] ON [Loans].[LoanRepayment] ([LoanAccountId]);
GO

CREATE INDEX [IX_LoanRepayment_LoanOffsetId] ON [Loans].[LoanRepayment] ([LoanOffsetId]);
GO

CREATE INDEX [IX_LoanRepayment_LoanRepaymentScheduleId] ON [Loans].[LoanRepayment] ([LoanRepaymentScheduleId]);
GO

CREATE INDEX [IX_LoanRepayment_PaymentAccountId] ON [Loans].[LoanRepayment] ([PaymentAccountId]);
GO

CREATE INDEX [IX_LoanRepayment_PayrollDeductionScheduleItemId] ON [Loans].[LoanRepayment] ([PayrollDeductionScheduleItemId]);
GO

CREATE INDEX [IX_LoanRepayment_TransactionJournalId] ON [Loans].[LoanRepayment] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LoanRepaymentCharge_LoanRepaymentId] ON [Loans].[LoanRepaymentCharge] ([LoanRepaymentId]);
GO

CREATE INDEX [IX_LoanRepaymentCharge_LoanRepaymentId1] ON [Loans].[LoanRepaymentCharge] ([LoanRepaymentId1]);
GO

CREATE INDEX [IX_LoanRepaymentCharge_RepaymentChargeId] ON [Loans].[LoanRepaymentCharge] ([RepaymentChargeId]);
GO

CREATE INDEX [IX_LoanRepaymentCharge_TransactionJournalId] ON [Loans].[LoanRepaymentCharge] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LoanRepaymentSchedule_LoanAccountId] ON [Loans].[LoanRepaymentSchedule] ([LoanAccountId]);
GO

CREATE INDEX [IX_LoanTopup_ApprovalId] ON [Loans].[LoanTopup] ([ApprovalId]);
GO

CREATE INDEX [IX_LoanTopup_CustomerBankAccountId] ON [Loans].[LoanTopup] ([CustomerBankAccountId]);
GO

CREATE INDEX [IX_LoanTopup_LoanAccountId] ON [Loans].[LoanTopup] ([LoanAccountId]);
GO

CREATE INDEX [IX_LoanTopup_SpecialDepositAccountId] ON [Loans].[LoanTopup] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_LoanTopup_TransactionJournalId] ON [Loans].[LoanTopup] ([TransactionJournalId]);
GO

CREATE INDEX [IX_LoanTopupCharge_LoanTopupId] ON [Loans].[LoanTopupCharge] ([LoanTopupId]);
GO

CREATE INDEX [IX_LoanTopupCharge_LoanTopupId1] ON [Loans].[LoanTopupCharge] ([LoanTopupId1]);
GO

CREATE INDEX [IX_LoanTopupCharge_TopupChargeId] ON [Loans].[LoanTopupCharge] ([TopupChargeId]);
GO

CREATE INDEX [IX_LoanTopupCharge_TransactionJournalId] ON [Loans].[LoanTopupCharge] ([TransactionJournalId]);
GO

CREATE UNIQUE INDEX [IX_Location_Name] ON [MasterData].[Location] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Location_Name_Code] ON [MasterData].[Location] ([Name], [Code]);
GO

CREATE INDEX [IX_Location_ParentId] ON [MasterData].[Location] ([ParentId]);
GO

CREATE INDEX [IX_MemberBankAccount_BankId] ON [Security].[MemberBankAccount] ([BankId]);
GO

CREATE INDEX [IX_MemberBankAccount_ProfileId] ON [Security].[MemberBankAccount] ([ProfileId]);
GO

CREATE INDEX [IX_MemberBeneficiary_ProfileId] ON [Security].[MemberBeneficiary] ([ProfileId]);
GO

CREATE INDEX [IX_MemberNextOfKin_ProfileId] ON [Security].[MemberNextOfKin] ([ProfileId]);
GO

CREATE UNIQUE INDEX [IX_MemberProfile_ApplicationUserId] ON [Security].[MemberProfile] ([ApplicationUserId]);
GO

CREATE INDEX [IX_MemberProfile_DepartmentId] ON [Security].[MemberProfile] ([DepartmentId]);
GO

CREATE UNIQUE INDEX [IX_OfficeDocument_DocumentNo] ON [Docs].[OfficeDocument] ([DocumentNo]);
GO

CREATE INDEX [IX_OfficeDocument_DocumentTypeId] ON [Docs].[OfficeDocument] ([DocumentTypeId]);
GO

CREATE UNIQUE INDEX [IX_OfficePhoto_DocumentNo] ON [Docs].[OfficePhoto] ([DocumentNo]);
GO

CREATE INDEX [IX_OfficePhoto_DocumentTypeId] ON [Docs].[OfficePhoto] ([DocumentTypeId]);
GO

CREATE UNIQUE INDEX [IX_OfficeSheet_DocumentNo] ON [Docs].[OfficeSheet] ([DocumentNo]);
GO

CREATE INDEX [IX_OfficeSheet_DocumentTypeId] ON [Docs].[OfficeSheet] ([DocumentTypeId]);
GO

CREATE UNIQUE INDEX [IX_PaymentMode_Name] ON [Accounting].[PaymentMode] ([Name]);
GO

CREATE INDEX [IX_PayrollCronJobConfig_DeductionScheduleId] ON [Payroll].[PayrollCronJobConfig] ([DeductionScheduleId]);
GO

CREATE INDEX [IX_PayrollDeductionDocument_PayrollDeductionScheduleId] ON [Docs].[PayrollDeductionDocument] ([PayrollDeductionScheduleId]);
GO

CREATE INDEX [IX_PayrollDeductionItem_PayrollDeductionScheduleId] ON [Payroll].[PayrollDeductionItem] ([PayrollDeductionScheduleId]);
GO

CREATE INDEX [IX_PayrollDeductionSchedule_BankAccountId] ON [Payroll].[PayrollDeductionSchedule] ([BankAccountId]);
GO

CREATE INDEX [IX_PayrollDeductionSchedule_FixedDepositBankAccountId] ON [Payroll].[PayrollDeductionSchedule] ([FixedDepositBankAccountId]);
GO

CREATE UNIQUE INDEX [IX_PayrollDeductionSchedule_ScheduleName] ON [Payroll].[PayrollDeductionSchedule] ([ScheduleName]);
GO

CREATE INDEX [IX_PayrollDeductionSchedule_SpecialDepositBankAccountId] ON [Payroll].[PayrollDeductionSchedule] ([SpecialDepositBankAccountId]);
GO

CREATE INDEX [IX_PayrollDeductionScheduleItem_LoanRepaymentScheduleId] ON [Payroll].[PayrollDeductionScheduleItem] ([LoanRepaymentScheduleId]);
GO

CREATE INDEX [IX_PayrollDeductionScheduleItem_PayrollCronJobConfigId] ON [Payroll].[PayrollDeductionScheduleItem] ([PayrollCronJobConfigId]);
GO

CREATE INDEX [IX_PayrollDeductionScheduleItem_PayrollDeductionScheduleId] ON [Payroll].[PayrollDeductionScheduleItem] ([PayrollDeductionScheduleId]);
GO

CREATE INDEX [IX_PayrollDeductionScheduleItem_SavingsAccountDeductionScheduleId] ON [Payroll].[PayrollDeductionScheduleItem] ([SavingsAccountDeductionScheduleId]);
GO

CREATE INDEX [IX_PayrollDeductionScheduleItem_SpecialDepositAccountDeductionScheduleId] ON [Payroll].[PayrollDeductionScheduleItem] ([SpecialDepositAccountDeductionScheduleId]);
GO

CREATE UNIQUE INDEX [IX_Permission_Name_Code] ON [Security].[Permission] ([Name], [Code]);
GO

CREATE UNIQUE INDEX [IX_Permission_Name_Code_Category] ON [Security].[Permission] ([Name], [Code], [Category]) WHERE [Name] IS NOT NULL AND [Code] IS NOT NULL AND [Category] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Permission_Name_Code_Group] ON [Security].[Permission] ([Name], [Code], [Group]) WHERE [Name] IS NOT NULL AND [Code] IS NOT NULL AND [Group] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Permission_Name_Code_Module] ON [Security].[Permission] ([Name], [Code], [Module]) WHERE [Name] IS NOT NULL AND [Code] IS NOT NULL AND [Module] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_SavingsAccount_AccountNo] ON [Deposits].[SavingsAccount] ([AccountNo]);
GO

CREATE INDEX [IX_SavingsAccount_ApplicationId] ON [Deposits].[SavingsAccount] ([ApplicationId]);
GO

CREATE INDEX [IX_SavingsAccount_ChargesAccruedAccountId] ON [Deposits].[SavingsAccount] ([ChargesAccruedAccountId]);
GO

CREATE INDEX [IX_SavingsAccount_ChargesIncomeAccountId] ON [Deposits].[SavingsAccount] ([ChargesIncomeAccountId]);
GO

CREATE INDEX [IX_SavingsAccount_ChargesPayableAccountId] ON [Deposits].[SavingsAccount] ([ChargesPayableAccountId]);
GO

CREATE INDEX [IX_SavingsAccount_ChargesWaivedAccountId] ON [Deposits].[SavingsAccount] ([ChargesWaivedAccountId]);
GO

CREATE INDEX [IX_SavingsAccount_ClosedByUserId] ON [Deposits].[SavingsAccount] ([ClosedByUserId]);
GO

CREATE INDEX [IX_SavingsAccount_CustomerId] ON [Deposits].[SavingsAccount] ([CustomerId]);
GO

CREATE INDEX [IX_SavingsAccount_DepositProductId] ON [Deposits].[SavingsAccount] ([DepositProductId]);
GO

CREATE INDEX [IX_SavingsAccount_LedgerDepositAccountId] ON [Deposits].[SavingsAccount] ([LedgerDepositAccountId]);
GO

CREATE UNIQUE INDEX [IX_SavingsAccountApplication_ApplicationNo] ON [Deposits].[SavingsAccountApplication] ([ApplicationNo]);
GO

CREATE INDEX [IX_SavingsAccountApplication_ApprovalId] ON [Deposits].[SavingsAccountApplication] ([ApprovalId]);
GO

CREATE INDEX [IX_SavingsAccountApplication_CustomerId] ON [Deposits].[SavingsAccountApplication] ([CustomerId]);
GO

CREATE INDEX [IX_SavingsAccountApplication_DepositProductId] ON [Deposits].[SavingsAccountApplication] ([DepositProductId]);
GO

CREATE INDEX [IX_SavingsAccountDeductionSchedule_SavingsAccountId] ON [Deposits].[SavingsAccountDeductionSchedule] ([SavingsAccountId]);
GO

CREATE INDEX [IX_SavingsCashAddition_ApprovalId] ON [Deposits].[SavingsCashAddition] ([ApprovalId]);
GO

CREATE UNIQUE INDEX [IX_SavingsCashAddition_BatchRefNo] ON [Deposits].[SavingsCashAddition] ([BatchRefNo]) WHERE [BatchRefNo] IS NOT NULL;
GO

CREATE INDEX [IX_SavingsCashAddition_CustomerPaymentDocumentId] ON [Deposits].[SavingsCashAddition] ([CustomerPaymentDocumentId]);
GO

CREATE INDEX [IX_SavingsCashAddition_SavingsAccountId] ON [Deposits].[SavingsCashAddition] ([SavingsAccountId]);
GO

CREATE INDEX [IX_SavingsCashAddition_TransactionJournalId] ON [Deposits].[SavingsCashAddition] ([TransactionJournalId]);
GO

CREATE INDEX [IX_SavingsIncreaseDecrease_ApprovalId] ON [Deposits].[SavingsIncreaseDecrease] ([ApprovalId]);
GO

CREATE INDEX [IX_SavingsIncreaseDecrease_SavingsAccountId] ON [Deposits].[SavingsIncreaseDecrease] ([SavingsAccountId]);
GO

CREATE UNIQUE INDEX [IX_SpecialDepositAccount_AccountNo] ON [Deposits].[SpecialDepositAccount] ([AccountNo]) WHERE [AccountNo] IS NOT NULL;
GO

CREATE INDEX [IX_SpecialDepositAccount_ApplicationId] ON [Deposits].[SpecialDepositAccount] ([ApplicationId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_ChargesAccruedAccountId] ON [Deposits].[SpecialDepositAccount] ([ChargesAccruedAccountId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_ChargesIncomeAccountId] ON [Deposits].[SpecialDepositAccount] ([ChargesIncomeAccountId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_ChargesWaivedAccountId] ON [Deposits].[SpecialDepositAccount] ([ChargesWaivedAccountId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_ClosedByUserId] ON [Deposits].[SpecialDepositAccount] ([ClosedByUserId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_CustomerId] ON [Deposits].[SpecialDepositAccount] ([CustomerId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_DepositAccountId] ON [Deposits].[SpecialDepositAccount] ([DepositAccountId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_DepositProductId] ON [Deposits].[SpecialDepositAccount] ([DepositProductId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_InterestEarnedAccountId] ON [Deposits].[SpecialDepositAccount] ([InterestEarnedAccountId]);
GO

CREATE INDEX [IX_SpecialDepositAccount_InterestPayoutAccountId] ON [Deposits].[SpecialDepositAccount] ([InterestPayoutAccountId]);
GO

CREATE UNIQUE INDEX [IX_SpecialDepositAccountApplication_ApplicationNo] ON [Deposits].[SpecialDepositAccountApplication] ([ApplicationNo]) WHERE [ApplicationNo] IS NOT NULL;
GO

CREATE INDEX [IX_SpecialDepositAccountApplication_ApprovalId] ON [Deposits].[SpecialDepositAccountApplication] ([ApprovalId]);
GO

CREATE INDEX [IX_SpecialDepositAccountApplication_CustomerId] ON [Deposits].[SpecialDepositAccountApplication] ([CustomerId]);
GO

CREATE INDEX [IX_SpecialDepositAccountApplication_DepositProductId] ON [Deposits].[SpecialDepositAccountApplication] ([DepositProductId]);
GO

CREATE INDEX [IX_SpecialDepositAccountApplication_PaymentDocumentId] ON [Deposits].[SpecialDepositAccountApplication] ([PaymentDocumentId]);
GO

CREATE INDEX [IX_SpecialDepositAccountDeductionSchedule_SpecialDepositAccountId] ON [Deposits].[SpecialDepositAccountDeductionSchedule] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_SpecialDepositCashAddition_ApprovalId] ON [Deposits].[SpecialDepositCashAddition] ([ApprovalId]);
GO

CREATE UNIQUE INDEX [IX_SpecialDepositCashAddition_BatchRefNo] ON [Deposits].[SpecialDepositCashAddition] ([BatchRefNo]) WHERE [BatchRefNo] IS NOT NULL;
GO

CREATE INDEX [IX_SpecialDepositCashAddition_CustomerPaymentDocumentId] ON [Deposits].[SpecialDepositCashAddition] ([CustomerPaymentDocumentId]);
GO

CREATE INDEX [IX_SpecialDepositCashAddition_SpecialDepositAccountId] ON [Deposits].[SpecialDepositCashAddition] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_SpecialDepositCashAddition_TransactionJournalId] ON [Deposits].[SpecialDepositCashAddition] ([TransactionJournalId]);
GO

CREATE INDEX [IX_SpecialDepositFundTransfer_ApprovalId] ON [Deposits].[SpecialDepositFundTransfer] ([ApprovalId]);
GO

CREATE INDEX [IX_SpecialDepositFundTransfer_FixedDepositDestinationAccountId] ON [Deposits].[SpecialDepositFundTransfer] ([FixedDepositDestinationAccountId]);
GO

CREATE INDEX [IX_SpecialDepositFundTransfer_SavingsDestinationAccountId] ON [Deposits].[SpecialDepositFundTransfer] ([SavingsDestinationAccountId]);
GO

CREATE INDEX [IX_SpecialDepositFundTransfer_SpecialDepositAccountId] ON [Deposits].[SpecialDepositFundTransfer] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_SpecialDepositFundTransfer_TransactionJournalId] ON [Deposits].[SpecialDepositFundTransfer] ([TransactionJournalId]);
GO

CREATE INDEX [IX_SpecialDepositInterestAddition_InterestScheduleItemId] ON [Deposits].[SpecialDepositInterestAddition] ([InterestScheduleItemId]);
GO

CREATE INDEX [IX_SpecialDepositInterestAddition_SpecialDepositAccountId] ON [Deposits].[SpecialDepositInterestAddition] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_SpecialDepositInterestAddition_TransactionJournalId] ON [Deposits].[SpecialDepositInterestAddition] ([TransactionJournalId]);
GO

CREATE INDEX [IX_SpecialDepositInterestSchedule_CronJobConfigId] ON [Deposits].[SpecialDepositInterestSchedule] ([CronJobConfigId]);
GO

CREATE INDEX [IX_SpecialDepositInterestScheduleItem_SpecialDepositAccountId] ON [Deposits].[SpecialDepositInterestScheduleItem] ([SpecialDepositAccountId]);
GO

CREATE INDEX [IX_SpecialDepositInterestScheduleItem_SpecialDepositInterestScheduleId] ON [Deposits].[SpecialDepositInterestScheduleItem] ([SpecialDepositInterestScheduleId]);
GO

CREATE INDEX [IX_SpecialDepositInterestScheduleItem_SpecialDepositInterestScheduleId1] ON [Deposits].[SpecialDepositInterestScheduleItem] ([SpecialDepositInterestScheduleId1]);
GO

CREATE INDEX [IX_SpecialDepositWithdrawal_ApprovalId] ON [Deposits].[SpecialDepositWithdrawal] ([ApprovalId]);
GO

CREATE INDEX [IX_SpecialDepositWithdrawal_CustomerDestinationBankAccountId] ON [Deposits].[SpecialDepositWithdrawal] ([CustomerDestinationBankAccountId]);
GO

CREATE INDEX [IX_SpecialDepositWithdrawal_SpecialDepositSourceAccountId] ON [Deposits].[SpecialDepositWithdrawal] ([SpecialDepositSourceAccountId]);
GO

CREATE INDEX [IX_SpecialDepositWithdrawal_TransactionJournalId] ON [Deposits].[SpecialDepositWithdrawal] ([TransactionJournalId]);
GO

CREATE UNIQUE INDEX [IX_TransactionDocument_DocumentNo] ON [Accounting].[TransactionDocument] ([DocumentNo]);
GO

CREATE INDEX [IX_TransactionDocument_DocumentTypeId] ON [Accounting].[TransactionDocument] ([DocumentTypeId]);
GO

CREATE INDEX [IX_TransactionDocument_TransactionJournalId] ON [Accounting].[TransactionDocument] ([TransactionJournalId]);
GO

CREATE INDEX [IX_TransactionDocument_TransactionJournalId1] ON [Accounting].[TransactionDocument] ([TransactionJournalId1]);
GO

CREATE INDEX [IX_TransactionJournal_ApprovalId] ON [Accounting].[TransactionJournal] ([ApprovalId]);
GO

CREATE INDEX [IX_TransactionJournal_PostedByUserId] ON [Accounting].[TransactionJournal] ([PostedByUserId]);
GO

CREATE INDEX [IX_TransactionJournal_ReversedByUserId] ON [Accounting].[TransactionJournal] ([ReversedByUserId]);
GO

CREATE UNIQUE INDEX [IX_TransactionJournal_TransactionNo] ON [Accounting].[TransactionJournal] ([TransactionNo]);
GO

CREATE UNIQUE INDEX [IX_TransactionJournal_TransactionNo_Title] ON [Accounting].[TransactionJournal] ([TransactionNo], [Title]);
GO

ALTER TABLE [Loans].[LoanAccount] ADD CONSTRAINT [FK_LoanAccount_LoanTopup_LoanTopupId1] FOREIGN KEY ([LoanTopupId1]) REFERENCES [Loans].[LoanTopup] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230710041216_migrate_task_3856', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customer].[CustomerBankAccount] DROP CONSTRAINT [FK_CustomerBankAccount_Bank_BankId];
GO

ALTER TABLE [Customer].[CustomerBankAccount] DROP CONSTRAINT [FK_CustomerBankAccount_Customer_CustomerId];
GO

ALTER TABLE [Customer].[CustomerBankAccount] DROP CONSTRAINT [FK_CustomerBankAccount_LedgerAccount_LedgerAccountId];
GO

DROP INDEX [IX_CustomerBankAccount_LedgerAccountId] ON [Customer].[CustomerBankAccount];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[CustomerBankAccount]') AND [c].[name] = N'LedgerAccountId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[CustomerBankAccount] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [Customer].[CustomerBankAccount] SET [LedgerAccountId] = N'' WHERE [LedgerAccountId] IS NULL;
ALTER TABLE [Customer].[CustomerBankAccount] ALTER COLUMN [LedgerAccountId] nvarchar(40) NOT NULL;
ALTER TABLE [Customer].[CustomerBankAccount] ADD DEFAULT N'' FOR [LedgerAccountId];
CREATE INDEX [IX_CustomerBankAccount_LedgerAccountId] ON [Customer].[CustomerBankAccount] ([LedgerAccountId]);
GO

DROP INDEX [IX_CustomerBankAccount_CustomerId] ON [Customer].[CustomerBankAccount];
DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[CustomerBankAccount]') AND [c].[name] = N'CustomerId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[CustomerBankAccount] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [Customer].[CustomerBankAccount] SET [CustomerId] = N'' WHERE [CustomerId] IS NULL;
ALTER TABLE [Customer].[CustomerBankAccount] ALTER COLUMN [CustomerId] nvarchar(40) NOT NULL;
ALTER TABLE [Customer].[CustomerBankAccount] ADD DEFAULT N'' FOR [CustomerId];
CREATE INDEX [IX_CustomerBankAccount_CustomerId] ON [Customer].[CustomerBankAccount] ([CustomerId]);
GO

DROP INDEX [IX_CustomerBankAccount_BankId] ON [Customer].[CustomerBankAccount];
DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[CustomerBankAccount]') AND [c].[name] = N'BankId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[CustomerBankAccount] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [Customer].[CustomerBankAccount] SET [BankId] = N'' WHERE [BankId] IS NULL;
ALTER TABLE [Customer].[CustomerBankAccount] ALTER COLUMN [BankId] nvarchar(40) NOT NULL;
ALTER TABLE [Customer].[CustomerBankAccount] ADD DEFAULT N'' FOR [BankId];
CREATE INDEX [IX_CustomerBankAccount_BankId] ON [Customer].[CustomerBankAccount] ([BankId]);
GO

ALTER TABLE [Customer].[CustomerBankAccount] ADD CONSTRAINT [FK_CustomerBankAccount_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [MasterData].[Bank] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Customer].[CustomerBankAccount] ADD CONSTRAINT [FK_CustomerBankAccount_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Customer].[CustomerBankAccount] ADD CONSTRAINT [FK_CustomerBankAccount_LedgerAccount_LedgerAccountId] FOREIGN KEY ([LedgerAccountId]) REFERENCES [Accounting].[LedgerAccount] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230710081636_migrate_task_3874', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Accounting].[TransactionDocument] DROP CONSTRAINT [FK_TransactionDocument_TransactionJournal_TransactionJournalId1];
GO

DROP INDEX [IX_TransactionDocument_TransactionJournalId1] ON [Accounting].[TransactionDocument];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Accounting].[TransactionDocument]') AND [c].[name] = N'TransactionJournalId1');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Accounting].[TransactionDocument] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Accounting].[TransactionDocument] DROP COLUMN [TransactionJournalId1];
GO

ALTER TABLE [Security].[Approval] ADD [ApprovalViewModelPayload] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230714125803_migrate_task_3936', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230716010715_migrate_task_3965', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Security].[MemberBulkUploadTemp].[MemberBulkUploadSessionId]', N'SessionId', N'COLUMN';
GO

ALTER TABLE [Security].[MemberBulkUploadSession] ADD [SessionId] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Security].[Approval]') AND [c].[name] = N'CurrentSequence');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Security].[Approval] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Security].[Approval] ADD DEFAULT 0 FOR [CurrentSequence];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230717072441_migrate_task_3980', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Security].[MemberProfile]') AND [c].[name] = N'YearsOfService');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Security].[MemberProfile] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Security].[MemberProfile] ADD DEFAULT 0 FOR [YearsOfService];
GO

ALTER TABLE [Security].[MemberProfile] ADD [DateOfEmployment] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230717085401_migrate_task_3988', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Accounting].[TransactionJournal]') AND [c].[name] = N'TransactionType');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Accounting].[TransactionJournal] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Accounting].[TransactionJournal] ADD DEFAULT N'GENERAL_TRANSACTION' FOR [TransactionType];
GO

ALTER TABLE [Deposits].[DepositProduct] ADD [IsDefaultProduct] bit NULL;
GO

CREATE TABLE [Deposits].[SpecialDepositIncreaseDecrease] (
    [Id] nvarchar(40) NOT NULL,
    [SpecialDepositAccountId] nvarchar(40) NULL,
    [Amount] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ContributionChangeRequest] nvarchar(100) NOT NULL,
    [ApprovalId] nvarchar(40) NULL,
    [Description] nvarchar(255) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(128) NULL,
    [DateCreated] datetimeoffset NULL,
    [UpdatedByUserId] nvarchar(128) NULL,
    [DateUpdated] datetimeoffset NULL,
    [DeletedByUserId] nvarchar(128) NULL,
    [IsDeleted] bit NOT NULL,
    [DateDeleted] datetimeoffset NULL,
    [RowVersion] uniqueidentifier NOT NULL,
    [FullText] nvarchar(512) NULL,
    [Tags] nvarchar(max) NULL,
    [Caption] nvarchar(256) NULL,
    CONSTRAINT [PK_SpecialDepositIncreaseDecrease] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialDepositIncreaseDecrease_Approval_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [Security].[Approval] ([Id]),
    CONSTRAINT [FK_SpecialDepositIncreaseDecrease_SpecialDepositAccount_SpecialDepositAccountId] FOREIGN KEY ([SpecialDepositAccountId]) REFERENCES [Deposits].[SpecialDepositAccount] ([Id])
);
GO

CREATE INDEX [IX_SpecialDepositIncreaseDecrease_ApprovalId] ON [Deposits].[SpecialDepositIncreaseDecrease] ([ApprovalId]);
GO

CREATE INDEX [IX_SpecialDepositIncreaseDecrease_SpecialDepositAccountId] ON [Deposits].[SpecialDepositIncreaseDecrease] ([SpecialDepositAccountId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230717230410_migrate_task_4034', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230724103941_migrate_task_4196', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_CompanyBankAccount_BankId] ON [Accounting].[CompanyBankAccount];
GO

CREATE INDEX [IX_CompanyBankAccount_BankId] ON [Accounting].[CompanyBankAccount] ([BankId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230724105953_migrate_task_4201', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230725011419_migrate_task_4230', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Payroll].[PayrollCronJobConfig] ADD [TotalAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Payroll].[PayrollCronJobConfig] ADD [TotalCount] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

ALTER TABLE [Deposits].[DepositProductCharge] ADD [ChargeType] nvarchar(128) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230725113915_migrate_task_4259', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customer].[Customer] ADD [DateOfEmployment] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230726230014_migrate_task_4288', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Loans].[LoanOffset] ADD [RepaymentSchedules] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230727182950_migrate_task_4300', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Deposits].[FixedDepositLiquidation] ADD [IsMatured] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Deposits].[FixedDepositLiquidation] ADD [LiquidationDate] datetime2 NULL;
GO

ALTER TABLE [Deposits].[FixedDepositLiquidation] ADD [MaturityDate] datetime2 NULL;
GO

ALTER TABLE [Deposits].[FixedDepositAccount] ADD [ParentAccountId] nvarchar(40) NULL;
GO

ALTER TABLE [Deposits].[FixedDepositAccount] ADD [RootParentAccountId] nvarchar(40) NULL;
GO

CREATE INDEX [IX_FixedDepositAccount_ParentAccountId] ON [Deposits].[FixedDepositAccount] ([ParentAccountId]);
GO

CREATE INDEX [IX_FixedDepositAccount_RootParentAccountId] ON [Deposits].[FixedDepositAccount] ([RootParentAccountId]);
GO

ALTER TABLE [Deposits].[FixedDepositAccount] ADD CONSTRAINT [FK_FixedDepositAccount_FixedDepositAccount_ParentAccountId] FOREIGN KEY ([ParentAccountId]) REFERENCES [Deposits].[FixedDepositAccount] ([Id]);
GO

ALTER TABLE [Deposits].[FixedDepositAccount] ADD CONSTRAINT [FK_FixedDepositAccount_FixedDepositAccount_RootParentAccountId] FOREIGN KEY ([RootParentAccountId]) REFERENCES [Deposits].[FixedDepositAccount] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230729153225_migrate_task_4338', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230730045507_migrate_task_4413', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Loans].[LoanAccount] DROP CONSTRAINT [FK_LoanAccount_LedgerAccount_InterestEarnedAccountId];
GO

ALTER TABLE [Loans].[LoanDisbursement] DROP CONSTRAINT [FK_LoanDisbursement_LoanAccount_LoanAccountId1];
GO

DROP INDEX [IX_LoanDisbursement_LoanAccountId1] ON [Loans].[LoanDisbursement];
GO

DROP INDEX [IX_LoanAccount_InterestEarnedAccountId] ON [Loans].[LoanAccount];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans].[LoanDisbursement]') AND [c].[name] = N'LoanAccountId1');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Loans].[LoanDisbursement] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Loans].[LoanDisbursement] DROP COLUMN [LoanAccountId1];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans].[LoanAccount]') AND [c].[name] = N'InterestEarnedAccountId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Loans].[LoanAccount] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Loans].[LoanAccount] DROP COLUMN [InterestEarnedAccountId];
GO

ALTER TABLE [Loans].[LoanDisbursement] ADD [TransactionType] nvarchar(100) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230730123618_migrate_task_4465', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Accounting].[AccountingPeriod] DROP CONSTRAINT [FK_AccountingPeriod_FinancialCalendar_FinancialCalendarId];
GO

ALTER TABLE [Loans].[LoanOffSetCharge] DROP CONSTRAINT [FK_LoanOffSetCharge_LoanOffset_LoanOffsetId1];
GO

DROP INDEX [IX_LoanOffSetCharge_LoanOffsetId1] ON [Loans].[LoanOffSetCharge];
GO

DROP INDEX [IX_AccountingPeriod_FinancialCalendarId] ON [Accounting].[AccountingPeriod];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans].[LoanOffSetCharge]') AND [c].[name] = N'LoanOffsetId1');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Loans].[LoanOffSetCharge] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Loans].[LoanOffSetCharge] DROP COLUMN [LoanOffsetId1];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Accounting].[AccountingPeriod]') AND [c].[name] = N'FinancialCalendarId');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Accounting].[AccountingPeriod] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Accounting].[AccountingPeriod] DROP COLUMN [FinancialCalendarId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230731000801_migrate_task_4494', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Deposits].[FixedDepositInterestScheduleItem] DROP CONSTRAINT [FK_FixedDepositInterestScheduleItem_FixedDepositInterestSchedule_FixedDepositInterestScheduleId1];
GO

ALTER TABLE [Loans].[LoanTopupCharge] DROP CONSTRAINT [FK_LoanTopupCharge_LoanTopup_LoanTopupId1];
GO

ALTER TABLE [Deposits].[SpecialDepositInterestScheduleItem] DROP CONSTRAINT [FK_SpecialDepositInterestScheduleItem_SpecialDepositInterestSchedule_SpecialDepositInterestScheduleId1];
GO

DROP INDEX [IX_SpecialDepositInterestScheduleItem_SpecialDepositInterestScheduleId1] ON [Deposits].[SpecialDepositInterestScheduleItem];
GO

DROP INDEX [IX_LoanTopupCharge_LoanTopupId1] ON [Loans].[LoanTopupCharge];
GO

DROP INDEX [IX_FixedDepositInterestScheduleItem_FixedDepositInterestScheduleId1] ON [Deposits].[FixedDepositInterestScheduleItem];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Deposits].[SpecialDepositInterestScheduleItem]') AND [c].[name] = N'SpecialDepositInterestScheduleId1');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Deposits].[SpecialDepositInterestScheduleItem] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Deposits].[SpecialDepositInterestScheduleItem] DROP COLUMN [SpecialDepositInterestScheduleId1];
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans].[LoanTopupCharge]') AND [c].[name] = N'LoanTopupId1');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Loans].[LoanTopupCharge] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Loans].[LoanTopupCharge] DROP COLUMN [LoanTopupId1];
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Deposits].[FixedDepositInterestScheduleItem]') AND [c].[name] = N'FixedDepositInterestScheduleId1');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Deposits].[FixedDepositInterestScheduleItem] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Deposits].[FixedDepositInterestScheduleItem] DROP COLUMN [FixedDepositInterestScheduleId1];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230731022801_migrate_task_4503', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Security].[MemberBulkUploadTemp] ADD [MemberType] nvarchar(100) NOT NULL DEFAULT N'REGULAR';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230801090428_migrate_task_4534', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Loans].[LoanAccount] DROP CONSTRAINT [FK_LoanAccount_LoanTopup_LoanTopupId1];
GO

DROP INDEX [IX_LoanAccount_LoanTopupId1] ON [Loans].[LoanAccount];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans].[LoanAccount]') AND [c].[name] = N'LoanTopupId1');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Loans].[LoanAccount] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Loans].[LoanAccount] DROP COLUMN [LoanTopupId1];
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans].[LoanAccount]') AND [c].[name] = N'LoanTopupId');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Loans].[LoanAccount] DROP CONSTRAINT [' + @var15 + '];');
UPDATE [Loans].[LoanAccount] SET [LoanTopupId] = N'' WHERE [LoanTopupId] IS NULL;
ALTER TABLE [Loans].[LoanAccount] ALTER COLUMN [LoanTopupId] nvarchar(40) NOT NULL;
ALTER TABLE [Loans].[LoanAccount] ADD DEFAULT N'' FOR [LoanTopupId];
GO

CREATE INDEX [IX_LoanAccount_LoanTopupId] ON [Loans].[LoanAccount] ([LoanTopupId]);
GO

ALTER TABLE [Loans].[LoanAccount] ADD CONSTRAINT [FK_LoanAccount_LoanTopup_LoanTopupId] FOREIGN KEY ([LoanTopupId]) REFERENCES [Loans].[LoanTopup] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230801103640_migrate_task_4547', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230801111245_migrate_task_4557', N'7.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Security].[MemberBulkUploadTemp]') AND [c].[name] = N'MemberType');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Security].[MemberBulkUploadTemp] DROP CONSTRAINT [' + @var16 + '];');
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230801120146_migrate_task_4568', N'7.0.8');
GO

COMMIT;
GO

