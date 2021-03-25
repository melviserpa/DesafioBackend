using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.CrossCutting.Helpers;
using Investimentos.Custodia.Domain.Entities;
using Investimentos.Custodia.Infra.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

using Serilog;

namespace Investimentos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvestimentosController : Controller
    {
        private readonly IDistributedCache cache;
        private readonly TesouroDiretoService tesouroDiretoService;
        private readonly RendaFixaService rendaFixaService;
        private readonly FundosService fundosService;
        private readonly IOptions<BasesCalculoConfig> basesCalculoConfig;

        public InvestimentosController(
            TesouroDiretoService tesouroDiretoService,
            RendaFixaService rendaFixaService,
            FundosService fundosService,
            IOptions<BasesCalculoConfig> basesCalculoConfig,
            IDistributedCache cache)
        {
            this.tesouroDiretoService = tesouroDiretoService;
            this.rendaFixaService = rendaFixaService;
            this.fundosService = fundosService;
            this.basesCalculoConfig = basesCalculoConfig;
            this.cache = cache;
        }

        private async Task<ListaInvestimentos> GetInvestimentosAsync()
        {
            var tds = tesouroDiretoService.GetFundosAsync();
            var lcis = rendaFixaService.GetFundosAsync();
            var fundos = fundosService.GetFundosAsync();

            var tdsResult = (await tds).CalculaInvestimentos(basesCalculoConfig.Value);
            var lcisResult = (await lcis).CalculaInvestimentos(basesCalculoConfig.Value);
            var fundosResult = (await fundos).CalculaInvestimentos(basesCalculoConfig.Value);

            ListaInvestimentos result = tdsResult;
            result.AddRange(lcisResult);
            result.AddRange(fundosResult);

            return result;
        }

        private async Task<ListaInvestimentos> GetInvestimentosComCacheAsync()
        {
            var cacheKey = "ListaInvestimentos";

            var jsonCache = await cache.GetStringAsync(cacheKey);

            if (jsonCache == null)
            {
                Log.Information("Cache expirado, obtendo ados dos endpoints");
                var result = await GetInvestimentosAsync();

                string json = JsonSerializer.Serialize(result, JsonHelpers.GetJsonOptions());

                var opcoesCache = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Today.AddDays(1));

                cache.SetString(cacheKey, json, opcoesCache);

                return result;
            }
            Log.Information("Obtendo os dados do Cache");
            return JsonSerializer.Deserialize<ListaInvestimentos>(jsonCache, JsonHelpers.GetJsonOptions());
        }



        /// <summary>
        /// GET api/v1/Investimentos
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna os investimentos do cliente com valor total, cálculo de IR e valor para resgate na data. 
        /// </remarks>
        /// <returns>Investimentos consolidado</returns>
        /// <response code="200">Consulta realizada com sucesso</response>
        /// <response code="400">Problema na consulta</response>
        [HttpGet("Connections")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<ListaInvestimentos>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ListaInvestimentos>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetResourcesAsync()
        {
            try
            {
                var result = await GetInvestimentosComCacheAsync();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro na soliocitação");
                return this.BadRequest();
            }

        }
    }
}
