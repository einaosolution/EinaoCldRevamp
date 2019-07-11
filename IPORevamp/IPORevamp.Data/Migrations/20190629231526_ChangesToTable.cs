using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class ChangesToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Info_Application_applicationId",
                table: "Mark_Info");

            migrationBuilder.DropColumn(
                name: "auth_doc",
                table: "Mark_Info");

            migrationBuilder.RenameColumn(
                name: "applicationId",
                table: "Mark_Info",
                newName: "applicationid");

            migrationBuilder.RenameColumn(
                name: "tm_typeID",
                table: "Mark_Info",
                newName: "SupportDocument2");

            migrationBuilder.RenameColumn(
                name: "sup_doc2",
                table: "Mark_Info",
                newName: "SupportDocument1");

            migrationBuilder.RenameColumn(
                name: "sup_doc1",
                table: "Mark_Info",
                newName: "RegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "reg_number",
                table: "Mark_Info",
                newName: "ProductTitle");

            migrationBuilder.RenameColumn(
                name: "pwalletid",
                table: "Mark_Info",
                newName: "TradeMarkTypeID");

            migrationBuilder.RenameColumn(
                name: "product_title",
                table: "Mark_Info",
                newName: "NiceClass");

            migrationBuilder.RenameColumn(
                name: "nice_class",
                table: "Mark_Info",
                newName: "NationClassID");

            migrationBuilder.RenameColumn(
                name: "nation_classID",
                table: "Mark_Info",
                newName: "LogoPicture");

            migrationBuilder.RenameColumn(
                name: "logo_pic",
                table: "Mark_Info",
                newName: "ApprovalDocument");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_Info_applicationId",
                table: "Mark_Info",
                newName: "IX_Mark_Info_applicationid");

            migrationBuilder.AlterColumn<int>(
                name: "applicationid",
                table: "Mark_Info",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TrademarkType",
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
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrademarkType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mark_Info_TradeMarkTypeID",
                table: "Mark_Info",
                column: "TradeMarkTypeID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Info_TrademarkType_TradeMarkTypeID",
                table: "Mark_Info");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Info_Application_applicationid",
                table: "Mark_Info");

            migrationBuilder.DropTable(
                name: "TrademarkType");

            migrationBuilder.DropIndex(
                name: "IX_Mark_Info_TradeMarkTypeID",
                table: "Mark_Info");

            migrationBuilder.RenameColumn(
                name: "applicationid",
                table: "Mark_Info",
                newName: "applicationId");

            migrationBuilder.RenameColumn(
                name: "TradeMarkTypeID",
                table: "Mark_Info",
                newName: "pwalletid");

            migrationBuilder.RenameColumn(
                name: "SupportDocument2",
                table: "Mark_Info",
                newName: "tm_typeID");

            migrationBuilder.RenameColumn(
                name: "SupportDocument1",
                table: "Mark_Info",
                newName: "sup_doc2");

            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "Mark_Info",
                newName: "sup_doc1");

            migrationBuilder.RenameColumn(
                name: "ProductTitle",
                table: "Mark_Info",
                newName: "reg_number");

            migrationBuilder.RenameColumn(
                name: "NiceClass",
                table: "Mark_Info",
                newName: "product_title");

            migrationBuilder.RenameColumn(
                name: "NationClassID",
                table: "Mark_Info",
                newName: "nice_class");

            migrationBuilder.RenameColumn(
                name: "LogoPicture",
                table: "Mark_Info",
                newName: "nation_classID");

            migrationBuilder.RenameColumn(
                name: "ApprovalDocument",
                table: "Mark_Info",
                newName: "logo_pic");

            migrationBuilder.RenameIndex(
                name: "IX_Mark_Info_applicationid",
                table: "Mark_Info",
                newName: "IX_Mark_Info_applicationId");

            migrationBuilder.AlterColumn<int>(
                name: "applicationId",
                table: "Mark_Info",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "auth_doc",
                table: "Mark_Info",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Info_Application_applicationId",
                table: "Mark_Info",
                column: "applicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
