using CarLocadora.Infra.Entity;
using CarLocadora.Negocio.Categoria;
using CarLocadora.Negocio.Cliente;
using CarLocadora.Negocio.FormaDePagamento;
using CarLocadora.Negocio.Usuario;
using CarLocadora.Negocio.Veiculo;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.API.Extencoes
{
    public static class ServicoExtencoes
    {
        public static void ConfigurarServicos(this IServiceCollection services)
        {
            string connectionString = "Data Source=localhost,1434;User ID=sa;Password=senha@1234xxxY;Initial Catalog=DBCarLocadora;";

            services.AddDbContext<EntityContext>(item => item.UseSqlServer(connectionString));
            services.AddScoped<ICliente, Cliente>();
            services.AddScoped<ICategoria, Categoria>();
            services.AddScoped<IVeiculo, Veiculo>();
            services.AddScoped<IFormaPagamento, FormaPagamento>();
            services.AddScoped<IUsuario, Usuario>();
        }
    }
}
