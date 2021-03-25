namespace Investimentos.Custodia.CrossCutting.Config
{
    public class BasesCalculoConfig
    {
        public const string Key = "BasesCalculoConfig";
        public TaxaSobreRentabilidadeIR TaxaSobreRentabilidadeIR { get; set; }
        public RegrasDeResgate RegrasDeResgate { get; set; }
    }

    public class TaxaSobreRentabilidadeIR
    {
        public const string Key = "BasesCalculoConfig:TaxaSobreRentabilidadeIR";
        public decimal TesouroDireto { get; set; }
        public decimal LCI { get; set; }
        public decimal Fundos { get; set; }
    }

    public class RegrasDeResgate
    {
        public const string Key = "BasesCalculoConfig:RegrasDeResgate";
        public decimal PorcentagemMetadeDoPrazo { get; set; }
        public int AteXMeses { get; set; }
        public decimal PorcentagemAteXMeses { get; set; }
        public decimal PorcentagemOutros { get; set; }
    }
}
