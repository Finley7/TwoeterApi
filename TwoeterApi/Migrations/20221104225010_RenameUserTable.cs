using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoeterApi.Migrations
{
    public partial class RenameUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRepository",
                table: "UserRepository");

            migrationBuilder.RenameTable(
                name: "UserRepository",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserRepository");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRepository",
                table: "UserRepository",
                column: "Id");
        }
    }
}
