using Microsoft.EntityFrameworkCore.Migrations;

namespace Planner.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workitems_userprofiles_Creator",
                table: "workitems");

            migrationBuilder.DropIndex(
                name: "IX_workitems_Creator",
                table: "workitems");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "workitems",
                newName: "CreatorId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "workitems",
                newName: "Creator");

            migrationBuilder.CreateIndex(
                name: "IX_workitems_Creator",
                table: "workitems",
                column: "Creator");

            migrationBuilder.AddForeignKey(
                name: "FK_workitems_userprofiles_Creator",
                table: "workitems",
                column: "Creator",
                principalTable: "userprofiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
