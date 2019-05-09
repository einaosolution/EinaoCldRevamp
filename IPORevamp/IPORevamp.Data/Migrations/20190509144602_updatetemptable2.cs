using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class updatetemptable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VerificationId",
                table: "UserVerificationTemp",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "UserVerificationTemp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserVerificationTemp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserVerificationTemp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "UserVerificationTemp",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserVerificationTemp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserVerificationTemp",
                newName: "VerificationId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "UserVerificationTemp",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
