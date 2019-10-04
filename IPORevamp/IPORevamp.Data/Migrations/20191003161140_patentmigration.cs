using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class patentmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "migratedapplicationid",
                table: "PatentApplication",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "migratedapplicationid",
                table: "DesignApplication",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "migratedapplicationid",
                table: "PatentApplication");

            migrationBuilder.DropColumn(
                name: "migratedapplicationid",
                table: "DesignApplication");
        }
    }
}
