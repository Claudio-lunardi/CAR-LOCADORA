using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.FormaPagamento
{
    public class FormaPagamentoController : Controller
    {

        #region CONSTRUTORES
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;
        private readonly HttpClient _httpClient;

        public FormaPagamentoController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken, IHttpClientFactory httpClient)
        {
            _UrlApi = urlApi;
            _IApiToken = iApiToken;
            _httpClient = httpClient.CreateClient(); 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Index
        public async Task<ActionResult> Index(string mensagem = null, bool sucesso = true)
        {
            try
            {
                if (sucesso)
                    TempData["sucesso"] = mensagem;
                else
                    TempData["erro"] = mensagem;

                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<FormasDePagamentosModel>>(json));
                }
                else
                {
                    throw new Exception("Erro ao tentar carregar forma de pagamento!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region GetSingle
        public async Task<ActionResult> Details(int valor)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento/ObterUmaFormaPagamento?valor={valor}");


            if (response.IsSuccessStatusCode)
            {

                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<FormasDePagamentosModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro ao tentar carregar forma de pagamento!");

            }
        }
        #endregion

        #region Post
        public ActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] FormasDePagamentosModel formasDePagamentosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento", formasDePagamentosModel);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar incluir uma nova forma de pagamento!");
                    }
                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando preenchimento";
                    return View();

                }
            }
            catch (Exception z)
            {
                TempData["erro"] = "Algum erro aconteceu - " + z.Message;
                return View();
            }




        }
        #endregion

        #region Put
        public async Task<ActionResult> Edit(int valor)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento/ObterUmaFormaPagamento?valor={valor}");


            if (response.IsSuccessStatusCode)
            {

                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<FormasDePagamentosModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro ao tentar carregar forma de pagamento!");

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] FormasDePagamentosModel formasDePagamentosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento", formasDePagamentosModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro editado", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar editarforma de pagamento!");
                    }
                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando preenchimento";
                    return View();
                }

            }
            catch (Exception z)
            {
                TempData["erro"] = "Algum erro aconteceu - " + z.Message;
                return View();
            }
        }
        #endregion

    }
}
