using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.ManutencaoVeiculo
{
    public class ManutencaoVeiculoController : Controller
    {
        #region CONSTRUTORES
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;
        private readonly HttpClient _httpClient;

        public ManutencaoVeiculoController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken, IHttpClientFactory httpClient)
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
                HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<ManutencaoVeiculoModel>>(json));
                }
                else
                {
                    throw new Exception("Erro ao tentar carregar manutenção veiculo!");
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
            HttpResponseMessage response = _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo/ObterUmaMnutencaoVeiculo?valor={valor}").Result;


            if (response.IsSuccessStatusCode)
            {

                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<ManutencaoVeiculoModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro ao tentar carregar manutenção veiculo!");

            }
        }
        #endregion

        #region Post
        public async Task<IActionResult> Create()
        {
            ViewBag.Veiculos = await CarregarVeiculos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo", manutencaoVeiculoModel);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar incluir manutenção veiculo!");
                    }
                }
                else
                {
                    ViewBag.Veiculos = await CarregarVeiculos();
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
            HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo/ObterUmaMnutencaoVeiculo?valor={valor}");


            if (response.IsSuccessStatusCode)
            {
                ViewBag.Veiculos = await CarregarVeiculos();
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<ManutencaoVeiculoModel>(conteudo));
            }
            else
            {
                throw new Exception("Erro ao tentar carregar manutenção veiculo!");

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] ManutencaoVeiculoModel formasDePagamentosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo", formasDePagamentosModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro editado", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Erro ao tentar editar umanutenção veiculo!");
                    }
                }
                else
                {
                    ViewBag.Veiculos = await CarregarVeiculos();
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

                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo?valor={valor}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception("Erro ao tentar deletar manutenção veiculo!");
                }

            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region ViewBags
        private async Task<List<SelectListItem>> CarregarVeiculos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await _httpClient.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                List<VeiculosModel> Veiculos = JsonConvert.DeserializeObject<List<VeiculosModel>>(conteudo);

                foreach (var linha in Veiculos)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Placa.ToString(),
                        Text = linha.Placa + " - " + linha.Modelo,
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
