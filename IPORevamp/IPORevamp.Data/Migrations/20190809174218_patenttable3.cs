using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class patenttable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatentInvention",
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
                    PatentApplicationID = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    InventorName = table.Column<string>(nullable: true),
                    InventorAddress = table.Column<string>(nullable: true),
                    InventorEmail = table.Column<string>(nullable: true),
                    InventorMobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentInvention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentInvention_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentInvention_PatentApplication_PatentApplicationID",
                        column: x => x.PatentApplicationID,
                        principalTable: "PatentApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatentInvention_CountryId",
                table: "PatentInvention",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentInvention_PatentApplicationID",
                table: "PatentInvention",
                column: "PatentApplicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatentInvention");
        }
    }
}
