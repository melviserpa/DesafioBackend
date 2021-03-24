namespace Investimentos.Custodia.CrossCutting.Config
{
    public class BasesCalculoConfig
    {
        public const string Key = "BasesCalculoConfig";
        public TaxaSobreRentabilidadeIR TaxaSobreRentabilidadeIR { get; set; }
    }

    public class TaxaSobreRentabilidadeIR
    {
        public const string Key = "BasesCalculoConfig:TaxaSobreRentabilidadeIR";
        public decimal TesouroDireto { get; set; }
        public decimal LCI { get; set; }
        public decimal Fundos { get; set; }
    }
}
