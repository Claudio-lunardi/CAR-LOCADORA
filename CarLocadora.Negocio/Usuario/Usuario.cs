using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task AlterarUsuario(UsuariosModel usuariosModel)
        {
            usuariosModel.DataAlteracao = DateTime.Now;
            _entityContext.Usuarios.Update(usuariosModel);
           await _entityContext.SaveChangesAsync();
        }

        public async Task IncluirUsuario(UsuariosModel usuariosModel)
        {
            usuariosModel.DataInclusao = DateTime.Now;
         await   _entityContext.Usuarios.AddAsync(usuariosModel);
           await _entityContext.SaveChangesAsync();
        }

        public async Task<UsuariosModel> ListaUmUsuario(string cpf)
        {
            return await _entityContext.Usuarios.SingleAsync(x => x.CPF.Equals(cpf));
        }

        public async Task< List<UsuariosModel>> ListaUsuarios()
        {
            return await _entityContext.Usuarios.ToListAsync();
        }
    }
}
