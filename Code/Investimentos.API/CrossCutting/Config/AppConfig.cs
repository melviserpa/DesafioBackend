namespace Investimentos.API.CrossCutting.Config
{
    public static partial class Constantes
    {
        public static class AppConfig
        {
            public const string OcultarMetodosInternos = "AppConfig:OcultarMetodosInternos";
            public const string Leitura = "AppConfig:LocalLeituraConfig";
        }
        public static class Serilog
        {
            public const string MinimumLevel = "Serilog:MinimumLevel";
        }
    }

    public class AppConfig
    {
        public bool OcultarMetodosInternos { get; set; }
        public string LocalLeituraConfig { get; set; }
    }
}
