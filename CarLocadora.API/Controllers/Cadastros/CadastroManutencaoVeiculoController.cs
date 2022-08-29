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

        [HttpPost]
        public void IncluirManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            _ManutencaoVeiculo.IncluirManutencaoVeiculo(manutencaoVeiculoModel);

        }



    }
}
