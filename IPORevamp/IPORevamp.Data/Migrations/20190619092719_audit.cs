using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordAfter",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordBefore",
                table: "AuditTrails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "RecordAfter",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "RecordBefore",
                table: "AuditTrails");
        }
    }
}
