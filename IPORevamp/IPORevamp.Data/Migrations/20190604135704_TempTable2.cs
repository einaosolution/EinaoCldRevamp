using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class TempTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "auth",
                table: "UserVerificationTemp",
                newName: "Unit");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postal",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "UserVerificationTemp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "UserVerificationTemp",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "Postal",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "State",
                table: "UserVerificationTemp");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "UserVerificationTemp");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "UserVerificationTemp",
                newName: "auth");
        }
    }
}
