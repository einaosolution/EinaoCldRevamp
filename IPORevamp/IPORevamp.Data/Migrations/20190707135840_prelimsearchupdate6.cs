using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class prelimsearchupdate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Sector_SectorId",
                table: "PreliminarySearch");

            migrationBuilder.DropColumn(
                name: "serviceid",
                table: "PreliminarySearch");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "PreliminarySearch",
                newName: "sectorid");

            migrationBuilder.RenameIndex(
                name: "IX_PreliminarySearch_SectorId",
                table: "PreliminarySearch",
                newName: "IX_PreliminarySearch_sectorid");

            migrationBuilder.AlterColumn<int>(
                name: "sectorid",
                table: "PreliminarySearch",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Sector_sectorid",
                table: "PreliminarySearch",
                column: "sectorid",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreliminarySearch_Sector_sectorid",
                table: "PreliminarySearch");

            migrationBuilder.RenameColumn(
                name: "sectorid",
                table: "PreliminarySearch",
                newName: "SectorId");

            migrationBuilder.RenameIndex(
                name: "IX_PreliminarySearch_sectorid",
                table: "PreliminarySearch",
                newName: "IX_PreliminarySearch_SectorId");

            migrationBuilder.AlterColumn<int>(
                name: "SectorId",
                table: "PreliminarySearch",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "serviceid",
                table: "PreliminarySearch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PreliminarySearch_Sector_SectorId",
                table: "PreliminarySearch",
                column: "SectorId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
