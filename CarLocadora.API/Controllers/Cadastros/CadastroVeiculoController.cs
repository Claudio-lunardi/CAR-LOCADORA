using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Veiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task IncluirVeiculo([FromBody] VeiculosModel veiculosModel)
        {
            await _veiculo.IncluirVeiculos(veiculosModel);
        }

        [HttpPut()]
        public async Task AlterarVeiculo([FromBody] VeiculosModel veiculosModel)
        {
            await _veiculo.AlterarVeiculos(veiculosModel);
        }






    }
}
