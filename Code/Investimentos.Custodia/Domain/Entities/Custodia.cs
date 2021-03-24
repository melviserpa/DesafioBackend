using System;
using System.Collections.Generic;

namespace Investimentos.Custodia.Domain.Entities
{
    public abstract class Custodia : ICustodia
    {
        internal bool RegraPassouMetadeDaCustodia(DateTime DataDeCompra, DateTime DataDeVencimento)
        {
            var Hoje = DateTime.Today;

            var diferenca = DataDeVencimento.Subtract(DataDeCompra);
            var metade = DataDeCompra.AddDays((Math.Truncate(diferenca.TotalDays / 2)));
            var passouMetade = Hoje > metade;

            return passouMetade;
        }

        public abstract Investimento CalculaInvestimento(decimal taxaIR);
    }



    public abstract class ListaCustodia<T> : IListaCustodia
        where T : ICustodia
    {
        internal List<T> Custodias { get; private set; }

        internal ListaCustodia(List<T> custodias)
            => this.Custodias = custodias ?? new List<T>();


        public ListaInvestimentos CalculaInvestimentos(decimal taxaIR)
        {
            ListaInvestimentos listaInvestimentos = null;

            foreach (var item in Custodias)
            {
                listaInvestimentos = item.CalculaInvestimento(taxaIR);
            }

            return listaInvestimentos;
        }
    }
}
