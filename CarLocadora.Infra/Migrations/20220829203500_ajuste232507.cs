using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class ajuste232507 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes");

            migrationBuilder.RenameColumn(
                name: "VeiculoPlaca1",
                table: "Locacoes",
                newName: "VeiculosPlaca1");

            migrationBuilder.RenameColumn(
                name: "VeiculoPlaca",
                table: "Locacoes",
                newName: "VeiculosPlaca");

            migrationBuilder.RenameIndex(
                name: "IX_Locacoes_VeiculoPlaca1",
                table: "Locacoes",
                newName: "IX_Locacoes_VeiculosPlaca1");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes",
                column: "VeiculosPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes");

            migrationBuilder.RenameColumn(
                name: "VeiculosPlaca1",
                table: "Locacoes",
                newName: "VeiculoPlaca1");

            migrationBuilder.RenameColumn(
                name: "VeiculosPlaca",
                table: "Locacoes",
                newName: "VeiculoPlaca");

            migrationBuilder.RenameIndex(
                name: "IX_Locacoes_VeiculosPlaca1",
                table: "Locacoes",
                newName: "IX_Locacoes_VeiculoPlaca1");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes",
                column: "VeiculoPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa");
        }
    }
}
