﻿using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Categoria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<List<CategoriasModel>> ListaClientes()
        {
            return _Categoria.ListaCategorias();
        }
        [HttpGet("ObterUmaCategoria")]
        public CategoriasModel ListaUmaCategoria([FromQuery] string valor)
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
        public void DeletarCategoria(string valor)
        {
            _Categoria.ExcluirCategoria(valor);
        }

    }
}