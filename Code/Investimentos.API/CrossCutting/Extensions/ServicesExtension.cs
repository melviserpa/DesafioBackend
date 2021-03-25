
using System;
using System.Net.Http;

using Investimentos.Custodia.Infra.Services;

using Microsoft.Extensions.DependencyInjection;

using Polly;

using Serilog;

namespace Investimentos.API.CrossCutting.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServicesBackEnd(this IServiceCollection services)
        {
            var retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(2, onRetry: (message, retryCount) =>
                {
                    string msg = $"Retentativa: {retryCount}";
                    Log.Warning(msg);
                })
            ;

            services.AddHttpClient<TesouroDiretoService>().AddPolicyHandler(retryPolicy);
            services.AddHttpClient<FundosService>().AddPolicyHandler(retryPolicy);
            services.AddHttpClient<RendaFixaService>().AddPolicyHandler(retryPolicy);

            return services;
        }
    }
}
