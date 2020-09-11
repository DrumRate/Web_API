using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Factories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Factories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Factories");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Factories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
