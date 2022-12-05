
using CarLocadora.Comum.Modelo;
using CarLocadora.Modelo.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CarLocadora.Comum.Servico
{
    public class ApiToken : IApiToken
    {

        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IOptions<LoginRespostaModel> _LoginRespostaModel;
        private readonly HttpClient _httpClient;

        public ApiToken(IOptions<WebConfigUrl> urlApi, IOptions<LoginRespostaModel> loginRespostaModel, IHttpClientFactory httpClient)
        {
            _UrlApi = urlApi;
            _LoginRespostaModel = loginRespostaModel;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task ObterToken()
        {
            LoginRequisicaoModel loginRequisicaoModel = new LoginRequisicaoModel();
            loginRequisicaoModel.Usuario = "UsuarioDevPratica";
            loginRequisicaoModel.Senha = "SenhaDevPratica";

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}Login", loginRequisicaoModel);

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                LoginRespostaModel loginRespostaModel = JsonConvert.DeserializeObject<LoginRespostaModel>(conteudo);


                if (loginRespostaModel.Autenticado == true)
                {
                    _LoginRespostaModel.Value.Autenticado = loginRespostaModel.Autenticado;
                    _LoginRespostaModel.Value.Usuario = loginRespostaModel.Usuario;
                    _LoginRespostaModel.Value.DataExpiracao = loginRespostaModel.DataExpiracao;
                    _LoginRespostaModel.Value.Token = loginRespostaModel.Token;
                }
            }

            else
            {
                throw new Exception("DEU ZIKA");
            }

        }
        public async Task<string> Obter()
        {
            if (_LoginRespostaModel.Value.Autenticado == false)
            {
                await ObterToken();
            }
            else
            {
                if (DateTime.Now >= _LoginRespostaModel.Value.DataExpiracao)
                {
                    await ObterToken();
                }
            }
            return _LoginRespostaModel.Value.Token;
        }
    }
}
