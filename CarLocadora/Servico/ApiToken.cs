
using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Servico
{
    public class ApiToken
    {

        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IOptions<LoginRespostaModel> _LoginRespostaModel;

        public ApiToken(IOptions<WebConfigUrl> urlApi, IOptions<LoginRespostaModel> loginRespostaModel)
        {
            _UrlApi = urlApi;
            _LoginRespostaModel = loginRespostaModel;
        }

        private void ObterToken()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LoginRequisicaoModel loginRequisicaoModel = new LoginRequisicaoModel();
            loginRequisicaoModel.Usuario = "UsuarioDevPratica";
            loginRequisicaoModel.Senha = "SenhaDevPratica";

            HttpResponseMessage response = client.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}Login", loginRequisicaoModel).Result;


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


        public string Obter()
        {
            if (_LoginRespostaModel.Value.Autenticado == false)
            {
                ObterToken();
            }
            else
            {
                if (DateTime.Now >= _LoginRespostaModel.Value.DataExpiracao)
                {
                    ObterToken();
                }
            }
            return _LoginRespostaModel.Value.Token;

        }

    }
}
