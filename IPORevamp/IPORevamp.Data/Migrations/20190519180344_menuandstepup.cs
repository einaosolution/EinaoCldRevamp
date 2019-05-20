using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class menuandstepup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGAs_States_StateId",
                table: "LGAs");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Country_CountryId",
                table: "States");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "States",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "LGAs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "LGAs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RolesId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuManager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleManager",
                columns: table => new
                {
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkRolesMenus",
                columns: table => new
                {
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RolesId = table.Column<int>(nullable: false),
                    MenusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkRolesMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkRolesMenus_MenuManager_MenusId",
                        column: x => x.MenusId,
                        principalTable: "MenuManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkRolesMenus_RoleManager_RolesId",
                        column: x => x.RolesId,
                        principalTable: "RoleManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LGAs_CountryId",
                table: "LGAs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RolesId",
                table: "AspNetUsers",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkRolesMenus_MenusId",
                table: "LinkRolesMenus",
                column: "MenusId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkRolesMenus_RolesId",
                table: "LinkRolesMenus",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RoleManager_RolesId",
                table: "AspNetUsers",
                column: "RolesId",
                principalTable: "RoleManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAs_Country_CountryId",
                table: "LGAs",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAs_States_StateId",
                table: "LGAs",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Country_CountryId",
                table: "States",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RoleManager_RolesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAs_Country_CountryId",
                table: "LGAs");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAs_States_StateId",
                table: "LGAs");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Country_CountryId",
                table: "States");

            migrationBuilder.DropTable(
                name: "LinkRolesMenus");

            migrationBuilder.DropTable(
                name: "MenuManager");

            migrationBuilder.DropTable(
                name: "RoleManager");

            migrationBuilder.DropIndex(
                name: "IX_LGAs_CountryId",
                table: "LGAs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RolesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "LGAs");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "States",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "LGAs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LGAs_States_StateId",
                table: "LGAs",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Country_CountryId",
                table: "States",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
