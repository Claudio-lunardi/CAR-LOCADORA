

using CarLocadora.API.Login;
using CarLocadora.Comum.Modelo;
using CarLocadora.Modelo.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CarLocadora.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginRespostaModel>> Login([FromBody] LoginRequisicaoModel loginRequisicaoModel)
        {
            return Ok(await new LoginServico().Login(loginRequisicaoModel));
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult<LoginRespostaSeguradora>> LoginSeguradora([FromBody] LoginRequisicaoSeguradora loginRequisicaoModel)
        //{
        //    return Ok(await new LoginServico().LoginSeguro(loginRequisicaoModel));
        //}
    }
}
