using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class patenttable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatentAssignment",
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
                    AssigneeName = table.Column<string>(nullable: true),
                    AssigneeAddress = table.Column<string>(nullable: true),
                    AssigneeNationality = table.Column<string>(nullable: true),
                    AssignorName = table.Column<string>(nullable: true),
                    AssignorAddress = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    DateOfAssignment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentAssignment_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentAssignment_PatentApplication_PatentApplicationID",
                        column: x => x.PatentApplicationID,
                        principalTable: "PatentApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentPriorityInformation",
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
                    ApplicationNumber = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentPriorityInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentPriorityInformation_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentPriorityInformation_PatentApplication_PatentApplicationID",
                        column: x => x.PatentApplicationID,
                        principalTable: "PatentApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatentAssignment_CountryId",
                table: "PatentAssignment",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentAssignment_PatentApplicationID",
                table: "PatentAssignment",
                column: "PatentApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPriorityInformation_CountryId",
                table: "PatentPriorityInformation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentPriorityInformation_PatentApplicationID",
                table: "PatentPriorityInformation",
                column: "PatentApplicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatentAssignment");

            migrationBuilder.DropTable(
                name: "PatentPriorityInformation");
        }
    }
}
