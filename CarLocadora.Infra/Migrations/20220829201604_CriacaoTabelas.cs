using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class CriacaoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteCPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    FormaPagamentoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    VeiculoPlaca = table.Column<int>(type: "int", maxLength: 8, nullable: false),
                    VeiculoPlaca1 = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    DataHoraReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraRetiradaPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraDevolucaoPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacoes_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Clientes_ClienteCPF",
                        column: x => x.ClienteCPF,
                        principalTable: "Clientes",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_FormasDePagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormasDePagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Veiculos_VeiculoPlaca1",
                        column: x => x.VeiculoPlaca1,
                        principalTable: "Veiculos",
                        principalColumn: "Placa");
                });

            migrationBuilder.CreateTable(
                name: "Vistorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocacoesId = table.Column<int>(type: "int", nullable: false),
                    KmSaida = table.Column<long>(type: "bigint", nullable: false),
                    CombustivelSaida = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObservacaoSaida = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DataHoraRetiradaPatio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KmEntrada = table.Column<long>(type: "bigint", nullable: false),
                    CombustivelEntrada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ObservacaoEntrada = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DataHoraDevolucaoPatio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vistorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vistorias_Locacoes_LocacoesId",
                        column: x => x.LocacoesId,
                        principalTable: "Locacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_CategoriaId",
                table: "Locacoes",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_ClienteCPF",
                table: "Locacoes",
                column: "ClienteCPF");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_FormaPagamentoId",
                table: "Locacoes",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_VeiculoPlaca1",
                table: "Locacoes",
                column: "VeiculoPlaca1");

            migrationBuilder.CreateIndex(
                name: "IX_Vistorias_LocacoesId",
                table: "Vistorias",
                column: "LocacoesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vistorias");

            migrationBuilder.DropTable(
                name: "Locacoes");
        }
    }
}
