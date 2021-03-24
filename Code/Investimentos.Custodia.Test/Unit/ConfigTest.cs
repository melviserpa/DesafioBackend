using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using FluentAssertions;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.Infra.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Investimentos.Custodia.Test.Unit
{
    [TestClass]
    public class ConfigTest
    {
        private IConfigurationRoot configuration;
        private ServiceProvider services;


        [TestInitialize]
        public void TestInitialize()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services = new ServiceCollection()
               .Configure<FundosServiceConfig>(configuration.GetSection(FundosServiceConfig.Key))
               .BuildServiceProvider();
        }

        [TestMethod]
        public void Fundos_Get_Test_Ok()
        {
            FundosServiceConfig config = new FundosServiceConfig();
            configuration.GetSection(FundosServiceConfig.Key).Bind(config);
            config.Timeout.Should().Be(10);

            var config1 = configuration.GetSection(FundosServiceConfig.Key).Get<FundosServiceConfig>();
            config1.Timeout.Should().Be(10);

            var config3 = services.GetService<IOptions<FundosServiceConfig>>();
            config3.Value.Timeout.Should().Be(10);
        }
    }
}
