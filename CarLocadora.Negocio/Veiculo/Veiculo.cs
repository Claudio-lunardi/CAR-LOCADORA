using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Veiculo
{
    public class Veiculo : IVeiculo
    {
        private readonly EntityContext _entityContext;

        public Veiculo(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task AlterarVeiculos(VeiculosModel veiculosModel)
        {
            veiculosModel.DataAlteracao = DateTime.Now;
            _entityContext.Veiculos.Update(veiculosModel);
            await _entityContext.SaveChangesAsync();
        }

        public async Task IncluirVeiculos(VeiculosModel veiculosModel)
        {
            veiculosModel.DataInclusao = DateTime.Now;
            await _entityContext.Veiculos.AddAsync(veiculosModel);
            await _entityContext.SaveChangesAsync();
        }

        public async Task<VeiculosModel> ListaUmVeiculo(string valor)
        {
            return await _entityContext.Veiculos.SingleAsync(x => x.Placa.Equals(valor));
        }

        public async Task<List<VeiculosModel>> ListaVeiculos()
        {
            return await _entityContext.Veiculos.ToListAsync();
        }
    }
}
