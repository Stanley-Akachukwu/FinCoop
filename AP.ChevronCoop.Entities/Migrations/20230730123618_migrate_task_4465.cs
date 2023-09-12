using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4465 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LedgerAccount_InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDisbursement_LoanAccount_LoanAccountId1",
                schema: "Loans",
                table: "LoanDisbursement");

            migrationBuilder.DropIndex(
                name: "IX_LoanDisbursement_LoanAccountId1",
                schema: "Loans",
                table: "LoanDisbursement");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccount_InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropColumn(
                name: "LoanAccountId1",
                schema: "Loans",
                table: "LoanDisbursement");

            migrationBuilder.DropColumn(
                name: "InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                schema: "Loans",
                table: "LoanDisbursement",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                schema: "Loans",
                table: "LoanDisbursement");

            migrationBuilder.AddColumn<string>(
                name: "LoanAccountId1",
                schema: "Loans",
                table: "LoanDisbursement",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDisbursement_LoanAccountId1",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "LoanAccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "InterestEarnedAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccount_LedgerAccount_InterestEarnedAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "InterestEarnedAccountId",
                principalSchema: "Accounting",
                principalTable: "LedgerAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDisbursement_LoanAccount_LoanAccountId1",
                schema: "Loans",
                table: "LoanDisbursement",
                column: "LoanAccountId1",
                principalSchema: "Loans",
                principalTable: "LoanAccount",
                principalColumn: "Id");
        }
    }
}
