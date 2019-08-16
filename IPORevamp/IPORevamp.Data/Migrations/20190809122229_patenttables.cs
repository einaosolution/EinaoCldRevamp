using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class patenttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       

            migrationBuilder.CreateTable(
                name: "PatentApplication",
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
                    TransactionID = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: true),
                    ApplicationStatus = table.Column<string>(nullable: true),
                    DataStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentApplication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentType",
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
                    table.PrimaryKey("PK_PatentType", x => x.Id);
                });

        

            migrationBuilder.CreateTable(
                name: "PatentApplicationHistory",
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
                    patentcomment = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    PatentApplicationID = table.Column<int>(nullable: false),
                    TransactionID = table.Column<string>(nullable: true),
                    FromDataStatus = table.Column<string>(nullable: true),
                    FromStatus = table.Column<string>(nullable: true),
                    ToStatus = table.Column<string>(nullable: true),
                    ToDataStatus = table.Column<string>(nullable: true),
                    UploadsPath1 = table.Column<string>(nullable: true),
                    userid = table.Column<int>(nullable: false),
                    UploadsPath2 = table.Column<string>(nullable: true),
                    AcceptanceFilePath = table.Column<string>(nullable: true),
                    RefusalFilePath = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentApplicationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentApplicationHistory_PatentApplication_PatentApplicationID",
                        column: x => x.PatentApplicationID,
                        principalTable: "PatentApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentInformation",
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
                    PatentTypeID = table.Column<int>(nullable: false),
                    TitleOfInvention = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    InventionDescription = table.Column<string>(nullable: true),
                    LetterOfAuthorization = table.Column<string>(nullable: true),
                    Claims = table.Column<string>(nullable: true),
                    PctDocument = table.Column<string>(nullable: true),
                    DeedOfAssignment = table.Column<string>(nullable: true),
                    CompleteSpecificationForm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentInformation_PatentApplication_PatentApplicationID",
                        column: x => x.PatentApplicationID,
                        principalTable: "PatentApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentInformation_PatentType_PatentTypeID",
                        column: x => x.PatentTypeID,
                        principalTable: "PatentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatentApplicationHistory_PatentApplicationID",
                table: "PatentApplicationHistory",
                column: "PatentApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentInformation_PatentApplicationID",
                table: "PatentInformation",
                column: "PatentApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PatentInformation_PatentTypeID",
                table: "PatentInformation",
                column: "PatentTypeID");

     

     
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeMarkwallet_FeeList_feelistid",
                table: "TradeMarkwallet");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeMarkwallet_Payment_paymentid",
                table: "TradeMarkwallet");

            migrationBuilder.DropTable(
                name: "PatentApplicationHistory");

            migrationBuilder.DropTable(
                name: "PatentInformation");

            migrationBuilder.DropTable(
                name: "TrademarkComments");

            migrationBuilder.DropTable(
                name: "PatentApplication");

            migrationBuilder.DropTable(
                name: "PatentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TradeMarkwallet",
                table: "TradeMarkwallet");

            migrationBuilder.RenameTable(
                name: "TradeMarkwallet",
                newName: "Twallet");

            migrationBuilder.RenameIndex(
                name: "IX_TradeMarkwallet_paymentid",
                table: "Twallet",
                newName: "IX_Twallet_paymentid");

            migrationBuilder.RenameIndex(
                name: "IX_TradeMarkwallet_feelistid",
                table: "Twallet",
                newName: "IX_Twallet_feelistid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Twallet",
                table: "Twallet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Twallet_FeeList_feelistid",
                table: "Twallet",
                column: "feelistid",
                principalTable: "FeeList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Twallet_Payment_paymentid",
                table: "Twallet",
                column: "paymentid",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
