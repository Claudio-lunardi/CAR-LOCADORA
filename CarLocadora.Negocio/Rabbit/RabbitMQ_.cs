using CarLocadora.Modelo.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Rabbit
{
    public class RabbitMQ_ : IRabbitMQ
    {
        private readonly ConnectionFactory _factory;
        public RabbitMQ_()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",

                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

        }
        public async Task EnviarMensagemRabbit(ClientesModel clientesModel)
        {
            ClienteModelRabbitMq clienteModelRabbitMq = new ClienteModelRabbitMq();
            clienteModelRabbitMq.Nome = clientesModel.Nome;
            clienteModelRabbitMq.Email = clientesModel.Email;
            clienteModelRabbitMq.CPF = clientesModel.CPF;

            var connectarRabbit = _factory.CreateConnection();
            var canal = connectarRabbit.CreateModel();
            IBasicProperties ibasicProperties = canal.CreateBasicProperties();

            var corpoMensagem = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clienteModelRabbitMq));

            //publicar na exchenge
            canal.BasicPublish(exchange: "emails", routingKey: "", basicProperties: ibasicProperties, body: corpoMensagem);
        }

        public async Task<ClienteModelRabbitMq> PegarMensagemRabbit()
        {
            var connectarRabbit = _factory.CreateConnection();
            var canal = connectarRabbit.CreateModel();
            var dados = new ClienteModelRabbitMq();

            while (true)
            {
                BasicGetResult retorno = canal.BasicGet("cliente", false);
                if (retorno == null)
                {
                    break;

                }
                else
                {
                    dados = JsonConvert.DeserializeObject<ClienteModelRabbitMq>(Encoding.UTF8.GetString(retorno.Body.ToArray()));

                    canal.BasicAck(retorno.DeliveryTag, true);


                }
            }

            return dados;
        }










    }
}