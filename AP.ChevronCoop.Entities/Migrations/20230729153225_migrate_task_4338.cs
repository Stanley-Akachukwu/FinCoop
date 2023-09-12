using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4338 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMatured",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LiquidationDate",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MaturityDate",
                schema: "Deposits",
                table: "FixedDepositLiquidation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RootParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_ParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedDepositAccount_RootParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "RootParentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedDepositAccount_FixedDepositAccount_ParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "ParentAccountId",
                principalSchema: "Deposits",
                principalTable: "FixedDepositAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedDepositAccount_FixedDepositAccount_RootParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount",
                column: "RootParentAccountId",
                principalSchema: "Deposits",
                principalTable: "FixedDepositAccount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedDepositAccount_FixedDepositAccount_ParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_FixedDepositAccount_FixedDepositAccount_RootParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount");

            migrationBuilder.DropIndex(
                name: "IX_FixedDepositAccount_ParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount");

            migrationBuilder.DropIndex(
                name: "IX_FixedDepositAccount_RootParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount");

            migrationBuilder.DropColumn(
                name: "IsMatured",
                schema: "Deposits",
                table: "FixedDepositLiquidation");

            migrationBuilder.DropColumn(
                name: "LiquidationDate",
                schema: "Deposits",
                table: "FixedDepositLiquidation");

            migrationBuilder.DropColumn(
                name: "MaturityDate",
                schema: "Deposits",
                table: "FixedDepositLiquidation");

            migrationBuilder.DropColumn(
                name: "ParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount");

            migrationBuilder.DropColumn(
                name: "RootParentAccountId",
                schema: "Deposits",
                table: "FixedDepositAccount");
        }
    }
}
