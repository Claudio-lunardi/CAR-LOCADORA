

using CarLocadora.Comum.Modelo;
using CarLocadora.Modelo.Models;

namespace CarLocadora.API.Login
{
    public class LoginServico
    {
        public async Task<LoginRespostaModel> Login(LoginRequisicaoModel loginRequisicaoModel)
        {
            LoginRespostaModel loginRespostaModel = new LoginRespostaModel();
            loginRespostaModel.Autenticado = false;
            loginRespostaModel.Usuario = loginRequisicaoModel.Usuario;
            loginRespostaModel.Token = "";
            loginRespostaModel.DataExpiracao = null;

            if (loginRequisicaoModel.Usuario == "UsuarioDevPratica" && loginRequisicaoModel.Senha == "SenhaDevPratica")
            {
                loginRespostaModel = new GeradorToken().GerarToken(loginRespostaModel);
            }

            return loginRespostaModel;

        }
        public async Task<LoginRespostaSeguradora> LoginSeguro(LoginRequisicaoSeguradora loginRequisicaoModel)
        {
            LoginRespostaSeguradora loginRespostaModel = new LoginRespostaSeguradora();
            loginRespostaModel.Autenticado = false;
            loginRespostaModel.Usuario = loginRequisicaoModel.Usuario;
            loginRespostaModel.Token = "";
            loginRespostaModel.DataExpiracao = null;

            if (loginRequisicaoModel.Usuario == "claudio" && loginRequisicaoModel.Senha == "RD4mYmXb30$2")
            {
                loginRespostaModel = new GeradorToken().GerarTokenSeguradora(loginRespostaModel);
            }

            return loginRespostaModel;

        }






    }
}
