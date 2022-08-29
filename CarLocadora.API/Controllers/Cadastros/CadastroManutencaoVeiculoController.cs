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
        public List<ManutencaoVeiculoModel> ListaManutencaoModels()
        {
            return _ManutencaoVeiculo.ListaManutencaoVeiculo();
        }

        [HttpGet("ObterUmaMnutencaoVeiculo")]
        public ManutencaoVeiculoModel ObterUmaMnutencaoVeiculo([FromQuery]int valor)
        {

            return _ManutencaoVeiculo.ObterUmManutencaoVeiculo(valor);

        }

        [HttpPut]
        public void AlterarManutencaoVeiculo([FromBody] ManutencaoVeiculoModel  manutencaoVeiculoModel)
        {
            _ManutencaoVeiculo.AlterarManutencaoVeiculo(manutencaoVeiculoModel);

        }

        [HttpPost]
        public void IncluirManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            _ManutencaoVeiculo.IncluirManutencaoVeiculo(manutencaoVeiculoModel);

        }
        [HttpDelete]
        public void DeletarManutencaoVeiculo([FromQuery]int valor)
        {
            _ManutencaoVeiculo.DeletarManutencaoVeiculo(valor);
        }





    }
}
