using System.Net.Http;
using System.Threading.Tasks;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.Domain.Entities;

using Microsoft.Extensions.Options;

using Serilog;

namespace Investimentos.Custodia.Infra.Services
{
    public class RendaFixaService : BaseService
    {
        public RendaFixaService(HttpClient httpClient, IOptions<RendaFixaServiceConfig> options)
            : base(httpClient, options.Value)
        {
        }

        public async Task<ListaRendaFixa> GetFundosAsync()
        {
            Log.Information("Obtendo custodias em Renda Fixa - LCIs");
            return await GetAsync<ListaRendaFixa>();
        }
    }
}
