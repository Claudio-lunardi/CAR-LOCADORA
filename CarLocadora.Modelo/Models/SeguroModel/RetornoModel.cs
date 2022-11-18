using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models.SeguroModel
{
    public class RetornoModel
    {
        public int IdLocacao { get; set; }  
        public int protocolo { get; set; }
        public string cliente { get; set; }
        public string? apolice { get; set; }
        public string status { get; set; }
        public string observacao { get; set; }
    }
}
