using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4300 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepaymentSchedules",
                schema: "Loans",
                table: "LoanOffset",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepaymentSchedules",
                schema: "Loans",
                table: "LoanOffset");
        }
    }
}
