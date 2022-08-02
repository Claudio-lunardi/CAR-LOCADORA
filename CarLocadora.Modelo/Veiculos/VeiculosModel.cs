using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Veiculos
{
    public class VeiculosModel
    {
        [StringLength(8)]
        public string Placa { get; set; }
        [StringLength(100)]
        public string? Chassi { get; set; }
        [StringLength(100)]
        public string Marca { get; set; }
        [StringLength(150)]
        public string Modelo { get; set; }
        [StringLength(100)]
        public string Combustivel { get; set; }
        [StringLength(100)]
        public string Cor { get; set; }
        [StringLength(2000)]
        public string? Opcionais { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
