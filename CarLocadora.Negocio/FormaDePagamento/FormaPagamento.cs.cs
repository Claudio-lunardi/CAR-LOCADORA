using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task AlterarFormaPagamento(FormasDePagamentosModel formasDePagamentosModel)
        {
            formasDePagamentosModel.DataAlteracao = DateTime.Now;
            _entityContext.FormasDePagamento.Update(formasDePagamentosModel);
          await _entityContext.SaveChangesAsync();
        }

        public async Task IncluirFormaPagamento(FormasDePagamentosModel formasDePagamentosModel)
        {
            formasDePagamentosModel.DataInclusao = DateTime.Now;
          await  _entityContext.FormasDePagamento.AddAsync(formasDePagamentosModel);
          await  _entityContext.SaveChangesAsync();
        }

        public async Task<List<FormasDePagamentosModel>> ListaFormaPagamentos()
        {
            return await _entityContext.FormasDePagamento.ToListAsync();
        }

        public async Task<FormasDePagamentosModel> ListaFormaUmPagamento(int valor)
        {
            return await _entityContext.FormasDePagamento.SingleAsync(x => x.Id.Equals(valor));
        }
    }
}
