using Investimentos.Custodia.CrossCutting.Config;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investimentos.API.CrossCutting.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddConfigurationBackEnd(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TesouroDiretoServiceConfig>(configuration.GetSection(TesouroDiretoServiceConfig.Key));
            services.Configure<FundosServiceConfig>(configuration.GetSection(FundosServiceConfig.Key));
            services.Configure<RendaFixaServiceConfig>(configuration.GetSection(RendaFixaServiceConfig.Key));
            services.Configure<BasesCalculoConfig>(configuration.GetSection(BasesCalculoConfig.Key));
            services.Configure<ConnectionStrings>(configuration.GetSection(ConnectionStrings.Key));

            return services;
        }
    }
}
