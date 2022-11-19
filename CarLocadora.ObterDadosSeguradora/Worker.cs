using CarLocadora.Comum.Servico.APItokenSeguradora;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Models.SeguroModel;
using CarLocadora.Negocio.Rabbit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace CarLocadora.ObterDadosSeguradora
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _rabbitMQFactory;
        private readonly HttpClient _httpClient;
        private readonly IApiTokenSeguro _apiTokenSeguro;
        private readonly IMensageria _mensageria;

        public Worker(ILogger<Worker> logger, RabbitMQFactory rabbitMQFactory, IHttpClientFactory httpClient, IApiTokenSeguro apiTokenSeguro, IMensageria mensageria)
        {
            _logger = logger;
            _rabbitMQFactory = rabbitMQFactory;
            _httpClient = httpClient.CreateClient();
            _apiTokenSeguro = apiTokenSeguro;
            _mensageria = mensageria;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult retorno = canal.BasicGet("seguro-protocolo", false);

                if (retorno != null)
                {
                    var protocolo = JsonConvert.DeserializeObject<RetornoProtocoloModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiTokenSeguro.Obter());
                    HttpResponseMessage response = await _httpClient.GetAsync($"http://apidevpraticaseguradora.kinghost.net/api/Seguro?protocolo={protocolo.Protocolo}");

                    if (response.IsSuccessStatusCode)
                    {
                        var Getprotocolo = JsonConvert.DeserializeObject<RetornoModel>(await response.Content.ReadAsStringAsync());
                        Getprotocolo.IdLocacao = protocolo.IdLocacao;

                        if (Getprotocolo.status == "processando")
                        {
                            canal.BasicNack(retorno.DeliveryTag, false, true);
                        }
                        else
                        {
                            _mensageria.EnviarMensagemRabbit(Getprotocolo, "", "seguro-dados-retorno");
                            canal.BasicAck(retorno.DeliveryTag, true);
                        }
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}