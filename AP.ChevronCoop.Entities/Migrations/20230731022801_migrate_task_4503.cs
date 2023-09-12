using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4503 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedDepositInterestScheduleItem_FixedDepositInterestSchedule_FixedDepositInterestScheduleId1",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTopupCharge_LoanTopup_LoanTopupId1",
                schema: "Loans",
                table: "LoanTopupCharge");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDepositInterestScheduleItem_SpecialDepositInterestSchedule_SpecialDepositInterestScheduleId1",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem");

            migrationBuilder.DropIndex(
                name: "IX_SpecialDepositInterestScheduleItem_SpecialDepositInterestScheduleId1",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem");

            migrationBuilder.DropIndex(
                name: "IX_LoanTopupCharge_LoanTopupId1",
                schema: "Loans",
                table: "LoanTopupCharge");

            migrationBuilder.DropIndex(
                name: "IX_FixedDepositInterestScheduleItem_FixedDepositInterestScheduleId1",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem");

            migrationBuilder.DropColumn(
                name: "SpecialDepositInterestScheduleId1",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem");

            migrationBuilder.DropColumn(
                name: "LoanTopupId1",
                schema: "Loans",
                table: "LoanTopupCharge");

            migrationBuilder.DropColumn(
                name: "FixedDepositInterestScheduleId1",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecialDepositInterestScheduleId1",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoanTopupId1",
                schema: "Loans",
                table: "LoanTopupCharge",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FixedDepositInterestScheduleId1",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDepositInterestScheduleItem_SpecialDepositInterestScheduleId1",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem",
                column: "SpecialDepositInterestScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTopupCharge_LoanTopupId1",
                schema: "Loans",
                table: "LoanTopupCharge",
                column: "LoanTopupId1");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositInterestScheduleItem_FixedDepositInterestScheduleId1",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem",
                column: "FixedDepositInterestScheduleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedDepositInterestScheduleItem_FixedDepositInterestSchedule_FixedDepositInterestScheduleId1",
                schema: "Deposits",
                table: "FixedDepositInterestScheduleItem",
                column: "FixedDepositInterestScheduleId1",
                principalSchema: "Deposits",
                principalTable: "FixedDepositInterestSchedule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanTopupCharge_LoanTopup_LoanTopupId1",
                schema: "Loans",
                table: "LoanTopupCharge",
                column: "LoanTopupId1",
                principalSchema: "Loans",
                principalTable: "LoanTopup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialDepositInterestScheduleItem_SpecialDepositInterestSchedule_SpecialDepositInterestScheduleId1",
                schema: "Deposits",
                table: "SpecialDepositInterestScheduleItem",
                column: "SpecialDepositInterestScheduleId1",
                principalSchema: "Deposits",
                principalTable: "SpecialDepositInterestSchedule",
                principalColumn: "Id");
        }
    }
}
