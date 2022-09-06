using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Cliente
{
    public class ClienteController : Controller
    {
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;

        public ClienteController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken)
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

                HttpClient Cliente = new HttpClient();

                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCliente").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<ClientesModel>>(json));
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

        #region Edit
        public async Task<ActionResult> Edit(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCliente/ObterUmCliente?cpf={valor}").Result;


            if (response.IsSuccessStatusCode)
            {

                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<ClientesModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");

            }
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] ClientesModel clientesModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    HttpClient Client = new HttpClient();
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await Client.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCliente", clientesModel);

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
        #endregion

        #region Details
        public async Task<ActionResult> Details(string valor)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage response = await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCliente/ObterUmCliente?cpf={valor}");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<ClientesModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] ClientesModel clientesModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();

                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await Cliente.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCliente", clientesModel);


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
        #endregion
    }
}
