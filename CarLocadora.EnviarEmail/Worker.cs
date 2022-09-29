using CarLocadora.Modelo.Models;
using Newtonsoft.Json;

namespace CarLocadora.EnviarEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;

        public Worker(ILogger<Worker> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                HttpResponseMessage retorno = await _httpClient.GetAsync("https://localhost:7142/api/CadastroCliente/ObterListaEnviarEmail");
                var a = JsonConvert.DeserializeObject<List<ClientesModel>>(await retorno.Content.ReadAsStringAsync());


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}