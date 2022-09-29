using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class CriacaoCampo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "emailEnviado",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "emailEnviado",
                table: "Clientes");
        }
    }
}
