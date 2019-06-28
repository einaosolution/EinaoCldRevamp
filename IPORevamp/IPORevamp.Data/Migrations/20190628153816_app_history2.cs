using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class app_history2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "TrademarkApplicationHistory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userid",
                table: "TrademarkApplicationHistory");
        }
    }
}
