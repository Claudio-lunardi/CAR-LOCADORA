using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Cliente
{
    public class Cliente : ICliente
    {

        #region Chamando Interface

        private readonly EntityContext _entityContext;
        public Cliente(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }
        #endregion

        public async Task AlterarCliente(ClientesModel clientesModel)
        {
            clientesModel.DataAlteracao = DateTime.Now;
            _entityContext.Clientes.Update(clientesModel);
         await _entityContext.SaveChangesAsync();
        }

        public async Task IncluirCliente(ClientesModel clientesModel)
        {
            clientesModel.DataInclusao = DateTime.Now;
            _entityContext.Clientes.Add(clientesModel);
            _entityContext.SaveChanges();
        }  
        public async Task<List<ClientesModel>> ListaClientes()
        {
           return await _entityContext.Clientes.OrderBy(nome => nome.Nome).ToListAsync();
        }

        public async Task<ClientesModel> ListaUmCliente(string cpf)
        {
            return await _entityContext.Clientes.SingleAsync(x => x.CPF.Equals(cpf));
        }

    }
}
