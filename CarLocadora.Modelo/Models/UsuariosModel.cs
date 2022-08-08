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
        [StringLength(14)]
        public string CPF { get; set; }
        [StringLength(50)]
        public string RG { get; set; }
        [StringLength(150)]
        public string Nome { get; set; }
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        [StringLength(15)]
        public string? Telefone { get; set; }
        [StringLength(15)]
        public string Celular { get; set; }     
        public string? Senha { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
