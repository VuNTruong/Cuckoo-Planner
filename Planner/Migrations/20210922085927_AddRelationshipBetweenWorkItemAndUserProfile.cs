using Microsoft.EntityFrameworkCore.Migrations;

namespace Planner.Migrations
{
    public partial class AddRelationshipBetweenWorkItemAndUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "workitems",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "doneStatus",
                table: "workitems",
                newName: "DoneStatus");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "workitems",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "creator",
                table: "workitems",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "workitems",
                newName: "Content");

            migrationBuilder.AlterColumn<int>(
                name: "Creator",
                table: "workitems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workitems_userprofiles_Creator",
                table: "workitems");

            migrationBuilder.DropIndex(
                name: "IX_workitems_Creator",
                table: "workitems");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "workitems",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "DoneStatus",
                table: "workitems",
                newName: "doneStatus");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "workitems",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "workitems",
                newName: "creator");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "workitems",
                newName: "content");

            migrationBuilder.AlterColumn<string>(
                name: "creator",
                table: "workitems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
