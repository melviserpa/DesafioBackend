using System;
using System.Collections.Generic;

namespace Investimentos.Custodia.Domain.Entities
{
    public class Investimento
    {
        public string Nome { get; private set; }
        public decimal ValorInvestido { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime Vencimento { get; private set; }
        public decimal IR { get; private set; }
        public decimal ValorResgate { get; private set; }

        public Investimento(string nome, decimal valorInvestido, decimal valorTotal, DateTime vencimento, decimal iR, decimal valorResgate)
        {
            Nome = nome;
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            Vencimento = vencimento;
            IR = iR;
            ValorResgate = valorResgate;
        }
    }

    public class ListaInvestimentos
    {
        public decimal ValorTotal { get; private set; }
        public List<Investimento> Investimentos { get; private set; }

        public ListaInvestimentos(decimal valorTotal, List<Investimento> investimentos)
        {
            ValorTotal = valorTotal;
            Investimentos = investimentos ?? new List<Investimento>();
        }
    }
}
