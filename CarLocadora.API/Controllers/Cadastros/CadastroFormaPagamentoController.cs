using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.FormaDePagamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            return _formaPagamento.ListaFormaPagamentos();
        }
        [HttpGet("ObterUmaFormaPagamento")]
        public FormasDePagamentosModel ListaFormasUmDePagamento([FromQuery] int valor)
        {

            return _formaPagamento.ListaFormaUmPagamento(valor);
        }

        [HttpPost()]
        public void IncluirFormasDePagamento([FromBody] FormasDePagamentosModel formasDePagamentosModel)
        {
            _formaPagamento.IncluirFormaPagamento(formasDePagamentosModel);
        }
        [HttpPut()]
        public void AlterarFormasDePagamento([FromBody] FormasDePagamentosModel formasDePagamentosModel)
        {
            _formaPagamento.AlterarFormaPagamento(formasDePagamentosModel);
        }


    }
}
