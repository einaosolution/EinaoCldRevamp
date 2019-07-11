using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class prelimsearchupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "PreliminarySearch",
                newName: "service");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "PreliminarySearch",
                newName: "product");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PreliminarySearch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PreliminarySearch");

            migrationBuilder.RenameColumn(
                name: "service",
                table: "PreliminarySearch",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "product",
                table: "PreliminarySearch",
                newName: "description");
        }
    }
}
