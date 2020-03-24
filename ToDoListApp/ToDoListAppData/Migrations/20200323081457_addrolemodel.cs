using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoListAppData.Migrations
{
    public partial class addrolemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDate",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "AspNetRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "AspNetRoles");
        }
    }
}
