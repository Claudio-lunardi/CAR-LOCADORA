using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;



namespace CarLocadora.EnviarEmail
{
    public class Worker : BackgroundService
    {
        private readonly ConnectionFactory _factory;
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;
        private readonly IOptions<WebConfigUrl> _WebConfigUrl;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClient, IApiToken apiToken, IOptions<WebConfigUrl> WebConfigUrl)
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            _logger = logger;
            _httpClient = httpClient.CreateClient();
            _apiToken = apiToken;
            _WebConfigUrl = WebConfigUrl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var ClienteModelRabbitMq = await ObterMensagemRabbit();        
                    try
                    {
                        await EnviarEmail(ClienteModelRabbitMq.Email, ClienteModelRabbitMq.Nome);

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                

                await Task.Delay(35000, stoppingToken);
            }
        }


        private async Task<ClienteModelRabbitMq> ObterMensagemRabbit()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_WebConfigUrl.Value.API_WebConfig_URL}CadastroCliente/ObterMensagemRabbit");

                if (r.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ClienteModelRabbitMq>(await r.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception("Erro ao obter mensagem");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //private ClienteModelRabbitMq PegarMensagem()
        //{
        //    var connectarRabbit = _factory.CreateConnection();
        //    var canal = connectarRabbit.CreateModel();

        //   var dados = new ClienteModelRabbitMq();

        //    while (true)
        //    {
        //        BasicGetResult retorno = canal.BasicGet("cliente", false);
        //        if (retorno == null)
        //        {
        //            break;

        //        }
        //        else
        //        {
        //             dados = JsonConvert.DeserializeObject<ClienteModelRabbitMq>(Encoding.UTF8.GetString(retorno.Body.ToArray()));

        //            canal.BasicAck(retorno.DeliveryTag, true);


        //        }

        //    }

        //    return dados;
        //}


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