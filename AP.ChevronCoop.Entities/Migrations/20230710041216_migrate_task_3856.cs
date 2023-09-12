using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_3856 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounting");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.EnsureSchema(
                name: "MasterData");

            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.EnsureSchema(
                name: "Deposits");

            migrationBuilder.EnsureSchema(
                name: "Loans");

            migrationBuilder.EnsureSchema(
                name: "Docs");

            migrationBuilder.EnsureSchema(
                name: "HR");

            migrationBuilder.EnsureSchema(
                name: "Payroll");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateSequence(
                name: "Asset",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "COGS",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Deposits",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Equity",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Expense",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "General",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Income",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Liability",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Loans",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Repayments",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "Withdrawals",
                schema: "Core",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "ApplicationRole",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    SecondaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPhoneConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalEmailAlert",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalWorkflowId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailTitle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    TaskCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalEmailAlert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalWorkflow",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    WorkflowName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IsDefaultApprovalWorkflow = table.Column<bool>(type: "bit", nullable: false),
                    RequiredApprovers = table.Column<int>(type: "int", nullable: false),
                    RequiredGroups = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalWorkflow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    SortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ContactDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    IsoSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DecimalPlaces = table.Column<int>(type: "int", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SystemFlag = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialCalendar",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    ClosedByUserName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialCalendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalCode",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CodeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SystemFlag = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LienType",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LienType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LocationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SystemFlag = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Location_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "MasterData",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberBulkUploadSession",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberBulkUploadSession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberBulkUploadTemp",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RecordId = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    UploadedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberBulkUploadSessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccessfullyRegistered = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberBulkUploadTemp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMode",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "CASH"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Module = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserLogin",
                schema: "Security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ApplicationUserLogin_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "ApplicationRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserToken",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ApplicationUserToken_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ApprovalType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentSequence = table.Column<int>(type: "int", nullable: false),
                    ApprovalWorkflowId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApprovalCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TriedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Approval_ApprovalWorkflow_ApprovalWorkflowId",
                        column: x => x.ApprovalWorkflowId,
                        principalSchema: "Security",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalGroup",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ApprovalWorkflowId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalGroup_ApprovalWorkflow_ApprovalWorkflowId",
                        column: x => x.ApprovalWorkflowId,
                        principalSchema: "Security",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalNotification",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalWorkflowId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Reminder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Escalation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalNotification_ApprovalWorkflow_ApprovalWorkflowId",
                        column: x => x.ApprovalWorkflowId,
                        principalSchema: "Security",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Charge",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "FLAT"),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "VALUE"),
                    CalculationMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "ADD"),
                    CurrencyId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    MaximumCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinimimumCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charge_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "MasterData",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccount",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UOM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ClearedBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    UnclearedBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    LedgerBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    AvailableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsOfficeAccount = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AllowManualEntry = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedByUserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "MasterData",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_LedgerAccount_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberProfile",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YearsOfService = table.Column<int>(type: "int", nullable: false),
                    IsKycStarted = table.Column<bool>(type: "bit", nullable: false),
                    IsKycCompleted = table.Column<bool>(type: "bit", nullable: false),
                    KycStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KycCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MemberType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    KycSubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KycApproved = table.Column<bool>(type: "bit", nullable: false),
                    KycApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KycApprovedBy = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MembershipId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CAI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetireeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwitchToRetireeRequested = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    JobRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberProfile_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberProfile_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "MasterData",
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfficeDocument",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DocumentTypeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DocumentData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficeDocument_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "Docs",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficePhoto",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DocumentTypeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DocumentData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficePhoto_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "Docs",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficeSheet",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DocumentTypeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DocumentData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficeSheet_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "Docs",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountingPeriod",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CalendarId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ClosedByUserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinancialCalendarId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingPeriod_FinancialCalendar_CalendarId",
                        column: x => x.CalendarId,
                        principalSchema: "Accounting",
                        principalTable: "FinancialCalendar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingPeriod_FinancialCalendar_FinancialCalendarId",
                        column: x => x.FinancialCalendarId,
                        principalSchema: "Accounting",
                        principalTable: "FinancialCalendar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRole",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EventGlobalCodeId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRole_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "ApplicationRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalRole_GlobalCode_EventGlobalCodeId",
                        column: x => x.EventGlobalCodeId,
                        principalSchema: "MasterData",
                        principalTable: "GlobalCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventGlobalCodeId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrail_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditTrail_GlobalCode_EventGlobalCodeId",
                        column: x => x.EventGlobalCodeId,
                        principalSchema: "MasterData",
                        principalTable: "GlobalCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRoleClaim",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationRoleClaim_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "ApplicationRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationRoleClaim_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Security",
                        principalTable: "Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserClaim",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserClaim_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserClaim_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Security",
                        principalTable: "Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalDocument",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalDocument_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionJournal",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TransactionNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "GENERAL"),
                    DocumentRef = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DocumentRefId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PostingRef = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PostingRefId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EntityRef = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EntityRefId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    PostedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReversed = table.Column<bool>(type: "bit", nullable: false),
                    ReversedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReversalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionJournal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionJournal_ApplicationUser_PostedByUserId",
                        column: x => x.PostedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionJournal_ApplicationUser_ReversedByUserId",
                        column: x => x.ReversedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionJournal_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalGroupMember",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalGroupId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    ApprovalSequence = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalGroupId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalGroupMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalGroupMember_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalGroupMember_ApprovalGroup_ApprovalGroupId",
                        column: x => x.ApprovalGroupId,
                        principalSchema: "Security",
                        principalTable: "ApprovalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalGroupMember_ApprovalGroup_ApprovalGroupId1",
                        column: x => x.ApprovalGroupId1,
                        principalSchema: "Security",
                        principalTable: "ApprovalGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalGroupWorkflow",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalWorkflowId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    ApprovalGroupId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    RequiredApprovers = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalGroupWorkflow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalGroupWorkflow_ApprovalGroup_ApprovalGroupId",
                        column: x => x.ApprovalGroupId,
                        principalSchema: "Security",
                        principalTable: "ApprovalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalGroupWorkflow_ApprovalWorkflow_ApprovalWorkflowId",
                        column: x => x.ApprovalWorkflowId,
                        principalSchema: "Security",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalLog",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApprovalGroupId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateApproved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalLog_ApplicationUser_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalLog_ApprovalGroup_ApprovalGroupId",
                        column: x => x.ApprovalGroupId,
                        principalSchema: "Security",
                        principalTable: "ApprovalGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalLog_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyBankAccount",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LedgerAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BranchAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CurrencyId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BVN = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBankAccount_Bank_BankId",
                        column: x => x.BankId,
                        principalSchema: "MasterData",
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBankAccount_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "MasterData",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBankAccount_LedgerAccount_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CashAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    YearsOfService = table.Column<int>(type: "int", nullable: false),
                    IsKycStarted = table.Column<bool>(type: "bit", nullable: false),
                    IsKycCompleted = table.Column<bool>(type: "bit", nullable: false),
                    KycStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KycCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MemberType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    KycSubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KycApproved = table.Column<bool>(type: "bit", nullable: false),
                    KycApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KycApprovedBy = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CAI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetireeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "MasterData",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_LedgerAccount_CashAccountId",
                        column: x => x.CashAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentPaymentInfo",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberProfileId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Evidence = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentPaymentInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentPaymentInfo_MemberProfile_MemberProfileId",
                        column: x => x.MemberProfileId,
                        principalSchema: "Security",
                        principalTable: "MemberProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberBankAccount",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    BankId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BVN = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberBankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberBankAccount_Bank_BankId",
                        column: x => x.BankId,
                        principalSchema: "MasterData",
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemberBankAccount_MemberProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "Security",
                        principalTable: "MemberProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberBeneficiary",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberBeneficiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberBeneficiary_MemberProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "Security",
                        principalTable: "MemberProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberNextOfKin",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberNextOfKin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberNextOfKin_MemberProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "Security",
                        principalTable: "MemberProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JournalEntry",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TransactionEntryNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EntryType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DecimalPlaces = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    PostedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReversed = table.Column<bool>(type: "bit", nullable: false),
                    ReversedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReversalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntry_LedgerAccount_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntry_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDocument",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DocumentTypeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TransactionJournalId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDocument_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "Docs",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionDocument_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionDocument_TransactionJournal_TransactionJournalId1",
                        column: x => x.TransactionJournalId1,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepositProduct",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ApprovalWorkflowId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    MinimumAge = table.Column<int>(type: "int", nullable: false),
                    MaximumAge = table.Column<int>(type: "int", nullable: false),
                    Tenure = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "NONE"),
                    TenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "PENDING_APPROVAL"),
                    PublicationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "ALL"),
                    PublishedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DefaultCurrencyId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BankDepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ProductDepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesIncomeAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesAccrualAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestPayableAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestPayoutAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IsInterestEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MinimumContributionRegular = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinimumContributionRetiree = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositProduct_ApplicationUser_PublishedByUserId",
                        column: x => x.PublishedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepositProduct_ApprovalWorkflow_ApprovalWorkflowId",
                        column: x => x.ApprovalWorkflowId,
                        principalSchema: "Security",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepositProduct_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepositProduct_CompanyBankAccount_BankDepositAccountId",
                        column: x => x.BankDepositAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProduct_Currency_DefaultCurrencyId",
                        column: x => x.DefaultCurrencyId,
                        principalSchema: "MasterData",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProduct_LedgerAccount_ChargesAccrualAccountId",
                        column: x => x.ChargesAccrualAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProduct_LedgerAccount_ChargesIncomeAccountId",
                        column: x => x.ChargesIncomeAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProduct_LedgerAccount_ChargesWaivedAccountId",
                        column: x => x.ChargesWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProduct_LedgerAccount_InterestPayableAccountId",
                        column: x => x.InterestPayableAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProduct_LedgerAccount_InterestPayoutAccountId",
                        column: x => x.InterestPayoutAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProduct_LedgerAccount_ProductDepositAccountId",
                        column: x => x.ProductDepositAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanProduct",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PayrollCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ApprovalWorkflowId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PrincipalMultiple = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PrincipalMinLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PrincipalMaxLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TenureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinTenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxTenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepaymentPeriod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "MONTHLY"),
                    InterestMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InterestCalculationMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "FLAT_RATE"),
                    DaysInYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 365m),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    HasAdminCharges = table.Column<bool>(type: "bit", nullable: false),
                    IsTargetLoan = table.Column<bool>(type: "bit", nullable: false),
                    BenefitCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AllowedOffsetType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OffsetPeriodUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OffsetPeriodValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnableSavingsOffset = table.Column<bool>(type: "bit", nullable: false),
                    EnableChargeWaiver = table.Column<bool>(type: "bit", nullable: false),
                    EnableTopUp = table.Column<bool>(type: "bit", nullable: false),
                    EnableTopUpCharges = table.Column<bool>(type: "bit", nullable: false),
                    EnableAdminOffsetCharge = table.Column<bool>(type: "bit", nullable: false),
                    EnableWaitingPeriod = table.Column<bool>(type: "bit", nullable: false),
                    WaitingPeriodUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WaitingPeriodValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnableWaitingPeriodCharge = table.Column<bool>(type: "bit", nullable: false),
                    IsGuarantorRequired = table.Column<bool>(type: "bit", nullable: false),
                    GuarantorMinYear = table.Column<int>(type: "int", nullable: false),
                    GuarantorAmountLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    EmployeeGuarantorCount = table.Column<int>(type: "int", nullable: false),
                    NonEmployeeGuarantorCount = table.Column<int>(type: "int", nullable: false),
                    QualificationTargetProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QualificationMinBalancePercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    SavingsOffSetProductIdList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberTypeIdList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumAge = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    MaximumAge = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    DefaultCurrencyId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    LoanProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankDepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DisbursementAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PrincipalAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PrincipalLossAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestIncomeAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    UnearnedInterestAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestLossAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PenalInterestReceivableAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesIncomeAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesAccrualAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PublicationType = table.Column<int>(type: "int", nullable: false),
                    PublishedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanProduct_ApplicationUser_PublishedByUserId",
                        column: x => x.PublishedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanProduct_ApprovalWorkflow_ApprovalWorkflowId",
                        column: x => x.ApprovalWorkflowId,
                        principalSchema: "Security",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanProduct_CompanyBankAccount_BankDepositAccountId",
                        column: x => x.BankDepositAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanProduct_CompanyBankAccount_DisbursementAccountId",
                        column: x => x.DisbursementAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanProduct_Currency_DefaultCurrencyId",
                        column: x => x.DefaultCurrencyId,
                        principalSchema: "MasterData",
                        principalTable: "Currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_ChargesAccrualAccountId",
                        column: x => x.ChargesAccrualAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_ChargesIncomeAccountId",
                        column: x => x.ChargesIncomeAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_ChargesWaivedAccountId",
                        column: x => x.ChargesWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_InterestIncomeAccountId",
                        column: x => x.InterestIncomeAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_InterestLossAccountId",
                        column: x => x.InterestLossAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_InterestWaivedAccountId",
                        column: x => x.InterestWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_PenalInterestReceivableAccountId",
                        column: x => x.PenalInterestReceivableAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_PrincipalAccountId",
                        column: x => x.PrincipalAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_PrincipalLossAccountId",
                        column: x => x.PrincipalLossAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProduct_LedgerAccount_UnearnedInterestAccountId",
                        column: x => x.UnearnedInterestAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollDeductionSchedule",
                schema: "Payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ScheduleName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ScheduleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    SpecialDepositBankAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    FixedDepositBankAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    DeductionsCount = table.Column<int>(type: "int", nullable: false),
                    TotalDeductions = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MinDecimalPlace = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MaxDecimalPlace = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AdviseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    PayrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUploaded = table.Column<bool>(type: "bit", nullable: false),
                    LastUploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenerateDeductionCronJobStatus = table.Column<int>(type: "int", nullable: false),
                    GenerateDeductionCronJobStartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GenerateDeductionCronJobCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessDeductionCronJobStatus = table.Column<int>(type: "int", nullable: false),
                    ProcessDeductionCronJobStartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessDeductionCronJobCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollDeductionSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollDeductionSchedule_CompanyBankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollDeductionSchedule_CompanyBankAccount_FixedDepositBankAccountId",
                        column: x => x.FixedDepositBankAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollDeductionSchedule_CompanyBankAccount_SpecialDepositBankAccountId",
                        column: x => x.SpecialDepositBankAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerBankAccount",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LedgerAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    BankId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BVN = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBankAccount_Bank_BankId",
                        column: x => x.BankId,
                        principalSchema: "MasterData",
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerBankAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerBankAccount_LedgerAccount_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerBeneficiary",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBeneficiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBeneficiary_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerNextOfKin",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNextOfKin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerNextOfKin_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPaymentDocument",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    DocumentData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPaymentDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPaymentDocument_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmployeeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmploymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "MasterData",
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerDepositProductPublication",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PublicationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDepositProductPublication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDepositProductPublication_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerDepositProductPublication_DepositProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentDepositProductPublication",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PublicationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDepositProductPublication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentDepositProductPublication_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "MasterData",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentDepositProductPublication_DepositProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositProductCharge",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositProductCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositProductCharge_Charge_ChargeId",
                        column: x => x.ChargeId,
                        principalSchema: "MasterData",
                        principalTable: "Charge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositProductCharge_DepositProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositProductInterestRange",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LowerLimit = table.Column<decimal>(type: "decimal(30,6)", precision: 30, scale: 6, nullable: false),
                    UpperLimit = table.Column<decimal>(type: "decimal(30,6)", precision: 30, scale: 6, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(30,6)", precision: 30, scale: 6, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositProductInterestRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositProductInterestRange_DepositProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SavingsAccountApplication",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    DepositProductId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsAccountApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingsAccountApplication_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsAccountApplication_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavingsAccountApplication_DepositProduct_DepositProductId",
                        column: x => x.DepositProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLoanProductPublication",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PublicationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLoanProductPublication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLoanProductPublication_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLoanProductPublication_LoanProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Loans",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentLoanProductPublication",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PublicationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentLoanProductPublication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentLoanProductPublication_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "MasterData",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentLoanProductPublication_LoanProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Loans",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanProductCharge",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProductCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanProductCharge_Charge_ChargeId",
                        column: x => x.ChargeId,
                        principalSchema: "MasterData",
                        principalTable: "Charge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanProductCharge_LoanProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Loans",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollCronJobConfig",
                schema: "Payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeductionScheduleId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CronJobType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ComputationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComputationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordsProcessed = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollCronJobConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollCronJobConfig_PayrollDeductionSchedule_DeductionScheduleId",
                        column: x => x.DeductionScheduleId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollDeductionSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayrollDeductionDocument",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PayrollDeductionScheduleId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ProcessSequence = table.Column<int>(type: "int", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentData = table.Column<string>(type: "nvarchar", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollDeductionDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollDeductionDocument_PayrollDeductionSchedule_PayrollDeductionScheduleId",
                        column: x => x.PayrollDeductionScheduleId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollDeductionSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayrollDeductionItem",
                schema: "Payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PayrollDeductionScheduleId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BatchRefNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmployeeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PayrollCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PayrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeductionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalDeduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollDeductionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollDeductionItem_PayrollDeductionSchedule_PayrollDeductionScheduleId",
                        column: x => x.PayrollDeductionScheduleId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollDeductionSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositAccountApplication",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    DepositProductId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaymentDocumentId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    PaymentAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentBankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositAccountApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccountApplication_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccountApplication_CustomerPaymentDocument_PaymentDocumentId",
                        column: x => x.PaymentDocumentId,
                        principalSchema: "Docs",
                        principalTable: "CustomerPaymentDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccountApplication_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccountApplication_DepositProduct_DepositProductId",
                        column: x => x.DepositProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SavingsAccount",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    DepositProductId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    LedgerDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ChargesPayableAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ChargesAccruedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesIncomeAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PayrollAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaximumBalanceLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumBalanceLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SingleWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeeklyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingsAccount_ApplicationUser_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsAccount_DepositProduct_DepositProductId",
                        column: x => x.DepositProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsAccount_LedgerAccount_ChargesAccruedAccountId",
                        column: x => x.ChargesAccruedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavingsAccount_LedgerAccount_ChargesIncomeAccountId",
                        column: x => x.ChargesIncomeAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavingsAccount_LedgerAccount_ChargesPayableAccountId",
                        column: x => x.ChargesPayableAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsAccount_LedgerAccount_ChargesWaivedAccountId",
                        column: x => x.ChargesWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavingsAccount_LedgerAccount_LedgerDepositAccountId",
                        column: x => x.LedgerDepositAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsAccount_SavingsAccountApplication_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccountApplication",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositInterestSchedule",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CronJobConfigId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositInterestSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositInterestSchedule_PayrollCronJobConfig_CronJobConfigId",
                        column: x => x.CronJobConfigId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollCronJobConfig",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositInterestSchedule",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CronJobConfigId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositInterestSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositInterestSchedule_PayrollCronJobConfig_CronJobConfigId",
                        column: x => x.CronJobConfigId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollCronJobConfig",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositAccount",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    DepositProductId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    DepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ChargesAccruedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesIncomeAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestEarnedAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    InterestPayoutAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    FundingAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    LastInterestComputationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaximumBalanceLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MinimumBalanceLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    SingleWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    DailyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    WeeklyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MonthlyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_ApplicationUser_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_DepositProduct_DepositProductId",
                        column: x => x.DepositProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_LedgerAccount_ChargesAccruedAccountId",
                        column: x => x.ChargesAccruedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_LedgerAccount_ChargesIncomeAccountId",
                        column: x => x.ChargesIncomeAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_LedgerAccount_ChargesWaivedAccountId",
                        column: x => x.ChargesWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_LedgerAccount_DepositAccountId",
                        column: x => x.DepositAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_LedgerAccount_InterestEarnedAccountId",
                        column: x => x.InterestEarnedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_LedgerAccount_InterestPayoutAccountId",
                        column: x => x.InterestPayoutAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccount_SpecialDepositAccountApplication_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccountApplication",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SavingsAccountDeductionSchedule",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SavingsAccountId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    BatchRefNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MemberId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    EmployeeNo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    PayrollCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsAccountDeductionSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingsAccountDeductionSchedule_SavingsAccount_SavingsAccountId",
                        column: x => x.SavingsAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SavingsCashAddition",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SavingsAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerPaymentDocumentId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    BatchRefNo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsCashAddition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingsCashAddition_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsCashAddition_CustomerPaymentDocument_CustomerPaymentDocumentId",
                        column: x => x.CustomerPaymentDocumentId,
                        principalSchema: "Docs",
                        principalTable: "CustomerPaymentDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsCashAddition_SavingsAccount_SavingsAccountId",
                        column: x => x.SavingsAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsCashAddition_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SavingsIncreaseDecrease",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SavingsAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    ContributionChangeRequest = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsIncreaseDecrease", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingsIncreaseDecrease_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavingsIncreaseDecrease_SavingsAccount_SavingsAccountId",
                        column: x => x.SavingsAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositAccountApplication",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    DepositProductId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TenureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "NONE"),
                    TenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MaturityInstructionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LiquidationAccountType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SavingsLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    SpecialDepositLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    CustomerBankLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecialDepositFundingSourceAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    CustomerBankFundingSourceAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    PaymentDocumentId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositAccountApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_CustomerBankAccount_CustomerBankFundingSourceAccountId",
                        column: x => x.CustomerBankFundingSourceAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_CustomerBankAccount_CustomerBankLiquidationAccountId",
                        column: x => x.CustomerBankLiquidationAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_CustomerPaymentDocument_PaymentDocumentId",
                        column: x => x.PaymentDocumentId,
                        principalSchema: "Docs",
                        principalTable: "CustomerPaymentDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_DepositProduct_DepositProductId",
                        column: x => x.DepositProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_SavingsAccount_SavingsLiquidationAccountId",
                        column: x => x.SavingsLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_SpecialDepositAccount_SpecialDepositFundingSourceAccountId",
                        column: x => x.SpecialDepositFundingSourceAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccountApplication_SpecialDepositAccount_SpecialDepositLiquidationAccountId",
                        column: x => x.SpecialDepositLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanApplication",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LoanProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TenureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepaymentCommencementDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UseSpecialDeposit = table.Column<bool>(type: "bit", nullable: false),
                    SpecialDepositId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CustomerDisbursementAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    QualificationTargetProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QualificationTargetProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplication_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanApplication_CustomerBankAccount_CustomerDisbursementAccountId",
                        column: x => x.CustomerDisbursementAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanApplication_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplication_LoanProduct_LoanProductId",
                        column: x => x.LoanProductId,
                        principalSchema: "Loans",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplication_SpecialDepositAccount_SpecialDepositId",
                        column: x => x.SpecialDepositId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositAccountDeductionSchedule",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    BatchRefNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MemberId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    EmployeeNo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    PayrollCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositAccountDeductionSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositAccountDeductionSchedule_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositCashAddition",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CustomerPaymentDocumentId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BatchRefNo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositCashAddition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositCashAddition_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositCashAddition_CustomerPaymentDocument_CustomerPaymentDocumentId",
                        column: x => x.CustomerPaymentDocumentId,
                        principalSchema: "Docs",
                        principalTable: "CustomerPaymentDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositCashAddition_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositCashAddition_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositInterestScheduleItem",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    SpecialDepositInterestScheduleId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    OldBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PeriodCashAddition = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestEarned = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    NewBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    SpecialDepositInterestScheduleId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositInterestScheduleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositInterestScheduleItem_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositInterestScheduleItem_SpecialDepositInterestSchedule_SpecialDepositInterestScheduleId",
                        column: x => x.SpecialDepositInterestScheduleId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositInterestSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositInterestScheduleItem_SpecialDepositInterestSchedule_SpecialDepositInterestScheduleId1",
                        column: x => x.SpecialDepositInterestScheduleId1,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositInterestSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositWithdrawal",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SpecialDepositSourceAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    WithdrawalDestinationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerDestinationBankAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositWithdrawal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositWithdrawal_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositWithdrawal_CustomerBankAccount_CustomerDestinationBankAccountId",
                        column: x => x.CustomerDestinationBankAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositWithdrawal_SpecialDepositAccount_SpecialDepositSourceAccountId",
                        column: x => x.SpecialDepositSourceAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositWithdrawal_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositAccount",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DepositProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesAccruedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesIncomeAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestEarnedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestPayoutAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TenureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "NONE"),
                    TenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MaturityInstructionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LiquidationAccountType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SavingsLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    SpecialDepositLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CustomerBankLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    LastInterestComputationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasMature = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaximumBalanceLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MinimumBalanceLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    SingleWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    DailyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    WeeklyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MonthlyWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_ApplicationUser_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_CustomerBankAccount_CustomerBankLiquidationAccountId",
                        column: x => x.CustomerBankLiquidationAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_DepositProduct_DepositProductId",
                        column: x => x.DepositProductId,
                        principalSchema: "Deposits",
                        principalTable: "DepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_FixedDepositAccountApplication_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositAccountApplication",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_LedgerAccount_ChargesAccruedAccountId",
                        column: x => x.ChargesAccruedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_LedgerAccount_ChargesIncomeAccountId",
                        column: x => x.ChargesIncomeAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_LedgerAccount_ChargesWaivedAccountId",
                        column: x => x.ChargesWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_LedgerAccount_DepositAccountId",
                        column: x => x.DepositAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_LedgerAccount_InterestEarnedAccountId",
                        column: x => x.InterestEarnedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_LedgerAccount_InterestPayoutAccountId",
                        column: x => x.InterestPayoutAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_SavingsAccount_SavingsLiquidationAccountId",
                        column: x => x.SavingsLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositAccount_SpecialDepositAccount_SpecialDepositLiquidationAccountId",
                        column: x => x.SpecialDepositLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanApplicationApproval",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoanApplicationId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicationApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplicationApproval_LoanApplication_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalSchema: "Loans",
                        principalTable: "LoanApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplicationGuarantor",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanApplicationId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    GuarantorType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GuarantorId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    GuarantorApprovalType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicationGuarantor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplicationGuarantor_Customer_GuarantorId",
                        column: x => x.GuarantorId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplicationGuarantor_LoanApplication_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalSchema: "Loans",
                        principalTable: "LoanApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplicationItem",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanApplicationId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplicationItem_LoanApplication_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalSchema: "Loans",
                        principalTable: "LoanApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplicationSchedule",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanApplicationId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RepaymentNo = table.Column<int>(type: "int", nullable: false),
                    TenureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaysInPeriod = table.Column<int>(type: "int", nullable: true),
                    PeriodPayment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CumulativeTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PeriodPrincipal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CumulativePrincipal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PrincipalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PeriodInterest = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CumulativeInterest = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicationSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplicationSchedule_LoanApplication_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalSchema: "Loans",
                        principalTable: "LoanApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositInterestAddition",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    InterestScheduleItemId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    InterestEarned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositInterestAddition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositInterestAddition_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositInterestAddition_SpecialDepositInterestScheduleItem_InterestScheduleItemId",
                        column: x => x.InterestScheduleItemId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositInterestScheduleItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositInterestAddition_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositChangeInMaturity",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MaturityInstructionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FixedDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    LiquidationAccountType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SavingsLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    SpecialDepositLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    CustomerBankLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositChangeInMaturity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositChangeInMaturity_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositChangeInMaturity_CustomerBankAccount_CustomerBankLiquidationAccountId",
                        column: x => x.CustomerBankLiquidationAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositChangeInMaturity_FixedDepositAccount_FixedDepositAccountId",
                        column: x => x.FixedDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositChangeInMaturity_SavingsAccount_SavingsLiquidationAccountId",
                        column: x => x.SavingsLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositChangeInMaturity_SpecialDepositAccount_SpecialDepositLiquidationAccountId",
                        column: x => x.SpecialDepositLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositInterestScheduleItem",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FixedDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    FixedDepositInterestScheduleId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    OldBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PeriodCashAddition = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestEarned = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    NewBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    FixedDepositInterestScheduleId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositInterestScheduleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositInterestScheduleItem_FixedDepositAccount_FixedDepositAccountId",
                        column: x => x.FixedDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositInterestScheduleItem_FixedDepositInterestSchedule_FixedDepositInterestScheduleId",
                        column: x => x.FixedDepositInterestScheduleId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositInterestSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositInterestScheduleItem_FixedDepositInterestSchedule_FixedDepositInterestScheduleId1",
                        column: x => x.FixedDepositInterestScheduleId1,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositInterestSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositLiquidation",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FixedDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    LiquidationAccountType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SavingsLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    SpecialDepositLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    CustomerBankLiquidationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositLiquidation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidation_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidation_CustomerBankAccount_CustomerBankLiquidationAccountId",
                        column: x => x.CustomerBankLiquidationAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidation_FixedDepositAccount_FixedDepositAccountId",
                        column: x => x.FixedDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidation_SavingsAccount_SavingsLiquidationAccountId",
                        column: x => x.SavingsLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidation_SpecialDepositAccount_SpecialDepositLiquidationAccountId",
                        column: x => x.SpecialDepositLiquidationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidation_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialDepositFundTransfer",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DestinationAccountType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SavingsDestinationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    FixedDepositDestinationAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDepositFundTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositFundTransfer_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositFundTransfer_FixedDepositAccount_FixedDepositDestinationAccountId",
                        column: x => x.FixedDepositDestinationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositFundTransfer_SavingsAccount_SavingsDestinationAccountId",
                        column: x => x.SavingsDestinationAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositFundTransfer_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositFundTransfer_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositInterestAddition",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FixedDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    InterestScheduleItemId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    InterestEarned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositInterestAddition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositInterestAddition_FixedDepositAccount_FixedDepositAccountId",
                        column: x => x.FixedDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositInterestAddition_FixedDepositInterestScheduleItem_InterestScheduleItemId",
                        column: x => x.InterestScheduleItemId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositInterestScheduleItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositInterestAddition_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixedDepositLiquidationCharge",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FixedDepositLiquidationId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    ChargeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "FIXED_DEPOSIT_LIQUIDATION_CHARGE"),
                    LiquidationCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDepositLiquidationCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidationCharge_FixedDepositLiquidation_FixedDepositLiquidationId",
                        column: x => x.FixedDepositLiquidationId,
                        principalSchema: "Deposits",
                        principalTable: "FixedDepositLiquidation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixedDepositLiquidationCharge_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanAccount",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LoanApplicationId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PrincipalBalanceAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PrincipalLossAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EarnedInterestAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestBalanceAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    UnearnedInterestAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestLossAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesAccruedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesIncomeAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargesWaivedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TenureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepaymentCommencementDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UseSpecialDeposit = table.Column<bool>(type: "bit", nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DestinationAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedByUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LoanTopupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanTopupId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    InterestEarnedAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InterestPayoutAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanAccount_ApplicationUser_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccount_CustomerBankAccount_DestinationAccountId",
                        column: x => x.DestinationAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_ChargesAccruedAccountId",
                        column: x => x.ChargesAccruedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_ChargesIncomeAccountId",
                        column: x => x.ChargesIncomeAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_ChargesWaivedAccountId",
                        column: x => x.ChargesWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_EarnedInterestAccountId",
                        column: x => x.EarnedInterestAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_InterestBalanceAccountId",
                        column: x => x.InterestBalanceAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_InterestEarnedAccountId",
                        column: x => x.InterestEarnedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_InterestLossAccountId",
                        column: x => x.InterestLossAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_InterestPayoutAccountId",
                        column: x => x.InterestPayoutAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_InterestWaivedAccountId",
                        column: x => x.InterestWaivedAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_PrincipalBalanceAccountId",
                        column: x => x.PrincipalBalanceAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_PrincipalLossAccountId",
                        column: x => x.PrincipalLossAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LedgerAccount_UnearnedInterestAccountId",
                        column: x => x.UnearnedInterestAccountId,
                        principalSchema: "Accounting",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_LoanApplication_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalSchema: "Loans",
                        principalTable: "LoanApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanAccount_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanDisbursement",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DisbursedByUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DisbursementStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisbursementAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DisbursementDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DisbursementMode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CustomerBankAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApprovalDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoanAccountId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDisbursement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_ApplicationUser_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_ApplicationUser_DisbursedByUserId",
                        column: x => x.DisbursedByUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_CompanyBankAccount_DisbursementAccountId",
                        column: x => x.DisbursementAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_CustomerBankAccount_CustomerBankAccountId",
                        column: x => x.CustomerBankAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_LoanAccount_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalSchema: "Loans",
                        principalTable: "LoanAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_LoanAccount_LoanAccountId1",
                        column: x => x.LoanAccountId1,
                        principalSchema: "Loans",
                        principalTable: "LoanAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanDisbursement_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanOffset",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    OffsetAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    OldPrincipalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    NewPrincipalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    OldInterestBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    NewInterestBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalOffsetCharges = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    IsLiquidated = table.Column<bool>(type: "bit", nullable: false),
                    AllowedOffsetType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoanRepaymentMode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OffSetRepaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SavingsAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CustomerBankAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ModeOfPayment = table.Column<int>(type: "int", nullable: false),
                    CustomerPaymentDocumentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanOffset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanOffset_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanOffset_CustomerBankAccount_CustomerBankAccountId",
                        column: x => x.CustomerBankAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanOffset_CustomerPaymentDocument_CustomerPaymentDocumentId",
                        column: x => x.CustomerPaymentDocumentId,
                        principalSchema: "Docs",
                        principalTable: "CustomerPaymentDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanOffset_LoanAccount_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalSchema: "Loans",
                        principalTable: "LoanAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanOffset_SavingsAccount_SavingsAccountId",
                        column: x => x.SavingsAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanOffset_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanOffset_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanRepaymentSchedule",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RepaymentNo = table.Column<int>(type: "int", nullable: false),
                    BatchRefNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenureValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysInPeriod = table.Column<int>(type: "int", nullable: true),
                    PeriodPayment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CumulativeTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PeriodPrincipal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CumulativePrincipal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PrincipalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PeriodInterest = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CumulativeInterest = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    InterestBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsPrincipalAllocated = table.Column<bool>(type: "bit", nullable: false),
                    IsInterestAllocated = table.Column<bool>(type: "bit", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRepaymentSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanRepaymentSchedule_LoanAccount_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalSchema: "Loans",
                        principalTable: "LoanAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanTopup",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TopupAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DestinationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CustomerBankAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    OldPrincipalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    NewPrincipalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    OldInterestBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    NewInterestBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalTopupCharges = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TopupDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CommencementDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTopup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanTopup_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanTopup_CustomerBankAccount_CustomerBankAccountId",
                        column: x => x.CustomerBankAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanTopup_LoanAccount_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalSchema: "Loans",
                        principalTable: "LoanAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanTopup_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanTopup_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanDisbursementCharge",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanDisbursementId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DisbursementChargeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalCharge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDisbursementCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanDisbursementCharge_Charge_DisbursementChargeId",
                        column: x => x.DisbursementChargeId,
                        principalSchema: "MasterData",
                        principalTable: "Charge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanDisbursementCharge_LoanDisbursement_LoanDisbursementId",
                        column: x => x.LoanDisbursementId,
                        principalSchema: "Loans",
                        principalTable: "LoanDisbursement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanDisbursementCharge_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanOffSetCharge",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanOffsetId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    OffsetChargeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalCharge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoanOffsetId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanOffSetCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanOffSetCharge_Charge_OffsetChargeId",
                        column: x => x.OffsetChargeId,
                        principalSchema: "MasterData",
                        principalTable: "Charge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanOffSetCharge_LoanOffset_LoanOffsetId",
                        column: x => x.LoanOffsetId,
                        principalSchema: "Loans",
                        principalTable: "LoanOffset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanOffSetCharge_LoanOffset_LoanOffsetId1",
                        column: x => x.LoanOffsetId1,
                        principalSchema: "Loans",
                        principalTable: "LoanOffset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanOffSetCharge_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayrollDeductionScheduleItem",
                schema: "Payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PayrollDeductionScheduleId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    BatchRefNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PayrollCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    PayrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentStatus = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DeductionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoanRepaymentScheduleId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    SavingsAccountDeductionScheduleId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PayrollCronJobConfigId = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    SpecialDepositAccountDeductionScheduleId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollDeductionScheduleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollDeductionScheduleItem_LoanRepaymentSchedule_LoanRepaymentScheduleId",
                        column: x => x.LoanRepaymentScheduleId,
                        principalSchema: "Loans",
                        principalTable: "LoanRepaymentSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollDeductionScheduleItem_PayrollCronJobConfig_PayrollCronJobConfigId",
                        column: x => x.PayrollCronJobConfigId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollCronJobConfig",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollDeductionScheduleItem_PayrollDeductionSchedule_PayrollDeductionScheduleId",
                        column: x => x.PayrollDeductionScheduleId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollDeductionSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollDeductionScheduleItem_SavingsAccountDeductionSchedule_SavingsAccountDeductionScheduleId",
                        column: x => x.SavingsAccountDeductionScheduleId,
                        principalSchema: "Deposits",
                        principalTable: "SavingsAccountDeductionSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayrollDeductionScheduleItem_SpecialDepositAccountDeductionSchedule_SpecialDepositAccountDeductionScheduleId",
                        column: x => x.SpecialDepositAccountDeductionScheduleId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccountDeductionSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanTopupCharge",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanTopupId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TopupChargeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalCharge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    LoanTopupId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTopupCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanTopupCharge_Charge_TopupChargeId",
                        column: x => x.TopupChargeId,
                        principalSchema: "MasterData",
                        principalTable: "Charge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanTopupCharge_LoanTopup_LoanTopupId",
                        column: x => x.LoanTopupId,
                        principalSchema: "Loans",
                        principalTable: "LoanTopup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanTopupCharge_LoanTopup_LoanTopupId1",
                        column: x => x.LoanTopupId1,
                        principalSchema: "Loans",
                        principalTable: "LoanTopup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanTopupCharge_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanRepayment",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RepaymentMode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoanAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanRepaymentScheduleId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PayrollDeductionScheduleItemId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    LoanOffsetId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodStartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RepaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PaymentAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CustomerBankAccountId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRepayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanRepayment_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanRepayment_CompanyBankAccount_PaymentAccountId",
                        column: x => x.PaymentAccountId,
                        principalSchema: "Accounting",
                        principalTable: "CompanyBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanRepayment_CustomerBankAccount_CustomerBankAccountId",
                        column: x => x.CustomerBankAccountId,
                        principalSchema: "Customer",
                        principalTable: "CustomerBankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanRepayment_LoanAccount_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalSchema: "Loans",
                        principalTable: "LoanAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanRepayment_LoanOffset_LoanOffsetId",
                        column: x => x.LoanOffsetId,
                        principalSchema: "Loans",
                        principalTable: "LoanOffset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanRepayment_LoanRepaymentSchedule_LoanRepaymentScheduleId",
                        column: x => x.LoanRepaymentScheduleId,
                        principalSchema: "Loans",
                        principalTable: "LoanRepaymentSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanRepayment_PayrollDeductionScheduleItem_PayrollDeductionScheduleItemId",
                        column: x => x.PayrollDeductionScheduleItemId,
                        principalSchema: "Payroll",
                        principalTable: "PayrollDeductionScheduleItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanRepayment_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanRepaymentCharge",
                schema: "Loans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoanRepaymentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RepaymentChargeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ChargeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalCharge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TransactionJournalId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    LoanRepaymentId1 = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullText = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRepaymentCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanRepaymentCharge_Charge_RepaymentChargeId",
                        column: x => x.RepaymentChargeId,
                        principalSchema: "MasterData",
                        principalTable: "Charge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanRepaymentCharge_LoanRepayment_LoanRepaymentId",
                        column: x => x.LoanRepaymentId,
                        principalSchema: "Loans",
                        principalTable: "LoanRepayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanRepaymentCharge_LoanRepayment_LoanRepaymentId1",
                        column: x => x.LoanRepaymentId1,
                        principalSchema: "Loans",
                        principalTable: "LoanRepayment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanRepaymentCharge_TransactionJournal_TransactionJournalId",
                        column: x => x.TransactionJournalId,
                        principalSchema: "Accounting",
                        principalTable: "TransactionJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriod_CalendarId",
                schema: "Accounting",
                table: "AccountingPeriod",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriod_FinancialCalendarId",
                schema: "Accounting",
                table: "AccountingPeriod",
                column: "FinancialCalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriod_Name",
                schema: "Accounting",
                table: "AccountingPeriod",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "ApplicationRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleClaim_PermissionId",
                schema: "Security",
                table: "ApplicationRoleClaim",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleClaim_RoleId",
                schema: "Security",
                table: "ApplicationRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "ApplicationUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "ApplicationUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserClaim_PermissionId",
                schema: "Security",
                table: "ApplicationUserClaim",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserClaim_UserId",
                schema: "Security",
                table: "ApplicationUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLogin_UserId",
                schema: "Security",
                table: "ApplicationUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_RoleId",
                schema: "Security",
                table: "ApplicationUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ApprovalWorkflowId",
                schema: "Security",
                table: "Approval",
                column: "ApprovalWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalDocument_ApprovalId",
                schema: "Security",
                table: "ApprovalDocument",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalGroup_ApprovalWorkflowId",
                schema: "Security",
                table: "ApprovalGroup",
                column: "ApprovalWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalGroupMember_ApplicationUserId",
                schema: "Security",
                table: "ApprovalGroupMember",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalGroupMember_ApprovalGroupId",
                schema: "Security",
                table: "ApprovalGroupMember",
                column: "ApprovalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalGroupMember_ApprovalGroupId1",
                schema: "Security",
                table: "ApprovalGroupMember",
                column: "ApprovalGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalGroupWorkflow_ApprovalGroupId",
                schema: "Security",
                table: "ApprovalGroupWorkflow",
                column: "ApprovalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalGroupWorkflow_ApprovalWorkflowId",
                schema: "Security",
                table: "ApprovalGroupWorkflow",
                column: "ApprovalWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLog_ApprovalGroupId",
                schema: "Security",
                table: "ApprovalLog",
                column: "ApprovalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLog_ApprovalId",
                schema: "Security",
                table: "ApprovalLog",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLog_ApprovedByUserId",
                schema: "Security",
                table: "ApprovalLog",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalNotification_ApprovalWorkflowId",
                schema: "Security",
                table: "ApprovalNotification",
                column: "ApprovalWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRole_EventGlobalCodeId_RoleId",
                schema: "Security",
                table: "ApprovalRole",
                columns: new[] { "EventGlobalCodeId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRole_EventGlobalCodeId_RoleId_Order",
                schema: "Security",
                table: "ApprovalRole",
                columns: new[] { "EventGlobalCodeId", "RoleId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRole_RoleId",
                schema: "Security",
                table: "ApprovalRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrail_ApplicationUserId",
                schema: "Security",
                table: "AuditTrail",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrail_EventGlobalCodeId",
                schema: "Security",
                table: "AuditTrail",
                column: "EventGlobalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_Name",
                schema: "MasterData",
                table: "Bank",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bank_Name_Code",
                schema: "MasterData",
                table: "Bank",
                columns: new[] { "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charge_Code",
                schema: "MasterData",
                table: "Charge",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charge_Code_Name",
                schema: "MasterData",
                table: "Charge",
                columns: new[] { "Code", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charge_CurrencyId",
                schema: "MasterData",
                table: "Charge",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccount_BankId",
                schema: "Accounting",
                table: "CompanyBankAccount",
                column: "BankId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccount_BankId_LedgerAccountId",
                schema: "Accounting",
                table: "CompanyBankAccount",
                columns: new[] { "BankId", "LedgerAccountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccount_CurrencyId",
                schema: "Accounting",
                table: "CompanyBankAccount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccount_LedgerAccountId",
                schema: "Accounting",
                table: "CompanyBankAccount",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApplicationUserId",
                schema: "Customer",
                table: "Customer",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CashAccountId",
                schema: "Customer",
                table: "Customer",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DepartmentId",
                schema: "Customer",
                table: "Customer",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankAccount_BankId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankAccount_CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankAccount_LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBeneficiary_CustomerId",
                schema: "Customer",
                table: "CustomerBeneficiary",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDepositProductPublication_CustomerId",
                schema: "Deposits",
                table: "CustomerDepositProductPublication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDepositProductPublication_ProductId",
                schema: "Deposits",
                table: "CustomerDepositProductPublication",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLoanProductPublication_CustomerId",
                schema: "Loans",
                table: "CustomerLoanProductPublication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLoanProductPublication_ProductId",
                schema: "Loans",
                table: "CustomerLoanProductPublication",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNextOfKin_CustomerId",
                schema: "Customer",
                table: "CustomerNextOfKin",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDocument_CustomerId",
                schema: "Docs",
                table: "CustomerPaymentDocument",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                schema: "MasterData",
                table: "Department",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentDepositProductPublication_DepartmentId",
                schema: "Deposits",
                table: "DepartmentDepositProductPublication",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentDepositProductPublication_ProductId",
                schema: "Deposits",
                table: "DepartmentDepositProductPublication",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLoanProductPublication_DepartmentId",
                schema: "Loans",
                table: "DepartmentLoanProductPublication",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLoanProductPublication_ProductId",
                schema: "Loans",
                table: "DepartmentLoanProductPublication",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_ApprovalId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_ApprovalWorkflowId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "ApprovalWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_BankDepositAccountId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "BankDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_ChargesAccrualAccountId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "ChargesAccrualAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_ChargesIncomeAccountId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "ChargesIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_ChargesWaivedAccountId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "ChargesWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_DefaultCurrencyId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "DefaultCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_InterestPayableAccountId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "InterestPayableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_InterestPayoutAccountId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "InterestPayoutAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_Name",
                schema: "Deposits",
                table: "DepositProduct",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_Name_Code",
                schema: "Deposits",
                table: "DepositProduct",
                columns: new[] { "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_ProductDepositAccountId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "ProductDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProduct_PublishedByUserId",
                schema: "Deposits",
                table: "DepositProduct",
                column: "PublishedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProductCharge_ChargeId",
                schema: "Deposits",
                table: "DepositProductCharge",
                column: "ChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositProductCharge_ProductId_ChargeId",
                schema: "Deposits",
                table: "DepositProductCharge",
                columns: new[] { "ProductId", "ChargeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepositProductInterestRange_ProductId_LowerLimit_UpperLimit",
                schema: "Deposits",
                table: "DepositProductInterestRange",
                columns: new[] { "ProductId", "LowerLimit", "UpperLimit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_Name",
                schema: "Docs",
                table: "DocumentType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CustomerId",
                schema: "HR",
                table: "Employee",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                schema: "HR",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeNo",
                schema: "HR",
                table: "Employee",
                column: "EmployeeNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPaymentInfo_MemberProfileId",
                schema: "Security",
                table: "EnrollmentPaymentInfo",
                column: "MemberProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCalendar_Name",
                schema: "Accounting",
                table: "FinancialCalendar",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCalendar_Name_Code",
                schema: "Accounting",
                table: "FinancialCalendar",
                columns: new[] { "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_AccountNo",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "AccountNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_ApplicationId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_ChargesAccruedAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "ChargesAccruedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_ChargesIncomeAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "ChargesIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_ChargesWaivedAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "ChargesWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_ClosedByUserId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_CustomerBankLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "CustomerBankLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_CustomerId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_DepositAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "DepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_DepositProductId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "DepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_InterestEarnedAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "InterestEarnedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_InterestPayoutAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "InterestPayoutAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_SavingsLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "SavingsLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_SpecialDepositLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "SpecialDepositLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_ApplicationNo",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "ApplicationNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_ApprovalId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_CustomerBankFundingSourceAccountId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "CustomerBankFundingSourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_CustomerBankLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "CustomerBankLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_CustomerId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_DepositProductId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "DepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_PaymentDocumentId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "PaymentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_SavingsLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "SavingsLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_SpecialDepositFundingSourceAccountId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "SpecialDepositFundingSourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccountApplication_SpecialDepositLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositAccountApplication",
                column: "SpecialDepositLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositChangeInMaturity_ApprovalId",
                schema: "Deposits",
                table: "FixedDepositChangeInMaturity",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositChangeInMaturity_CustomerBankLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositChangeInMaturity",
                column: "CustomerBankLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositChangeInMaturity_FixedDepositAccountId",
                schema: "Deposits",
                table: "FixedDepositChangeInMaturity",
                column: "FixedDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositChangeInMaturity_SavingsLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositChangeInMaturity",
                column: "SavingsLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositChangeInMaturity_SpecialDepositLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositChangeInMaturity",
                column: "SpecialDepositLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestAddition_FixedDepositAccountId",
                schema: "Deposits",
                table: "FixedDepositInterestAddition",
                column: "FixedDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestAddition_InterestScheduleItemId",
                schema: "Deposits",
                table: "FixedDepositInterestAddition",
                column: "InterestScheduleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestAddition_TransactionJournalId",
                schema: "Deposits",
                table: "FixedDepositInterestAddition",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestSchedule_CronJobConfigId",
                schema: "Deposits",
                table: "FixedDepositInterestSchedule",
                column: "CronJobConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestScheduleItem_FixedDepositAccountId",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem",
                column: "FixedDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestScheduleItem_FixedDepositInterestScheduleId",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem",
                column: "FixedDepositInterestScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestScheduleItem_FixedDepositInterestScheduleId1",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem",
                column: "FixedDepositInterestScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidation_ApprovalId",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidation_CustomerBankLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                column: "CustomerBankLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidation_FixedDepositAccountId",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                column: "FixedDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidation_SavingsLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                column: "SavingsLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidation_SpecialDepositLiquidationAccountId",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                column: "SpecialDepositLiquidationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidation_TransactionJournalId",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidationCharge_FixedDepositLiquidationId",
                schema: "Deposits",
                table: "FixedDepositLiquidationCharge",
                column: "FixedDepositLiquidationId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositLiquidationCharge_TransactionJournalId",
                schema: "Deposits",
                table: "FixedDepositLiquidationCharge",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalCode_Name_Code_CodeType",
                schema: "MasterData",
                table: "GlobalCode",
                columns: new[] { "Name", "Code", "CodeType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_AccountId",
                schema: "Accounting",
                table: "JournalEntry",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_TransactionEntryNo",
                schema: "Accounting",
                table: "JournalEntry",
                column: "TransactionEntryNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_TransactionEntryNo_AccountId",
                schema: "Accounting",
                table: "JournalEntry",
                columns: new[] { "TransactionEntryNo", "AccountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_TransactionJournalId",
                schema: "Accounting",
                table: "JournalEntry",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_CurrencyId",
                schema: "Accounting",
                table: "LedgerAccount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_Name",
                schema: "Accounting",
                table: "LedgerAccount",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_Name_Code",
                schema: "Accounting",
                table: "LedgerAccount",
                columns: new[] { "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_ParentId",
                schema: "Accounting",
                table: "LedgerAccount",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_LienType_Name",
                schema: "Accounting",
                table: "LienType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_AccountNo",
                schema: "Loans",
                table: "LoanAccount",
                column: "AccountNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_ChargesAccruedAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "ChargesAccruedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_ChargesIncomeAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "ChargesIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_ChargesWaivedAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "ChargesWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_ClosedByUserId",
                schema: "Loans",
                table: "LoanAccount",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_CustomerId",
                schema: "Loans",
                table: "LoanAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_DestinationAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "DestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_EarnedInterestAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "EarnedInterestAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_InterestBalanceAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "InterestBalanceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "InterestEarnedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_InterestLossAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "InterestLossAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_InterestPayoutAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "InterestPayoutAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_InterestWaivedAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "InterestWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_LoanApplicationId",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanTopupId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_PrincipalBalanceAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "PrincipalBalanceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_PrincipalLossAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "PrincipalLossAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_SpecialDepositAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_UnearnedInterestAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "UnearnedInterestAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_ApplicationNo",
                schema: "Loans",
                table: "LoanApplication",
                column: "ApplicationNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_ApprovalId",
                schema: "Loans",
                table: "LoanApplication",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_CustomerDisbursementAccountId",
                schema: "Loans",
                table: "LoanApplication",
                column: "CustomerDisbursementAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_CustomerId",
                schema: "Loans",
                table: "LoanApplication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_LoanProductId",
                schema: "Loans",
                table: "LoanApplication",
                column: "LoanProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_SpecialDepositId",
                schema: "Loans",
                table: "LoanApplication",
                column: "SpecialDepositId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationApproval_LoanApplicationId",
                schema: "Loans",
                table: "LoanApplicationApproval",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationGuarantor_GuarantorId",
                schema: "Loans",
                table: "LoanApplicationGuarantor",
                column: "GuarantorId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationGuarantor_LoanApplicationId",
                schema: "Loans",
                table: "LoanApplicationGuarantor",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationItem_LoanApplicationId",
                schema: "Loans",
                table: "LoanApplicationItem",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationItem_Name_Model_BrandName_Color",
                schema: "Loans",
                table: "LoanApplicationItem",
                columns: new[] { "Name", "Model", "BrandName", "Color" });

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationSchedule_LoanApplicationId",
                schema: "Loans",
                table: "LoanApplicationSchedule",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationSchedule_LoanApplicationId_RepaymentNo",
                schema: "Loans",
                table: "LoanApplicationSchedule",
                columns: new[] { "LoanApplicationId", "RepaymentNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_ApprovalId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_ApprovedByUserId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_CustomerBankAccountId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "CustomerBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_DisbursedByUserId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "DisbursedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_DisbursementAccountId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "DisbursementAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_LoanAccountId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_LoanAccountId1",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "LoanAccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_SpecialDepositAccountId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_TransactionJournalId",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursementCharge_DisbursementChargeId",
                schema: "Loans",
                table: "LoanDisbursementCharge",
                column: "DisbursementChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursementCharge_LoanDisbursementId",
                schema: "Loans",
                table: "LoanDisbursementCharge",
                column: "LoanDisbursementId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursementCharge_TransactionJournalId",
                schema: "Loans",
                table: "LoanDisbursementCharge",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffset_ApprovalId",
                schema: "Loans",
                table: "LoanOffset",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffset_CustomerBankAccountId",
                schema: "Loans",
                table: "LoanOffset",
                column: "CustomerBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffset_CustomerPaymentDocumentId",
                schema: "Loans",
                table: "LoanOffset",
                column: "CustomerPaymentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffset_LoanAccountId",
                schema: "Loans",
                table: "LoanOffset",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffset_SavingsAccountId",
                schema: "Loans",
                table: "LoanOffset",
                column: "SavingsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffset_SpecialDepositAccountId",
                schema: "Loans",
                table: "LoanOffset",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffset_TransactionJournalId",
                schema: "Loans",
                table: "LoanOffset",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffSetCharge_LoanOffsetId",
                schema: "Loans",
                table: "LoanOffSetCharge",
                column: "LoanOffsetId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffSetCharge_LoanOffsetId1",
                schema: "Loans",
                table: "LoanOffSetCharge",
                column: "LoanOffsetId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffSetCharge_OffsetChargeId",
                schema: "Loans",
                table: "LoanOffSetCharge",
                column: "OffsetChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffSetCharge_TransactionJournalId",
                schema: "Loans",
                table: "LoanOffSetCharge",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_ApprovalId",
                schema: "Loans",
                table: "LoanProduct",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_ApprovalWorkflowId",
                schema: "Loans",
                table: "LoanProduct",
                column: "ApprovalWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_BankDepositAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "BankDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_ChargesAccrualAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "ChargesAccrualAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_ChargesIncomeAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "ChargesIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_ChargesWaivedAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "ChargesWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_DefaultCurrencyId",
                schema: "Loans",
                table: "LoanProduct",
                column: "DefaultCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_DisbursementAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "DisbursementAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_InterestIncomeAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "InterestIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_InterestLossAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "InterestLossAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_InterestWaivedAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "InterestWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_Name",
                schema: "Loans",
                table: "LoanProduct",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_Name_Code",
                schema: "Loans",
                table: "LoanProduct",
                columns: new[] { "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_PenalInterestReceivableAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "PenalInterestReceivableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_PrincipalAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "PrincipalAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_PrincipalLossAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "PrincipalLossAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_PublishedByUserId",
                schema: "Loans",
                table: "LoanProduct",
                column: "PublishedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_UnearnedInterestAccountId",
                schema: "Loans",
                table: "LoanProduct",
                column: "UnearnedInterestAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProductCharge_ChargeId",
                schema: "Loans",
                table: "LoanProductCharge",
                column: "ChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProductCharge_ProductId",
                schema: "Loans",
                table: "LoanProductCharge",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_ApprovalId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_CustomerBankAccountId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "CustomerBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_LoanAccountId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_LoanOffsetId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "LoanOffsetId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_LoanRepaymentScheduleId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "LoanRepaymentScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_PaymentAccountId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "PaymentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_PayrollDeductionScheduleItemId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "PayrollDeductionScheduleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_TransactionJournalId",
                schema: "Loans",
                table: "LoanRepayment",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentCharge_LoanRepaymentId",
                schema: "Loans",
                table: "LoanRepaymentCharge",
                column: "LoanRepaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentCharge_LoanRepaymentId1",
                schema: "Loans",
                table: "LoanRepaymentCharge",
                column: "LoanRepaymentId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentCharge_RepaymentChargeId",
                schema: "Loans",
                table: "LoanRepaymentCharge",
                column: "RepaymentChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentCharge_TransactionJournalId",
                schema: "Loans",
                table: "LoanRepaymentCharge",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentSchedule_LoanAccountId",
                schema: "Loans",
                table: "LoanRepaymentSchedule",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopup_ApprovalId",
                schema: "Loans",
                table: "LoanTopup",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopup_CustomerBankAccountId",
                schema: "Loans",
                table: "LoanTopup",
                column: "CustomerBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopup_LoanAccountId",
                schema: "Loans",
                table: "LoanTopup",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopup_SpecialDepositAccountId",
                schema: "Loans",
                table: "LoanTopup",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopup_TransactionJournalId",
                schema: "Loans",
                table: "LoanTopup",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopupCharge_LoanTopupId",
                schema: "Loans",
                table: "LoanTopupCharge",
                column: "LoanTopupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopupCharge_LoanTopupId1",
                schema: "Loans",
                table: "LoanTopupCharge",
                column: "LoanTopupId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopupCharge_TopupChargeId",
                schema: "Loans",
                table: "LoanTopupCharge",
                column: "TopupChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopupCharge_TransactionJournalId",
                schema: "Loans",
                table: "LoanTopupCharge",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                schema: "MasterData",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name_Code",
                schema: "MasterData",
                table: "Location",
                columns: new[] { "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParentId",
                schema: "MasterData",
                table: "Location",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberBankAccount_BankId",
                schema: "Security",
                table: "MemberBankAccount",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberBankAccount_ProfileId",
                schema: "Security",
                table: "MemberBankAccount",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberBeneficiary_ProfileId",
                schema: "Security",
                table: "MemberBeneficiary",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberNextOfKin_ProfileId",
                schema: "Security",
                table: "MemberNextOfKin",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberProfile_ApplicationUserId",
                schema: "Security",
                table: "MemberProfile",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberProfile_DepartmentId",
                schema: "Security",
                table: "MemberProfile",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeDocument_DocumentNo",
                schema: "Docs",
                table: "OfficeDocument",
                column: "DocumentNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfficeDocument_DocumentTypeId",
                schema: "Docs",
                table: "OfficeDocument",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficePhoto_DocumentNo",
                schema: "Docs",
                table: "OfficePhoto",
                column: "DocumentNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfficePhoto_DocumentTypeId",
                schema: "Docs",
                table: "OfficePhoto",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeSheet_DocumentNo",
                schema: "Docs",
                table: "OfficeSheet",
                column: "DocumentNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfficeSheet_DocumentTypeId",
                schema: "Docs",
                table: "OfficeSheet",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMode_Name",
                schema: "Accounting",
                table: "PaymentMode",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollCronJobConfig_DeductionScheduleId",
                schema: "Payroll",
                table: "PayrollCronJobConfig",
                column: "DeductionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionDocument_PayrollDeductionScheduleId",
                schema: "Docs",
                table: "PayrollDeductionDocument",
                column: "PayrollDeductionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionItem_PayrollDeductionScheduleId",
                schema: "Payroll",
                table: "PayrollDeductionItem",
                column: "PayrollDeductionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionSchedule_BankAccountId",
                schema: "Payroll",
                table: "PayrollDeductionSchedule",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionSchedule_FixedDepositBankAccountId",
                schema: "Payroll",
                table: "PayrollDeductionSchedule",
                column: "FixedDepositBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionSchedule_ScheduleName",
                schema: "Payroll",
                table: "PayrollDeductionSchedule",
                column: "ScheduleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionSchedule_SpecialDepositBankAccountId",
                schema: "Payroll",
                table: "PayrollDeductionSchedule",
                column: "SpecialDepositBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionScheduleItem_LoanRepaymentScheduleId",
                schema: "Payroll",
                table: "PayrollDeductionScheduleItem",
                column: "LoanRepaymentScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionScheduleItem_PayrollCronJobConfigId",
                schema: "Payroll",
                table: "PayrollDeductionScheduleItem",
                column: "PayrollCronJobConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionScheduleItem_PayrollDeductionScheduleId",
                schema: "Payroll",
                table: "PayrollDeductionScheduleItem",
                column: "PayrollDeductionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionScheduleItem_SavingsAccountDeductionScheduleId",
                schema: "Payroll",
                table: "PayrollDeductionScheduleItem",
                column: "SavingsAccountDeductionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductionScheduleItem_SpecialDepositAccountDeductionScheduleId",
                schema: "Payroll",
                table: "PayrollDeductionScheduleItem",
                column: "SpecialDepositAccountDeductionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name_Code",
                schema: "Security",
                table: "Permission",
                columns: new[] { "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name_Code_Category",
                schema: "Security",
                table: "Permission",
                columns: new[] { "Name", "Code", "Category" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Code] IS NOT NULL AND [Category] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name_Code_Group",
                schema: "Security",
                table: "Permission",
                columns: new[] { "Name", "Code", "Group" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Code] IS NOT NULL AND [Group] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name_Code_Module",
                schema: "Security",
                table: "Permission",
                columns: new[] { "Name", "Code", "Module" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Code] IS NOT NULL AND [Module] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_AccountNo",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "AccountNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_ApplicationId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_ChargesAccruedAccountId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "ChargesAccruedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_ChargesIncomeAccountId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "ChargesIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_ChargesPayableAccountId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "ChargesPayableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_ChargesWaivedAccountId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "ChargesWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_ClosedByUserId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_CustomerId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_DepositProductId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "DepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccount_LedgerDepositAccountId",
                schema: "Deposits",
                table: "SavingsAccount",
                column: "LedgerDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccountApplication_ApplicationNo",
                schema: "Deposits",
                table: "SavingsAccountApplication",
                column: "ApplicationNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccountApplication_ApprovalId",
                schema: "Deposits",
                table: "SavingsAccountApplication",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccountApplication_CustomerId",
                schema: "Deposits",
                table: "SavingsAccountApplication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccountApplication_DepositProductId",
                schema: "Deposits",
                table: "SavingsAccountApplication",
                column: "DepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccountDeductionSchedule_SavingsAccountId",
                schema: "Deposits",
                table: "SavingsAccountDeductionSchedule",
                column: "SavingsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsCashAddition_ApprovalId",
                schema: "Deposits",
                table: "SavingsCashAddition",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsCashAddition_BatchRefNo",
                schema: "Deposits",
                table: "SavingsCashAddition",
                column: "BatchRefNo",
                unique: true,
                filter: "[BatchRefNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsCashAddition_CustomerPaymentDocumentId",
                schema: "Deposits",
                table: "SavingsCashAddition",
                column: "CustomerPaymentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsCashAddition_SavingsAccountId",
                schema: "Deposits",
                table: "SavingsCashAddition",
                column: "SavingsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsCashAddition_TransactionJournalId",
                schema: "Deposits",
                table: "SavingsCashAddition",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsIncreaseDecrease_ApprovalId",
                schema: "Deposits",
                table: "SavingsIncreaseDecrease",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsIncreaseDecrease_SavingsAccountId",
                schema: "Deposits",
                table: "SavingsIncreaseDecrease",
                column: "SavingsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_AccountNo",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "AccountNo",
                unique: true,
                filter: "[AccountNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_ApplicationId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_ChargesAccruedAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "ChargesAccruedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_ChargesIncomeAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "ChargesIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_ChargesWaivedAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "ChargesWaivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_ClosedByUserId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_CustomerId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_DepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "DepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_DepositProductId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "DepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_InterestEarnedAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "InterestEarnedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccount_InterestPayoutAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount",
                column: "InterestPayoutAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccountApplication_ApplicationNo",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication",
                column: "ApplicationNo",
                unique: true,
                filter: "[ApplicationNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccountApplication_ApprovalId",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccountApplication_CustomerId",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccountApplication_DepositProductId",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication",
                column: "DepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccountApplication_PaymentDocumentId",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication",
                column: "PaymentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositAccountDeductionSchedule_SpecialDepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccountDeductionSchedule",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositCashAddition_ApprovalId",
                schema: "Deposits",
                table: "SpecialDepositCashAddition",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositCashAddition_BatchRefNo",
                schema: "Deposits",
                table: "SpecialDepositCashAddition",
                column: "BatchRefNo",
                unique: true,
                filter: "[BatchRefNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositCashAddition_CustomerPaymentDocumentId",
                schema: "Deposits",
                table: "SpecialDepositCashAddition",
                column: "CustomerPaymentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositCashAddition_SpecialDepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositCashAddition",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositCashAddition_TransactionJournalId",
                schema: "Deposits",
                table: "SpecialDepositCashAddition",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositFundTransfer_ApprovalId",
                schema: "Deposits",
                table: "SpecialDepositFundTransfer",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositFundTransfer_FixedDepositDestinationAccountId",
                schema: "Deposits",
                table: "SpecialDepositFundTransfer",
                column: "FixedDepositDestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositFundTransfer_SavingsDestinationAccountId",
                schema: "Deposits",
                table: "SpecialDepositFundTransfer",
                column: "SavingsDestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositFundTransfer_SpecialDepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositFundTransfer",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositFundTransfer_TransactionJournalId",
                schema: "Deposits",
                table: "SpecialDepositFundTransfer",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestAddition_InterestScheduleItemId",
                schema: "Deposits",
                table: "SpecialDepositInterestAddition",
                column: "InterestScheduleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestAddition_SpecialDepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositInterestAddition",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestAddition_TransactionJournalId",
                schema: "Deposits",
                table: "SpecialDepositInterestAddition",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestSchedule_CronJobConfigId",
                schema: "Deposits",
                table: "SpecialDepositInterestSchedule",
                column: "CronJobConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestScheduleItem_SpecialDepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem",
                column: "SpecialDepositAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestScheduleItem_SpecialDepositInterestScheduleId",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem",
                column: "SpecialDepositInterestScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestScheduleItem_SpecialDepositInterestScheduleId1",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem",
                column: "SpecialDepositInterestScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositWithdrawal_ApprovalId",
                schema: "Deposits",
                table: "SpecialDepositWithdrawal",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositWithdrawal_CustomerDestinationBankAccountId",
                schema: "Deposits",
                table: "SpecialDepositWithdrawal",
                column: "CustomerDestinationBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositWithdrawal_SpecialDepositSourceAccountId",
                schema: "Deposits",
                table: "SpecialDepositWithdrawal",
                column: "SpecialDepositSourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositWithdrawal_TransactionJournalId",
                schema: "Deposits",
                table: "SpecialDepositWithdrawal",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocument_DocumentNo",
                schema: "Accounting",
                table: "TransactionDocument",
                column: "DocumentNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocument_DocumentTypeId",
                schema: "Accounting",
                table: "TransactionDocument",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocument_TransactionJournalId",
                schema: "Accounting",
                table: "TransactionDocument",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocument_TransactionJournalId1",
                schema: "Accounting",
                table: "TransactionDocument",
                column: "TransactionJournalId1");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionJournal_ApprovalId",
                schema: "Accounting",
                table: "TransactionJournal",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionJournal_PostedByUserId",
                schema: "Accounting",
                table: "TransactionJournal",
                column: "PostedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionJournal_ReversedByUserId",
                schema: "Accounting",
                table: "TransactionJournal",
                column: "ReversedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionJournal_TransactionNo",
                schema: "Accounting",
                table: "TransactionJournal",
                column: "TransactionNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionJournal_TransactionNo_Title",
                schema: "Accounting",
                table: "TransactionJournal",
                columns: new[] { "TransactionNo", "Title" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanTopupId1",
                principalSchema: "Loans",
                principalTable: "LoanTopup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ApplicationUser_ApplicationUserId",
                schema: "Customer",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_ApplicationUser_PublishedByUserId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_ApplicationUser_ClosedByUserId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_ApplicationUser_PublishedByUserId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_ApplicationUser_ClosedByUserId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionJournal_ApplicationUser_PostedByUserId",
                schema: "Accounting",
                table: "TransactionJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionJournal_ApplicationUser_ReversedByUserId",
                schema: "Accounting",
                table: "TransactionJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_Approval_ApprovalWorkflow_ApprovalWorkflowId",
                schema: "Security",
                table: "Approval");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_ApprovalWorkflow_ApprovalWorkflowId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_ApprovalWorkflow_ApprovalWorkflowId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_Approval_ApprovalId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_Approval_ApprovalId",
                schema: "Loans",
                table: "LoanApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_Approval_ApprovalId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTopup_Approval_ApprovalId",
                schema: "Loans",
                table: "LoanTopup");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccountApplication_Approval_ApprovalId",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionJournal_Approval_ApprovalId",
                schema: "Accounting",
                table: "TransactionJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBankAccount_Currency_CurrencyId",
                schema: "Accounting",
                table: "CompanyBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_Currency_DefaultCurrencyId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LedgerAccount_Currency_CurrencyId",
                schema: "Accounting",
                table: "LedgerAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_Currency_DefaultCurrencyId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBankAccount_Bank_BankId",
                schema: "Accounting",
                table: "CompanyBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_Bank_BankId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBankAccount_LedgerAccount_LedgerAccountId",
                schema: "Accounting",
                table: "CompanyBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_LedgerAccount_CashAccountId",
                schema: "Customer",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_LedgerAccount_LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_LedgerAccount_ChargesAccrualAccountId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_LedgerAccount_ChargesIncomeAccountId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_LedgerAccount_ChargesWaivedAccountId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_LedgerAccount_InterestPayableAccountId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_LedgerAccount_InterestPayoutAccountId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositProduct_LedgerAccount_ProductDepositAccountId",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_ChargesAccruedAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_ChargesIncomeAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_ChargesWaivedAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_EarnedInterestAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_InterestBalanceAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_InterestLossAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_InterestPayoutAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_InterestWaivedAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_PrincipalBalanceAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_PrincipalLossAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_UnearnedInterestAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_ChargesAccrualAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_ChargesIncomeAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_ChargesWaivedAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_InterestIncomeAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_InterestLossAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_InterestWaivedAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_PenalInterestReceivableAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_PrincipalAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_PrincipalLossAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanProduct_LedgerAccount_UnearnedInterestAccountId",
                schema: "Loans",
                table: "LoanProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_LedgerAccount_ChargesAccruedAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_LedgerAccount_ChargesIncomeAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_LedgerAccount_ChargesWaivedAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_LedgerAccount_DepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_LedgerAccount_InterestEarnedAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_LedgerAccount_InterestPayoutAccountId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Department_DepartmentId",
                schema: "Customer",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_Customer_CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPaymentDocument_Customer_CustomerId",
                schema: "Docs",
                table: "CustomerPaymentDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_Customer_CustomerId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_Customer_CustomerId",
                schema: "Loans",
                table: "LoanApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_Customer_CustomerId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccountApplication_Customer_CustomerId",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccount_DepositProduct_DepositProductId",
                schema: "Deposits",
                table: "SpecialDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositAccountApplication_DepositProduct_DepositProductId",
                schema: "Deposits",
                table: "SpecialDepositAccountApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_LoanProduct_LoanProductId",
                schema: "Loans",
                table: "LoanApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_CustomerBankAccount_DestinationAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_CustomerBankAccount_CustomerDisbursementAccountId",
                schema: "Loans",
                table: "LoanApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTopup_CustomerBankAccount_CustomerBankAccountId",
                schema: "Loans",
                table: "LoanTopup");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_SpecialDepositAccount_SpecialDepositAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_SpecialDepositAccount_SpecialDepositId",
                schema: "Loans",
                table: "LoanApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTopup_SpecialDepositAccount_SpecialDepositAccountId",
                schema: "Loans",
                table: "LoanTopup");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTopup_TransactionJournal_TransactionJournalId",
                schema: "Loans",
                table: "LoanTopup");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanApplication_LoanApplicationId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropTable(
                name: "AccountingPeriod",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "ApplicationRoleClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApplicationUserClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApplicationUserLogin",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApplicationUserRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApplicationUserToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalDocument",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalEmailAlert",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalGroupMember",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalGroupWorkflow",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalLog",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalNotification",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "AuditTrail",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "CustomerBeneficiary",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerDepositProductPublication",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "CustomerLoanProductPublication",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "CustomerNextOfKin",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "DepartmentDepositProductPublication",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "DepartmentLoanProductPublication",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "DepositProductCharge",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "DepositProductInterestRange",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "EnrollmentPaymentInfo",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "FixedDepositChangeInMaturity",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "FixedDepositInterestAddition",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "FixedDepositLiquidationCharge",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "JournalEntry",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "LienType",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "LoanApplicationApproval",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanApplicationGuarantor",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanApplicationItem",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanApplicationSchedule",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanDisbursementCharge",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanOffSetCharge",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanProductCharge",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanRepaymentCharge",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanTopupCharge",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "MemberBankAccount",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "MemberBeneficiary",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "MemberBulkUploadSession",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "MemberBulkUploadTemp",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "MemberNextOfKin",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "OfficeDocument",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "OfficePhoto",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "OfficeSheet",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "PaymentMode",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "PayrollDeductionDocument",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "PayrollDeductionItem",
                schema: "Payroll");

            migrationBuilder.DropTable(
                name: "SavingsCashAddition",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "SavingsIncreaseDecrease",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "SpecialDepositCashAddition",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "SpecialDepositFundTransfer",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "SpecialDepositInterestAddition",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "SpecialDepositWithdrawal",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "TransactionDocument",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "FinancialCalendar",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalGroup",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApplicationRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "GlobalCode",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "FixedDepositInterestScheduleItem",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "FixedDepositLiquidation",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "LoanDisbursement",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanRepayment",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "Charge",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "MemberProfile",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "SpecialDepositInterestScheduleItem",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "FixedDepositInterestSchedule",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "FixedDepositAccount",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "LoanOffset",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "PayrollDeductionScheduleItem",
                schema: "Payroll");

            migrationBuilder.DropTable(
                name: "SpecialDepositInterestSchedule",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "FixedDepositAccountApplication",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "LoanRepaymentSchedule",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "SavingsAccountDeductionSchedule",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "SpecialDepositAccountDeductionSchedule",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "PayrollCronJobConfig",
                schema: "Payroll");

            migrationBuilder.DropTable(
                name: "SavingsAccount",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "PayrollDeductionSchedule",
                schema: "Payroll");

            migrationBuilder.DropTable(
                name: "SavingsAccountApplication",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "ApplicationUser",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApprovalWorkflow",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Approval",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "Bank",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "LedgerAccount",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "DepositProduct",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "LoanProduct",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "CompanyBankAccount",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "CustomerBankAccount",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "SpecialDepositAccount",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "SpecialDepositAccountApplication",
                schema: "Deposits");

            migrationBuilder.DropTable(
                name: "CustomerPaymentDocument",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "TransactionJournal",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "LoanApplication",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanTopup",
                schema: "Loans");

            migrationBuilder.DropTable(
                name: "LoanAccount",
                schema: "Loans");

            migrationBuilder.DropSequence(
                name: "Asset",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "COGS",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Deposits",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Equity",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Expense",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "General",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Income",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Liability",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Loans",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Repayments",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "Withdrawals",
                schema: "Core");
        }
    }
}
