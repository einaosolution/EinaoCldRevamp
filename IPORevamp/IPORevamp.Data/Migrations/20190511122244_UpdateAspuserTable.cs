using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class UpdateAspuserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Twitter",
                table: "AspNetUsers",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "Interests",
                table: "AspNetUsers",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Instagram",
                table: "AspNetUsers",
                newName: "Rcno");

            migrationBuilder.RenameColumn(
                name: "GooglePlus",
                table: "AspNetUsers",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "FaceBook",
                table: "AspNetUsers",
                newName: "City");

            migrationBuilder.AddColumn<bool>(
                name: "ChangePassword",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangePassword",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "AspNetUsers",
                newName: "Twitter");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "AspNetUsers",
                newName: "Interests");

            migrationBuilder.RenameColumn(
                name: "Rcno",
                table: "AspNetUsers",
                newName: "Instagram");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "AspNetUsers",
                newName: "GooglePlus");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "AspNetUsers",
                newName: "FaceBook");
        }
    }
}
