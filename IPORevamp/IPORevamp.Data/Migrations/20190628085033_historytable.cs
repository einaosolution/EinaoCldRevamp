using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class historytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrademarkApplicationHistory_TrademarkComments_TrademarkCommentsId",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropIndex(
                name: "IX_TrademarkApplicationHistory_TrademarkCommentsId",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "TrademarkCommentsId",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "trademarkcommentid",
                table: "TrademarkApplicationHistory");

            migrationBuilder.AddColumn<string>(
                name: "UploadsPath1",
                table: "TrademarkApplicationHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadsPath2",
                table: "TrademarkApplicationHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trademarkcomment",
                table: "TrademarkApplicationHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadsPath1",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "UploadsPath2",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "trademarkcomment",
                table: "TrademarkApplicationHistory");

            migrationBuilder.AddColumn<int>(
                name: "TrademarkCommentsId",
                table: "TrademarkApplicationHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "trademarkcommentid",
                table: "TrademarkApplicationHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrademarkApplicationHistory_TrademarkCommentsId",
                table: "TrademarkApplicationHistory",
                column: "TrademarkCommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrademarkApplicationHistory_TrademarkComments_TrademarkCommentsId",
                table: "TrademarkApplicationHistory",
                column: "TrademarkCommentsId",
                principalTable: "TrademarkComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
