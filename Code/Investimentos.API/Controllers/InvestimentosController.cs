using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

using Investimentos.Custodia.Domain.Entities;
using Investimentos.Custodia.Infra.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetResourcesAsync([FromServices] FundosService service)
        {
            var result = await service.GetFundosAsync();

            return this.Ok(result);
        }
    }
}
