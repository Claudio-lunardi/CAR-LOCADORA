namespace CarLocadora.Servico
{
    public interface IApiToken
    {
      Task<string> Obter();
    }
}
