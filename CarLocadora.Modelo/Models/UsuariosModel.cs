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
        public DateTime DataNascimento { get; set; }
        [StringLength(15)]
        public string? Telefone { get; set; }
        [StringLength(15)]
        public string Celular { get; set; }     
        public string? Senha { get; set; }
        public bool Ativo { get; set; }       
        public DateTime DataInclusao { get; set; } = DateTime.Now;      
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
