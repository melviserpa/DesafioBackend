using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.CrossCutting.Helpers;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using Serilog;

namespace Investimentos.Custodia.Infra.Services
{
    public abstract class BaseService : IHealthCheck
    {
        protected readonly HttpClient client;
        protected readonly ServicesConfig configService;

        public BaseService(HttpClient client, ServicesConfig configService)
        {
            this.client = client;
            this.configService = configService;

            Configure();
        }

        private void Configure()
        {
            this.client.BaseAddress = new Uri(configService.BaseAddress);
            this.client.Timeout = TimeSpan.FromSeconds(configService.Timeout);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await client.GetAsync(configService.HealthCheckUrn);

            if (result.IsSuccessStatusCode)
            {
                Log.Information("Verificado saúde da API {0}{1}, está OK!. Status code {2}-{3}", configService.BaseAddress, configService.HealthCheckUrn, (int)result.StatusCode, result.StatusCode);
                return HealthCheckResult.Healthy("The API is working fine!");
            }
            else
            {
                Log.Error("API {0}{1} está indisponível. Status code {2}-{3}", configService.BaseAddress, configService.HealthCheckUrn, (int)result.StatusCode, result.StatusCode);
                return HealthCheckResult.Unhealthy("The API is DOWN!");
            }
        }

        protected async Task<T> GetAsync<T>(string route = "")
        {
            Log.Warning("Relizando requisição na url: {0}{1}{2}", configService.BaseAddress, configService.EndpointUrn, route);

            string response = await client.GetStringAsync($"{configService.EndpointUrn}{route}");

            Log.Information("Resultado: {0}", response);

            T result = JsonSerializer.Deserialize<T>(response, JsonHelpers.GetJsonOptions());

            return result;
        }
    }
}
