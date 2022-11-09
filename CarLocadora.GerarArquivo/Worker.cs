using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CarLocadora.GerarArquivo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _rabbitMQFactory;

        public Worker(ILogger<Worker> logger, RabbitMQFactory rabbitMQFactory)
        {
            _logger = logger;
            _rabbitMQFactory = rabbitMQFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult retorno = canal.BasicGet("veiculo", false);
                if (retorno != null)
                {

                    var dados = JsonConvert.DeserializeObject<VeiculosModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));

                    using (StreamWriter sw = new StreamWriter($@"C:\Teste\{dados.Placa}" + ".txt"))
                    {
                        sw.WriteLine(dados.Placa);
                        sw.WriteLine(dados.Chassi);
                        sw.WriteLine(dados.Marca);
                        sw.WriteLine(dados.Modelo);
                        sw.WriteLine(dados.Combustivel);
                        sw.WriteLine(dados.Cor);
                        sw.WriteLine(dados.Opcionais);
                        sw.WriteLine(dados.Ativo.ToString());
                        sw.WriteLine(dados.DataInclusao.ToString());
                        sw.WriteLine(dados.DataAlteracao.ToString());
                        sw.WriteLine(dados.CategoriaId.ToString());
                  
                    }








                }

                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}