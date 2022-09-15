using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.ManutencaoVeiculo;
using Microsoft.AspNetCore.Mvc;



namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroManutencaoVeiculoController : ControllerBase
    {
        private readonly IManutencaoVeiculo _ManutencaoVeiculo;
        public CadastroManutencaoVeiculoController(IManutencaoVeiculo manutencaoVeiculo)
        {
            _ManutencaoVeiculo = manutencaoVeiculo;
        }


        [HttpGet]
        public async Task<List<ManutencaoVeiculoModel>> ListaManutencaoModels()
        {
            return await _ManutencaoVeiculo.ListaManutencaoVeiculo();
        }

        [HttpGet("ObterUmaMnutencaoVeiculo")]
        public async Task<ManutencaoVeiculoModel> ObterUmaMnutencaoVeiculo([FromQuery] int valor)
        {
            return await _ManutencaoVeiculo.ObterUmManutencaoVeiculo(valor);
        }

        [HttpPut]
        public async Task AlterarManutencaoVeiculo([FromBody] ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            await _ManutencaoVeiculo.AlterarManutencaoVeiculo(manutencaoVeiculoModel);

        }

        [HttpPost]
        public async Task IncluirManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            await _ManutencaoVeiculo.IncluirManutencaoVeiculo(manutencaoVeiculoModel);

        }
        [HttpDelete]
        public async Task DeletarManutencaoVeiculo([FromQuery] int valor)
        {
            await _ManutencaoVeiculo.DeletarManutencaoVeiculo(valor);
        }





    }
}
