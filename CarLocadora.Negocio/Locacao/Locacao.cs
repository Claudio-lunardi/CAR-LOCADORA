using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
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

        public void AlterarLocacao(LocacoesModel locacoesModel)
        {
            locacoesModel.DataAlteracao = DateTime.Now;
            _entityContext.Locacoes.Update(locacoesModel);
            _entityContext.SaveChanges();
        }


        public void IncluirLocacao(LocacoesModel locacoesModel)
        {
            locacoesModel.DataInclusao = DateTime.Now;
            _entityContext.Locacoes.Add(locacoesModel);
            _entityContext.SaveChanges();
        }

        public List<LocacoesModel> ListaLocacoes()
        {
          return _entityContext.Locacoes.ToList();
        }

        public LocacoesModel ObterUmaLocacoes(int valor)
        {
            return _entityContext.Locacoes.Single(x => x.Id.Equals(valor));
        }
    }
}
