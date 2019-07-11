using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class today : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "from_status",
                table: "TrademarkApplicationHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "to_status",
                table: "TrademarkApplicationHistory",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomFields");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "RemitaAccountSplit");

            migrationBuilder.DropTable(
                name: "RemitaBankCode");

            migrationBuilder.DropTable(
                name: "RemitaPayments");

            migrationBuilder.DropColumn(
                name: "from_status",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "to_status",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "RRR",
                table: "BillLogs");
        }
    }
}
