using AspNetCoreRateLimit;
using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Categoria;
using CarLocadora.Negocio.Cliente;
using CarLocadora.Negocio.FormaDePagamento;
using CarLocadora.Negocio.Locacao;
using CarLocadora.Negocio.ManutencaoVeiculo;
using CarLocadora.Negocio.Usuario;
using CarLocadora.Negocio.Veiculo;
using CarLocadora.Negocio.Vistoria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;

namespace CarLocadora.API.Extencoes
{
    public static class ServicoExtencoes
    {
        public static void ConfigurarJwt(this IServiceCollection services)
        {
            Environment.SetEnvironmentVariable("JWT_SECRETO",
                    Convert.ToBase64String(new HMACSHA256().Key), EnvironmentVariableTarget.Process);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = "EmitenteDoJWT",
                        ValidAudience = "DestinatarioDoJWT",
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Convert.FromBase64String(Environment.GetEnvironmentVariable("JWT_SECRETO")))
                    };

                });

        }
        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "post:/api/Login",
                    Limit = 2,
                    Period = "10s",
                },
                //new RateLimitRule
                //{
                //    Endpoint = "*",
                //    Period = "10s",
                //    Limit = 2
                //}
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.EnableEndpointRateLimiting = true;
                opt.StackBlockedRequests = false;
                opt.GeneralRules = rateLimitRules;
            });

            services.AddInMemoryRateLimiting();

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
        public static void ConfigurarSwagger(this IServiceCollection services) =>
             services.AddSwaggerGen(c =>
             {

                 c.SwaggerDoc("v1", new OpenApiInfo
                 {

                     Title = "Api - Claudio ",
                     Version = "v1",
                     Description = "Apis para cadastros"

                 });

                 c.EnableAnnotations();
                 var securityScheme = new OpenApiSecurityScheme
                 {
                     Name = "Autenticação JWT",
                     Description = "Informe o token JTW Bearer **_somente_**",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                     Scheme = "bearer",
                     BearerFormat = "JWT",
                     Reference = new OpenApiReference
                     {
                         Id = JwtBearerDefaults.AuthenticationScheme,
                         Type = ReferenceType.SecurityScheme
                     }
                 };
                 c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {securityScheme, Array.Empty<string>() }
                 });
             });
        public static void ConfigurarServicos(this IServiceCollection services)
        {
            //string connectionString = "Data Source=localhost,1434;User ID=sa;Password=senha@1234xxxY;Initial Catalog=DBCarLocadora;";
            string connectionString = "Data Source=host.docker.internal,1434;User ID=sa;Password=senha@1234xxxY;Initial Catalog=DBCarLocadora;";
            services.AddHttpClient();

            services.AddDbContext<EntityContext>(item => item.UseSqlServer(connectionString));
            services.AddScoped<ICliente, Cliente>();
            services.AddScoped<ICategoria, Categoria>();
            services.AddScoped<IVeiculo, Veiculo>();
            services.AddScoped<IFormaPagamento, FormaPagamento>();
            services.AddScoped<IUsuario, Usuario>();
            services.AddScoped<IManutencaoVeiculo, ManutencaoVeiculo>();
            services.AddScoped<Ilocacao, Locacao>();
            services.AddScoped<IVistoria, Vistoria>();

        }
    }
}