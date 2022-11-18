using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico.APItokenSeguradora;
using CarLocadora.EnviarDadosSeguradora;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Negocio.Rabbit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();


        services.AddHttpClient();
        services.AddSingleton<IApiTokenSeguro, ApiTokenSeguro>();
        services.AddSingleton<LoginRespostaSeguradora>();

        #region RabbitMQ
        services.AddSingleton<IMensageria, Mensageria>();
        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));    
        services.AddSingleton<RabbitMQFactory>();
       
        #endregion

    })
    .Build();

await host.RunAsync();
