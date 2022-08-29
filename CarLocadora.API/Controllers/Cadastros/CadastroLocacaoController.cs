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

        [HttpPost]
        public void IncluirLocacao([FromBody]LocacoesModel locacoesModel)
        {
            _locacao.IncluirLocacao(locacoesModel);

        }

        [HttpPut]
        public void Alterarlocacao([FromBody]LocacoesModel locacoesModel)
        {
            _locacao.AlterarLocacao(locacoesModel);
        }
    }
}
