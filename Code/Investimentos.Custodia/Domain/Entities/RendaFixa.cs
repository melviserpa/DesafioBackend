using System;
using System.Collections.Generic;

namespace Investimentos.Custodia.Domain.Entities
{
    public class RendaFixa
    {
        public decimal CapitalInvestido { get; private set; }
        public decimal CapitalAtual { get; private set; }
        public double Quantidade { get; private set; }
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

        public RendaFixa(decimal capitalInvestido, decimal capitalAtual, double quantidade, DateTime vencimento, decimal iOF, decimal outrasTaxas, decimal taxas, string indice, string tipo, string nome, bool guarantidoFGC, DateTime dataOperacao, decimal precoUnitario, bool primario)
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
    }


    public class ListRendaFixa
    {
        public ListRendaFixa(List<RendaFixa> lcis)
        {
            LCIs = lcis ?? new List<RendaFixa>();
        }

        public List<RendaFixa> LCIs { get; private set; }
    }
}