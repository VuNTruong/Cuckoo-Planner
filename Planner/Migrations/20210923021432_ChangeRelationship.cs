using Microsoft.EntityFrameworkCore.Migrations;

namespace Planner.Migrations
{
    public partial class ChangeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workitems_userprofiles_UserProfileId",
                table: "workitems");

            migrationBuilder.DropIndex(
                name: "IX_workitems_UserProfileId",
                table: "workitems");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "workitems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "workitems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_workitems_UserProfileId",
                table: "workitems",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_workitems_userprofiles_UserProfileId",
                table: "workitems",
                column: "UserProfileId",
                principalTable: "userprofiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
