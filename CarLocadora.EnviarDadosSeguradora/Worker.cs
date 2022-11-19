using CarLocadora.Comum.Servico;
using CarLocadora.Comum.Servico.APItokenSeguradora;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Models;
using CarLocadora.Modelo.Models.SeguroModel;
using CarLocadora.Negocio.Rabbit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace CarLocadora.EnviarDadosSeguradora
{
    public class Worker : BackgroundService
    {

        private readonly IMensageria _mensageria;
        private readonly RabbitMQFactory _rabbitMQFactory;
        private readonly HttpClient _httpClient;
        private readonly IApiTokenSeguro _IApiToken;

        public Worker(RabbitMQFactory rabbitMQFactory, IHttpClientFactory httpClient, IApiTokenSeguro iApiToken, IMensageria mensageria)
        {

            _rabbitMQFactory = rabbitMQFactory;
            _httpClient = httpClient.CreateClient();
            _IApiToken = iApiToken;
            _mensageria = mensageria;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult retorno = canal.BasicGet("seguro", false);
                if (retorno != null)
                {
                    var dados = JsonConvert.DeserializeObject<SeguroModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    int protocolo = await APISeguradora(dados);

                    RetornoProtocoloModel retornoProtocolo = new RetornoProtocoloModel()
                    { IdLocacao = dados.locacaoId, Protocolo = protocolo };

                    _mensageria.EnviarMensagemRabbit(retornoProtocolo, "", "seguro-protocolo");
                    canal.BasicAck(retorno.DeliveryTag, true);
                }               
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task<int> APISeguradora(SeguroModel dados)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"http://apidevpraticaseguradora.kinghost.net/api/Seguro", dados);

            if (response.IsSuccessStatusCode)
            {
                var protocolo = response.Content.ReadAsStringAsync();


                return protocolo.Id;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }



        }


    }
}