using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using CarLocadora.Negocio.Rabbit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CarLocadora.Negocio.Cliente
{
    public class Cliente : ICliente
    {

        #region Chamando Interface

        //private readonly ConnectionFactory _factory;
        private readonly EntityContext _entityContext;

        private readonly IMensageria _rabbitMQ;
        public Cliente(EntityContext entityContext, IMensageria rabbitMQ)
        {
            _entityContext = entityContext;
            _rabbitMQ = rabbitMQ;
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

            await _entityContext.Clientes.AddAsync(clientesModel);
            await _entityContext.SaveChangesAsync();

             _rabbitMQ.EnviarMensagemRabbit(clientesModel, "emails", "");

        }
        public async Task<List<ClientesModel>> ListaClientes()
        {
            return await _entityContext.Clientes.OrderBy(nome => nome.Nome).ToListAsync();
        }

        public async Task<ClientesModel> ListaUmCliente(string cpf)
        {
            return await _entityContext.Clientes.SingleAsync(x => x.CPF.Equals(cpf));
        }

        public async Task<List<ClientesModel>> ObterListaEnviarEmail()
        {
            return await _entityContext.Clientes.Where(x => x.Email != null && x.emailEnviado.Equals(false)).ToListAsync();
        }


        public async Task AlterarEnvioDeEmail(string cpf)
        {
            var cliente = await _entityContext.Clientes.FirstAsync(x => x.CPF.Equals(cpf));
            cliente.emailEnviado = true;

            _entityContext.Clientes.Update(cliente);
            await _entityContext.SaveChangesAsync();

        }

    }
}
