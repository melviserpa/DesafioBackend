using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.Infra.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Investimentos.Custodia.Test.Unit
{
    [TestClass]
    public class ServicesTest
    {
        private readonly FundosService service;
        private HttpClient httpClient;


        [TestInitialize]
        public void TestInitialize()
        {
            var fundosJson = File.ReadAllText("Unit/json-mock/fundos.json");
            httpClient= A.Fake<HttpClient>();
        }

        [TestMethod]
        public void Fundos_Get_Test_Ok()
        {

        }
    }
}
