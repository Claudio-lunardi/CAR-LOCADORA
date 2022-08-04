using CarLocadora.Modelo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Cliente
{
    public class ClienteController : Controller
    {
        public async Task<ActionResult> Index()
        {
            HttpClient Cliente = new HttpClient();

            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Cliente.GetAsync("https://localhost:7142/api/CadastroCliente").Result;


            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<List<ClientesModel>>(json));
            }
            else
            {
                throw new Exception("aaa");
            }
        }




    }
}
