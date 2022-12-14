using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Categoria
{
    [Authorize(/*Roles ="administrador"*/)]
    public class CategoriaController : Controller
    {
        #region CONSTRUTORES
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;
        private readonly HttpClient _httpClient;

        public CategoriaController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken, IHttpClientFactory httpClient)
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
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<CategoriasModel>>(json));
                }
                else
                {
                    throw new Exception("Erro ao tentar mostrar Categoria!");
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
            HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria/ObterUmaCategoria?valor={valor}");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<CategoriasModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro ao tentar mostrar Categoria!");
            }
        }
        #endregion

        #region Post
        public async Task<ActionResult> Create()
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
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria", categoriasModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro salvo!", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar incluir uma nova Categoria!");
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
            HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria/ObterUmaCategoria?valor={valor}");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<CategoriasModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro Categoria!");
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
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria", categoriasModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro editado", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar editar uma nova Categoria!");
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

        #region Delete
        public async Task<ActionResult> Delete(int valor)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria?valor={valor}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception("Erro ao deletar Categoria!");
                }

            }
            catch
            {
                return View();
            }
        }

        #endregion

    }
}
