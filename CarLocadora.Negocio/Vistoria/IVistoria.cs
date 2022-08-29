using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Vistoria
{
    public interface IVistoria
    {
        void IncluirVistoria(VistoriaModel vistoriaModel);
        void AlterarLocacao(VistoriaModel vistoriaModel);

    }
}
