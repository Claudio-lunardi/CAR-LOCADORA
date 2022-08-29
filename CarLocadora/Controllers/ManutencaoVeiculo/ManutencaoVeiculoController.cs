using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.ManutencaoVeiculo
{
    public class ManutencaoVeiculoController : Controller
    {
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;


        public ManutencaoVeiculoController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken)
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
                HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<ManutencaoVeiculoModel>>(json));
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


        public ActionResult Details(int valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo/ObterUmaMnutencaoVeiculo?valor={valor}").Result;


            if (response.IsSuccessStatusCode)
            {

                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<ManutencaoVeiculoModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");

            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Veiculos = await CarregarVeiculos();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();
                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
                    HttpResponseMessage response = Cliente.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo", manutencaoVeiculoModel).Result;


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

        public async Task<ActionResult> Edit(int valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo/ObterUmaMnutencaoVeiculo?valor={valor}").Result;


            if (response.IsSuccessStatusCode)
            {
                ViewBag.Veiculos = await CarregarVeiculos();
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<ManutencaoVeiculoModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] ManutencaoVeiculoModel formasDePagamentosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    HttpClient Cliente = new HttpClient();
                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
                    HttpResponseMessage response = Cliente.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo", formasDePagamentosModel).Result;

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


        public ActionResult Delete(int valor)
        {
            try
            {
                HttpClient Cliente = new HttpClient();
                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

                HttpResponseMessage response = Cliente.DeleteAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroManutencaoVeiculo?valor={valor}").Result;

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





    













        private async Task<List<SelectListItem>> CarregarVeiculos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

            HttpResponseMessage response = client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
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






    }
}
