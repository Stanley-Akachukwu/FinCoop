using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4034 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                schema: "Accounting",
                table: "TransactionJournal",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "GENERAL_TRANSACTION",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "GENERAL");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultProduct",
                schema: "Deposits",
                table: "DepositProduct",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpecialDepositIncreaseDecrease",
                schema: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SpecialDepositAccountId = table.Column<string>(type: "nvarchar(40)", nullable: true),
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
                    table.PrimaryKey("PK_SpecialDepositIncreaseDecrease", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDepositIncreaseDecrease_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "Security",
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecialDepositIncreaseDecrease_SpecialDepositAccount_SpecialDepositAccountId",
                        column: x => x.SpecialDepositAccountId,
                        principalSchema: "Deposits",
                        principalTable: "SpecialDepositAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositIncreaseDecrease_ApprovalId",
                schema: "Deposits",
                table: "SpecialDepositIncreaseDecrease",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositIncreaseDecrease_SpecialDepositAccountId",
                schema: "Deposits",
                table: "SpecialDepositIncreaseDecrease",
                column: "SpecialDepositAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialDepositIncreaseDecrease",
                schema: "Deposits");

            migrationBuilder.DropColumn(
                name: "IsDefaultProduct",
                schema: "Deposits",
                table: "DepositProduct");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                schema: "Accounting",
                table: "TransactionJournal",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "GENERAL",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "GENERAL_TRANSACTION");
        }
    }
}
