using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Categoria
{
    public interface ICategoria
    {
        Task<List<CategoriasModel>> ListaCategorias();
        Task<CategoriasModel> ListaUmaCategoria(int valor);
        Task AlterarCategoria(CategoriasModel categoriasModel);
        Task IncluirCategoria(CategoriasModel categoriasModel);
        Task ExcluirCategoria(int valor);
    }
}
