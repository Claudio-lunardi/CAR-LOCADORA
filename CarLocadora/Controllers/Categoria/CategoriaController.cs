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
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IOptions<LoginRespostaModel> _LoginRespostaModel;
  

        public CategoriaController(IOptions<WebConfigUrl> urlApi, IOptions<LoginRespostaModel> loginRespostaModel)
        {
            _UrlApi = urlApi;
            _LoginRespostaModel = loginRespostaModel;
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

                Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_UrlApi, _LoginRespostaModel).Obter());

                HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria").Result;

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

        








        public ActionResult Details(int valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_UrlApi, _LoginRespostaModel).Obter());

            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria/ObterUmaCategoria?valor={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
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
        public ActionResult Create([FromForm] CategoriasModel categoriasModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();

                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_UrlApi, _LoginRespostaModel).Obter());
                    HttpResponseMessage response = Cliente.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria", categoriasModel).Result;


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










        public ActionResult Edit(int valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria/ObterUmaCategoria?valor={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<CategoriasModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] CategoriasModel categoriasModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Cliente = new HttpClient();
                    Cliente.DefaultRequestHeaders.Accept.Clear();
                    Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = Cliente.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria", categoriasModel).Result;

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

                HttpResponseMessage response = Cliente.DeleteAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCategoria?valor={valor}").Result;

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
