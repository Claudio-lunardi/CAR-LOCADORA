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


        List<CategoriasModel> ListaCategoria();
        CategoriasModel ListaUmaCategoria();
        void AlterarCategoria(CategoriasModel categoriasModel);
        void IncluirCategoria(CategoriasModel categoriasModel);
        void ExcluirCategoria(string valor);
        




    }
}
