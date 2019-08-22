using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class patentdataresult2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "PatentDataResult");
        }
    }
}
