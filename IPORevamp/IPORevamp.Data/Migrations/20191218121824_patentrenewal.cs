using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class patentrenewal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "RecordalDesignRenewal",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "RecordalDesignRenewal",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecordalPatentRenewal",
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
                    RenewalDueDate = table.Column<DateTime>(nullable: true),
                    RenewalType = table.Column<string>(nullable: true),
                    DetailOfRequest = table.Column<string>(nullable: true),
                    PowerOfAttorney = table.Column<string>(nullable: true),
                    CertificateOfTrademark = table.Column<string>(nullable: true),
                    PaymentReference = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    PreviousApplicationStatus = table.Column<string>(nullable: true),
                    PreviousDataStatus = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    applicationid = table.Column<int>(nullable: false),
                    PatentapplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordalPatentRenewal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordalPatentRenewal_PatentApplication_PatentapplicationId",
                        column: x => x.PatentapplicationId,
                        principalTable: "PatentApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordalPatentRenewal_PatentapplicationId",
                table: "RecordalPatentRenewal",
                column: "PatentapplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordalPatentRenewal");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "RecordalDesignRenewal");

            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "RecordalDesignRenewal");
        }
    }
}
