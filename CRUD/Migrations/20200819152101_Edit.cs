using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD.Migrations
{
    public partial class Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tank",
                table: "Tanks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factory",
                table: "Factories");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "Tanks",
                newName: "Tanks");

            migrationBuilder.RenameTable(
                name: "Factories",
                newName: "Factories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tanks",
                table: "Tanks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factories",
                table: "Factories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Units_FactoryId",
                table: "Units",
                column: "FactoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Factories_FactoryId",
                table: "Units",
                column: "FactoryId",
                principalTable: "Factories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Factories_FactoryId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_FactoryId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tanks",
                table: "Tanks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factories",
                table: "Factories");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "Tanks",
                newName: "Tanks");

            migrationBuilder.RenameTable(
                name: "Factories",
                newName: "Factory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tank",
                table: "Tanks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factory",
                table: "Factories",
                column: "Id");
        }
    }
}
