using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4629 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualInterestAllocated",
                schema: "Loans",
                table: "LoanRepaymentSchedule",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualInterestBalance",
                schema: "Loans",
                table: "LoanRepaymentSchedule",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualPrincipalAllocated",
                schema: "Loans",
                table: "LoanRepaymentSchedule",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualPrincipalBalance",
                schema: "Loans",
                table: "LoanRepaymentSchedule",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                schema: "Deposits",
                table: "DepositProductCharge",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualInterestAllocated",
                schema: "Loans",
                table: "LoanRepaymentSchedule");

            migrationBuilder.DropColumn(
                name: "ActualInterestBalance",
                schema: "Loans",
                table: "LoanRepaymentSchedule");

            migrationBuilder.DropColumn(
                name: "ActualPrincipalAllocated",
                schema: "Loans",
                table: "LoanRepaymentSchedule");

            migrationBuilder.DropColumn(
                name: "ActualPrincipalBalance",
                schema: "Loans",
                table: "LoanRepaymentSchedule");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                schema: "Deposits",
                table: "DepositProductCharge",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
