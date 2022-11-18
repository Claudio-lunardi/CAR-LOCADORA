using CarLocadora.Modelo.Models.SeguroModel;

namespace CarLocadora.Negocio.Seguradora
{
    public interface ISeguradora
    {
        Task SalvarDadosSeguradora(RetornoModel retornoModel);
    }
}
