using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class ajuste2325 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes");

            migrationBuilder.AlterColumn<string>(
                name: "VeiculoPlaca1",
                table: "Locacoes",
                type: "nvarchar(8)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes",
                column: "VeiculoPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes");

            migrationBuilder.AlterColumn<string>(
                name: "VeiculoPlaca1",
                table: "Locacoes",
                type: "nvarchar(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                table: "Locacoes",
                column: "VeiculoPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa");
        }
    }
}
