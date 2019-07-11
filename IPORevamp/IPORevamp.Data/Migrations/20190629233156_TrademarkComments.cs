using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class TrademarkComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrademarkApplicationHistory_Application_applicationId",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "pwalletid",
                table: "TrademarkApplicationHistory");

            migrationBuilder.RenameColumn(
                name: "applicationId",
                table: "TrademarkApplicationHistory",
                newName: "ApplicationID");

            migrationBuilder.RenameIndex(
                name: "IX_TrademarkApplicationHistory_applicationId",
                table: "TrademarkApplicationHistory",
                newName: "IX_TrademarkApplicationHistory_ApplicationID");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationID",
                table: "TrademarkApplicationHistory",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrademarkApplicationHistory_Application_ApplicationID",
                table: "TrademarkApplicationHistory",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrademarkApplicationHistory_Application_ApplicationID",
                table: "TrademarkApplicationHistory");

            migrationBuilder.RenameColumn(
                name: "ApplicationID",
                table: "TrademarkApplicationHistory",
                newName: "applicationId");

            migrationBuilder.RenameIndex(
                name: "IX_TrademarkApplicationHistory_ApplicationID",
                table: "TrademarkApplicationHistory",
                newName: "IX_TrademarkApplicationHistory_applicationId");

            migrationBuilder.AlterColumn<int>(
                name: "applicationId",
                table: "TrademarkApplicationHistory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "pwalletid",
                table: "TrademarkApplicationHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TrademarkApplicationHistory_Application_applicationId",
                table: "TrademarkApplicationHistory",
                column: "applicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
