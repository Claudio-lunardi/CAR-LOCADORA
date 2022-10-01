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

            while (!stoppingToken.IsCancellationRequested)
            {
                var EmailCliente = await GetEmailCliente();

                if (EmailCliente.Any(x => x.emailEnviado == false))
                {
                    foreach (var item in EmailCliente)
                    {
                        mail.To.Add(item.Email);
                    }

                    mail.Subject = "aa";
                    mail.Body = "av";

                    SmtpClient client = new SmtpClient("smtp.kinghost.net");
                    client.Port = 587;
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential("turma1@devpratica.com.br", "Turma1@1");

                    for (int i = 0; i < EmailCliente.Count; i++)
                    {
                        await EmailTrue(EmailCliente[i]);
                    }

                    client.Send(mail);
                }
           
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }


        private async Task<List<ClientesModel>> GetEmailCliente()
        {
            HttpResponseMessage r = await _httpClient.GetAsync("https://localhost:7142/api/CadastroCliente/ObterListaEnviarEmail");
            if (r.IsSuccessStatusCode)
            {
                var re = JsonConvert.DeserializeObject<List<ClientesModel>>(await r.Content.ReadAsStringAsync());
                return re;
            }
            else
            {
                return null;
            }




        }

        private async Task EmailTrue(ClientesModel clientesModel)
        {
            clientesModel.emailEnviado = true;
            HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"https://localhost:7142/api/CadastroCliente", clientesModel);
            if (r.IsSuccessStatusCode)
            {

            }
        }



    }
}