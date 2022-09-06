using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Usuario
{
    public interface IUsuario
    {
        Task<List<UsuariosModel>> ListaUsuarios();
        Task<UsuariosModel> ListaUmUsuario(string cpf);
        Task IncluirUsuario(UsuariosModel usuariosModel);
        Task AlterarUsuario(UsuariosModel usuariosModel);
    }
}
