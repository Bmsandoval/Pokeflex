using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddingIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Group_Number",
                table: "Pokemons",
                columns: new[] { "Group", "Number" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pokemons_Group_Number",
                table: "Pokemons");
        }
    }
}
