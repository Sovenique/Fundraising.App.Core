using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundraising.App.Core.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectStatus",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectStatus",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
