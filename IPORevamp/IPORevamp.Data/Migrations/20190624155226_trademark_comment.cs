using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class trademark_comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrademarkComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    pwalletid = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrademarkComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrademarkComments_Pwallet_pwalletid",
                        column: x => x.pwalletid,
                        principalTable: "Pwallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrademarkApplicationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    trademarkcommentid = table.Column<int>(nullable: false),
                    pwalletid = table.Column<int>(nullable: false),
                    transaction_id = table.Column<string>(nullable: true),
                    from_datastatus = table.Column<string>(nullable: true),
                    to_datastatus = table.Column<string>(nullable: true),
                    TrademarkCommentsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrademarkApplicationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrademarkApplicationHistory_TrademarkComments_TrademarkCommentsId",
                        column: x => x.TrademarkCommentsId,
                        principalTable: "TrademarkComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrademarkApplicationHistory_Pwallet_pwalletid",
                        column: x => x.pwalletid,
                        principalTable: "Pwallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrademarkApplicationHistory_TrademarkCommentsId",
                table: "TrademarkApplicationHistory",
                column: "TrademarkCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrademarkApplicationHistory_pwalletid",
                table: "TrademarkApplicationHistory",
                column: "pwalletid");

            migrationBuilder.CreateIndex(
                name: "IX_TrademarkComments_pwalletid",
                table: "TrademarkComments",
                column: "pwalletid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrademarkApplicationHistory");

            migrationBuilder.DropTable(
                name: "TrademarkComments");
        }
    }
}
