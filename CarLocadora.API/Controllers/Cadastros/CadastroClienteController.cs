using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CadastroClienteController : ControllerBase
    {
        #region Chamando Interface
        private readonly ICliente _cliente;

        public CadastroClienteController(ICliente cliente)
        {
            _cliente = cliente;
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
    }
}
