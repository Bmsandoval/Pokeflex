using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class NullableGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Groups_GroupId",
                table: "Pokemons");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Pokemons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Groups_GroupId",
                table: "Pokemons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Groups_GroupId",
                table: "Pokemons");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Groups_GroupId",
                table: "Pokemons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
