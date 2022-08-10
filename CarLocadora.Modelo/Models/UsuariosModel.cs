using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class UsuariosModel : EnderecoModel
    {
        [Key]
        [Required(ErrorMessage = "CPF é obrigatório!")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Este campo deve ter no mínimo 14 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "RG é obrigatório!")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Este campo deve ter no mínimo 8 a 50 caracteres.")]
        public string RG { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Este campo deve ter no mínimo 3 a 150 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        [StringLength(15)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Celular é obrigatório!")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Este campo deve ter no mínimo 15 caracteres.")]
        public string Celular { get; set; }     
        public string? Senha { get; set; }

        [Required(ErrorMessage = "Ativo é obrigatório!")]
        public bool Ativo { get; set; }
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; } 
        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; } 
    }
}
