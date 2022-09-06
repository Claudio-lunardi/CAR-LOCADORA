using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Controllers.Locacao
{
    public class LocacaoController : Controller
    {
        private readonly IOptions<WebConfigUrl> _UrlApi;
        private readonly IApiToken _IApiToken;

        public LocacaoController(IOptions<WebConfigUrl> urlApi, IApiToken iApiToken)
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
                Cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage response = Cliente.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return View(JsonConvert.DeserializeObject<List<LocacoesModel>>(json));
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

        public async Task<ActionResult> Details(int valor)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao/ObterUmalocacao?valor={valor}");

            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<LocacoesModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        public async Task<ActionResult> Create()
        {
            //ViewBag.CarregarLocacao = CarregarLocacaoCPF();
            //ViewBag.CarregarFormaPagamento = CarregarFormaPagamento();  
            //ViewBag.CarregarVeiculoPlaca = CarregarVeiculoPlaca();

            ViewBag.Clientes = await CarregarClientes();
            ViewBag.FormaPagamentos = await CarregarFormasDePagamento();
            ViewBag.Veiculos = await CarregarVeiculos();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] LocacoesModel locacoesModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient Client = new HttpClient();

                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage response = await Client.PostAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao", locacoesModel);


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
                    ViewBag.Clientes = await CarregarClientes();
                    ViewBag.FormaPagamentos = await CarregarFormasDePagamento();
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

        public async Task<ActionResult> Edit(int valor)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await Client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao/ObterUmalocacao?valor={valor}");

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Clientes = await CarregarClientes();
                ViewBag.FormaPagamentos = await CarregarFormasDePagamento();
                ViewBag.Veiculos = await CarregarVeiculos();

                string conteudo = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<LocacoesModel>(conteudo));
            }
            else
            {
                throw new Exception("aaa");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] LocacoesModel locacoesModel)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    HttpClient Client = new HttpClient();
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

                    HttpResponseMessage response = await Client.PutAsJsonAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroLocacao", locacoesModel);

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
                    ViewBag.Clientes = await CarregarClientes();
                    ViewBag.FormaPagamentos = await CarregarFormasDePagamento();
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

        #region ViewBags

        //private List<SelectListItem> CarregarLocacaoCPF()
        //{
        //    List<SelectListItem> lista = new List<SelectListItem>();

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

        //    HttpResponseMessage response = client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCliente").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string conteudo = response.Content.ReadAsStringAsync().Result;
        //        List<ClientesModel> locacao = JsonConvert.DeserializeObject<List<ClientesModel>>(conteudo);

        //        foreach (var linha in locacao)
        //        {
        //            lista.Add(new SelectListItem()
        //            {
        //                Value = linha.CPF,
        //                Text =  linha.Nome +" - "+ linha.CPF,
        //                Selected = false,
        //            });
        //        }

        //        return lista;
        //    }
        //    else
        //    {
        //        throw new Exception(response.ReasonPhrase);
        //    }
        //}
        //private List<SelectListItem> CarregarFormaPagamento() 
        //{ 
        //    List<SelectListItem> lista = new List<SelectListItem>();

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

        //    HttpResponseMessage response = client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string conteudo = response.Content.ReadAsStringAsync().Result;
        //        List<FormasDePagamentosModel> Pagamento = JsonConvert.DeserializeObject<List<FormasDePagamentosModel>>(conteudo);

        //        foreach (var linha in Pagamento)
        //        {
        //            lista.Add(new SelectListItem()
        //            {
        //                Value = linha.Id.ToString(),
        //                Text = linha.Descricao,
        //                Selected = false,
        //            });
        //        }

        //        return lista;
        //    }
        //    else
        //    {
        //        throw new Exception(response.ReasonPhrase);
        //    }
        //}
        //private List<SelectListItem> CarregarVeiculoPlaca()
        //{
        //    List<SelectListItem> lista = new List<SelectListItem>();

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _IApiToken.Obter());

        //    HttpResponseMessage response = client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string conteudo = response.Content.ReadAsStringAsync().Result;
        //        List<VeiculosModel> Veiculos = JsonConvert.DeserializeObject<List<VeiculosModel>>(conteudo);

        //        foreach (var linha in Veiculos)
        //        {
        //            lista.Add(new SelectListItem()
        //            {
        //                Value = linha.Placa.ToString(),
        //                Text = linha.Placa + " - " + linha.Modelo,
        //                Selected = false,
        //            });
        //        }

        //        return lista;
        //    }
        //    else
        //    {
        //        throw new Exception(response.ReasonPhrase);
        //    }
        //}

        private async Task<List<SelectListItem>> CarregarVeiculos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroVeiculo");

            if (response.IsSuccessStatusCode)
            {
                List<VeiculosModel> veiculos = JsonConvert.DeserializeObject<List<VeiculosModel>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in veiculos)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Placa,
                        Text = linha.Modelo + " - " + linha.Marca,
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
        private async Task<List<SelectListItem>> CarregarClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroCliente");

            if (response.IsSuccessStatusCode)
            {
                List<ClientesModel> Clientes = JsonConvert.DeserializeObject<List<ClientesModel>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in Clientes)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.CPF,
                        Text = linha.Nome + " - " + linha.CPF,
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
        private async Task<List<SelectListItem>> CarregarFormasDePagamento()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await client.GetAsync($"{_UrlApi.Value.API_WebConfig_URL}CadastroFormaPagamento");

            if (response.IsSuccessStatusCode)
            {
                List<FormasDePagamentosModel> formasPagamento = JsonConvert.DeserializeObject<List<FormasDePagamentosModel>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in formasPagamento)
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


        #endregion

    }
}
