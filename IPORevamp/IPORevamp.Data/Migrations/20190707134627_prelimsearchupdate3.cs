using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class prelimsearchupdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Product_ProductId",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "product",
                table: "PreliminarySearch");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PreliminarySearch",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "service",
                table: "PreliminarySearch",
                newName: "serviceid");

            migrationBuilder.RenameIndex(
                name: "IX_PreliminarySearch_ProductId",
                table: "PreliminarySearch",
                newName: "IX_PreliminarySearch_productid");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Product_productid",
                table: "PreliminarySearch");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "PreliminarySearch",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "serviceid",
                table: "PreliminarySearch",
                newName: "service");

            migrationBuilder.RenameIndex(
                name: "IX_PreliminarySearch_productid",
                table: "PreliminarySearch",
                newName: "IX_PreliminarySearch_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "PreliminarySearch",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "product",
                table: "PreliminarySearch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Product_ProductId",
                table: "PreliminarySearch",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
