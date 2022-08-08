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
        [Required(ErrorMessage = "Celular é obrigatório!")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Este campo deve ter no mínimo 5 caracteres.")]
        public string Descricao { get; set; }
        [Display(Name = "Valor Diária")]
        public decimal ValorDiaria { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
