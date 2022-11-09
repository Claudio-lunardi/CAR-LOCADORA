using CarLocadora.Infra.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CarLocadora.Negocio.Rabbit
{
    public class Mensageria : IMensageria
    {
        private readonly RabbitMQFactory _rabbitMQFactory;

        public Mensageria(RabbitMQFactory rabbitMQFactory)
        {
            _rabbitMQFactory = rabbitMQFactory;
        }

        public void EnviarMensagemRabbit(object conteudo, string exchange, string fila)
        {
            var canal = _rabbitMQFactory.GetChannel();
            IBasicProperties ibasicProperties = canal.CreateBasicProperties();
            var corpoMensagem = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(conteudo));
            //publicar na exchenge
            canal.BasicPublish(exchange: exchange, routingKey: fila, basicProperties: ibasicProperties, body: corpoMensagem);
        }

    }
}