using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Factory");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Factory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Factory");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Factory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
