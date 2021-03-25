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
               .Configure<TesouroDiretoServiceConfig>(configuration.GetSection(TesouroDiretoServiceConfig.Key))
               .Configure<RendaFixaServiceConfig>(configuration.GetSection(RendaFixaServiceConfig.Key))
               .Configure<BasesCalculoConfig>(configuration.GetSection(BasesCalculoConfig.Key))
               .BuildServiceProvider();
        }

        [TestMethod]
        public void FundosServiceConfig_Get_Test_3metodosOk()
        {
            FundosServiceConfig config = new FundosServiceConfig();
            configuration.GetSection(FundosServiceConfig.Key).Bind(config);
            config.Timeout.Should().Be(10);

            var config1 = configuration.GetSection(FundosServiceConfig.Key).Get<FundosServiceConfig>();
            config1.Timeout.Should().Be(10);

            var options = services.GetService<IOptions<FundosServiceConfig>>();
            options.Value.Timeout.Should().Be(10);
        }

        [TestMethod]
        public void FundosServiceConfig_Get_Test_Ok()
        {
            var options = services.GetService<IOptions<FundosServiceConfig>>();

            options.Value.BaseAddress.Should().Be("http://www.mocky.io/");
            options.Value.EndpointUrn.Should().Be("v2/5e342ab33000008c00d96342-URN");
            options.Value.Timeout.Should().Be(10);
            options.Value.HealthCheckUrn.Should().Be("v2/5e342ab33000008c00d96342");
        }

        [TestMethod]
        public void BasesCalculoConfig_TaxaSobreRentabilidadeIR_Get_Test_Ok()
        {
            var options = services.GetService<IOptions<BasesCalculoConfig>>();

            options.Value.TaxaSobreRentabilidadeIR.TesouroDireto.Should().Be(10m);
            options.Value.TaxaSobreRentabilidadeIR.LCI.Should().Be(5m);
            options.Value.TaxaSobreRentabilidadeIR.Fundos.Should().Be(15m);
        }

        [TestMethod]
        public void BasesCalculoConfig_RegrasDeResgate_Get_Test_Ok()
        {
            var options = services.GetService<IOptions<BasesCalculoConfig>>();

            options.Value.RegrasDeResgate.PorcentagemMetadeDoPrazo.Should().Be(15m);
            options.Value.RegrasDeResgate.AteXMeses.Should().Be(3m);
            options.Value.RegrasDeResgate.PorcentagemAteXMeses.Should().Be(6m);
            options.Value.RegrasDeResgate.Outros.Should().Be(30m);
        }
    }
}
