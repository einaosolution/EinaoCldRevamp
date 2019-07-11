using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class opposition2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "NoticeOfOpposition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "NoticeOfOpposition",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "NoticeOfOpposition");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "NoticeOfOpposition");
        }
    }
}
