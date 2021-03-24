namespace Investimentos.Custodia.CrossCutting.Config
{
    public abstract class ServicesConfig
    {
        public string BaseAddress { get; set; }
        public string EndpointUrn { get; set; }
        public int Timeout { get; set; }
        public string HealthCheckUrn { get; set; }
    }

    public class FundosServiceConfig : ServicesConfig
    {
        public const string Key = "Services:FundosServiceConfig";
    }

    public class TesouroDiretoServiceConfig : ServicesConfig
    {
        public const string Key = "Services:TesouroDiretoServiceConfig";
    }

    public class RendaFixaServiceConfig : ServicesConfig
    {
        public const string Key = "Services:RendaFixaServiceConfig";
    }
}
