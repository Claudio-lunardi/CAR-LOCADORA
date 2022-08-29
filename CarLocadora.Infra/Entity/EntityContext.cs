using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Vistoria;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Infra.Entity
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }
        public DbSet<CategoriasModel> Categorias { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<FormasDePagamentosModel> FormasDePagamento{ get; set; }
        public DbSet<UsuariosModel> Usuarios { get; set; }
        public DbSet<VeiculosModel> Veiculos { get; set; }
        public DbSet<ManutencaoVeiculoModel> ManutencaoVeiculo { get; set; }
        public DbSet<VistoriaModel> Vistorias { get; set; }
        public DbSet<LocacoesModel> Locacoes { get; set; }
    }
}
