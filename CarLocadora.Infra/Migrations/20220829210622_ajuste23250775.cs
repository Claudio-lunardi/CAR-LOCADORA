using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class ajuste23250775 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_VeiculosPlaca1",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "VeiculosPlaca",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "VeiculosPlaca1",
                table: "Locacoes");

            migrationBuilder.AddColumn<int>(
                name: "VeiculoPlaca",
                table: "Locacoes",
                type: "int",
                maxLength: 8,
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "VeiculosPlaca",
                table: "Locacoes",
                type: "int",
                maxLength: 8,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VeiculosPlaca1",
                table: "Locacoes",
                type: "nvarchar(8)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_VeiculosPlaca1",
                table: "Locacoes",
                column: "VeiculosPlaca1");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes",
                column: "VeiculosPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
