using System.Net.Http;
using System.Threading.Tasks;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.Domain.Entities;

using Microsoft.Extensions.Options;

using Serilog;

namespace Investimentos.Custodia.Infra.Services
{
    public class FundosService : BaseService
    {
        public FundosService(HttpClient httpClient, IOptions<FundosServiceConfig> options)
            : base(httpClient, options.Value)
        {
        }

        public async Task<ListFundos> GetFundosAsync()
        {
            Log.Information("Obtendo custodias em Fundos");
            return await GetAsync<ListFundos>();
        }
    }
}
