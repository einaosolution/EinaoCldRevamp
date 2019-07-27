using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class Recordalrenewal2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RecordalRenewal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "RecordalRenewal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RecordalRenewal");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "RecordalRenewal");
        }
    }
}
