using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CadastroUsuariosController : ControllerBase
    {
        #region Chamando Interface
        private readonly IUsuario _usuario;
        public CadastroUsuariosController(IUsuario usuario)
        {
            _usuario = usuario;
        }
        #endregion

        [HttpGet()]
        public async Task<List<UsuariosModel>> ListaClientes()
        {
            return await _usuario.ListaUsuarios();
        }
        [HttpGet("ObterUmUsuario")]
        public async Task<UsuariosModel> ListaUmUsuario([FromQuery] string cpf)
        {
            return await _usuario.ListaUmUsuario(cpf);
        }
        [HttpPost()]
        public async Task IncluirUsuario([FromBody] UsuariosModel clientesModel)
        {
            await _usuario.IncluirUsuario(clientesModel);
        }
        [HttpPut()]
        public async Task AlterarUsuario([FromBody] UsuariosModel clientesModel)
        {
            await _usuario.AlterarUsuario(clientesModel);
        }











    }
}
