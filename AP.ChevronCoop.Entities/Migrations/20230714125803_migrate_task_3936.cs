using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_3936 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDocument_TransactionJournal_TransactionJournalId1",
                schema: "Accounting",
                table: "TransactionDocument");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDocument_TransactionJournalId1",
                schema: "Accounting",
                table: "TransactionDocument");

            migrationBuilder.DropColumn(
                name: "TransactionJournalId1",
                schema: "Accounting",
                table: "TransactionDocument");

            migrationBuilder.AddColumn<string>(
                name: "ApprovalViewModelPayload",
                schema: "Security",
                table: "Approval",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalViewModelPayload",
                schema: "Security",
                table: "Approval");

            migrationBuilder.AddColumn<string>(
                name: "TransactionJournalId1",
                schema: "Accounting",
                table: "TransactionDocument",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocument_TransactionJournalId1",
                schema: "Accounting",
                table: "TransactionDocument",
                column: "TransactionJournalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDocument_TransactionJournal_TransactionJournalId1",
                schema: "Accounting",
                table: "TransactionDocument",
                column: "TransactionJournalId1",
                principalSchema: "Accounting",
                principalTable: "TransactionJournal",
                principalColumn: "Id");
        }
    }
}
