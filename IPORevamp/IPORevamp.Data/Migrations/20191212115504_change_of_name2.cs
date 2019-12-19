﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class change_of_name2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Change_Of_Name");

            migrationBuilder.CreateTable(
                name: "ChangeOfName",
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
                    OldApplicantFirstName = table.Column<string>(nullable: true),
                    OldApplicantSurname = table.Column<string>(nullable: true),
                    NewApplicantFirstname = table.Column<string>(nullable: true),
                    NewApplicantSurname = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    PaymentReference = table.Column<string>(nullable: true),
                    PreviousApplicationStatus = table.Column<string>(nullable: true),
                    PreviousDataStatus = table.Column<string>(nullable: true),
                    applicationid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeOfName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeOfName_Application_applicationid",
                        column: x => x.applicationid,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeOfName_applicationid",
                table: "ChangeOfName",
                column: "applicationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeOfName");

            migrationBuilder.CreateTable(
                name: "Change_Of_Name",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    NewApplicantFirstname = table.Column<string>(nullable: true),
                    NewApplicantSurname = table.Column<string>(nullable: true),
                    OldApplicantFirstName = table.Column<string>(nullable: true),
                    OldApplicantSurname = table.Column<string>(nullable: true),
                    PaymentReference = table.Column<string>(nullable: true),
                    PreviousApplicationStatus = table.Column<string>(nullable: true),
                    PreviousDataStatus = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    applicationid = table.Column<int>(nullable: false),
                    userid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Change_Of_Name", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Change_Of_Name_Application_applicationid",
                        column: x => x.applicationid,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Change_Of_Name_applicationid",
                table: "Change_Of_Name",
                column: "applicationid");
        }
    }
}
