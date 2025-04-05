using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurosCompostos.AppServices.Models
{
    public class JurosModel
    {
        public decimal CapitalInicial { get; set; }
        public decimal TaxaJuros { get; set; } 
        public int Periodo { get; set; }
    }
}
