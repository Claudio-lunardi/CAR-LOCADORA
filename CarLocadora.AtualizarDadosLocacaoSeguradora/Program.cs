using CarLocadora.AtualizarDadosLocacaoSeguradora;
using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Comum.Servico.APItokenSeguradora;
using CarLocadora.Infra.Entity;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Models;
using CarLocadora.Modelo.Models.SeguroModel;
using CarLocadora.Negocio.Rabbit;
using CarLocadora.Negocio.Seguradora;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        //services.AddHttpClient();

        services.AddSingleton<IApiToken, ApiToken>();
        services.AddSingleton<LoginRespostaModel>();

        services.AddHttpClient("").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        });

        services.Configure<WebConfigUrl>(hostContext.Configuration.GetSection("WebConfigUrl"));

        #region RabbitMQ
        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
        services.AddSingleton<RabbitMQFactory>();
        #endregion
    })
    .Build();

await host.RunAsync();
