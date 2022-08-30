using CarLocadora.Modelo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Vistoria
{
    public class VistoriaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        public int LocacoesId { get; set; }
        public LocacoesModel? Locacoes { get; set; }

        [Required(ErrorMessage = "KM Saida é obrigatorio")]
        [Display(Name = "KM de saida")]
        public long KmSaida { get; set; }

        [StringLength(50, ErrorMessage = "Este campo deve ter no maximo 50 caracteres")]
        [Required(ErrorMessage = "Combustivel de saida é obrigatorio")]
        [Display(Name = "Combustivel de saida")]
        public string CombustivelSaida { get; set; }


        [StringLength(2000, ErrorMessage = "Este campo deve ter no maximo 2000 caracteres")]   
        [Display(Name = "Observação de saida")]
        public string? ObservacaoSaida { get; set; }


        [Required(ErrorMessage = "Data de retirada é obrigatorio")]
        [Display(Name = "Data de retirada")]
        public DateTime DataHoraRetiradaPatio { get; set; }

        [Required(ErrorMessage = "O campo KM Entrada é obrigatorio")]
        [Display(Name = "KM de Entrada")]
        public long? KmEntrada { get; set; }


        [StringLength(50, ErrorMessage = "Este campo deve ter 50 caracteres")]
        [Display(Name = "Combustivel de Entrada")]
        public string? CombustivelEntrada { get; set; }


        [StringLength(2000, ErrorMessage = "Este campo deve ter 2000 caracteres")]
        [Display(Name = "Observação de Entrada")]
        public string? ObservacaoEntrada { get; set; }


        [Display(Name = "Hora da devolução")]
        public DateTime? DataHoraDevolucaoPatio { get; set; }


        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}