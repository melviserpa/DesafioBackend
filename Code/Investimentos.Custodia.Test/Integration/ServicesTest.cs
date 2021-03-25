
using Investimentos.Custodia.CrossCutting.Config;

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
