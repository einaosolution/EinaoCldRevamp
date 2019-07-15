using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class prelimsearchupdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Product_productid",
                table: "PreliminarySearch");

            migrationBuilder.AlterColumn<int>(
                name: "productid",
                table: "PreliminarySearch",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Product_productid",
                table: "PreliminarySearch",
                column: "productid",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Product_productid",
                table: "PreliminarySearch");

            migrationBuilder.AlterColumn<int>(
                name: "productid",
                table: "PreliminarySearch",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Product_productid",
                table: "PreliminarySearch",
                column: "productid",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
