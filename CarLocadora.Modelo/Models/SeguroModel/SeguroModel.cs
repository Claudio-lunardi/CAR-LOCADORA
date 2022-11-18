using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models.SeguroModel
{
    public class SeguroModel
    {
        public int locacaoId { get; set; }
        public string cpf { get; set; }
        public string cnh { get; set; }
        public string nome { get; set; }
        public DateTime dataNascimento { get; set; }
        public string? telefone { get; set; }
        public string placa { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string combustivel { get; set; }
        public DateTime dataHoraRetiradaPrevista { get; set; }
        public DateTime dataHoraDevolucaoPrevista { get; set; }

    }
}
