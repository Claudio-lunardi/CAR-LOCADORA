using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico.APItokenSeguradora;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Negocio.Rabbit;
using CarLocadora.ObterDadosSeguradora;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddHttpClient();
        services.AddSingleton<IApiTokenSeguro, ApiTokenSeguro>();
        services.AddSingleton<LoginRespostaSeguradora>();

        #region RabbitMQ

        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
        services.AddSingleton<RabbitMQFactory>();
        services.AddSingleton<IMensageria, Mensageria>();
        #endregion
    })
    .Build();

await host.RunAsync();
