using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class ApplicationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tMApplicationStatuses",
                table: "tMApplicationStatuses");

            migrationBuilder.RenameTable(
                name: "tMApplicationStatuses",
                newName: "TMApplicationStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMApplicationStatus",
                table: "TMApplicationStatus",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PTApplicationStatus",
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
                    RoleId = table.Column<int>(nullable: false),
                    StatusDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PTApplicationStatus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PTApplicationStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMApplicationStatus",
                table: "TMApplicationStatus");

            migrationBuilder.RenameTable(
                name: "TMApplicationStatus",
                newName: "tMApplicationStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tMApplicationStatuses",
                table: "tMApplicationStatuses",
                column: "Id");
        }
    }
}
