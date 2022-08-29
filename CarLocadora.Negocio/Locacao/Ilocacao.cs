using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Locacao
{
    public interface Ilocacao
    {
        void IncluirLocacao(LocacoesModel locacoesModel);

        void AlterarLocacao(LocacoesModel locacoesModel);

    }
}
