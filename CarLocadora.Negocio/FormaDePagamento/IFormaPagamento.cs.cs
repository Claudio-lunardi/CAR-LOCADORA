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

       Task<List<FormasDePagamentosModel>> ListaFormaPagamentos();
       Task <FormasDePagamentosModel> ListaFormaUmPagamento(int valor);
       Task IncluirFormaPagamento(FormasDePagamentosModel formasDePagamentosModel);
       Task AlterarFormaPagamento(FormasDePagamentosModel formasDePagamentosModel);
    }
}
