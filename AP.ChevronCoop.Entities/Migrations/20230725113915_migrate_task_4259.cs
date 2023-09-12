using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4259 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                schema: "Payroll",
                table: "PayrollCronJobConfig",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "TotalCount",
                schema: "Payroll",
                table: "PayrollCronJobConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ChargeType",
                schema: "Deposits",
                table: "DepositProductCharge",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                schema: "Payroll",
                table: "PayrollCronJobConfig");

            migrationBuilder.DropColumn(
                name: "TotalCount",
                schema: "Payroll",
                table: "PayrollCronJobConfig");

            migrationBuilder.DropColumn(
                name: "ChargeType",
                schema: "Deposits",
                table: "DepositProductCharge");
        }
    }
}
