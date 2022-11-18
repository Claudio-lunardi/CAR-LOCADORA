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

                    GerarArquivo(dados);

                    canal.BasicAck(retorno.DeliveryTag, true);
                }

                await Task.Delay(5000, stoppingToken);

            }
        }



        private void GerarArquivo(VeiculosModel veiculosModel)
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Teste\{veiculosModel.Placa}" + ".txt"))
            {
                sw.WriteLine(veiculosModel.Placa);
                sw.WriteLine(veiculosModel?.Opcionais);
                sw.WriteLine(veiculosModel?.Chassi);
                sw.WriteLine(veiculosModel.Marca);
                sw.WriteLine(veiculosModel.Combustivel);
                sw.WriteLine(veiculosModel.Modelo);            
                sw.WriteLine(veiculosModel.Cor);              
                sw.WriteLine(veiculosModel.Ativo.ToString());
                sw.WriteLine(veiculosModel.DataInclusao.ToString());
                sw.WriteLine(veiculosModel?.DataAlteracao.ToString());
                sw.WriteLine(veiculosModel?.CategoriaId.ToString());
               
            }
        }
    }
}