using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Cliente;
using CarLocadora.Negocio.Rabbit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CadastroClienteController : ControllerBase
    {
        #region Chamando Interface

        private readonly ICliente _cliente;
        private readonly IMensageria _rabbitMQ;

        public CadastroClienteController(ICliente cliente, IMensageria rabbitMQ)
        {
            _cliente = cliente;
            _rabbitMQ = rabbitMQ;
        }
        #endregion

        [HttpGet()]
        public async Task<List<ClientesModel>> ListaClientes()
        {
            return await _cliente.ListaClientes();
        }
        [HttpGet("ObterUmCliente")]
        public async Task<ClientesModel> ListaUmCliente([FromQuery] string cpf)
        {
            return await _cliente.ListaUmCliente(cpf);
        }

        [HttpPost()]
        public async Task IncluirCliente([FromBody] ClientesModel clientesModel)
        {

            await _cliente.IncluirCliente(clientesModel);
        }

        [HttpPut()]
        public async Task AlterarCliente([FromBody] ClientesModel clientesModel)
        {
            await _cliente.AlterarCliente(clientesModel);
        }

        [HttpGet("ObterListaEnviarEmail")]
        public async Task<List<ClientesModel>> GetObterListaEmail()
        {
            return await _cliente.ObterListaEnviarEmail();
        }

        [HttpPut("AlterarEnvioDeEmail")]
        public async Task AlterarEnvioDeEmail([FromBody] string cpf)
        {
            await _cliente.AlterarEnvioDeEmail(cpf);
        }

    }
}
