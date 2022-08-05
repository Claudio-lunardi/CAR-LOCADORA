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
        List<UsuariosModel> ListaUsuarios();
        UsuariosModel ListaUmUsuario(string cpf);
        void IncluirUsuario(UsuariosModel usuariosModel);
        void AlterarUsuario(UsuariosModel usuariosModel);
    }
}
