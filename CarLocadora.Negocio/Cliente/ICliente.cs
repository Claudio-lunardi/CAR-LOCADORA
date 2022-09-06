using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Cliente
{
    public interface ICliente
    {
        Task<List<ClientesModel>> ListaClientes();
        Task<ClientesModel> ListaUmCliente(string cpf);
        Task IncluirCliente(ClientesModel clientesModel);
        Task AlterarCliente(ClientesModel clientesModel);
    }
}
