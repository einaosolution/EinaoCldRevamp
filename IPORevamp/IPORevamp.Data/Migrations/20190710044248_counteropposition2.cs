using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class counteropposition2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "CounterOpposition",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CounterOpposition_ApplicationId",
                table: "CounterOpposition",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CounterOpposition_Application_ApplicationId",
                table: "CounterOpposition",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CounterOpposition_Application_ApplicationId",
                table: "CounterOpposition");

            migrationBuilder.DropIndex(
                name: "IX_CounterOpposition_ApplicationId",
                table: "CounterOpposition");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "CounterOpposition");
        }
    }
}
