using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
            return await _Categoria.ListaCategorias();
        }

        [HttpGet("ObterUmaCategoria")]
        public async Task<CategoriasModel> ListaUmaCategoria(int valor)
        {
            return await _Categoria.ListaUmaCategoria(valor);
        }

        [HttpPost()]
        public async Task IncluirCategoria([FromBody] CategoriasModel categoriasModel)
        {
          await _Categoria.IncluirCategoria(categoriasModel);
        }

        [HttpPut()]
        public async Task AlterarCategoria([FromBody] CategoriasModel categoriasModel)
        {
          await _Categoria.AlterarCategoria(categoriasModel);
        }

        [HttpDelete()]
        public async Task DeletarCategoria(int valor)
        {
          await _Categoria.ExcluirCategoria(valor);
        }

    }
}
