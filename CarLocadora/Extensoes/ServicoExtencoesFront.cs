using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Servico;

namespace CarLocadora.Extensoes
{
    public static class ServicoExtencoesFront
    {
        public static void ConfigurarServicos(this IServiceCollection services)
        {
            services.AddScoped<IApiToken, ApiToken>();            
            services.AddSingleton<LoginRespostaModel>();
            services.AddHttpClient();
        }

        public static void ConfiguraAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WebConfigUrl>(configuration.GetSection("WebConfigUrl"));
        }
    }
}
