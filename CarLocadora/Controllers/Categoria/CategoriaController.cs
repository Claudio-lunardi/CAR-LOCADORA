using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Categoria
{
    public class CategoriaController : Controller
    {
        private readonly IOptions<LoginRespostaModel> _loginRespostaModel;
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;

        public CategoriaController(IOptions<LoginRespostaModel> loginRespostaModel, IOptions<WebConfigUrl> urlApi, IApiToken iApiToken)
        {
            _loginRespostaModel = loginRespostaModel;
            _UrlApi = urlApi;
            _IApiToken = iApiToken;
        }

        public async Task<ActionResult> Index(string mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;
            try
            {
                HttpClient Cliente = new HttpClient();

                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

                HttpResponseMessage response = await Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<CategoriasModel>>(json));
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



        public async Task<ActionResult> Details(int valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria/ObterUmaCategoria?valor={valor}");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<CategoriasModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CategoriasModel categoriasModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Client = new HttpClient();

                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await Client.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria", categoriasModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro salvo!", sucesso = true });
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

        public async Task<ActionResult> Edit(int valor)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria/ObterUmaCategoria?valor={valor}");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<CategoriasModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] CategoriasModel categoriasModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();
                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

                    HttpResponseMessage response = await Cliente.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria", categoriasModel);

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


        public async Task<ActionResult> Delete(int valor)
        {
            try
            {
                HttpClient Cliente = new HttpClient();
                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

                HttpResponseMessage response = await Cliente.DeleteAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria?valor={valor}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception("aaa");
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
