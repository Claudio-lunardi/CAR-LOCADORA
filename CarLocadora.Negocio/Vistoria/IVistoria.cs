using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Vistoria
{
    public interface IVistoria
    {

        List<VistoriaModel> ListaVistoriaModel();

        VistoriaModel ObterUmaVistoria(int valor);
        void IncluirVistoria(VistoriaModel vistoriaModel);
        void AlterarLocacao(VistoriaModel vistoriaModel);

    }
}
