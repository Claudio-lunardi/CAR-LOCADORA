using CarLocadora.Modelo.Models.SeguroModel;

namespace CarLocadora.Negocio.Rabbit
{
    public interface IMensageria
    {
        void EnviarMensagemRabbit(object conteudo, string exchange = "", string fila = "");

      
    }
}
