using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Investimentos.Custodia.CrossCutting.Config;

namespace Investimentos.Custodia.Domain.Entities
{
    public class Fundos : Custodia
    {
        public decimal CapitalInvestido { get; private set; }
        public decimal ValorAtual { get; private set; }
        public DateTime DataResgate { get; private set; }
        public DateTime DataCompra { get; private set; }
        public decimal IOF { get; private set; }
        public string Nome { get; private set; }
        public decimal TotalTaxas { get; private set; }
        public decimal Quantity { get; private set; }

        public Fundos(decimal capitalInvestido, decimal valorAtual, DateTime dataResgate, DateTime dataCompra, decimal iOF, string nome, decimal totalTaxas, decimal quantity)
        {
            CapitalInvestido = capitalInvestido;
            ValorAtual = valorAtual;
            DataResgate = dataResgate;
            DataCompra = dataCompra;
            IOF = iOF;
            Nome = nome;
            TotalTaxas = totalTaxas;
            Quantity = quantity;
        }

        private decimal CalculaIr(decimal taxaIR)
        {
            decimal Rentabilidade = this.ValorAtual - this.CapitalInvestido;
            return Rentabilidade * (taxaIR / 100);
        }

        private decimal CalcularValorResgate(RegrasDeResgate regras)
        {
            decimal resgate = RegraDeCalculoResgate(regras, this.ValorAtual, this.DataResgate, this.DataCompra);
            return resgate;
        }

        public override Investimento CalculaInvestimento(BasesCalculoConfig basesCalculo)
        {
            decimal ir = CalculaIr(basesCalculo.TaxaSobreRentabilidadeIR.TesouroDireto);
            decimal valorResgate = CalcularValorResgate(basesCalculo.RegrasDeResgate);

            return new Investimento(
                nome: $"Fundos {this.Nome}",
                valorInvestido: this.CapitalInvestido,
                valorTotal: this.ValorAtual,
                vencimento: this.DataResgate,
                iR: ir,
                valorResgate: valorResgate
                );
        }
    }



    public class ListaFundos : ListaCustodia<Fundos>
    {
        [JsonPropertyName("Fundos")]
        public List<Fundos> Entities { get; private set; }

        public ListaFundos(List<Fundos> entities) : base(entities)
            => Entities = entities ?? new List<Fundos>();
    }
}