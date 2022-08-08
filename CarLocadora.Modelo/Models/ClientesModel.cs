using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class ClientesModel : EnderecoModel
    {
        [Key]       
        [Required(ErrorMessage = "CPF é obrigatório!")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Este campo deve ter no mínimo 14 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "CNH é obrigatório!")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Este campo deve ter no mínimo 12 caracteres.")]
        public string CNH { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Este campo deve ter no mínimo 3 caracteres.")]
        public string Nome { get; set; }
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        
        [Required(ErrorMessage = "Telefone é obrigatório!")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Este campo deve ter no mínimo 15 caracteres.")]
        public string? Telefone { get; set; }
        [Required(ErrorMessage = "Celular é obrigatório!")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Este campo deve ter no mínimo 15 caracteres.")]
        public string Celular { get; set; }      
        public bool Ativo { get; set; }
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
