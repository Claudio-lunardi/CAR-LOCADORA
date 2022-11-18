using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models.SeguroModel;

namespace CarLocadora.Negocio.Seguradora
{
    public class Seguradora : ISeguradora
    {
        private readonly EntityContext _entityContext;

        public Seguradora(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task SalvarDadosSeguradora(RetornoModel retornoModel)
        {
            var locacao = _entityContext.Locacoes.First(c => c.Id == retornoModel.IdLocacao);
            locacao.SeguroApolice = retornoModel.apolice == null ? null : Guid.Parse(retornoModel.apolice);
            locacao.SeguroObservacao = retornoModel.observacao;
            locacao.SeguroAprovado = retornoModel.status.ToLower().Trim() == "aprovado" ? true : false;
            _entityContext.Update(locacao);
            await _entityContext.SaveChangesAsync();
        }
    }
}
