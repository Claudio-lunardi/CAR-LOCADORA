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
        List<ManutencaoVeiculoModel> ListaManutencaoVeiculo();
        void IncluirManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel);

        void AlterarManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel);
    }
}
