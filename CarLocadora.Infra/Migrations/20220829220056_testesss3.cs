using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class testesss3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_VeiculoPlaca1",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "VeiculoPlaca",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "VeiculoPlaca1",
                table: "Locacoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VeiculoPlaca",
                table: "Locacoes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VeiculoPlaca1",
                table: "Locacoes",
                type: "nvarchar(8)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_VeiculoPlaca1",
                table: "Locacoes",
                column: "VeiculoPlaca1");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes",
                column: "VeiculoPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa");
        }
    }
}
