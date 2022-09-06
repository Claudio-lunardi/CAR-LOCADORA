using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task AlterarManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            manutencaoVeiculoModel.DataAlteracao = DateTime.Now;
            _entityContext.ManutencaoVeiculo.Update(manutencaoVeiculoModel);
            await _entityContext.SaveChangesAsync();
        }

        public async Task DeletarManutencaoVeiculo(int valor)
        {

            var id = await _entityContext.ManutencaoVeiculo.SingleAsync(x => x.Id.Equals(valor));
            _entityContext.ManutencaoVeiculo.Remove(id);
            await _entityContext.SaveChangesAsync();

        }

        public async Task IncluirManutencaoVeiculo(ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            manutencaoVeiculoModel.DataInclusao = DateTime.Now;
            await _entityContext.ManutencaoVeiculo.AddAsync(manutencaoVeiculoModel);
            await _entityContext.SaveChangesAsync();

        }

        public async Task<List<ManutencaoVeiculoModel>> ListaManutencaoVeiculo()
        {
            return await _entityContext.ManutencaoVeiculo.ToListAsync();
        }

        public async Task<ManutencaoVeiculoModel> ObterUmManutencaoVeiculo(int valor)
        {
            return await _entityContext.ManutencaoVeiculo.SingleAsync(x => x.Id.Equals(valor));
        }







    }
}
