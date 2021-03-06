﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class RecordalMerger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordalMerger",
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
                    AssignorName = table.Column<string>(nullable: true),
                    AssignorAddress = table.Column<string>(nullable: true),
                    AssigneeName = table.Column<string>(nullable: true),
                    AssigneeAddress = table.Column<string>(nullable: true),
                    applicationid = table.Column<int>(nullable: false),
                    AssigneeNationality = table.Column<int>(nullable: false),
                    DateOfAssignment = table.Column<string>(nullable: true),
                    DetailOfRequest = table.Column<string>(nullable: true),
                    PowerOfAttorney = table.Column<string>(nullable: true),
                    DeedOfAssigment = table.Column<string>(nullable: true),
                    Certificate = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordalMerger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordalMerger_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordalMerger_Application_applicationid",
                        column: x => x.applicationid,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordalMerger_CountryId",
                table: "RecordalMerger",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordalMerger_applicationid",
                table: "RecordalMerger",
                column: "applicationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordalMerger");
        }
    }
}
