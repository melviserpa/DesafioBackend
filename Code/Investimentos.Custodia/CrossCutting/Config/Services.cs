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

    public class ServiceConfig
    {
        public const string FundosService = "FundosServiceConfig";
        public const string FundosServiceKey = "Services:FundosServiceConfig";

        public const string TesouroDireto = "TesouroDiretoServiceConfig";
        public const string TesouroDiretoKey = "Services:TesouroDiretoServiceConfig";

        public const string RendaFixa = "RendaFixaServiceConfig";
        public const string RendaFixaKey = "Services:RendaFixaServiceConfig";

        public string BaseAddress { get; set; }
        public string EndpointUrn { get; set; }
        public int Timeout { get; set; }
        public string HealthCheckUrn { get; set; }
    }
}
