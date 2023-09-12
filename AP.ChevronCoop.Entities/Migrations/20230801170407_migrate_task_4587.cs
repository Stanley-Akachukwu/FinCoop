using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4587 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.AlterColumn<string>(
                name: "MemberType",
                schema: "Security",
                table: "MemberBulkUploadTemp",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LoanTopupId",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId",
                schema: "Loans",
                table: "LoanAccount",
                column: "LoanTopupId",
                principalSchema: "Loans",
                principalTable: "LoanTopup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccount_LoanTopup_LoanTopupId",
                schema: "Loans",
                table: "LoanAccount");

            migrationBuilder.AlterColumn<string>(
                name: "MemberType",
                schema: "Security",
                table: "MemberBulkUploadTemp",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoanTopupId",
                schema: "Loans",
                table: "LoanAccount",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

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
    }
}
