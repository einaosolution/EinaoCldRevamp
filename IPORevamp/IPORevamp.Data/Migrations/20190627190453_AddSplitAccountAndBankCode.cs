using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class AddSplitAccountAndBankCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RemitaPostVerifyPayLoad",
                table: "RemitaPayments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemitaResponseVerifyPayLoad",
                table: "RemitaPayments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RemitaAccountSplit",
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
                    BeneficiaryAccount = table.Column<string>(nullable: true),
                    BeneficiaryName = table.Column<string>(nullable: true),
                    BeneficiaryBank = table.Column<string>(nullable: true),
                    DeductFee = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemitaAccountSplit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RemitaBankCode",
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
                    BankName = table.Column<string>(nullable: true),
                    BankCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemitaBankCode", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemitaAccountSplit");

            migrationBuilder.DropTable(
                name: "RemitaBankCode");

            migrationBuilder.DropColumn(
                name: "RemitaPostVerifyPayLoad",
                table: "RemitaPayments");

            migrationBuilder.DropColumn(
                name: "RemitaResponseVerifyPayLoad",
                table: "RemitaPayments");
        }
    }
}
