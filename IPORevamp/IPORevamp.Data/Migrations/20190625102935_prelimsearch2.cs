using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class prelimsearch2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "payment_reference",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "PreliminarySearch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "payment_reference",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "status",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "type",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "PreliminarySearch");
        }
    }
}
