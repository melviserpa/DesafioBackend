using System;
using System.Collections.Generic;

namespace Investimentos.Custodia.Domain.Entities
{
    public class Fundos
    {
        public decimal CapitalInvestido { get; private set; }
        public decimal ValorAtual { get; private set; }
        public DateTime DataResgate { get; private set; }
        public DateTime DataCompra { get; private set; }
        public decimal IOF { get; private set; }
        public string Nome { get; private set; }
        public decimal TotalTaxas { get; private set; }
        public double Quantity { get; private set; }

        public Fundos(decimal capitalInvestido, decimal valorAtual, DateTime dataResgate, DateTime dataCompra, decimal iOF, string nome, decimal totalTaxas, double quantity)
        {
            CapitalInvestido = capitalInvestido;
            ValorAtual = valorAtual;
            DataResgate = dataResgate;
            DataCompra = dataCompra;
            IOF = iOF;
            Nome = nome;
            TotalTaxas = totalTaxas;
            this.Quantity = quantity;
        }
    }


    public class ListFundos
    {
        public ListFundos(List<Fundos> fundos)
        {
            Fundos = fundos ?? new List<Fundos>();
        }

        public List<Fundos> Fundos { get; private set; }
    }
}
