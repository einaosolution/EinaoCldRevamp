using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class prelimsearchupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "service",
                table: "PreliminarySearch",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "product",
                table: "PreliminarySearch",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "PreliminarySearch",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreliminarySearch_ProductId",
                table: "PreliminarySearch",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PreliminarySearch_SectorId",
                table: "PreliminarySearch",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Product_ProductId",
                table: "PreliminarySearch",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Sector_SectorId",
                table: "PreliminarySearch",
                column: "SectorId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Product_ProductId",
                table: "PreliminarySearch");

            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Sector_SectorId",
                table: "PreliminarySearch");

            migrationBuilder.DropIndex(
                name: "IX_PreliminarySearch_ProductId",
                table: "PreliminarySearch");

            migrationBuilder.DropIndex(
                name: "IX_PreliminarySearch_SectorId",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "PreliminarySearch");

            migrationBuilder.AlterColumn<string>(
                name: "service",
                table: "PreliminarySearch",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "product",
                table: "PreliminarySearch",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
