using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class prelimsearch4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Product_productid",
                table: "PreliminarySearch");

            migrationBuilder.DropIndex(
                name: "IX_PreliminarySearch_productid",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "productid",
                table: "PreliminarySearch");

            migrationBuilder.AddColumn<string>(
                name: "product",
                table: "PreliminarySearch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product",
                table: "PreliminarySearch");

            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreliminarySearch_productid",
                table: "PreliminarySearch",
                column: "productid");

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Product_productid",
                table: "PreliminarySearch",
                column: "productid",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
