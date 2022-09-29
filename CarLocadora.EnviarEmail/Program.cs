using CarLocadora.EnviarEmail;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
