using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.FormaDePagamento
{
    public interface IFormaPagamento
    {

        List<FormasDePagamentosModel> ListaFormaPagamentos();
        FormasDePagamentosModel ListaFormaUmPagamento(int valor);
        void IncluirFormaPagamento(FormasDePagamentosModel formasDePagamentosModel);
        void AlterarFormaPagamento(FormasDePagamentosModel formasDePagamentosModel);
    }
}
