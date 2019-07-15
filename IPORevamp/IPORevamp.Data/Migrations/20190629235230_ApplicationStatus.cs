using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class ApplicationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "transactionid",
                table: "Application",
                newName: "TransactionID");

            migrationBuilder.RenameColumn(
                name: "data_status",
                table: "Application",
                newName: "DataStatus");

            migrationBuilder.RenameColumn(
                name: "application_status",
                table: "Application",
                newName: "ApplicationStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Application",
                newName: "transactionid");

            migrationBuilder.RenameColumn(
                name: "DataStatus",
                table: "Application",
                newName: "data_status");

            migrationBuilder.RenameColumn(
                name: "ApplicationStatus",
                table: "Application",
                newName: "application_status");
        }
    }
}
