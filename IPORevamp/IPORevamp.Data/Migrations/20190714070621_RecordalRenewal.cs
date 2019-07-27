using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class RecordalRenewal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordalRenewal",
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
                    TrademarkTitle = table.Column<string>(nullable: true),
                    ApplicantName = table.Column<string>(nullable: true),
                    ApplicantAddress = table.Column<string>(nullable: true),
                    RenewalDueDate = table.Column<DateTime>(nullable: false),
                    RenewalType = table.Column<string>(nullable: true),
                    DetailOfRequest = table.Column<string>(nullable: true),
                    PowerOfAttorney = table.Column<string>(nullable: true),
                    CertificateOfTrademark = table.Column<string>(nullable: true),
                    PaymentReference = table.Column<string>(nullable: true),
                    applicationid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordalRenewal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordalRenewal_Application_applicationid",
                        column: x => x.applicationid,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordalRenewal_applicationid",
                table: "RecordalRenewal",
                column: "applicationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordalRenewal");
        }
    }
}
