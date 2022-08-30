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
        public List<VistoriaModel> ListaVistoria()
        {
            return _vistoria.ListaVistoriaModel();
        }
        [HttpGet("ObterUmaVistoria")]
        public VistoriaModel ObterUmaVistoria(int valor)
        {
            return _vistoria.ObterUmaVistoria(valor);
        }


        [HttpPost]
        public void IncluirVistoria([FromBody] VistoriaModel vistoriaModel)
        {
            _vistoria.IncluirVistoria(vistoriaModel);
        }
        [HttpPut]
        public void AlterarVistoria([FromBody] VistoriaModel vistoriaModel)
        {
            _vistoria.AlterarLocacao(vistoriaModel);
        }




    }
}
