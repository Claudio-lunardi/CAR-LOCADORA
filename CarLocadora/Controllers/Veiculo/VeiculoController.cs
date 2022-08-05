using CarLocadora.Modelo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Veiculo
{
    public class VeiculoController : Controller
    {
        #region Index
        public async Task<ActionResult> Index()
        {
            try
            {
                HttpClient Cliente = new HttpClient();

                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = Cliente.GetAsync("https://localhost:7142/api/CadastroVeiculo").Result;

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

        // GET: VeiculoController/Details/5
        public ActionResult Details(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.GetAsync($"https://localhost:7142/api/CadastroVeiculo/ObterUmVeiculo?valor={valor}").Result;

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

        // GET: VeiculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VeiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] VeiculosModel veiculosModel)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.PostAsJsonAsync("https://localhost:7142/api/CadastroCategoria", veiculosModel).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        // GET: VeiculoController/Edit/5
        public ActionResult Edit(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.GetAsync($"https://localhost:7142/api/CadastroVeiculo/ObterUmVeiculo?valor={valor}").Result;

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

        // POST: VeiculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] VeiculosModel veiculosModel)
        {
            try
            {
                HttpClient Cliente = new HttpClient();
                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = Cliente.PutAsJsonAsync("https://localhost:7142/api/CadastroCliente", veiculosModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
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


      
        
    }
}
