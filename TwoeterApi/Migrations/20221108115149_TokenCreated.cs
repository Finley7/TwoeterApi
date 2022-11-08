using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoeterApi.Migrations
{
    public partial class TokenCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "Users",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "Users");
        }
    }
}
