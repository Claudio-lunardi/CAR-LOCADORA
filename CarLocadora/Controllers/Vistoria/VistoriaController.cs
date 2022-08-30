using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Negocio.Vistoria;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Vistoria
{
    public class VistoriaController : Controller
    {
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;

        public VistoriaController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken)
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
                HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<VistoriaModel>>(json));
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

        // GET: VistoriaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VistoriaController/Create
        public ActionResult Create()
        {
            ViewBag.CarregarLocacao = CarregarLocacao();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] VistoriaModel vistoriaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();

                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());
                    HttpResponseMessage response = Cliente.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria", vistoriaModel).Result;


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

        // GET: VistoriaController/Edit/5
        public ActionResult Edit(int valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria/ObterUmaVistoria?valor={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.CarregarLocacao = CarregarLocacao();
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<VistoriaModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }


        // POST: VistoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm]VistoriaModel vistoriaModel)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();
                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

                    HttpResponseMessage response = Cliente.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVistoria", vistoriaModel).Result;

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







        private List<SelectListItem> CarregarLocacao()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

            HttpResponseMessage response = client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                List<LocacoesModel> locacao = JsonConvert.DeserializeObject<List<LocacoesModel>>(conteudo);

                foreach (var linha in locacao)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Id.ToString(),
                        Text = linha.ClienteCPF,
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
