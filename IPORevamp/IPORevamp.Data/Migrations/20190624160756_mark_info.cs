using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class mark_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mark_Info",
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
                    reg_number = table.Column<string>(nullable: true),
                    logo_descriptionID = table.Column<string>(nullable: true),
                    nation_classID = table.Column<string>(nullable: true),
                    tm_typeID = table.Column<string>(nullable: true),
                    product_title = table.Column<string>(nullable: true),
                    nice_class = table.Column<string>(nullable: true),
                    logo_pic = table.Column<string>(nullable: true),
                    auth_doc = table.Column<string>(nullable: true),
                    sup_doc1 = table.Column<string>(nullable: true),
                    sup_doc2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark_Info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mark_Info_Pwallet_pwalletid",
                        column: x => x.pwalletid,
                        principalTable: "Pwallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mark_Info_pwalletid",
                table: "Mark_Info",
                column: "pwalletid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mark_Info");
        }
    }
}
