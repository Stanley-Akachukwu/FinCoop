using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4568 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MemberType",
                schema: "Security",
                table: "MemberBulkUploadTemp",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "REGULAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MemberType",
                schema: "Security",
                table: "MemberBulkUploadTemp",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "REGULAR",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
