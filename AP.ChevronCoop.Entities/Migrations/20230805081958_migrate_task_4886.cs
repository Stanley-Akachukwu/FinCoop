using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4886 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalGroupMember_ApprovalGroup_ApprovalGroupId1",
                schema: "Security",
                table: "ApprovalGroupMember");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalGroupMember_ApprovalGroupId1",
                schema: "Security",
                table: "ApprovalGroupMember");

            migrationBuilder.DropColumn(
                name: "ApprovalGroupId1",
                schema: "Security",
                table: "ApprovalGroupMember");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalGroupId1",
                schema: "Security",
                table: "ApprovalGroupMember",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalGroupMember_ApprovalGroupId1",
                schema: "Security",
                table: "ApprovalGroupMember",
                column: "ApprovalGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalGroupMember_ApprovalGroup_ApprovalGroupId1",
                schema: "Security",
                table: "ApprovalGroupMember",
                column: "ApprovalGroupId1",
                principalSchema: "Security",
                principalTable: "ApprovalGroup",
                principalColumn: "Id");
        }
    }
}
