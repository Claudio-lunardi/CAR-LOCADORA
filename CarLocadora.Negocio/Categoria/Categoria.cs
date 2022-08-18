using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
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

        public List<CategoriasModel> ListaCategorias()
        {
            return _entityContext.Categorias.OrderBy(id => id.Id).ToList();
        }
        public CategoriasModel ListaUmaCategoria(int valor)
        {
            return _entityContext.Categorias.Single(x => x.Id.Equals(valor));
            
        }
        public void AlterarCategoria(CategoriasModel categoriasModel)
        {
            categoriasModel.DataAlteracao = DateTime.Now;
            _entityContext.Categorias.Update(categoriasModel);
            _entityContext.SaveChanges();
        }
        public void IncluirCategoria(CategoriasModel categoriasModel)
        {
            categoriasModel.DataInclusao = DateTime.Now;
            _entityContext.Categorias.Add(categoriasModel);
            _entityContext.SaveChanges();
        }

        public void ExcluirCategoria(int valor)
        {
            var id = _entityContext.Categorias.Single(id => id.Id.Equals(valor));
            _entityContext.Categorias.Remove(id);
            _entityContext.SaveChanges();

        }
    }
}
