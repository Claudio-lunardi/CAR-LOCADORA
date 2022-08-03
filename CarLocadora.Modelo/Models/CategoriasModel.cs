using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class CategoriasModel
    {
        [Key]       
        public int Id { get; set; }
        [StringLength(100)]
        public string Descricao { get; set; }
        public decimal ValorDiaria { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
