using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.ManutencaoVeiculo
{
    public class ManutencaoVeiculo : IManutencaoVeiculo
    {
        private readonly EntityContext _entityContext;

        public ManutencaoVeiculo(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void AlterarManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            _entityContext.ManutencaoVeiculo.Update(manutencaoVeiculoModel);
            _entityContext.SaveChanges();
        }

        public void IncluirManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
             _entityContext.ManutencaoVeiculo.Add(manutencaoVeiculoModel);
            _entityContext.SaveChanges();
        }

        public List<ManutencaoVeiculoModel> ListaManutencaoVeiculo()
        {
            return _entityContext.ManutencaoVeiculo.ToList();
        }

    }
}
