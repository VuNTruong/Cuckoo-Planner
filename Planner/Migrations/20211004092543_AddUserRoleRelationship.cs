using Microsoft.EntityFrameworkCore.Migrations;

namespace Planner.Migrations
{
    public partial class AddUserRoleRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "workitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "workitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleDetailId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "roledetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roledetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roledetailuserprofile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    RoleDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roledetailuserprofile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roledetailuserprofile_roledetail_RoleDetailId",
                        column: x => x.RoleDetailId,
                        principalTable: "roledetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roledetailuserprofile_userprofiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "userprofiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleDetailId",
                table: "Roles",
                column: "RoleDetailId",
                unique: true,
                filter: "[RoleDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_roledetailuserprofile_RoleDetailId",
                table: "roledetailuserprofile",
                column: "RoleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_roledetailuserprofile_UserProfileId",
                table: "roledetailuserprofile",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_roledetail_RoleDetailId",
                table: "Roles",
                column: "RoleDetailId",
                principalTable: "roledetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_roledetail_RoleDetailId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "roledetailuserprofile");

            migrationBuilder.DropTable(
                name: "roledetail");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleDetailId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleDetailId",
                table: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "workitems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "workitems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
