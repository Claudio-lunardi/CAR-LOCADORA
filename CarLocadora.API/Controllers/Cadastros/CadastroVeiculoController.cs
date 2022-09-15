using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Veiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CadastroVeiculoController : ControllerBase
    {

        #region Chamando Interface
        private readonly IVeiculo _veiculo;
        public CadastroVeiculoController(IVeiculo veiculo)
        {
            _veiculo = veiculo;
        }

        #endregion


        [HttpGet()]
        public async Task<List<VeiculosModel>> ListaVeiculos()
        {
            return await _veiculo.ListaVeiculos();
        }

        [HttpGet("ObterUmVeiculo")]
        public async Task<VeiculosModel> ListaUmCliente([FromQuery] string valor)
        {
            return await _veiculo.ListaUmVeiculo(valor);
        }

        [HttpPost()]
        public void IncluirVeiculo([FromBody] VeiculosModel veiculosModel)
        {
            _veiculo.IncluirVeiculos(veiculosModel);
        }

        [HttpPut()]
        public void AlterarVeiculo([FromBody] VeiculosModel veiculosModel)
        {
            _veiculo.AlterarVeiculos(veiculosModel);
        }






    }
}
