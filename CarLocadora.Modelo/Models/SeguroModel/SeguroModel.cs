using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models.SeguroModel
{
    public class SeguroModel
    {
        public int locacaoId { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string cpf { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string cnh { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string nome { get; set; }

        [Required]
        public DateTime dataNascimento { get; set; }

        [StringLength(15)]
        public string? telefone { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 7)]
        public string placa { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string marca { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 4)]
        public string modelo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string combustivel { get; set; }

        [Required]
        public DateTime dataHoraRetiradaPrevista { get; set; }
        [Required]
        public DateTime dataHoraDevolucaoPrevista { get; set; }

    }
}
