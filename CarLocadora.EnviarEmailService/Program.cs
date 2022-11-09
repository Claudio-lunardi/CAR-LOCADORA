using CarLocadora.Comum.Modelo;
using CarLocadora.EnviarEmailService;
using CarLocadora.Infra.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        //services.AddHttpClient();
        //services.AddSingleton<IApiToken, ApiToken>();
        //services.AddSingleton<LoginRespostaModel>();
        //services.Configure<WebConfigUrl>(hostContext.Configuration.GetSection("WebConfigUrl"));

        #region RabbitMQ
        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
        services.AddSingleton<RabbitMQFactory>();
        #endregion

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

