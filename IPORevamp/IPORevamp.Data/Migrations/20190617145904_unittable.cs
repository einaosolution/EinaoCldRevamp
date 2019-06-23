using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class unittable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Units_DepartmentId",
                table: "Units",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Department_DepartmentId",
                table: "Units",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Department_DepartmentId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_DepartmentId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Units");
        }
    }
}
