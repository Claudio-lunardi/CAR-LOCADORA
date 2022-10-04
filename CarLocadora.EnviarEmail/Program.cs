using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.EnviarEmail;
using CarLocadora.Modelo.Models;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();
        services.AddSingleton<IApiToken, ApiToken>();
        services.AddSingleton<LoginRespostaModel>();
        services.Configure<WebConfigUrl>(hostContext.Configuration.GetSection("WebConfigUrl"));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
