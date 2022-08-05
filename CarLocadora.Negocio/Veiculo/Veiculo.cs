using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
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

        public void AlterarVeiculos(VeiculosModel veiculosModel)
        {
            _entityContext.Veiculos.Update(veiculosModel);
            _entityContext.SaveChanges();
        }

        public void IncluirVeiculos(VeiculosModel veiculosModel)
        {
            _entityContext.Veiculos.Add(veiculosModel);
            _entityContext.SaveChanges();
        }

        public VeiculosModel ListaUmVeiculo(string valor)
        {
            return _entityContext.Veiculos.Single(x => x.Placa.Equals(valor));
        }

        public List<VeiculosModel> ListaVeiculos()
        {
            return _entityContext.Veiculos.ToList();
        }
    }
}
