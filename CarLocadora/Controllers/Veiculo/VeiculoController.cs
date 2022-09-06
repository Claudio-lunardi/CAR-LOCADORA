using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Veiculo
{
    public class VeiculoController : Controller
    {
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;

        public VeiculoController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken)
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
                HttpResponseMessage response =await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<VeiculosModel>>(json));
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

        public async Task<ActionResult> Details(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo/ObterUmVeiculo?valor={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo =await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<VeiculosModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        public ActionResult Create()
        {
            ViewBag.CategoriasDeVeiculos = CarregarCategoriasDeVeiculos();

            return View();
        }



        // POST: VeiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] VeiculosModel veiculosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Client = new HttpClient();
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
                    HttpResponseMessage response =await Client.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo", veiculosModel);

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

        public async Task<ActionResult> Edit(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo/ObterUmVeiculo?valor={valor}").Result;

            if (response.IsSuccessStatusCode)
            {


                string conteudo =await response.Content.ReadAsStringAsync();

                ViewBag.CategoriasDeVeiculos = CarregarCategoriasDeVeiculos();
                return View(JsonConvert.DeserializeObject<VeiculosModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] VeiculosModel veiculosModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    HttpClient Client = new HttpClient();
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());
                    HttpResponseMessage response =await Client.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo", veiculosModel);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });
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




        private async Task<List<SelectListItem>> CarregarCategoriasDeVeiculos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",await _IApiToken.Obter());

            HttpResponseMessage response =await client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria");

            if (response.IsSuccessStatusCode)
            {
                string conteudo =await response.Content.ReadAsStringAsync();
                List<CategoriasModel> categorias = JsonConvert.DeserializeObject<List<CategoriasModel>>(conteudo);

                foreach (var linha in categorias)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Id.ToString(),
                        Text = linha.Descricao,
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

    }
}
