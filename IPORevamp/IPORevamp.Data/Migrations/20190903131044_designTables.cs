using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class designTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesignApplicationId",
                table: "PatentApplicationHistory",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DesignApplication",
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
                    DataStatus = table.Column<string>(nullable: true),
                    CertificatePayReference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignApplication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignType",
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
                    table.PrimaryKey("PK_DesignType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignApplicationHistory",
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
                    DesignApplicationID = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_DesignApplicationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignApplicationHistory_DesignApplication_DesignApplicationID",
                        column: x => x.DesignApplicationID,
                        principalTable: "DesignApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignAssignment",
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
                    AssigneeName = table.Column<string>(nullable: true),
                    AssigneeAddress = table.Column<string>(nullable: true),
                    AssignorName = table.Column<string>(nullable: true),
                    AssignorAddress = table.Column<string>(nullable: true),
                    DateOfAssignment = table.Column<DateTime>(nullable: false),
                    AssigneeNationalityId = table.Column<int>(nullable: false),
                    AssignorNationalityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignAssignment_Country_AssigneeNationalityId",
                        column: x => x.AssigneeNationalityId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignAssignment_Country_AssignorNationalityId",
                        column: x => x.AssignorNationalityId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DesignAssignment_DesignApplication_DesignApplicationID",
                        column: x => x.DesignApplicationID,
                        principalTable: "DesignApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignInformation",
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
                    DesignTypeID = table.Column<int>(nullable: false),
                    NationClassID = table.Column<int>(nullable: false),
                    TitleOfDesign = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    DesignDescription = table.Column<string>(nullable: true),
                    LetterOfAuthorization = table.Column<string>(nullable: true),
                    DeedOfAssignment = table.Column<string>(nullable: true),
                    PriorityDocument = table.Column<string>(nullable: true),
                    NoveltyStatement = table.Column<string>(nullable: true),
                    RepresentationOfDesign1 = table.Column<string>(nullable: true),
                    RepresentationOfDesign2 = table.Column<string>(nullable: true),
                    RepresentationOfDesign3 = table.Column<string>(nullable: true),
                    RepresentationOfDesign4 = table.Column<string>(nullable: true),
                    NationalClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignInformation_DesignApplication_DesignApplicationID",
                        column: x => x.DesignApplicationID,
                        principalTable: "DesignApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignInformation_DesignType_DesignTypeID",
                        column: x => x.DesignTypeID,
                        principalTable: "DesignType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignInformation_NationalClass_NationalClassId",
                        column: x => x.NationalClassId,
                        principalTable: "NationalClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatentApplicationHistory_DesignApplicationId",
                table: "PatentApplicationHistory",
                column: "DesignApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignApplicationHistory_DesignApplicationID",
                table: "DesignApplicationHistory",
                column: "DesignApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignAssignment_AssigneeNationalityId",
                table: "DesignAssignment",
                column: "AssigneeNationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignAssignment_AssignorNationalityId",
                table: "DesignAssignment",
                column: "AssignorNationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignAssignment_DesignApplicationID",
                table: "DesignAssignment",
                column: "DesignApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignInformation_DesignApplicationID",
                table: "DesignInformation",
                column: "DesignApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignInformation_DesignTypeID",
                table: "DesignInformation",
                column: "DesignTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignInformation_NationalClassId",
                table: "DesignInformation",
                column: "NationalClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatentApplicationHistory_DesignApplication_DesignApplicationId",
                table: "PatentApplicationHistory",
                column: "DesignApplicationId",
                principalTable: "DesignApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatentApplicationHistory_DesignApplication_DesignApplicationId",
                table: "PatentApplicationHistory");

            migrationBuilder.DropTable(
                name: "DesignApplicationHistory");

            migrationBuilder.DropTable(
                name: "DesignAssignment");

            migrationBuilder.DropTable(
                name: "DesignInformation");

            migrationBuilder.DropTable(
                name: "DesignApplication");

            migrationBuilder.DropTable(
                name: "DesignType");

            migrationBuilder.DropIndex(
                name: "IX_PatentApplicationHistory_DesignApplicationId",
                table: "PatentApplicationHistory");

            migrationBuilder.DropColumn(
                name: "DesignApplicationId",
                table: "PatentApplicationHistory");
        }
    }
}
