using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
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
        public Cliente(EntityContext entityContext)
        {
            //_factory = new ConnectionFactory
            //{
            //    HostName = "localhost",
            //    //Port = 5672,
            //    //UserName = "guest",
            //    //Password = "guest"
            //};
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
            await _entityContext.SaveChangesAsync();

            //Publicar(clientesModel);
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

        //private void Publicar(ClientesModel clientesModel)
        //{
        //    ClienteModelRabbitMq clienteModelRabbitMq = new ClienteModelRabbitMq();
        //    clienteModelRabbitMq.Nome = clientesModel.Nome;
        //    clienteModelRabbitMq.Email = clientesModel.Email;
        //    clienteModelRabbitMq.CPF = clientesModel.CPF;

        //    var connectarRabbit = _factory.CreateConnection();
        //    var canal = connectarRabbit.CreateModel();
        //    IBasicProperties ibasicProperties = canal.CreateBasicProperties();

        //    var corpoMensagem = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clienteModelRabbitMq));

        //    //publicar na exchenge
        //    canal.BasicPublish(exchange: "emails", routingKey: "", basicProperties: ibasicProperties, body: corpoMensagem);

        //}

    }
}
