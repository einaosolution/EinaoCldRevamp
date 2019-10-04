using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class migrateusers5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "migrateduserid",
                table: "MigratedUsers",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "migrateduserid",
                table: "MigratedUsers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
