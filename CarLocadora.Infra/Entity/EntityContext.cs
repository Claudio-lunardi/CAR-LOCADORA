using CarLocadora.Modelo.Models;
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
    }
}
