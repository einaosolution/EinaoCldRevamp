using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class appuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Lga_Id",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeansOfIdentification_value",
                table: "AspNetUsers",
                nullable: true);

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGAs_Country_CountryId",
                table: "LGAs");

            migrationBuilder.DropColumn(
                name: "Lga_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MeansOfIdentification_value",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "LGAs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LGAs_Country_CountryId",
                table: "LGAs",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
