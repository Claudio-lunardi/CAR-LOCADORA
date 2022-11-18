using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Comum.Modelo
{
    public class LoginRequisicaoSeguradora
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
