using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Comum.Modelo
{
    public class LoginRespostaSeguradora
    {
        public string Usuario { get; set; }
        public bool Autenticado { get; set; }
        public string Token { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}
