using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class PatentAssignment3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatentAssignment_Country_CountryId",
                table: "PatentAssignment");

            migrationBuilder.DropColumn(
                name: "AssigneeNationality",
                table: "PatentAssignment");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "PatentAssignment",
                newName: "AssignorNationalityId");

            migrationBuilder.RenameIndex(
                name: "IX_PatentAssignment_CountryId",
                table: "PatentAssignment",
                newName: "IX_PatentAssignment_AssignorNationalityId");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeNationalityId",
                table: "PatentAssignment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatentAssignment_AssigneeNationalityId",
                table: "PatentAssignment",
                column: "AssigneeNationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatentAssignment_Country_AssigneeNationalityId",
                table: "PatentAssignment",
                column: "AssigneeNationalityId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatentAssignment_Country_AssignorNationalityId",
                table: "PatentAssignment",
                column: "AssignorNationalityId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatentAssignment_Country_AssigneeNationalityId",
                table: "PatentAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_PatentAssignment_Country_AssignorNationalityId",
                table: "PatentAssignment");

            migrationBuilder.DropIndex(
                name: "IX_PatentAssignment_AssigneeNationalityId",
                table: "PatentAssignment");

            migrationBuilder.DropColumn(
                name: "AssigneeNationalityId",
                table: "PatentAssignment");

            migrationBuilder.RenameColumn(
                name: "AssignorNationalityId",
                table: "PatentAssignment",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_PatentAssignment_AssignorNationalityId",
                table: "PatentAssignment",
                newName: "IX_PatentAssignment_CountryId");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeNationality",
                table: "PatentAssignment",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatentAssignment_Country_CountryId",
                table: "PatentAssignment",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
