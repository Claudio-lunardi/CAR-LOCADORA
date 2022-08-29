using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class ajuste2325077 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes");

            migrationBuilder.AlterColumn<string>(
                name: "VeiculosPlaca1",
                table: "Locacoes",
                type: "nvarchar(8)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VeiculosPlaca",
                table: "Locacoes",
                type: "int",
                maxLength: 8,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes",
                column: "VeiculosPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes");

            migrationBuilder.AlterColumn<string>(
                name: "VeiculosPlaca1",
                table: "Locacoes",
                type: "nvarchar(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)");

            migrationBuilder.AlterColumn<int>(
                name: "VeiculosPlaca",
                table: "Locacoes",
                type: "int",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 8);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Veiculos_VeiculosPlaca1",
                table: "Locacoes",
                column: "VeiculosPlaca1",
                principalTable: "Veiculos",
                principalColumn: "Placa");
        }
    }
}
