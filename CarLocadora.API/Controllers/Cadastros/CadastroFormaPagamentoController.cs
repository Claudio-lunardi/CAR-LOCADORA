using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.FormaDePagamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CadastroFormaPagamentoController : ControllerBase
    {
        #region Chamando Interface
        private readonly IFormaPagamento _formaPagamento;

        public CadastroFormaPagamentoController(IFormaPagamento formaPagamento)
        {
            _formaPagamento = formaPagamento;
        }
        #endregion

        [HttpGet()]
        public async Task<List<FormasDePagamentosModel>> ListaFormasDePagamentos()
        {
            return await _formaPagamento.ListaFormaPagamentos();
        }
        [HttpGet("ObterUmaFormaPagamento")]
        public async Task<FormasDePagamentosModel> ListaFormasUmDePagamento([FromQuery] int valor)
        {
            return await _formaPagamento.ListaFormaUmPagamento(valor);
        }

        [HttpPost()]
        public async Task IncluirFormasDePagamento([FromBody] FormasDePagamentosModel formasDePagamentosModel)
        {
           await _formaPagamento.IncluirFormaPagamento(formasDePagamentosModel);
        }
        [HttpPut()]
        public async Task AlterarFormasDePagamento([FromBody] FormasDePagamentosModel formasDePagamentosModel)
        {
           await  _formaPagamento.AlterarFormaPagamento(formasDePagamentosModel);
        }


    }
}
