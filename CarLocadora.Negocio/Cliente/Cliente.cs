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

        public List<ClientesModel> ListaClientes()
        {
           return _entityContext.Clientes.OrderBy(nome => nome.Nome).ToList();
        }




    }
}
