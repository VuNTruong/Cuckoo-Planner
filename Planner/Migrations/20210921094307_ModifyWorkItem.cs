using Microsoft.EntityFrameworkCore.Migrations;

namespace Planner.Migrations
{
    public partial class ModifyWorkItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "creator",
                table: "workitems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creator",
                table: "workitems");
        }
    }
}
