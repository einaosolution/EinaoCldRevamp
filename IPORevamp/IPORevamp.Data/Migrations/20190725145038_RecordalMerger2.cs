using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class RecordalMerger2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousApplicationStatus",
                table: "RecordalRenewal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousDataStatus",
                table: "RecordalRenewal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentReference",
                table: "RecordalMerger",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousApplicationStatus",
                table: "RecordalMerger",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousDataStatus",
                table: "RecordalMerger",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousApplicationStatus",
                table: "RecordalRenewal");

            migrationBuilder.DropColumn(
                name: "PreviousDataStatus",
                table: "RecordalRenewal");

            migrationBuilder.DropColumn(
                name: "PaymentReference",
                table: "RecordalMerger");

            migrationBuilder.DropColumn(
                name: "PreviousApplicationStatus",
                table: "RecordalMerger");

            migrationBuilder.DropColumn(
                name: "PreviousDataStatus",
                table: "RecordalMerger");
        }
    }
}
