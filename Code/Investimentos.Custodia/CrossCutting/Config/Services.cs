namespace Investimentos.Custodia.CrossCutting.Config
{
    public static partial class Constantes
    {
        public const string FundosServiceConfig = "FundosServiceConfig";
        public const string TesouroDiretoServiceConfig = "TesouroDiretoServiceConfig";
        public const string RendaFixaServiceConfig = "RendaFixaServiceConfig";
    }

    public interface IServices
    {
        string BaseAddress { get; set; }
        string EndpointUrn { get; set; }
        int Timeout { get; set; }
        string HealthCheckUrn { get; set; }
    }

    public abstract class Services : IServices
    {
        public string BaseAddress { get; set; }
        public string EndpointUrn { get; set; }
        public int Timeout { get; set; }
        public string HealthCheckUrn { get; set; }
    }

    public class FundosServiceConfig : Services
    {
    }

    public class TesouroDiretoServiceConfig : Services
    {
    }

    public class RendaFixaServiceConfig : Services
    {
    }
}
