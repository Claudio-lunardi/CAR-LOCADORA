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
        List<ClientesModel> ListaClientes();
        List<ClientesModel> ListaUmCliente(string cpf);
        void IncluirCliente(ClientesModel clientesModel);
        void AlterarCliente(ClientesModel clientesModel);
    }
}
