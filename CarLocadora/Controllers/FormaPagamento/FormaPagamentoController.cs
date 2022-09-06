using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.FormaPagamento
{
    public class FormaPagamentoController : Controller
    {
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;

        public FormaPagamentoController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken)
        {
            _UrlApi = urlApi;
            _IApiToken = iApiToken;
        }


        #region Index
        public async Task<ActionResult> Index(string mensagem = null, bool sucesso = true)
        {
            try
            {
                if (sucesso)
                    TempData["sucesso"] = mensagem;
                else
                    TempData["erro"] = mensagem;

                HttpClient Client = new HttpClient();

                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
                HttpResponseMessage response =await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<FormasDePagamentosModel>>(json));
                }
                else
                {
                    throw new Exception("aaa");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        // GET: FormaPagamentoController/Details/5
        public async Task<ActionResult> Details(int valor)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
            HttpResponseMessage response =await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento/ObterUmaFormaPagamento?valor={valor}");


            if (response.IsSuccessStatusCode)
            {

                string conteudo =await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<FormasDePagamentosModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");

            }
        }

        // GET: FormaPagamentoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormaPagamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] FormasDePagamentosModel formasDePagamentosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Client = new HttpClient();

                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
                    HttpResponseMessage response =await Client.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento", formasDePagamentosModel);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("aaa");
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

        // GET: FormaPagamentoController/Edit/5
        public async Task<ActionResult> Edit(int valor)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
            HttpResponseMessage response =await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento/ObterUmaFormaPagamento?valor={valor}");


            if (response.IsSuccessStatusCode)
            {

                string conteudo =await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<FormasDePagamentosModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");

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

                    HttpClient Client = new HttpClient();
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
                    HttpResponseMessage response =await Client.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento", formasDePagamentosModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro editado", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("aaa");
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
    }
}
