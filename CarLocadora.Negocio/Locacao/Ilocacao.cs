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
        Task<List<LocacoesModel>> ListaLocacoes();

        Task<LocacoesModel> ObterUmaLocacoes(int valor);

        Task IncluirLocacao(LocacoesModel locacoesModel);

        Task AlterarLocacao(LocacoesModel locacoesModel);

    }
}
