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
            return _usuario.ListaUsuarios();
        }
        [HttpGet("ObterUmUsuario")]
        public UsuariosModel ListaUmUsuario([FromQuery] string cpf)
        {

            return _usuario.ListaUmUsuario(cpf);

        }

        [HttpPost()]
        public void IncluirUsuario([FromBody] UsuariosModel clientesModel)
        {
            _usuario.IncluirUsuario(clientesModel);
        }
        [HttpPut()]
        public void AlterarUsuario([FromBody] UsuariosModel clientesModel)
        {
            _usuario.AlterarUsuario(clientesModel);
        }











    }
}
