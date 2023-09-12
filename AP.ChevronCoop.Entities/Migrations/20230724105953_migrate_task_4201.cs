using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4201 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyBankAccount_BankId",
                schema: "Accounting",
                table: "CompanyBankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccount_BankId",
                schema: "Accounting",
                table: "CompanyBankAccount",
                column: "BankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyBankAccount_BankId",
                schema: "Accounting",
                table: "CompanyBankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccount_BankId",
                schema: "Accounting",
                table: "CompanyBankAccount",
                column: "BankId",
                unique: true);
        }
    }
}
