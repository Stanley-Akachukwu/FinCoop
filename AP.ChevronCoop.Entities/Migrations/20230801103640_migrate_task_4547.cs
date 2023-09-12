using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4547 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccount_LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropColumn(
                name: "LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.AlterColumn<string>(
                name: "LoanTopupId",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_LoanTopupId",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanTopupId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanTopupId",
                principalSchema: "Loans",
                principalTable: "LoanTopup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccount_LoanTopupId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.AlterColumn<string>(
                name: "LoanTopupId",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddColumn<string>(
                name: "LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanTopupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId1",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanTopupId1",
                principalSchema: "Loans",
                principalTable: "LoanTopup",
                principalColumn: "Id");
        }
    }
}
