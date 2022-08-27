using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    internal class LocacoesModel
    {
        public int Id { get; set; }
        public string ClienteCPF { get; set; }
        public int FormaPagamentosId { get; set; }
        public int CategoriaId { get; set; }
        public DateTime DataHoraReserva { get; set; }
        public DateTime DataHoraRetiradaPrevista { get; set; }
        public DateTime DataHoraDevolucaoPrevista { get; set; }
        public string? VeiculoPlaca { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }





    }
}
