using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPORevamp.Data.Migrations
{
    public partial class DataResult2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DataResult",
                table: "DataResult");

            migrationBuilder.AlterColumn<string>(
                name: "sn",
                table: "DataResult",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "DataId",
                table: "DataResult",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataResult",
                table: "DataResult",
                column: "DataId");
        }
    }
}
