using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4867 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoanCreationType",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentAccountId",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RootParentAccountId",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_ParentAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_RootParentAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "RootParentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccount_LoanAccount_ParentAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "ParentAccountId",
                principalSchema: "Loans",
                principalTable: "LoanAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccount_LoanAccount_RootParentAccountId",
                schema: "Loans",
                table: "LoanAccount",
                column: "RootParentAccountId",
                principalSchema: "Loans",
                principalTable: "LoanAccount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanAccount_ParentAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanAccount_RootParentAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccount_ParentAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccount_RootParentAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropColumn(
                name: "LoanCreationType",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropColumn(
                name: "ParentAccountId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropColumn(
                name: "RootParentAccountId",
                schema: "Loans",
                table: "LoanAccount");
        }
    }
}
