using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class designpublication5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_Title",
                table: "DesignPublication");

            migrationBuilder.DropColumn(
                name: "logo",
                table: "DesignPublication");
        }
    }
}
