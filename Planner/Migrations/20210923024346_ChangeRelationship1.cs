using Microsoft.EntityFrameworkCore.Migrations;

namespace Planner.Migrations
{
    public partial class ChangeRelationship1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_workitems_CreatorId",
                table: "workitems",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_workitems_userprofiles_CreatorId",
                table: "workitems",
                column: "CreatorId",
                principalTable: "userprofiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workitems_userprofiles_CreatorId",
                table: "workitems");

            migrationBuilder.DropIndex(
                name: "IX_workitems_CreatorId",
                table: "workitems");
        }
    }
}
