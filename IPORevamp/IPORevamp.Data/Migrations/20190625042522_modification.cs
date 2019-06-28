using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class modification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "transaction_reference",
                table: "Twallet",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "cust_id",
                table: "Twallet",
                newName: "transactionid");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "TrademarkComments",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "transaction_id",
                table: "Pwallet",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "cust_id",
                table: "Pwallet",
                newName: "transactionid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Twallet",
                newName: "transaction_reference");

            migrationBuilder.RenameColumn(
                name: "transactionid",
                table: "Twallet",
                newName: "cust_id");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "TrademarkComments",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Pwallet",
                newName: "transaction_id");

            migrationBuilder.RenameColumn(
                name: "transactionid",
                table: "Pwallet",
                newName: "cust_id");
        }
    }
}
