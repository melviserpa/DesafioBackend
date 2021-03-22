using System;
using System.Collections.Generic;

namespace Investimentos.Custodia.Domain.Entities
{
    public class TesouroDireto
    {

        public decimal ValorInvestido { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime Vencimento { get; private set; }
        public DateTime DataDeCompra { get; private set; }
        public decimal IOF { get; private set; }
        public string Indice { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }

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
    }


    public class ListTesouroDireto
    {
        public ListTesouroDireto(List<TesouroDireto> tds)
        {
            TDs = tds ?? new List<TesouroDireto>();
        }

        public List<TesouroDireto> TDs { get; private set; }
    }
}
