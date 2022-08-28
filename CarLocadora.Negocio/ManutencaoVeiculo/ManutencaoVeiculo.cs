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

        public void IncluirManutencaoVeiculo(ImanutencaoVeiculosModel manutencaoVeiculoModel)
        {
             _entityContext.ManutencaoVeiculo.Add(manutencaoVeiculoModel);
            _entityContext.SaveChanges();
        }

        public List<ImanutencaoVeiculosModel> ListaManutencaoVeiculo()
        {
            return _entityContext.ManutencaoVeiculo.ToList();
        }

    }
}
