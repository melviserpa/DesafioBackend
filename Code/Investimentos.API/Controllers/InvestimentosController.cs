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
            var tds = await tesouroDiretoService.GetFundosAsync();
            var lcis = await rendaFixaService.GetFundosAsync();
            var fundos = await fundosService.GetFundosAsync();

            var tdsResult = tds.CalculaInvestimentos(basesCalculoConfig.Value);
            var lcisResult = lcis.CalculaInvestimentos(basesCalculoConfig.Value);
            var fundosResult = fundos.CalculaInvestimentos(basesCalculoConfig.Value);

            ListaInvestimentos result = tdsResult;
            result.AddRange(lcisResult);
            result.AddRange(fundosResult);

            return this.Ok(result);
        }
    }
}
