using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class ManutencaoVeiculoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public int Id { get; set; }

        [Display(Name = "Descrição do Serviço")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Este campo deve ter no mínimo 1 e no máximo 1000 caracteres.")]
        public string Descricao { get; set; }

        [Display(Name = "Data Serviço")]
        public DateTime DataServico { get; set; }

        [Display(Name = "Garantia (Meses)")]
        [Required(ErrorMessage = "Informe o tempo de garantia em meses.")]
        public int Garantia { get; set; }

        [Display(Name = "Valor do Serviço")]
        [Required(ErrorMessage = "O valor da diária é obrigatório.")]
        public decimal ValorServico { get; set; }

        [Display(Name = "Data de Inclusão")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }

        [Display(Name = "Placa do Veículo")]
        public string VeiculoPlaca { get; set; } = "";
        public VeiculosModel? Veiculo { get; set; }

    }
}
