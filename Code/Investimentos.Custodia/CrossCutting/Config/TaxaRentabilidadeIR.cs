using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimentos.Custodia.CrossCutting.Config
{
    public static partial class Constantes
    {
        public const string BasesCalculo = "BasesCalculo";
    }

    public class BasesCalculo
    {
        public TaxaSobreRentabilidadeIR TaxaSobreRentabilidadeIR { get; set; }
    }

    public class TaxaSobreRentabilidadeIR
    {
        public decimal TesouroDireto { get; set; }
        public decimal LCI { get; set; }
        public decimal Fundos { get; set; }
    }
}
