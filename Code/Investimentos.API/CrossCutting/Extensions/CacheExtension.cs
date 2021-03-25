
using System.Reflection;

using Investimentos.Custodia.CrossCutting.Config;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investimentos.API.CrossCutting.Extensions
{
    public static class CacheExtension
    {
        public static IServiceCollection AddCacheBackEnd(this IServiceCollection services, IConfiguration configuration)
        {
            var appName = Assembly.GetCallingAssembly().GetName().Name;
            var config = configuration.GetSection(ConnectionStrings.Key).Get<ConnectionStrings>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.RedisServer;
                options.InstanceName = appName;
            });

            return services;
        }
    }
}
