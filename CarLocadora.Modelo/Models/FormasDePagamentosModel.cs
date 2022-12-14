using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class FormasDePagamentosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descricao é obrigatório!")]
        [StringLength(150, ErrorMessage = "Este campo pode ter no máximo 150 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Ativo é obrigatório!")]
        public bool Ativo { get; set; }
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; } 
        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; } 



    }
}
