using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class addMenucolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MenuManager",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "MenuManager",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "MenuManager",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MenuManager",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MenuManager",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "MenuManager",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "MenuManager",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MenuManager",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MenuManager");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "MenuManager");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MenuManager");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MenuManager");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MenuManager");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "MenuManager");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "MenuManager");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MenuManager");
        }
    }
}
