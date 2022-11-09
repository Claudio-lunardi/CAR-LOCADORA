using CarLocadora.Comum.Modelo;
using CarLocadora.GerarArquivo;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Negocio.Rabbit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        #region RabbitMQ
        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
        services.AddScoped<IMensageria, Mensageria>();
        services.AddSingleton<RabbitMQFactory>();
        #endregion

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
