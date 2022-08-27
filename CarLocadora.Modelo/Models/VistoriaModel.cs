using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Vistoria
{
    public class VistoriaModel
    {
        public int Id { get; set; }
        public int LocacoesId { get; set; }
        public string KmSaida { get; set; }
        public string CombustivelSaida { get; set; }
        public string? ObservacaoSaida { get; set; }
        public DateTime DataHoraRetiradaPatio { get; set; }
        public string? KmEntrada { get; set; }
        public string? CombustivelEntrada { get; set; }
        public string? ObservacaoEntrada { get; set; }
        public DateTime? DataHoraDevolucaoPatio { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }








    }
}
