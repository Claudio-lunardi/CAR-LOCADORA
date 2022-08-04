using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Cliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
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




    }
}
