using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class ChangesToTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Info_TrademarkType_TradeMarkTypeID",
                table: "Mark_Info");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Info_Application_applicationid",
                table: "Mark_Info");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mark_Info",
                table: "Mark_Info");

            migrationBuilder.RenameTable(
                name: "Mark_Info",
                newName: "MarkInformation");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_Info_applicationid",
                table: "MarkInformation",
                newName: "IX_MarkInformation_applicationid");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_Info_TradeMarkTypeID",
                table: "MarkInformation",
                newName: "IX_MarkInformation_TradeMarkTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkInformation",
                table: "MarkInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkInformation_TrademarkType_TradeMarkTypeID",
                table: "MarkInformation",
                column: "TradeMarkTypeID",
                principalTable: "TrademarkType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkInformation_Application_applicationid",
                table: "MarkInformation",
                column: "applicationid",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkInformation_TrademarkType_TradeMarkTypeID",
                table: "MarkInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkInformation_Application_applicationid",
                table: "MarkInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkInformation",
                table: "MarkInformation");

            migrationBuilder.RenameTable(
                name: "MarkInformation",
                newName: "Mark_Info");

            migrationBuilder.RenameIndex(
                name: "IX_MarkInformation_applicationid",
                table: "Mark_Info",
                newName: "IX_Mark_Info_applicationid");

            migrationBuilder.RenameIndex(
                name: "IX_MarkInformation_TradeMarkTypeID",
                table: "Mark_Info",
                newName: "IX_Mark_Info_TradeMarkTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mark_Info",
                table: "Mark_Info",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Info_TrademarkType_TradeMarkTypeID",
                table: "Mark_Info",
                column: "TradeMarkTypeID",
                principalTable: "TrademarkType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Info_Application_applicationid",
                table: "Mark_Info",
                column: "applicationid",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
