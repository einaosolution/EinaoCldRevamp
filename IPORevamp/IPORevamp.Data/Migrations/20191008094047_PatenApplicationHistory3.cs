using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class PatenApplicationHistory3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatentApplicationHistory_DesignApplication_DesignApplicationId",
                table: "PatentApplicationHistory");

            migrationBuilder.DropIndex(
                name: "IX_PatentApplicationHistory_DesignApplicationId",
                table: "PatentApplicationHistory");

            migrationBuilder.DropColumn(
                name: "DesignApplicationId",
                table: "PatentApplicationHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesignApplicationId",
                table: "PatentApplicationHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatentApplicationHistory_DesignApplicationId",
                table: "PatentApplicationHistory",
                column: "DesignApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatentApplicationHistory_DesignApplication_DesignApplicationId",
                table: "PatentApplicationHistory",
                column: "DesignApplicationId",
                principalTable: "DesignApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
