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
        List<VeiculosModel> ListaVeiculos();
        VeiculosModel ListaUmVeiculo(string valor);
        void IncluirVeiculos(VeiculosModel veiculosModel);
        void AlterarVeiculos(VeiculosModel veiculosModel);
    }
}
