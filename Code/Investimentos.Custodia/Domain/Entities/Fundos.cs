using System;
using System.Collections.Generic;

using Investimentos.Custodia.CrossCutting.Config;

namespace Investimentos.Custodia.Domain.Entities
{
    public class Fundos : ICustodia
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

        public Investimento CalculaInvestimento(BasesCalculoConfig basesCalculo)
        {
            return new Investimento(
                nome: $"Fundos {this.Nome}",
                valorInvestido: calculaValorInvestido(),
                valorTotal: ValorAtual,
                vencimento: this.DataResgate,
                iR: calculaIR(basesCalculo.TaxaSobreRentabilidadeIR.Fundos),
                valorResgate: calculaValorParaResgate()
                );
        }


        private decimal calculaValorInvestido()
        {
            return (this.CapitalInvestido * this.Quantity);
        }

        private decimal calculaValorParaResgate()
        {
            return 0m - TotalTaxas;
        }

        private decimal calculaRentabilidade()
        {
            return 0m;
        }

        private decimal calculaIR(decimal taxaIR)
        {
            return 0m;
        }
    }


    public class ListFundos : IListaCustodia
    {
        public List<Fundos> Fundos { get; private set; }

        public ListFundos(List<Fundos> fundos)
        {
            Fundos = fundos ?? new List<Fundos>();
        }

        public ListaInvestimentos CalculaInvestimentos(BasesCalculoConfig basesCalculo)
        {
            throw new NotImplementedException();
        }
    }
}