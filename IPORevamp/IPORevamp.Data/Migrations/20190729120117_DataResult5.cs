using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class DataResult5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "renewalid",
                table: "DataResult",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextrenewalDate",
                table: "DataResult",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
