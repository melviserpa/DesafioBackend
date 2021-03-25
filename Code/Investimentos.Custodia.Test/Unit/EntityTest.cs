using System;
using System.IO;
using System.Text.Json;

using FluentAssertions;

using Investimentos.Custodia.CrossCutting.Config;
using Investimentos.Custodia.CrossCutting.Helpers;
using Investimentos.Custodia.Domain.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Investimentos.Custodia.Test.Unit
{
    [TestClass]
    public class EntityTest
    {
        private string fundos_json;
        private string renda_fixa_json;
        private string tesouro_direto_json;
        private string investimentos_json;
        private string empty;

        [TestInitialize]
        public void TestInitialize()
        {
            fundos_json = File.ReadAllText("Unit/json-mock/fundos.json");
            renda_fixa_json = File.ReadAllText("Unit/json-mock/renda_fixa.json");
            tesouro_direto_json = File.ReadAllText("Unit/json-mock/tesouro_direto.json");
            investimentos_json = File.ReadAllText("Unit/json-mock/investimentos.json");
            empty = "{}";
        }

        #region /* Fundos */

        [TestMethod]
        public void Fundos_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListaFundos>(fundos_json, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();

            result.Entities.Should().NotBeNull();
            result.Entities.Should().HaveCount(2);

            result.Entities[0].CapitalInvestido.Should().Be(1000m);
            result.Entities[0].ValorAtual.Should().Be(1159m);
            result.Entities[0].DataResgate.Should().Be(new DateTime(2022, 10, 01));
            result.Entities[0].DataCompra.Should().Be(new DateTime(2017, 10, 01));
            result.Entities[0].IOF.Should().Be(0m);
            result.Entities[0].Nome.Should().Be("ALASKA");
            result.Entities[0].TotalTaxas.Should().Be(53.49m);
            result.Entities[0].Quantity.Should().Be(1m);

            result.Entities[1].CapitalInvestido.Should().Be(10000.0m);
            result.Entities[1].ValorAtual.Should().Be(12300.52m);
            result.Entities[1].DataResgate.Should().Be(new DateTime(2022, 11, 15));
            result.Entities[1].DataCompra.Should().Be(new DateTime(2019, 11, 15));
            result.Entities[1].IOF.Should().Be(0m);
            result.Entities[1].Nome.Should().Be("REAL");
            result.Entities[1].TotalTaxas.Should().Be(134.49m);
            result.Entities[1].Quantity.Should().Be(1m);

            result.Entities.Should().Contain(re => re.CapitalInvestido == 10000.0m);
        }

        [TestMethod]
        public void Fundos_JsonParse_Test_LoadJsonEmpty()
        {
            var result = JsonSerializer.Deserialize<ListaFundos>(empty, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.Entities.Should().NotBeNull();
            result.Entities.Should().HaveCount(0);
        }

        [TestMethod]
        public void Fundos_JsonParse_Test_ErrorLoadStringEmpty()
        {
            Action action = () => JsonSerializer.Deserialize<ListaFundos>(string.Empty, JsonHelpers.GetJsonOptions());

            action.Should()
            .ThrowExactly<JsonException>()
            .WithMessage("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");
        }

        #endregion

        #region /* TesouroDireto */

        [TestMethod]
        public void TesouroDireto_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListaTesouroDireto>(tesouro_direto_json, JsonHelpers.GetJsonOptions());

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
            var result = JsonSerializer.Deserialize<ListaTesouroDireto>(empty, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.Entities.Should().NotBeNull();
            result.Entities.Should().HaveCount(0);
        }

        [TestMethod]
        public void TesouroDireto_JsonParse_Test_ErrorLoadStringEmpty()
        {
            Action action = () => JsonSerializer.Deserialize<ListaTesouroDireto>(string.Empty, JsonHelpers.GetJsonOptions());

            action.Should()
            .ThrowExactly<JsonException>()
            .WithMessage("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");
        }

        #endregion

        #region /* RendaFixa */

        [TestMethod]
        public void RendaFixa_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListaRendaFixa>(renda_fixa_json, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();

            result.Entities.Should().NotBeNull();
            result.Entities.Should().HaveCount(2);

            result.Entities[0].CapitalInvestido.Should().Be(2000.0m);
            result.Entities[0].CapitalAtual.Should().Be(2097.85m);
            result.Entities[0].Quantidade.Should().Be(2.0m);
            result.Entities[0].Vencimento.Should().Be(new DateTime(2021, 03, 09));
            result.Entities[0].IOF.Should().Be(0m);
            result.Entities[0].OutrasTaxas.Should().Be(0m);
            result.Entities[0].Taxas.Should().Be(0m);
            result.Entities[0].Indice.Should().Be("97% do CDI");
            result.Entities[0].Tipo.Should().Be("LCI");
            result.Entities[0].Nome.Should().Be("BANCO MAXIMA");
            result.Entities[0].GuarantidoFGC.Should().Be(true);
            result.Entities[0].DataOperacao.Should().Be(new DateTime(2019, 03, 14));
            result.Entities[0].PrecoUnitario.Should().Be(1048.927450m);
            result.Entities[0].Primario.Should().Be(false);

            result.Entities[1].CapitalInvestido.Should().Be(5000.0m);
            result.Entities[1].CapitalAtual.Should().Be(5509.76m);
            result.Entities[1].Quantidade.Should().Be(1.0m);
            result.Entities[1].Vencimento.Should().Be(new DateTime(2021, 03, 09));
            result.Entities[1].IOF.Should().Be(0m);
            result.Entities[1].OutrasTaxas.Should().Be(0m);
            result.Entities[1].Taxas.Should().Be(0m);
            result.Entities[1].Indice.Should().Be("97% do CDI");
            result.Entities[1].Tipo.Should().Be("LCI");
            result.Entities[1].Nome.Should().Be("BANCO BARI");
            result.Entities[1].GuarantidoFGC.Should().Be(true);
            result.Entities[1].DataOperacao.Should().Be(new DateTime(2019, 03, 14));
            result.Entities[1].PrecoUnitario.Should().Be(2754.88m);
            result.Entities[1].Primario.Should().Be(false);
        }

        [TestMethod]
        public void RendaFixa_JsonParse_Test_LoadJsonEmpty()
        {
            var result = JsonSerializer.Deserialize<ListaRendaFixa>(empty, JsonHelpers.GetJsonOptions());

            result.Should().NotBeNull();
            result.Entities.Should().NotBeNull();
            result.Entities.Should().HaveCount(0);
        }

        [TestMethod]
        public void RendaFixa_JsonParse_Test_ErrorLoadStringEmpty()
        {
            Action action = () => JsonSerializer.Deserialize<ListaRendaFixa>(string.Empty, JsonHelpers.GetJsonOptions());

            action.Should()
            .ThrowExactly<JsonException>()
            .WithMessage("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0.");
        }

        #endregion

        #region /* Custodia */

        public class TesteCustodia : Investimentos.Custodia.Domain.Entities.Custodia
        {
            public new bool RegraPassouMetadeDaCustodia(DateTime DataDeCompra, DateTime DataDeVencimento)
            {
                return base.RegraPassouMetadeDaCustodia(DataDeCompra, DataDeVencimento);
            }

            public new bool RegraAteXMeses(DateTime DataDeVencimento, int meses = 3)
            {
                return base.RegraAteXMeses(DataDeVencimento, meses);
            }

            public override Investimento CalculaInvestimento(BasesCalculoConfig basesCalculo)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_1()
        {
            TesteCustodia custodia = new TesteCustodia();

            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(-50);
            var Vencimento = Hoje.AddDays(50);

            var result = custodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_2()
        {
            TesteCustodia custodia = new TesteCustodia();

            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(-60);
            var Vencimento = Hoje.AddDays(40);

            var result = custodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_3()
        {
            TesteCustodia custodia = new TesteCustodia(); 
            
            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(-40);
            var Vencimento = Hoje.AddDays(60);

            var result = custodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void Custodia_RegraPassouMetadeDaCustodia_Test_4()
        {
            TesteCustodia custodia = new TesteCustodia();

            var Hoje = DateTime.Today;

            var DataDeCompra = Hoje.AddDays(0);
            var Vencimento = Hoje.AddDays(0);

            var result = custodia.RegraPassouMetadeDaCustodia(DataDeCompra, Vencimento);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void Custodia_RegraAteXMeses_Test_1()
        {
            TesteCustodia custodia = new TesteCustodia();

            var Hoje = DateTime.Today;

            var DataDeVencimento = Hoje.AddMonths(3).AddDays(-1);
            var meses = 3;

            var result = custodia.RegraAteXMeses(DataDeVencimento, meses);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Custodia_RegraAteXMeses_Test_2()
        {
            TesteCustodia custodia = new TesteCustodia();

            var Hoje = DateTime.Today;

            var DataDeVencimento = Hoje.AddMonths(3).AddDays(1);
            var meses = 3;

            var result = custodia.RegraAteXMeses(DataDeVencimento, meses);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void Custodia_RegraAteXMeses_Test_3()
        {
            TesteCustodia custodia = new TesteCustodia();

            var Hoje = DateTime.Today;

            var DataDeVencimento = Hoje.AddMonths(3);
            var meses = 3;

            var result = custodia.RegraAteXMeses(DataDeVencimento, meses);

            result.Should().BeTrue();
        }

        #endregion

        #region /* Investimentos */

        private static BasesCalculoConfig BaseCalguloConfigCarregaro()
        {
            return new BasesCalculoConfig()
            {
                TaxaSobreRentabilidadeIR = new TaxaSobreRentabilidadeIR()
                {
                    TesouroDireto = 10,
                    LCI = 5,
                    Fundos = 15
                },
                RegrasDeResgate = new RegrasDeResgate()
                {
                    PorcentagemMetadeDoPrazo = 15,
                    AteXMeses = 3,
                    PorcentagemAteXMeses = 6,
                    PorcentagemOutros = 30
                }
            };
        }

        [TestMethod]
        public void Investimentos_JsonParse_Test_LoadOk()
        {
            var result = JsonSerializer.Deserialize<ListaInvestimentos>(investimentos_json, JsonHelpers.GetJsonOptions());

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
        public void Investimentos_Sumarize_TesouroDireto_Test_1_Ok()
        {
            BasesCalculoConfig basesCalculo = BaseCalguloConfigCarregaro();

            string tesouro_direto = File.ReadAllText("Unit/json-mock/tesouro_direto_teste1.json");
            var lista = JsonSerializer.Deserialize<ListaTesouroDireto>(tesouro_direto, JsonHelpers.GetJsonOptions());

            ListaInvestimentos result = lista.CalculaInvestimentos(basesCalculo);

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
        public void Investimentos_Sumarize_TesouroDireto_Test_2_Ok()
        {
            BasesCalculoConfig basesCalculo = BaseCalguloConfigCarregaro();

            var lista = JsonSerializer.Deserialize<ListaTesouroDireto>(tesouro_direto_json, JsonHelpers.GetJsonOptions());
            ListaInvestimentos result = lista.CalculaInvestimentos(basesCalculo);

            result.Should().NotBeNull();
            result.ValorTotal.Should().Be(1332.4670m);

            result.Investimentos.Should().NotBeNull();
            result.Investimentos.Should().HaveCount(2);

            result.Investimentos[0].Nome.Should().Be("Tesouro Selic 2025");
            result.Investimentos[0].ValorInvestido.Should().Be(799.4720m);
            result.Investimentos[0].ValorTotal.Should().Be(829.68m);
            result.Investimentos[0].Vencimento.Should().Be(new DateTime(2025, 03, 01));
            result.Investimentos[0].IR.Should().Be(3.0208m);
            result.Investimentos[0].ValorResgate.Should().Be(705.228m);

            result.Investimentos[1].Nome.Should().Be("Tesouro IPCA 2035");
            result.Investimentos[1].ValorInvestido.Should().Be(467.1470m);
            result.Investimentos[1].ValorTotal.Should().Be(502.787m);
            result.Investimentos[1].Vencimento.Should().Be(new DateTime(2020, 02, 01));
            result.Investimentos[1].IR.Should().Be(3.56400m);
            result.Investimentos[1].ValorResgate.Should().Be(502.787m);
        }

        [TestMethod]
        public void Investimentos_Sumarize_RendaFixa_Test_1_Ok()
        {
            BasesCalculoConfig basesCalculo = BaseCalguloConfigCarregaro();

            var lista = JsonSerializer.Deserialize<ListaRendaFixa>(renda_fixa_json, JsonHelpers.GetJsonOptions());

            ListaInvestimentos result = lista.CalculaInvestimentos(basesCalculo);

            result.Should().NotBeNull();
            result.ValorTotal.Should().Be(7607.61m);

            result.Investimentos.Should().NotBeNull();
            result.Investimentos.Should().HaveCount(2);

            result.Investimentos[0].Nome.Should().Be("LCI 97% do CDI - BANCO MAXIMA");
            result.Investimentos[0].ValorInvestido.Should().Be(2000.0m);
            result.Investimentos[0].ValorTotal.Should().Be(2097.85m);
            result.Investimentos[0].Vencimento.Should().Be(new DateTime(2021, 03, 09));
            result.Investimentos[0].IR.Should().Be(9.785m);
            result.Investimentos[0].ValorResgate.Should().Be(2097.85m);

            result.Investimentos[1].Nome.Should().Be("LCI 97% do CDI - BANCO BARI");
            result.Investimentos[1].ValorInvestido.Should().Be(5000.0m);
            result.Investimentos[1].ValorTotal.Should().Be(5509.76m);
            result.Investimentos[1].Vencimento.Should().Be(new DateTime(2021, 03, 09));
            result.Investimentos[1].IR.Should().Be(50.976m);
            result.Investimentos[1].ValorResgate.Should().Be(5509.76m);
        }

        [TestMethod]
        public void Investimentos_Sumarize_Fundos_Test_1_Ok()
        {
            BasesCalculoConfig basesCalculo = BaseCalguloConfigCarregaro();

            var lista = JsonSerializer.Deserialize<ListaFundos>(fundos_json, JsonHelpers.GetJsonOptions());

            ListaInvestimentos result = lista.CalculaInvestimentos(basesCalculo);

            result.Should().NotBeNull();
            result.ValorTotal.Should().Be(13459.5200m);

            result.Investimentos.Should().NotBeNull();
            result.Investimentos.Should().HaveCount(2);

            result.Investimentos[0].Nome.Should().Be("Fundos ALASKA");
            result.Investimentos[0].ValorInvestido.Should().Be(1000m);
            result.Investimentos[0].ValorTotal.Should().Be(1159m);
            result.Investimentos[0].Vencimento.Should().Be(new DateTime(2022, 10, 01));
            result.Investimentos[0].IR.Should().Be(15.9m);
            result.Investimentos[0].ValorResgate.Should().Be(985.15m);

            result.Investimentos[1].Nome.Should().Be("Fundos REAL");
            result.Investimentos[1].ValorInvestido.Should().Be(10000.0m);
            result.Investimentos[1].ValorTotal.Should().Be(12300.52m);
            result.Investimentos[1].Vencimento.Should().Be(new DateTime(2022, 11, 15));
            result.Investimentos[1].IR.Should().Be(230.052m);
            result.Investimentos[1].ValorResgate.Should().Be(8610.364m);
        }

        #endregion
    }
}
