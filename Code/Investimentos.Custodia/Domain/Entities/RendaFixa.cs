using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Investimentos.Custodia.CrossCutting.Config;

namespace Investimentos.Custodia.Domain.Entities
{
    public class RendaFixa : Custodia
    {
        public decimal CapitalInvestido { get; private set; }
        public decimal CapitalAtual { get; private set; }
        public decimal Quantidade { get; private set; }
        public DateTime Vencimento { get; private set; }
        public decimal IOF { get; private set; }
        public decimal OutrasTaxas { get; private set; }
        public decimal Taxas { get; private set; }
        public string Indice { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }
        public bool GuarantidoFGC { get; private set; }
        public DateTime DataOperacao { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public bool Primario { get; private set; }

        public RendaFixa(decimal capitalInvestido, decimal capitalAtual, decimal quantidade, DateTime vencimento, decimal iOF, decimal outrasTaxas, decimal taxas, string indice, string tipo, string nome, bool guarantidoFGC, DateTime dataOperacao, decimal precoUnitario, bool primario)
        {
            CapitalInvestido = capitalInvestido;
            CapitalAtual = capitalAtual;
            Quantidade = quantidade;
            Vencimento = vencimento;
            IOF = iOF;
            OutrasTaxas = outrasTaxas;
            Taxas = taxas;
            Indice = indice;
            Tipo = tipo;
            Nome = nome;
            GuarantidoFGC = guarantidoFGC;
            DataOperacao = dataOperacao;
            PrecoUnitario = precoUnitario;
            Primario = primario;
        }

        private decimal CalculaIr(decimal taxaIR)
        {
            decimal Rentabilidade = this.CapitalAtual - this.CapitalInvestido;
            return Rentabilidade * (taxaIR / 100);
        }

        private decimal CalcularValorResgate(RegrasDeResgate regras)
        {
            decimal resgate = RegraDeCalculoResgate(regras, this.CapitalAtual, Vencimento, DataOperacao);
            return resgate;
        }

        public override Investimento CalculaInvestimento(BasesCalculoConfig basesCalculo)
        {
            decimal ir = CalculaIr(basesCalculo.TaxaSobreRentabilidadeIR.TesouroDireto);
            decimal valorResgate = CalcularValorResgate(basesCalculo.RegrasDeResgate);

            return new Investimento(
                nome: $"{this.Tipo} {this.Indice} - {this.Nome}",
                valorInvestido: this.CapitalInvestido,
                valorTotal: this.CapitalAtual,
                vencimento: this.Vencimento,
                iR: ir,
                valorResgate: valorResgate
                );
        }
    }



    public class ListaRendaFixa : ListaCustodia<RendaFixa>
    {
        [JsonPropertyName("LCIs")]
        public List<RendaFixa> Entities { get; private set; }

        public ListaRendaFixa(List<RendaFixa> entities) : base(entities)
            => Entities = entities ?? new List<RendaFixa>();
    }
}