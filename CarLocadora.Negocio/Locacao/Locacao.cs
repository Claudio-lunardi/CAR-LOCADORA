using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Locacao
{
    public class Locacao : Ilocacao
    {
        #region Chamando Interface

        private readonly EntityContext _entityContext;

        public Locacao(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        #endregion

        public async Task AlterarLocacao(LocacoesModel locacoesModel)
        {
            locacoesModel.DataAlteracao = DateTime.Now;
            _entityContext.Locacoes.Update(locacoesModel);
         await  _entityContext.SaveChangesAsync();
        }


        public async Task IncluirLocacao(LocacoesModel locacoesModel)
        {
            locacoesModel.DataInclusao = DateTime.Now;
          await _entityContext.Locacoes.AddAsync(locacoesModel);
          await _entityContext.SaveChangesAsync();
        }

        public  async Task<List<LocacoesModel>> ListaLocacoes()
        {
          return await _entityContext.Locacoes.ToListAsync();
        }

        public async Task<LocacoesModel> ObterUmaLocacoes(int valor)
        {
            return await _entityContext.Locacoes.SingleAsync(x => x.Id.Equals(valor));
        }
    }
}
