using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class app_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "TrademarkApplicationHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "TrademarkApplicationHistory");
        }
    }
}
