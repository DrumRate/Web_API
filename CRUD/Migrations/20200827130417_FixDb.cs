using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD.Migrations
{
    public partial class FixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    StorageValue = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    ResponsibleOperators = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Factories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Московский нефтеперерабатывающий завод", "МНПЗ" });

            migrationBuilder.InsertData(
                table: "Factories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Омский нефтеперерабатывающий завод", "ОНПЗ" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "FactoryId", "Name" },
                values: new object[] { 1, 1, "ГФУ-1" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "FactoryId", "Name" },
                values: new object[] { 2, 1, "ГФУ-2" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "FactoryId", "Name" },
                values: new object[] { 3, 2, "АВТ-6" });

            migrationBuilder.InsertData(
                table: "Tanks",
                columns: new[] { "Id", "MaxVolume", "Name", "UnitId", "Volume" },
                values: new object[,]
                {
                    { 1, 2000f, "Резервуар 1", 1, 1500f },
                    { 2, 3000f, "Резервуар 2", 1, 2500f },
                    { 3, 3000f, "Дополнительный резервуар 24", 2, 3000f },
                    { 4, 3000f, "Резервуар 35", 2, 3000f },
                    { 5, 5000f, "Резервуар 47", 2, 4000f },
                    { 6, 500f, "Резервуар 256", 3, 500f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
