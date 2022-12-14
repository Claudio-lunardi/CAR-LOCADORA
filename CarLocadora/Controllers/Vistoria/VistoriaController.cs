using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Vistoria;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Vistoria
{
    public class VistoriaController : Controller
    {
        #region CONSTRUTORES
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;
        private readonly HttpClient _httpClient;

        public VistoriaController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken, IHttpClientFactory httpClient)
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



                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
                HttpResponseMessage response =await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<VistoriaModel>>(json));
                }
                else
                {
                    throw new Exception("Erro ao tentar carregar vistoria!");
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


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());

            HttpResponseMessage response =await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria/ObterUmaVistoria?valor={valor}");

            if (response.IsSuccessStatusCode)
            {              
                string conteudo =await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<VistoriaModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro ao tentar carregar vistoria");
            }
            
        }

        #endregion

        #region Post
        public async Task<ActionResult> Create()
        {
            ViewBag.locacoes = await CarregarLocacao();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] VistoriaModel vistoriaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
                    HttpResponseMessage response =await _httpClient.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria", vistoriaModel);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar incluir vistoria!");
                    }
                }
                else
                {
                    ViewBag.locacoes =await CarregarLocacao();
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


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());

            HttpResponseMessage response =await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria/ObterUmaVistoria?valor={valor}");

            if (response.IsSuccessStatusCode)
            {
                ViewBag.locacoes =await CarregarLocacao();
                string conteudo =await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<VistoriaModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro ao tentar carregar vistoria!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm]VistoriaModel vistoriaModel)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());

                    HttpResponseMessage response =await _httpClient.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria", vistoriaModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro editado", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar editar vistoria!");
                    }
                }
                else
                {
                    ViewBag.locacoes =await CarregarLocacao();
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

        #region ViewBags
        private async Task<List<SelectListItem>> CarregarLocacao()
        {
            List<SelectListItem> lista = new List<SelectListItem>();


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());

            HttpResponseMessage response =await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao");

            if (response.IsSuccessStatusCode)
            {
                string conteudo =await response.Content.ReadAsStringAsync();
                List<LocacoesModel> locacao = JsonConvert.DeserializeObject<List<LocacoesModel>>(conteudo);

                foreach (var linha in locacao)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Id.ToString(),
                        Text = linha.VeiculoPlaca + " - " + linha.ClienteCPF,
                        Selected = false,
                    });
                }

                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        #endregion
    }
}
