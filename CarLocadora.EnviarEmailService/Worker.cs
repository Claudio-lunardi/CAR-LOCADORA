using System.Net.Mail;
using System.Net;
using System.Text;
using CarLocadora.Infra.RabbitMQ;
using RabbitMQ.Client;
using Newtonsoft.Json;
using CarLocadora.Modelo.Models;

namespace CarLocadora.EnviarEmailService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQFactory _rabbitMQFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(RabbitMQFactory rabbitMQFactory, ILogger<Worker> logger)
        {
            _rabbitMQFactory = rabbitMQFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult retorno = canal.BasicGet("cliente", false);

                if (retorno != null)
                {
                    var dados = JsonConvert.DeserializeObject<ClientesModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    await EnviarEmail(dados.Email, dados.Nome);
                    canal.BasicAck(retorno.DeliveryTag, true);
                }
                else
                {
                    canal.BasicAck(retorno.DeliveryTag, false);
                }
              

                await Task.Delay(5000, stoppingToken);
            }
        }





        #region METODOS API
        //private async Task<List<ClientesModel>> BuscarEmailCliente()
        //{
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
        //    HttpResponseMessage r = await _httpClient.GetAsync($"{_WebConfigUrl.Value.API_WebConfig_URL}CadastroCliente/ObterListaEnviarEmail");
        //    if (r.IsSuccessStatusCode)
        //    {
        //        return JsonConvert.DeserializeObject<List<ClientesModel>>(await r.Content.ReadAsStringAsync());
        //    }
        //    else
        //    {
        //        throw new Exception(r.ReasonPhrase);
        //    }

        //}

        //private async Task EditarCampoEmailEnviado(string cpf)
        //{
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
        //    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_WebConfigUrl.Value.API_WebConfig_URL}CadastroCliente/AlterarEnvioDeEmail", cpf);

        //    if (!r.IsSuccessStatusCode)
        //    {
        //        throw new Exception(r.ReasonPhrase);
        //    }
        //}

        #endregion

        #region GERAR EMAIL
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
            StreamReader leitor = new StreamReader(@"C:\Users\Claud\source\repos\Claudio-lunardi\CAR-LOCADORA\CarLocadora.EnviarEmail\TemplateEmail\TemplateEmail.cshtml", Encoding.UTF8);
            var conteudo = leitor.ReadToEnd();
            var TemplateEmail = conteudo.Replace("Nome¢", nome);

            //StringBuilder sb = new StringBuilder();
            //sb.Append($"<p>Parabéns <b>{nome},</b></p>");
            //sb.Append($"<p>Seja muito bem-vindo a <b>CAR-LOCADORA.</b></p>");
            //sb.Append($"<p>Estamos muito felizes de você fazer parte da <b>CAR-LOCADORA</b>.</p>");
            //sb.Append($"<br>");
            //sb.Append($"<p>Grande abraço</p>");

            return TemplateEmail;
        }
        #endregion
    }
}