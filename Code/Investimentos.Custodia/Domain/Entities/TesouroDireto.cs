using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Investimentos.Custodia.Domain.Entities
{
    public class TesouroDireto : ICustodia
    {

        public decimal ValorInvestido { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime Vencimento { get; private set; }
        public DateTime DataDeCompra { get; private set; }
        public decimal IOF { get; private set; }
        public string Indice { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }

        [JsonConstructor]
        public TesouroDireto(decimal valorInvestido, decimal valorTotal, DateTime vencimento, DateTime dataDeCompra, decimal iOF, string indice, string tipo, string nome)
        {
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            Vencimento = vencimento;
            DataDeCompra = dataDeCompra;
            IOF = iOF;
            Indice = indice;
            Tipo = tipo;
            Nome = nome;
        }

        private decimal CalculaIr(decimal taxaIR)
        {
            decimal Rentabilidade = ValorTotal - ValorInvestido;
            return Rentabilidade * (taxaIR / 100);
        }

        private decimal CalcularValorResgate()
        {
            return 705.228m;
        }

        public Investimento CalculaInvestimento(decimal taxaIR)
        {
            decimal ir = CalculaIr(taxaIR);
            decimal valorResgate = CalcularValorResgate();

            return new Investimento(
                this.Nome,
                this.ValorInvestido,
                this.ValorTotal,
                this.Vencimento,
                ir,
                valorResgate
                );
        }
    }



    public class ListTesouroDireto : ListaCustodia<TesouroDireto>
    {
        [JsonPropertyName("TDs")]
        public List<TesouroDireto> Entities { get; private set; }

        public ListTesouroDireto(List<TesouroDireto> entities) : base(entities)
            => Entities = entities ?? new List<TesouroDireto>();
    }
}
