using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class previouscomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreviousComments");
        }
    }
}
