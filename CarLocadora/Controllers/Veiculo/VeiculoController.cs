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

                HttpClient Cliente = new HttpClient();

                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
                HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo").Result;

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

        public ActionResult Details(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo/ObterUmVeiculo?valor={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
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
        public ActionResult Create([FromForm] VeiculosModel veiculosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();
                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
                    HttpResponseMessage response = Cliente.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo", veiculosModel).Result;

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
            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo/ObterUmVeiculo?valor={valor}").Result;

            if (response.IsSuccessStatusCode)
            {


                string conteudo = response.Content.ReadAsStringAsync().Result;

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
        public ActionResult Edit([FromForm] VeiculosModel veiculosModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();
                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
                    HttpResponseMessage response = Cliente.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo", veiculosModel).Result;

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





        private List<SelectListItem> CarregarCategoriasDeVeiculos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());


            HttpResponseMessage response = client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}​CadastroCategoria").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
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
