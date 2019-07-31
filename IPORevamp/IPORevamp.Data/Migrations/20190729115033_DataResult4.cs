using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class DataResult4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BatCount",
                table: "DataResult",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
