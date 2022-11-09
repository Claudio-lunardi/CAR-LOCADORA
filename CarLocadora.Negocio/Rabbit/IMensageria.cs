using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Rabbit
{
    public interface IMensageria
    {

        void EnviarMensagemRabbit(object conteudo, string exchange = "", string fila = "");



    }
}
