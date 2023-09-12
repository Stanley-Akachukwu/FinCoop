using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_3980 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberBulkUploadSessionId",
                schema: "Security",
                table: "MemberBulkUploadTemp",
                newName: "SessionId");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                schema: "Security",
                table: "MemberBulkUploadSession",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSequence",
                schema: "Security",
                table: "Approval",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                schema: "Security",
                table: "MemberBulkUploadSession");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                schema: "Security",
                table: "MemberBulkUploadTemp",
                newName: "MemberBulkUploadSessionId");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSequence",
                schema: "Security",
                table: "Approval",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
