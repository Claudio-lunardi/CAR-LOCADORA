using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Vistoria
{
    public interface IVistoria
    {

        Task<List<VistoriaModel>> ListaVistoriaModel();
        Task<VistoriaModel> ObterUmaVistoria(int valor);
        Task IncluirVistoria(VistoriaModel vistoriaModel);
        Task AlterarLocacao(VistoriaModel vistoriaModel);

    }
}
