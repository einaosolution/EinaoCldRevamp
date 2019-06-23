using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class tempuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ministry",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "staffid",
                table: "UserVerificationTemp",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "ministry",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "staffid",
                table: "UserVerificationTemp");
        }
    }
}
