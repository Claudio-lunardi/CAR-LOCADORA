using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarLocadora.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> Entrar(string usuario, string senha)
        {
            try
            {
                if ((usuario == "usuarioDaniel" && senha == "senhaDaniel")
                    || (usuario == "adminArrais" && senha == "senhaArrais"))
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario),
                        new Claim(ClaimTypes.Role, usuario == "usuarioDaniel" ? "usuario" : "administrador"),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return Json("OK");
                }
                else
                {
                    TempData["erroLogin"] = "Usuário ou Senha inválido!";
                    return Json("Usuário ou Senha inválido!");
                }
            }
            catch (Exception)
            {
                TempData["erroLogin"] = "Usuário ou Senha inválido!";
                return Json("Usuário ou Senha inválido!");
            }
        }
    }
}
