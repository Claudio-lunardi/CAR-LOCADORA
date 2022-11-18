using CarLocadora.Comum.Modelo;
using CarLocadora.Modelo.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Comum.Servico.APItokenSeguradora
{
    public class ApiTokenSeguro :IApiTokenSeguro
    {
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IOptions<LoginRespostaSeguradora> _LoginRespostaModel;

        public ApiTokenSeguro(IOptions<WebConfigUrl> urlApi, IOptions<LoginRespostaSeguradora> loginRespostaModel)
        {
            _UrlApi = urlApi;
            _LoginRespostaModel = loginRespostaModel;
        }

        private async Task ObterToken()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LoginRequisicaoSeguradora loginRequisicaoModel = new LoginRequisicaoSeguradora();
            loginRequisicaoModel.Usuario = "claudio";
            loginRequisicaoModel.Senha = "RD4mYmXb30$2";

            HttpResponseMessage response = await client.PostAsJsonAsync($"http://apidevpraticaseguradora.kinghost.net/api/Login", loginRequisicaoModel);

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                LoginRespostaSeguradora loginRespostaModel = JsonConvert.DeserializeObject<LoginRespostaSeguradora>(conteudo);


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
