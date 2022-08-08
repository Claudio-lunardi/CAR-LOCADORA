using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class CategoriasModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Descricao { get; set; }
        public decimal ValorDiaria { get; set; }
        public bool Ativo { get; set; }     
        public DateTime DataInclusao { get; set; } = DateTime.Now;   
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
