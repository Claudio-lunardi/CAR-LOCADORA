using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.FormaDePagamento
{
    public class FormaPagamento : IFormaPagamento
    {
        private readonly EntityContext _entityContext;

        public FormaPagamento(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void AlterarFormaPagamento(FormasDePagamentosModel formasDePagamentosModel)
        {
            formasDePagamentosModel.DataAlteracao = DateTime.Now;
            _entityContext.FormasDePagamento.Update(formasDePagamentosModel);
            _entityContext.SaveChanges();
        }

        public void IncluirFormaPagamento(FormasDePagamentosModel formasDePagamentosModel)
        {
            formasDePagamentosModel.DataAlteracao = DateTime.Now;
            _entityContext.FormasDePagamento.Add(formasDePagamentosModel);
            _entityContext.SaveChanges();
        }

        public List<FormasDePagamentosModel> ListaFormaPagamentos()
        {
            return _entityContext.FormasDePagamento.ToList();
        }

        public FormasDePagamentosModel ListaFormaUmPagamento(int valor)
        {
            return _entityContext.FormasDePagamento.Single(x => x.Id.Equals(valor));
        }
    }
}
