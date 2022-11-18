using CarLocadora.Modelo.Models;
using CarLocadora.Modelo.Models.SeguroModel;
using CarLocadora.Negocio.Locacao;
using CarLocadora.Negocio.Seguradora;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CadastroLocacaoController : ControllerBase
    {
        private readonly Ilocacao _locacao;
        private readonly ISeguradora _seguradora;

        public CadastroLocacaoController(Ilocacao locacao, ISeguradora seguradora)
        {
            _locacao = locacao;
            _seguradora = seguradora;
        }

        [HttpGet]
        public async Task<List<LocacoesModel>> ListaLocacoes()
        {

            return await _locacao.ListaLocacoes();


        }
        [HttpGet("ObterUmalocacao")]
        public async Task<LocacoesModel> ObterUmalocacao(int valor)
        {

            return await _locacao.ObterUmaLocacoes(valor);

        }

        [HttpPost]
        public async Task IncluirLocacao([FromBody] LocacoesModel locacoesModel)
        {
            await _locacao.IncluirLocacao(locacoesModel);

        }

        [HttpPut]
        public async Task Alterarlocacao([FromBody] LocacoesModel locacoesModel)
        {
            await _locacao.AlterarLocacao(locacoesModel);
        }

        [HttpPut("AtualizarCamposSeguradora")]
        public async Task AtualizarCamposSeguradora([FromBody] RetornoModel retornoModel)
        {
          await _seguradora.SalvarDadosSeguradora(retornoModel);
        }


    }
}
