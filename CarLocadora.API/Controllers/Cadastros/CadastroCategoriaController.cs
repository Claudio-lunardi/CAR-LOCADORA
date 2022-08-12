using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CadastroCategoriaController : ControllerBase
    {
        #region Chamando Interface
        private readonly ICategoria _Categoria;
        public CadastroCategoriaController(ICategoria categoria)
        {
            _Categoria = categoria;
        }
        #endregion

        [HttpGet()]
        public async Task<List<CategoriasModel>> ListaCategoria()
        {
            return _Categoria.ListaCategorias();
        }
        [HttpGet("ObterUmaCategoria")]
        public CategoriasModel ListaUmaCategoria(int valor)
        {
            return _Categoria.ListaUmaCategoria(valor);
        }
        [HttpPost()]

        public void IncluirCategoria([FromBody] CategoriasModel categoriasModel)
        {
            _Categoria.IncluirCategoria(categoriasModel);
        }

        [HttpPut()]
        public void AlterarCategoria([FromBody] CategoriasModel categoriasModel)
        {
            _Categoria.AlterarCategoria(categoriasModel);
        }
        [HttpDelete()]
        public void DeletarCategoria(int valor)
        {
            _Categoria.ExcluirCategoria(valor);
        }

    }
}
