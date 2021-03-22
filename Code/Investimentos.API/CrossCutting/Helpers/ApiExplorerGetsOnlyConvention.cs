
using Investimentos.API.CrossCutting.Config;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;

namespace Investimentos.API.CrossCutting.Helpers
{
    public class ApiExplorerGetsOnlyConvention : IActionModelConvention
    {
        private readonly IConfiguration configuration;

        public ApiExplorerGetsOnlyConvention(IConfiguration configuration) => this.configuration = configuration;

        public void Apply(ActionModel action)
        {
            var filtra = this.configuration[Constantes.AppConfig.OcultarMetodosInternos];

            if (filtra.ToLower() != "true")
                action.ApiExplorer.IsVisible = true;
        }
    }
}
