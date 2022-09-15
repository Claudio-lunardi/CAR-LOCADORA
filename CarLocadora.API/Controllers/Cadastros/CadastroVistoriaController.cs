using CarLocadora.Negocio.Vistoria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroVistoriaController : ControllerBase
    {

        private readonly IVistoria _vistoria;
        public CadastroVistoriaController(IVistoria vistoria)
        {
            _vistoria = vistoria;
        }


        [HttpGet]
        public async Task<List<VistoriaModel>> ListaVistoria()
        {
            return await _vistoria.ListaVistoriaModel();
        }
        [HttpGet("ObterUmaVistoria")]
        public async Task<VistoriaModel> ObterUmaVistoria(int valor)
        {
            return await _vistoria.ObterUmaVistoria(valor);
        }

        [HttpPost]
        public async Task IncluirVistoria([FromBody] VistoriaModel vistoriaModel)
        {
            await _vistoria.IncluirVistoria(vistoriaModel);
        }
        [HttpPut]
        public async Task AlterarVistoria([FromBody] VistoriaModel vistoriaModel)
        {
            await _vistoria.AlterarLocacao(vistoriaModel);
        }




    }
}
