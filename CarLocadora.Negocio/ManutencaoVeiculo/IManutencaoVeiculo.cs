using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.ManutencaoVeiculo
{
    public interface IManutencaoVeiculo
    {
        Task<List<ManutencaoVeiculoModel>> ListaManutencaoVeiculo();
        Task<ManutencaoVeiculoModel> ObterUmManutencaoVeiculo(int valor);
        Task DeletarManutencaoVeiculo(int valor);
        Task IncluirManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel);
        Task AlterarManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel);
    }
}
