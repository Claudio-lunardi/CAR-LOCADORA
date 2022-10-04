namespace CarLocadora.Comum.Servico
{
    public interface IApiToken
    {
      Task<string> Obter();
    }
}
