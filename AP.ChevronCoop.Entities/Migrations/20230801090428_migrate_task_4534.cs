using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.ChevronCoop.Entities.Migrations
{
    /// <inheritdoc />
    public partial class migrate_task_4534 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemberType",
                schema: "Security",
                table: "MemberBulkUploadTemp",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "REGULAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberType",
                schema: "Security",
                table: "MemberBulkUploadTemp");
        }
    }
}
