using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Usuario
{
    public class Usuario : IUsuario
    {
        private readonly EntityContext _entityContext;

        public Usuario(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void AlterarUsuario(UsuariosModel usuariosModel)
        {
            _entityContext.Usuarios.Update(usuariosModel);
            _entityContext.SaveChanges();
        }

        public void IncluirUsuario(UsuariosModel usuariosModel)
        {
            _entityContext.Usuarios.Add(usuariosModel);
            _entityContext.SaveChanges();
        }

        public UsuariosModel ListaUmUsuario(string cpf)
        {
            return _entityContext.Usuarios.Single(x => x.CPF.Equals(cpf));
        }

        public List<UsuariosModel> ListaUsuarios()
        {
            return _entityContext.Usuarios.ToList();
        }
    }
}
