using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Models
{
    public class EnderecoModel
    {
        [StringLength(50)]
        public string Logradouro { get; set; }
        [StringLength(20)]
        public string Numero { get; set; }
        [StringLength(50)]
        public string? Complemento { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [StringLength(2)]
        public string Estado { get; set; }
    }
}
