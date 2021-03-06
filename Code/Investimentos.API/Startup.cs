using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Investimentos.API.CrossCutting.Extensions;
using Investimentos.API.CrossCutting.Helpers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Investimentos.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsBackEnd();
            services.AddHttpContextAccessor();
            services.AddHeaderPropagation(config => config.Headers.Add("x-correlationid", item => Guid.NewGuid().ToString()));
            services.AddSwaggerDocumantation();

            services.AddControllers(opt => opt.Conventions.Add(new ApiExplorerGetsOnlyConvention(Configuration)))
                    .AddJsonOptions(opt =>
                    {
                        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        opt.JsonSerializerOptions.IgnoreNullValues = true;
                        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        opt.JsonSerializerOptions.WriteIndented = true;
                    });

            services.AddConfigurationBackEnd(Configuration);
            services.AddCacheBackEnd(Configuration);
            services.AddHealthChecksBackEnd();
            services.AddServicesBackEnd();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseHeaderPropagation();
            app.UseForwardedHeaders();
            app.UseStatusCodePages();

            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseCorsBackEnd();
            app.UseRouting();
            app.UseSwaggerDocumantation();
            app.UseHealthChecksBackEnd();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
