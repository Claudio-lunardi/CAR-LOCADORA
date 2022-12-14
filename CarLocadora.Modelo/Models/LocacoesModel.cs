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

        [StringLength(14, MinimumLength = 14, ErrorMessage = "Este campo deve ter 14 caracteres")]
        [Required(ErrorMessage = "CPF do cliente é obrigatório.")]
        [Display(Name = "CPF do cliente")]
        public string ClienteCPF { get; set; }
        public ClientesModel? Cliente { get; set; }

        [Required(ErrorMessage = "Forma de pagamento é obrigatório.")]
        [Display(Name = "Forma de pagamento")]
        public int FormaPagamentoId { get; set; }
        public FormasDePagamentosModel? FormaPagamento { get; set; }

        [Display(Name = "Placa")]
        [StringLength(8, MinimumLength = 7, ErrorMessage = "Este campo deve ter no mínimo 7 a 8 caracteres.")]
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


        #region Referente Seguro

        [Display(Name = "Seguro Apólice")]
        public Guid? SeguroApolice { get; set; }

        [Display(Name = "Seguro Aprovado")]
        public bool? SeguroAprovado { get; set; }

        [Display(Name = "Seguro Observação")]
        [StringLength(1000, ErrorMessage = "Este campo deve ter máximo 1000 caracteres.")]
        public string? SeguroObservacao { get; set; } = "";
        #endregion










    }
}