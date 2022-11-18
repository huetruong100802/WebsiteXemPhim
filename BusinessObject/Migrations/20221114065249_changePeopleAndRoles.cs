using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class changePeopleAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_People_PeopleName",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Role_RoleName",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "MoviePeople",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "PeopleName",
                table: "MoviePeople",
                newName: "PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePeople_RoleName",
                table: "MoviePeople",
                newName: "IX_MoviePeople_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePeople_PeopleName",
                table: "MoviePeople",
                newName: "IX_MoviePeople_PeopleId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Role",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "People",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CommentDetail",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_People_PeopleId",
                table: "MoviePeople",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Role_RoleId",
                table: "MoviePeople",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_People_PeopleId",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Role_RoleId",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "MoviePeople",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "MoviePeople",
                newName: "PeopleName");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePeople_RoleId",
                table: "MoviePeople",
                newName: "IX_MoviePeople_RoleName");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePeople_PeopleId",
                table: "MoviePeople",
                newName: "IX_MoviePeople_PeopleName");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "People",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CommentDetail",
                table: "Comments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_People_PeopleName",
                table: "MoviePeople",
                column: "PeopleName",
                principalTable: "People",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Role_RoleName",
                table: "MoviePeople",
                column: "RoleName",
                principalTable: "Role",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
