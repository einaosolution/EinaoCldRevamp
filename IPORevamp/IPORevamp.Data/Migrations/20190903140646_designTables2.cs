using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class designTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesignAddressOfService",
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
                    DesignApplicationID = table.Column<int>(nullable: false),
                    StateID = table.Column<int>(nullable: false),
                    AttorneyCode = table.Column<string>(nullable: true),
                    AttorneyName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignAddressOfService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignAddressOfService_DesignApplication_DesignApplicationID",
                        column: x => x.DesignApplicationID,
                        principalTable: "DesignApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignAddressOfService_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignInvention",
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
                    DesignApplicationID = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    InventorName = table.Column<string>(nullable: true),
                    InventorAddress = table.Column<string>(nullable: true),
                    InventorEmail = table.Column<string>(nullable: true),
                    InventorMobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignInvention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignInvention_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignInvention_DesignApplication_DesignApplicationID",
                        column: x => x.DesignApplicationID,
                        principalTable: "DesignApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignPriority",
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
                    DesignApplicationID = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    ApplicationNumber = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignPriority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignPriority_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignPriority_DesignApplication_DesignApplicationID",
                        column: x => x.DesignApplicationID,
                        principalTable: "DesignApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignAddressOfService_DesignApplicationID",
                table: "DesignAddressOfService",
                column: "DesignApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignAddressOfService_StateID",
                table: "DesignAddressOfService",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignInvention_CountryId",
                table: "DesignInvention",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignInvention_DesignApplicationID",
                table: "DesignInvention",
                column: "DesignApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignPriority_CountryId",
                table: "DesignPriority",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignPriority_DesignApplicationID",
                table: "DesignPriority",
                column: "DesignApplicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignAddressOfService");

            migrationBuilder.DropTable(
                name: "DesignInvention");

            migrationBuilder.DropTable(
                name: "DesignPriority");
        }
    }
}
