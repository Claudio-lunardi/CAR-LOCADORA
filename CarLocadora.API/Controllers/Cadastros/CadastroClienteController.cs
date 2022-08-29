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
            return _cliente.ListaClientes();
        }
        [HttpGet("ObterUmCliente")]
        public ClientesModel ListaUmCliente([FromQuery] string cpf)
        {

            return _cliente.ListaUmCliente(cpf);

        }

        [HttpPost()]
        public void IncluirCliente([FromBody] ClientesModel clientesModel)
        {
            _cliente.IncluirCliente(clientesModel);
        }
        [HttpPut()]
        public void AlterarCliente([FromBody] ClientesModel clientesModel)
        {
            _cliente.AlterarCliente(clientesModel);
        }
    }
}
