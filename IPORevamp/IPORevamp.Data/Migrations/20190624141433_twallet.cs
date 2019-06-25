using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class twallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Twallet",
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
                    transaction_reference = table.Column<string>(nullable: true),
                    cust_id = table.Column<string>(nullable: true),
                    transaction_status = table.Column<string>(nullable: true),
                    pay_ref = table.Column<string>(nullable: true),
                    paymentid = table.Column<int>(nullable: false),
                    feelistid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Twallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Twallet_FeeList_feelistid",
                        column: x => x.feelistid,
                        principalTable: "FeeList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Twallet_Payment_paymentid",
                        column: x => x.paymentid,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Twallet_feelistid",
                table: "Twallet",
                column: "feelistid");

            migrationBuilder.CreateIndex(
                name: "IX_Twallet_paymentid",
                table: "Twallet",
                column: "paymentid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Twallet");
        }
    }
}
