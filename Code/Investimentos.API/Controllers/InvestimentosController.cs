using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.Domain.Entities;
using Investimentos.Custodia.Infra.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Serilog;

namespace Investimentos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvestimentosController : Controller
    {
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
        public async Task<IActionResult> GetResourcesAsync(
            [FromServices] TesouroDiretoService tesouroDiretoService,
            [FromServices] RendaFixaService rendaFixaService,
            [FromServices] FundosService fundosService,
            [FromServices] IOptions<BasesCalculoConfig> basesCalculoConfig
            )
        {
            try
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
