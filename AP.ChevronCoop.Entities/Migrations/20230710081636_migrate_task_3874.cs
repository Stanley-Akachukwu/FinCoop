using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_3874 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_Bank_BankId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_Customer_CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_LedgerAccount_LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.AlterColumn<string>(
                name: "LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankId",
                schema: "Customer",
                table: "CustomerBankAccount",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBankAccount_Bank_BankId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "BankId",
                principalSchema: "MasterData",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBankAccount_Customer_CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "CustomerId",
                principalSchema: "Customer",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBankAccount_LedgerAccount_LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "LedgerAccountId",
                principalSchema: "Accounting",
                principalTable: "LedgerAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_Bank_BankId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_Customer_CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBankAccount_LedgerAccount_LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount");

            migrationBuilder.AlterColumn<string>(
                name: "LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount",
                type: "nvarchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount",
                type: "nvarchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "BankId",
                schema: "Customer",
                table: "CustomerBankAccount",
                type: "nvarchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBankAccount_Bank_BankId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "BankId",
                principalSchema: "MasterData",
                principalTable: "Bank",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBankAccount_Customer_CustomerId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "CustomerId",
                principalSchema: "Customer",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBankAccount_LedgerAccount_LedgerAccountId",
                schema: "Customer",
                table: "CustomerBankAccount",
                column: "LedgerAccountId",
                principalSchema: "Accounting",
                principalTable: "LedgerAccount",
                principalColumn: "Id");
        }
    }
}
