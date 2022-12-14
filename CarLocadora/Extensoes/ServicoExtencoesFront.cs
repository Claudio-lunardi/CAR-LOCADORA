 using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public static void ConfigurarCookiePolicy(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            }) ;

        }
        public static void ConfigurarAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => 

            options.LoginPath = new PathString("/Login/Index"));

        }








    }
}
