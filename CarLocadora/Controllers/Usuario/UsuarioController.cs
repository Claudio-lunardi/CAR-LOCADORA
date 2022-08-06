using CarLocadora.Modelo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Usuario
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        #region Index
        public async Task<ActionResult> Index()
        {
            try
            {
                HttpClient Cliente = new HttpClient();

                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = Cliente.GetAsync("https://localhost:7142/api/CadastroUsuarios").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<UsuariosModel>>(json));
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

        // GET: UsuarioController/Details/5
        public ActionResult Details(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.GetAsync($"https://localhost:7142/api/CadastroUsuarios/ObterUmUsuario?cpf={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<UsuariosModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UsuariosModel usuariosModel)
        {
            HttpClient Cliente = new HttpClient();

            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.PostAsJsonAsync("https://localhost:7142/api/CadastroUsuarios", usuariosModel).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(string valor)
        {
            HttpClient Cliente = new HttpClient();
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.GetAsync($"https://localhost:7142/api/CadastroUsuarios/ObterUmUsuario?cpf={valor}").Result;


            if (response.IsSuccessStatusCode)
            {

                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<UsuariosModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");

            }
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UsuariosModel usuariosModel)
        {
            try
            {
                HttpClient Cliente = new HttpClient();
                Cliente.DefaultRequestHeaders.Accept.Clear();
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = Cliente.PutAsJsonAsync("https://localhost:7142/api/CadastroUsuarios", usuariosModel).Result;

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
