
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Investimentos.API.CrossCutting.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsBackEnd(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsBackEnd(this IApplicationBuilder app)
        {
            app.UseCors("cors");

            return app;
        }
    }
}
