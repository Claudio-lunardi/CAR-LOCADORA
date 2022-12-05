using CarLocadora.Comum.Servico;
using CarLocadora.Comum.Servico.APItokenSeguradora;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Models;
using CarLocadora.Modelo.Models.SeguroModel;
using CarLocadora.Negocio.Cliente;
using CarLocadora.Negocio.Rabbit;
using CarLocadora.Negocio.Veiculo;
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
                    var locacoesModel = JsonConvert.DeserializeObject<LocacoesModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    var seguroModel = new SeguroModel()
                    {
                        locacaoId = locacoesModel.Id,
                        cpf = locacoesModel.ClienteCPF,
                        nome = locacoesModel.Cliente.Nome,
                        placa = locacoesModel.VeiculoPlaca,
                        cnh = locacoesModel.Cliente.CNH,
                        dataNascimento = locacoesModel.Cliente.DataNascimento,
                        telefone = locacoesModel.Cliente.Telefone,
                        marca = locacoesModel.Veiculo.Marca,
                        modelo = locacoesModel.Veiculo.Modelo,
                        combustivel = locacoesModel.Veiculo.Combustivel,
                        dataHoraRetiradaPrevista = locacoesModel.DataHoraRetiradaPrevista,
                        dataHoraDevolucaoPrevista = locacoesModel.DataHoraDevolucaoPrevista
                    };

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"http://apidevpraticaseguradora.kinghost.net/api/Seguro", seguroModel);

                    if (response.IsSuccessStatusCode)
                    {
                        var protocolo = response.Content.ReadAsStringAsync();

                        RetornoProtocoloModel retornoProtocolo = new RetornoProtocoloModel()
                        { IdLocacao = seguroModel.locacaoId, Protocolo = protocolo.Id };

                        _mensageria.EnviarMensagemRabbit(retornoProtocolo, "", "seguro-protocolo");
                        canal.BasicAck(retorno.DeliveryTag, true);
                    }
                    else
                    { 
                        canal.BasicNack(retorno.DeliveryTag, false, true);
                    }
                }
                await Task.Delay(10000, stoppingToken);
            }

        }




    }
}