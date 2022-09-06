using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Categoria
{
    public class Categoria : ICategoria
    {

        #region Chamada Inteface

        private readonly EntityContext _entityContext;

        public Categoria(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }
        #endregion

        public async Task<List<CategoriasModel>> ListaCategorias()
        {
            return await _entityContext.Categorias.OrderBy(id => id.Id).ToListAsync();
        }

        public async Task<CategoriasModel> ListaUmaCategoria(int valor)
        {
            return await _entityContext.Categorias.SingleAsync(x => x.Id.Equals(valor));     
        }

        public async Task AlterarCategoria(CategoriasModel categoriasModel)
        {
            categoriasModel.DataAlteracao = DateTime.Now;
            _entityContext.Categorias.Update(categoriasModel);
          await  _entityContext.SaveChangesAsync();
        }

        public async Task IncluirCategoria(CategoriasModel categoriasModel)
        {
            categoriasModel.DataInclusao = DateTime.Now;
          await  _entityContext.Categorias.AddAsync(categoriasModel);
          await _entityContext.SaveChangesAsync();
        }

        public async Task ExcluirCategoria(int valor)
        {
            var id = await _entityContext.Categorias.SingleAsync(x => x.Id.Equals(valor));
            _entityContext.Categorias.Remove(id);
           await _entityContext.SaveChangesAsync();
        }
    }
}
