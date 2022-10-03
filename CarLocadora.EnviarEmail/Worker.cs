using CarLocadora.Modelo.Models;
using CarLocadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;

namespace CarLocadora.EnviarEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;

        public Worker(ILogger<Worker> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("turma1@devpratica.com.br");

            SmtpClient client = new SmtpClient("smtp.kinghost.net");
            client.Port = 587;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential("turma1@devpratica.com.br", "Turma1@1");

            while (!stoppingToken.IsCancellationRequested)
            {
                var EmailCliente = await BuscarEmailCliente();

                if (EmailCliente.Any(x => x.emailEnviado == false))
{
                    foreach (var item in EmailCliente)
                    {
                        mail.To.Add(item.Email);
                    }

                    mail.Subject = "texto fixo";
                    mail.Body = "teste";

                    for (int i = 0; i < EmailCliente.Count; i++)
                    {
                        await EditarCampoEmailEnviado(EmailCliente[i]);
                    }

                    client.Send(mail);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }


        private async Task<List<ClientesModel>> BuscarEmailCliente()
        {
            HttpResponseMessage r = await _httpClient.GetAsync("https://localhost:7142/api/CadastroCliente/ObterListaEnviarEmail");
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
            clientesModel.emailEnviado = true;
            HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"https://localhost:7142/api/CadastroCliente", clientesModel);
            if (r.IsSuccessStatusCode)
            {

            }
            else
            {
                throw new Exception(r.ReasonPhrase);
            }
        }



    }
}