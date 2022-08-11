using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;

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

        public void AlterarCliente(ClientesModel clientesModel)
        {
            clientesModel.DataAlteracao = DateTime.Now;
            _entityContext.Clientes.Update(clientesModel);
            _entityContext.SaveChanges();
        }

        public void IncluirCliente(ClientesModel clientesModel)
        {
            clientesModel.DataInclusao = DateTime.Now;
            _entityContext.Clientes.Add(clientesModel);
            _entityContext.SaveChanges();
        }  
        public List<ClientesModel> ListaClientes()
        {
           return _entityContext.Clientes.OrderBy(nome => nome.Nome).ToList();
        }

        public ClientesModel ListaUmCliente(string cpf)
        {
            return _entityContext.Clientes.Single(x => x.CPF.Equals(cpf));
        }

    }
}
