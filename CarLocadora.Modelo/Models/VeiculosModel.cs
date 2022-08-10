using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class VeiculosModel
    {
        [Key]     
        [Required(ErrorMessage = "Placa é obrigatório!")]
        [StringLength(8, MinimumLength = 7, ErrorMessage = "Este campo deve ter no mínimo 7 a 8 caracteres.")]
        public string Placa { get; set; }
        [StringLength(100)]
        public string? Chassi { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Marca é obrigatório!")]
        public string Marca { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage = "Modelo é obrigatório!")]
        public string Modelo { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Combustivel é obrigatório!")]
        public string Combustivel { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Cor é obrigatório!")]
        public string Cor { get; set; }
        [StringLength(2000)]
        public string? Opcionais { get; set; }
        [Required(ErrorMessage = "Ativo é obrigatório!")]
        public bool Ativo { get; set; }
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; } 
        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; } 
    }
}
