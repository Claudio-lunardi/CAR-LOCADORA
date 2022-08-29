using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class LocacoesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [StringLength(14,MinimumLength =14, ErrorMessage = "Este campo deve ter 14 caracteres")]
        public string ClienteCPF { get; set; }
        public ClientesModel Cliente { get; set; }




        public int FormaPagamentoId { get; set; }
        public FormasDePagamentosModel FormaPagamento { get; set; }




        public int CategoriaId { get; set; }
        public CategoriasModel Categoria { get; set; }




        
        public string? VeiculoPlaca { get; set; }
        public VeiculosModel? Veiculo { get; set; }


        [Required(ErrorMessage = "Data da reserva é obrigatório.")]
        [Display(Name = " Data da reserva")]
        public DateTime DataHoraReserva { get; set; }

        [Required(ErrorMessage = "Data da retirada é obrigatório.")]
        [Display(Name = " Data da retirada")]
        public DateTime DataHoraRetiradaPrevista { get; set; }

        [Required(ErrorMessage = "Data da devolução é obrigatório.")]
        [Display(Name = " Data da devolução")]
        public DateTime DataHoraDevolucaoPrevista { get; set; }

        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}