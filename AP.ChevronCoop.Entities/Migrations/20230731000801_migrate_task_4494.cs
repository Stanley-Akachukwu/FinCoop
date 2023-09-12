using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4494 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingPeriod_FinancialCalendar_FinancialCalendarId",
                schema: "Accounting",
                table: "AccountingPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanOffSetCharge_LoanOffset_LoanOffsetId1",
                schema: "Loans",
                table: "LoanOffSetCharge");

            migrationBuilder.DropIndex(
                name: "IX_LoanOffSetCharge_LoanOffsetId1",
                schema: "Loans",
                table: "LoanOffSetCharge");

            migrationBuilder.DropIndex(
                name: "IX_AccountingPeriod_FinancialCalendarId",
                schema: "Accounting",
                table: "AccountingPeriod");

            migrationBuilder.DropColumn(
                name: "LoanOffsetId1",
                schema: "Loans",
                table: "LoanOffSetCharge");

            migrationBuilder.DropColumn(
                name: "FinancialCalendarId",
                schema: "Accounting",
                table: "AccountingPeriod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoanOffsetId1",
                schema: "Loans",
                table: "LoanOffSetCharge",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinancialCalendarId",
                schema: "Accounting",
                table: "AccountingPeriod",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanOffSetCharge_LoanOffsetId1",
                schema: "Loans",
                table: "LoanOffSetCharge",
                column: "LoanOffsetId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriod_FinancialCalendarId",
                schema: "Accounting",
                table: "AccountingPeriod",
                column: "FinancialCalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingPeriod_FinancialCalendar_FinancialCalendarId",
                schema: "Accounting",
                table: "AccountingPeriod",
                column: "FinancialCalendarId",
                principalSchema: "Accounting",
                principalTable: "FinancialCalendar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanOffSetCharge_LoanOffset_LoanOffsetId1",
                schema: "Loans",
                table: "LoanOffSetCharge",
                column: "LoanOffsetId1",
                principalSchema: "Loans",
                principalTable: "LoanOffset",
                principalColumn: "Id");
        }
    }
}
