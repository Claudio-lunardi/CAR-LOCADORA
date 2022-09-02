using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class ajusted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Categorias_CategoriaId",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_CategoriaId",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Locacoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_CategoriaId",
                table: "Locacoes",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Categorias_CategoriaId",
                table: "Locacoes",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
