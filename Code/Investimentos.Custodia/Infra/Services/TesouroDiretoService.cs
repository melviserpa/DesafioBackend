using System.Net.Http;
using System.Threading.Tasks;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.Domain.Entities;

using Microsoft.Extensions.Options;

using Serilog;

namespace Investimentos.Custodia.Infra.Services
{
    public class TesouroDiretoService : BaseService
    {
        public TesouroDiretoService(HttpClient httpClient, IOptions<TesouroDiretoServiceConfig> options)
            : base(httpClient, options.Value)
        {
        }

        public async Task<ListaTesouroDireto> GetFundosAsync()
        {
            Log.Information("Obtendo custodias em Tesouro Direto");
            return await GetAsync<ListaTesouroDireto>();
        }
    }
}
