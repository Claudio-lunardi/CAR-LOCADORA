using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Locacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroLocacaoController : ControllerBase
    {
        private readonly Ilocacao _locacao;

        public CadastroLocacaoController(Ilocacao locacao)
        {
            _locacao = locacao;
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
    }
}
