using CarLocadora.Infra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Vistoria
{
    public class Vistoria : IVistoria
    {
        private readonly EntityContext _entityContext;

        public Vistoria(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void AlterarLocacao(VistoriaModel vistoriaModel)
        {
            vistoriaModel.DataAlteracao = DateTime.Now;
            _entityContext.Vistorias.Update(vistoriaModel);
            _entityContext.SaveChanges();
        }

        public void IncluirVistoria(VistoriaModel vistoriaModel)
        {
            vistoriaModel.DataInclusao = DateTime.Now;
            _entityContext.Vistorias.Add(vistoriaModel);
            _entityContext.SaveChanges();
        }

        public List<VistoriaModel> ListaVistoriaModel()
        {
            return _entityContext.Vistorias.ToList();
        }

        public VistoriaModel ObterUmaVistoria(int valor)
        {
            return _entityContext.Vistorias.Single(x => x.Id.Equals(valor));
        }
    }
}
