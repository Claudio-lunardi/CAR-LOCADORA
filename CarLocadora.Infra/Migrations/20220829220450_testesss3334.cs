using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class testesss3334 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VeiculoPlaca",
                table: "Locacoes",
                type: "nvarchar(8)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_VeiculoPlaca",
                table: "Locacoes",
                column: "VeiculoPlaca");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca",
                table: "Locacoes",
                column: "VeiculoPlaca",
                principalTable: "Veiculos",
                principalColumn: "Placa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_VeiculoPlaca",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "VeiculoPlaca",
                table: "Locacoes");
        }
    }
}
