using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Veiculo
{
    public interface IVeiculo
    {
        Task <List<VeiculosModel>> ListaVeiculos();
        Task <VeiculosModel> ListaUmVeiculo(string valor);
        Task  IncluirVeiculos(VeiculosModel veiculosModel);
        Task AlterarVeiculos(VeiculosModel veiculosModel);
    }
}
