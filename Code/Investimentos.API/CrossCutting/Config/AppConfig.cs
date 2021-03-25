namespace Investimentos.API.CrossCutting.Config
{
    public class AppConfig
    {
        public const string Key = "AppConfig";
        public const string OcultarMetodosInternosKey = "AppConfig:OcultarMetodosInternos";
        public const string LeituraKey = "AppConfig:LocalLeituraConfig";
        public const string SerilogLevelKey = "Serilog:MinimumLevel";

        public bool OcultarMetodosInternos { get; set; }
        public string LocalLeituraConfig { get; set; }
    }
}
