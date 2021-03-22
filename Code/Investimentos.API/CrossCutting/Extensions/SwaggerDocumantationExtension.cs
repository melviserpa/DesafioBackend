using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Investimentos.API.CrossCutting.Extensions
{
    public static class SwaggerDocumantationExtension
    {
        public static IServiceCollection AddSwaggerDocumantation(this IServiceCollection services)
        {
            var appName = Assembly.GetEntryAssembly().GetName().Name;
            var appVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = appName + ".xml";
            var xmlPath = Path.Combine(path1: basePath, path2: fileName);

            var info = new OpenApiInfo()
            {
                Title = $"{appName} v1",
                Version = appVersion,
                Description = $"API em .Net Core (.net 5.0) {Documentation()}",
                Contact = new OpenApiContact() { Name = "Elvis Serpa", Email = "elvis.serpa@fatec.sp.gov.br" }
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerDocumantation(this IApplicationBuilder app)
        {
            var appName = Assembly.GetEntryAssembly().GetName().Name;

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } });
                c.RouteTemplate = "api/{documentName}/docs/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/api/v1/docs/swagger.json", appName);
            });

            return app;
        }

        public static string Documentation()
        {
            return @"</br>
                        <t1>Desafio Backend</t1>
                     </br>";
        }
    }
}
