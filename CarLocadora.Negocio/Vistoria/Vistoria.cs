using CarLocadora.Infra.Entity;
using Microsoft.EntityFrameworkCore;
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

        public async Task AlterarLocacao(VistoriaModel vistoriaModel)
        {
            vistoriaModel.DataAlteracao = DateTime.Now;
            _entityContext.Vistorias.Update(vistoriaModel);
            await _entityContext.SaveChangesAsync();
        }

        public async Task IncluirVistoria(VistoriaModel vistoriaModel)
        {
            vistoriaModel.DataInclusao = DateTime.Now;
            await _entityContext.Vistorias.AddAsync(vistoriaModel);
            await _entityContext.SaveChangesAsync();
        }

        public async Task<List<VistoriaModel>> ListaVistoriaModel()
        {
            return await _entityContext.Vistorias.ToListAsync();
        }

        public async Task<VistoriaModel> ObterUmaVistoria(int valor)
        {
            return await _entityContext.Vistorias.SingleAsync(x => x.Id.Equals(valor));
        }
    }
}
