
using Investimentos.Custodia.Infra.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Investimentos.API.CrossCutting.Extensions
{
    public static class HealthChecksExtension
    {
        public static IServiceCollection AddHealthChecksBackEnd(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<FundosService>("FundosServiceHealthCheck")
                ;

            return services;
        }

        public static IApplicationBuilder UseHealthChecksBackEnd(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/v1/healthcheck");

            return app;
        }
    }
}
