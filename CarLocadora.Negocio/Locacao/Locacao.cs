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

        public void AlterarLocacao(LocacoesModel locacoesModel)
        {
            _entityContext.Locacoes.Update(locacoesModel);
            _entityContext.SaveChanges();
        }

        #endregion


        public void IncluirLocacao(LocacoesModel locacoesModel)
        {
            _entityContext.Locacoes.Add(locacoesModel);
            _entityContext.SaveChanges();
        }
    }
}
