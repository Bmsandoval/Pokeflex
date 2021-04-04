using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddPokemonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flexmons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    number = table.Column<int>(type: "int", nullable: false),
                    apiSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flexmons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    number = table.Column<int>(type: "int", nullable: false),
                    apiSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flexmons");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
