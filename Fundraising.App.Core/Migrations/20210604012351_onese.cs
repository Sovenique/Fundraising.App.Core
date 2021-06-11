using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundraising.App.Core.Migrations
{
    public partial class onese : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectImage",
                table: "Projects",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Projects",
                newName: "ProjectImage");
        }
    }
}
