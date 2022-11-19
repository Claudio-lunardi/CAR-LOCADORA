using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Models.SeguroModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace CarLocadora.AtualizarDadosLocacaoSeguradora
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _rabbitMQFactory;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;
        private readonly IOptions<WebConfigUrl> _UrlApi;

        public Worker(ILogger<Worker> logger, RabbitMQFactory rabbitMQFactory, IHttpClientFactory httpClient, IApiToken apiToken, IOptions<WebConfigUrl> urlApi)
        {
            _logger = logger;
            _rabbitMQFactory = rabbitMQFactory;
            _httpClient = httpClient.CreateClient();
            _apiToken = apiToken;
            _UrlApi = urlApi;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult retorno = canal.BasicGet("seguro-dados-retorno", false);

                if (retorno != null)
                {
                    RetornoModel protocolo = JsonConvert.DeserializeObject<RetornoModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao/AtualizarCamposSeguradora", protocolo);

                    if (response.IsSuccessStatusCode)
                    {
                        canal.BasicAck(retorno.DeliveryTag, true);
                    }
                    else
                    {
                        canal.BasicNack(retorno.DeliveryTag, false, true);
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}