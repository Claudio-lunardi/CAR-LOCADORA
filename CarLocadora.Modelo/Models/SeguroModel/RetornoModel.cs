using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models.SeguroModel
{
    public class RetornoModel
    {
        [Required]
        public int IdLocacao { get; set; }
        [Required]
        public int protocolo { get; set; }
        [Required]
        public string cliente { get; set; }

        public Guid? apolice { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public string observacao { get; set; }
    }
}
