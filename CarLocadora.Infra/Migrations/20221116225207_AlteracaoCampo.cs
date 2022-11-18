using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class AlteracaoCampo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SeguroApolice",
                table: "Locacoes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SeguroAprovado",
                table: "Locacoes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeguroObservacao",
                table: "Locacoes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeguroApolice",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "SeguroAprovado",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "SeguroObservacao",
                table: "Locacoes");
        }
    }
}
