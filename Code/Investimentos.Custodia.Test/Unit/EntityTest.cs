using System;
using System.IO;
using System.Text.Json;

using FluentAssertions;

using Investimentos.Custodia.CrossCutting.Helpers;
using Investimentos.Custodia.Domain.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Investimentos.Custodia.Test.Unit
{
    [TestClass]
    public class EntityTest
    {
        private string fundosJson;
        private string renda_fixa;
        private string tesouro_direto;
        private string investimentos;
        private string empty;

        [TestInitialize]
        public void TestInitialize()
        {
            fundosJson = File.ReadAllText("Unit/json-mock/fundos.json");
            renda_fixa = File.ReadAllText("Unit/json-mock/renda_fixa.json");
            tesouro_direto = File.ReadAllText("Unit/json-mock/tesouro_direto.json");
            investimentos = File.ReadAllText("Unit/json-mock/investimentos.json");
            empty = "{}";
        }

        #region /* Fundos */

        [TestMethod]
        public void Fundos_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListFundos>(fundosJson, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();

            result.Fundos.Should().NotBeNull();
            result.Fundos.Should().HaveCount(2);

            result.Fundos[0].CapitalInvestido.Should().Be(1000m);
            result.Fundos[0].ValorAtual.Should().Be(1159m);
            result.Fundos[0].DataResgate.Should().Be(new DateTime(2022, 10, 01));
            result.Fundos[0].DataCompra.Should().Be(new DateTime(2017, 10, 01));
            result.Fundos[0].IOF.Should().Be(0m);
            result.Fundos[0].Nome.Should().Be("ALASKA");
            result.Fundos[0].TotalTaxas.Should().Be(53.49m);
            result.Fundos[0].Quantity.Should().Be(1m);

            result.Fundos[1].CapitalInvestido.Should().Be(10000.0m);
            result.Fundos[1].ValorAtual.Should().Be(12300.52m);
            result.Fundos[1].DataResgate.Should().Be(new DateTime(2022, 11, 15));
            result.Fundos[1].DataCompra.Should().Be(new DateTime(2019, 11, 15));
            result.Fundos[1].IOF.Should().Be(0m);
            result.Fundos[1].Nome.Should().Be("REAL");
            result.Fundos[1].TotalTaxas.Should().Be(134.49m);
            result.Fundos[1].Quantity.Should().Be(1m);

            result.Fundos.Should().Contain(re => re.CapitalInvestido == 10000.0m);
        }

        [TestMethod]
        public void Fundos_JsonParse_Test_LoadJsonEmpty()
        {
            var result = JsonSerializer.Deserialize<ListFundos>(empty, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.Fundos.Should().NotBeNull();
            result.Fundos.Should().HaveCount(0);
        }

        [TestMethod]
        public void Fundos_JsonParse_Test_ErrorLoadStringEmpty()
        {
            Action action = () => JsonSerializer.Deserialize<ListFundos>(string.Empty, JsonHelpers.GetJsonOptions());

            action.Should()
            .ThrowExactly<JsonException>()
            .WithMessage("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");
        }

        #endregion

        #region /* TesouroDireto */

        [TestMethod]
        public void TesouroDireto_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListTesouroDireto>(tesouro_direto, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();

            result.Entities.Should().NotBeNull();
            result.Entities.Should().HaveCount(2);

            result.Entities[0].ValorInvestido.Should().Be(799.4720m);
            result.Entities[0].ValorTotal.Should().Be(829.68m);
            result.Entities[0].Vencimento.Should().Be(new DateTime(2025, 03, 01));
            result.Entities[0].DataDeCompra.Should().Be(new DateTime(2015, 03, 01));
            result.Entities[0].IOF.Should().Be(0m);
            result.Entities[0].Indice.Should().Be("SELIC");
            result.Entities[0].Tipo.Should().Be("TD");
            result.Entities[0].Nome.Should().Be("Tesouro Selic 2025");

            result.Entities[1].ValorInvestido.Should().Be(467.1470m);
            result.Entities[1].ValorTotal.Should().Be(502.787m);
            result.Entities[1].Vencimento.Should().Be(new DateTime(2020, 02, 01));
            result.Entities[1].DataDeCompra.Should().Be(new DateTime(2010, 02, 10));
            result.Entities[1].IOF.Should().Be(0m);
            result.Entities[1].Indice.Should().Be("IPCA");
            result.Entities[1].Tipo.Should().Be("TD");
            result.Entities[1].Nome.Should().Be("Tesouro IPCA 2035");
        }

        [TestMethod]
        public void TesouroDireto_JsonParse_Test_LoadJsonEmpty()
        {
            var result = JsonSerializer.Deserialize<ListTesouroDireto>(empty, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.Entities.Should().NotBeNull();
            result.Entities.Should().HaveCount(0);
        }

        [TestMethod]
        public void TesouroDireto_JsonParse_Test_ErrorLoadStringEmpty()
        {
            Action action = () => JsonSerializer.Deserialize<ListTesouroDireto>(string.Empty, JsonHelpers.GetJsonOptions());

            action.Should()
            .ThrowExactly<JsonException>()
            .WithMessage("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");
        }

        #endregion

        #region /* RendaFixa */

        [TestMethod]
        public void RendaFixa_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListRendaFixa>(renda_fixa, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();

            result.LCIs.Should().NotBeNull();
            result.LCIs.Should().HaveCount(2);

            result.LCIs[0].CapitalInvestido.Should().Be(2000.0m);
            result.LCIs[0].CapitalAtual.Should().Be(2097.85m);
            result.LCIs[0].Quantidade.Should().Be(2.0m);
            result.LCIs[0].Vencimento.Should().Be(new DateTime(2021, 03, 09));
            result.LCIs[0].IOF.Should().Be(0m);
            result.LCIs[0].OutrasTaxas.Should().Be(0m);
            result.LCIs[0].Taxas.Should().Be(0m);
            result.LCIs[0].Indice.Should().Be("97% do CDI");
            result.LCIs[0].Tipo.Should().Be("LCI");
            result.LCIs[0].Nome.Should().Be("BANCO MAXIMA");
            result.LCIs[0].GuarantidoFGC.Should().Be(true);
            result.LCIs[0].DataOperacao.Should().Be(new DateTime(2019, 03, 14));
            result.LCIs[0].PrecoUnitario.Should().Be(1048.927450m);
            result.LCIs[0].Primario.Should().Be(false);

            result.LCIs[1].CapitalInvestido.Should().Be(5000.0m);
            result.LCIs[1].CapitalAtual.Should().Be(5509.76m);
            result.LCIs[1].Quantidade.Should().Be(1.0m);
            result.LCIs[1].Vencimento.Should().Be(new DateTime(2021, 03, 09));
            result.LCIs[1].IOF.Should().Be(0m);
            result.LCIs[1].OutrasTaxas.Should().Be(0m);
            result.LCIs[1].Taxas.Should().Be(0m);
            result.LCIs[1].Indice.Should().Be("97% do CDI");
            result.LCIs[1].Tipo.Should().Be("LCI");
            result.LCIs[1].Nome.Should().Be("BANCO BARI");
            result.LCIs[1].GuarantidoFGC.Should().Be(true);
            result.LCIs[1].DataOperacao.Should().Be(new DateTime(2019, 03, 14));
            result.LCIs[1].PrecoUnitario.Should().Be(2754.88m);
            result.LCIs[1].Primario.Should().Be(false);
        }

        [TestMethod]
        public void RendaFixa_JsonParse_Test_LoadJsonEmpty()
        {
            var result = JsonSerializer.Deserialize<ListRendaFixa>(empty, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.LCIs.Should().NotBeNull();
            result.LCIs.Should().HaveCount(0);
        }

        [TestMethod]
        public void RendaFixa_JsonParse_Test_ErrorLoadStringEmpty()
        {
            Action action = () => JsonSerializer.Deserialize<ListRendaFixa>(string.Empty, JsonHelpers.GetJsonOptions());

            action.Should()
            .ThrowExactly<JsonException>()
            .WithMessage("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");
        }

        #endregion

        #region /* Investimentos */

        [TestMethod]
        public void Investimentos_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListaInvestimentos>(investimentos, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.ValorTotal.Should().Be(829.68m);

            result.Investimentos.Should().NotBeNull();
            result.Investimentos.Should().HaveCount(1);

            result.Investimentos[0].Nome.Should().Be("Tesouro Selic 2025");
            result.Investimentos[0].ValorInvestido.Should().Be(799.4720m);
            result.Investimentos[0].ValorTotal.Should().Be(829.68m);
            result.Investimentos[0].Vencimento.Should().Be(new DateTime(2025, 03, 01));
            result.Investimentos[0].IR.Should().Be(3.0208m);
            result.Investimentos[0].ValorResgate.Should().Be(705.228m);
        }

        [TestMethod]
        public void Investimentos_JsonParse_Test_LoadJsonEmpty()
        {
            var result = JsonSerializer.Deserialize<ListaInvestimentos>(empty, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.Investimentos.Should().NotBeNull();
            result.Investimentos.Should().HaveCount(0);
        }

        [TestMethod]
        public void Investimentos_JsonParse_Test_ErrorLoadStringEmpty()
        {
            Action action = () => JsonSerializer.Deserialize<ListaInvestimentos>(string.Empty, JsonHelpers.GetJsonOptions());

            action.Should()
            .ThrowExactly<JsonException>()
            .WithMessage("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");
        }

        [TestMethod]
        public void Investimentos_Sumarize_TesouroDireto_Test_Ok()
        {
            string tesouro_direto = File.ReadAllText("Unit/json-mock/tesouro_direto_teste1.json");
            var listaTesouroDireto = JsonSerializer.Deserialize<ListTesouroDireto>(tesouro_direto, JsonHelpers.GetJsonOptions());
            decimal taxaIR = 10;

            ListaInvestimentos result = listaTesouroDireto.CalculaInvestimentos(taxaIR);

            result.Should().NotBeNull();
            result.ValorTotal.Should().Be(829.68m);

            result.Investimentos.Should().NotBeNull();
            result.Investimentos.Should().HaveCount(1);

            result.Investimentos[0].Nome.Should().Be("Tesouro Selic 2025");
            result.Investimentos[0].ValorInvestido.Should().Be(799.4720m);
            result.Investimentos[0].ValorTotal.Should().Be(829.68m);
            result.Investimentos[0].Vencimento.Should().Be(new DateTime(2025, 03, 01));
            result.Investimentos[0].IR.Should().Be(3.0208m);
            result.Investimentos[0].ValorResgate.Should().Be(705.228m);
        }

        #endregion

        #region /* Custodia */

         
        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_1()
        {
            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(-50);
            var Vencimento = Hoje.AddDays(50);

            var result = TesteCustodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_2()
        {
            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(-50);
            var Vencimento = Hoje.AddDays(50);

            var result = TesteCustodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_3()
        {
            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(-50);
            var Vencimento = Hoje.AddDays(50);

            var result = TesteCustodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_4()
        {
            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(0);
            var Vencimento = Hoje.AddDays(0);

            var result = TesteCustodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeTrue();
        }

        #endregion
    }

    public class TesteCustodia : Investimentos.Custodia.Domain.Entities.Custodia
    {
        public static bool RegraPassouMetadeDaCustodia(DateTime DataDeCompra, DateTime DataDeVencimento)
        {
            return RegraPassouMetadeDaCustodia(DataDeCompra, DataDeVencimento);
        }

        public override Investimento CalculaInvestimento(decimal taxaIR)
        {
            throw new NotImplementedException();
        }
    }
}
