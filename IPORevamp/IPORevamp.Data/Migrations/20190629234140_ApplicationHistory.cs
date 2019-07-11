using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class ApplicationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "transaction_id",
                table: "TrademarkApplicationHistory",
                newName: "TransactionID");

            migrationBuilder.RenameColumn(
                name: "to_status",
                table: "TrademarkApplicationHistory",
                newName: "ToStatus");

            migrationBuilder.RenameColumn(
                name: "to_datastatus",
                table: "TrademarkApplicationHistory",
                newName: "ToDataStatus");

            migrationBuilder.RenameColumn(
                name: "from_status",
                table: "TrademarkApplicationHistory",
                newName: "FromStatus");

            migrationBuilder.RenameColumn(
                name: "from_datastatus",
                table: "TrademarkApplicationHistory",
                newName: "FromDataStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "TrademarkApplicationHistory",
                newName: "transaction_id");

            migrationBuilder.RenameColumn(
                name: "ToStatus",
                table: "TrademarkApplicationHistory",
                newName: "to_status");

            migrationBuilder.RenameColumn(
                name: "ToDataStatus",
                table: "TrademarkApplicationHistory",
                newName: "to_datastatus");

            migrationBuilder.RenameColumn(
                name: "FromStatus",
                table: "TrademarkApplicationHistory",
                newName: "from_status");

            migrationBuilder.RenameColumn(
                name: "FromDataStatus",
                table: "TrademarkApplicationHistory",
                newName: "from_datastatus");
        }
    }
}
