using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.Infra.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Investimentos.Custodia.Test.Integration
{
    [TestClass]
    public class ServicesTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            IConfigurationRoot Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IConfigurationSection config = Configuration.GetSection("FundosServiceConfig");

            var services = new ServiceCollection()
               .Configure<FundosServiceConfig>(Configuration.GetSection(FundosServiceConfig.Key))
               .AddHttpClient()
               .BuildServiceProvider();
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
