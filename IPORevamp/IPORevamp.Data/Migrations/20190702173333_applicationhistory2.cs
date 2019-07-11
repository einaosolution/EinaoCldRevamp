using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class applicationhistory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcceptanceFilePath",
                table: "TrademarkApplicationHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefusalFilePath",
                table: "TrademarkApplicationHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptanceFilePath",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "RefusalFilePath",
                table: "TrademarkApplicationHistory");
        }
    }
}
