using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Categoria
{
    public class Categoria :ICategoria
    {

        #region Chamada Inteface

        private readonly ICategoria _entityContext;

        public Categoria(ICategoria categoria)
        {
            _entityContext = categoria;
        }
        #endregion


        







    }
}
