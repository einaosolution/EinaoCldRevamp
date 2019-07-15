using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class droptables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Info_Pwallet_pwalletid",
                table: "Mark_Info");

            migrationBuilder.DropForeignKey(
                name: "FK_TrademarkApplicationHistory_Pwallet_pwalletid",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropTable(
                name: "TrademarkComments");

            migrationBuilder.DropTable(
                name: "Pwallet");

            migrationBuilder.DropIndex(
                name: "IX_TrademarkApplicationHistory_pwalletid",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropIndex(
                name: "IX_Mark_Info_pwalletid",
                table: "Mark_Info");

            migrationBuilder.AddColumn<int>(
                name: "applicationId",
                table: "TrademarkApplicationHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "applicationId",
                table: "Mark_Info",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Application",
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
                    Applicationtypeid = table.Column<int>(nullable: false),
                    transactionid = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: true),
                    application_status = table.Column<string>(nullable: true),
                    data_status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_ApplicationType_Applicationtypeid",
                        column: x => x.Applicationtypeid,
                        principalTable: "ApplicationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrademarkApplicationHistory_applicationId",
                table: "TrademarkApplicationHistory",
                column: "applicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_Info_applicationId",
                table: "Mark_Info",
                column: "applicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_Applicationtypeid",
                table: "Application",
                column: "Applicationtypeid");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Info_Application_applicationId",
                table: "Mark_Info",
                column: "applicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrademarkApplicationHistory_Application_applicationId",
                table: "TrademarkApplicationHistory",
                column: "applicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Info_Application_applicationId",
                table: "Mark_Info");

            migrationBuilder.DropForeignKey(
                name: "FK_TrademarkApplicationHistory_Application_applicationId",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropIndex(
                name: "IX_TrademarkApplicationHistory_applicationId",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropIndex(
                name: "IX_Mark_Info_applicationId",
                table: "Mark_Info");

            migrationBuilder.DropColumn(
                name: "applicationId",
                table: "TrademarkApplicationHistory");

            migrationBuilder.DropColumn(
                name: "applicationId",
                table: "Mark_Info");

            migrationBuilder.CreateTable(
                name: "Pwallet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Applicationtypeid = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    application_status = table.Column<string>(nullable: true),
                    data_status = table.Column<string>(nullable: true),
                    transactionid = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pwallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pwallet_ApplicationType_Applicationtypeid",
                        column: x => x.Applicationtypeid,
                        principalTable: "ApplicationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrademarkComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    pwalletid = table.Column<int>(nullable: false),
                    userid = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_TrademarkApplicationHistory_pwalletid",
                table: "TrademarkApplicationHistory",
                column: "pwalletid");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_Info_pwalletid",
                table: "Mark_Info",
                column: "pwalletid");

            migrationBuilder.CreateIndex(
                name: "IX_Pwallet_Applicationtypeid",
                table: "Pwallet",
                column: "Applicationtypeid");

            migrationBuilder.CreateIndex(
                name: "IX_TrademarkComments_pwalletid",
                table: "TrademarkComments",
                column: "pwalletid");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Info_Pwallet_pwalletid",
                table: "Mark_Info",
                column: "pwalletid",
                principalTable: "Pwallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrademarkApplicationHistory_Pwallet_pwalletid",
                table: "TrademarkApplicationHistory",
                column: "pwalletid",
                principalTable: "Pwallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
