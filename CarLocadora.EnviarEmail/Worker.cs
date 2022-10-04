using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;

namespace CarLocadora.EnviarEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;
        private readonly IOptions<WebConfigUrl> _WebConfigUrl;

        public Worker(ILogger<Worker> logger, HttpClient httpClient, IApiToken apiToken, IOptions<WebConfigUrl> WebConfigUrl)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiToken = apiToken;
            _WebConfigUrl = WebConfigUrl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var EmailCliente = await BuscarEmailCliente();

            while (!stoppingToken.IsCancellationRequested)
            {

                foreach (var Cliente in EmailCliente)
                {
                    try
                    {
                        await EnviarEmail(Cliente.Email, Cliente.Nome);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    finally
                    {
                        for (int i = 0; i < EmailCliente.Count; i++)
                        {
                            EditarCampoEmailEnviado(EmailCliente[i]);
                        }
                      
                    }



                }

                await Task.Delay(35000, stoppingToken);
            }
        }


        private async Task<List<ClientesModel>> BuscarEmailCliente()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage r = await _httpClient.GetAsync($"{_WebConfigUrl.Value.API_WebConfig_URL}CadastroCliente/ObterListaEnviarEmail");
            if (r.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ClientesModel>>(await r.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(r.ReasonPhrase);
            }

        }

        private async Task EditarCampoEmailEnviado(ClientesModel clientesModel)
        {   
            try
            {
                clientesModel.emailEnviado = true;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_WebConfigUrl.Value.API_WebConfig_URL}CadastroCliente", clientesModel);

            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task EnviarEmail(string Email, string nome)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("Turma1@devpratica.com.br");
            message.To.Add(Email);
            message.Subject = "Bem-Vindo!";
            message.IsBodyHtml = true;
            message.Body = EmailBoasVindas(nome);

            var smtpCliente = new SmtpClient("smtp.kinghost.net")
            {
                Port = 587,
                Credentials = new NetworkCredential("Turma1@devpratica.com.br", "Senha@senha10"),
                EnableSsl = false,

            };
            smtpCliente.Send(message);
        }

        private string EmailBoasVindas(string nome)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Parabéns <b>{nome},</b></p>");
            sb.Append($"<p>Seja muito bem-vindo a <b>CAR-LOCADORA.</b></p>");
            sb.Append($"<p>Estamos muito felizes de você fazer parte da <b>CAR-LOCADORA</b>.</p>");
            sb.Append($"<br>");
            sb.Append($"<p>Grande abraço</p>");
            return sb.ToString();
        }




    }
}