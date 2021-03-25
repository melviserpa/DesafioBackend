using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Investimentos.Custodia.CrossCutting.Config;

[assembly: InternalsVisibleTo("Investimentos.Custodia.Test")]
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

        internal bool RegraAteXMeses(DateTime DataDeVencimento, int meses = 3)
        {
            var Hoje = DateTime.Today;

            var diferenca = DataDeVencimento.AddMonths(-meses);
            var passouDaData = Hoje >= diferenca;

            return passouDaData;
        }

        internal decimal RegraDeCalculoResgate(RegrasDeResgate regras, decimal valorTotal, DateTime DataDeVencimento, DateTime DataDeCompra)
        {
            decimal resgate = valorTotal;
            decimal valorDesconto = 0;

            decimal resgateMetadeDoTempo = regras.PorcentagemMetadeDoPrazo / 100m;
            decimal reasgateXMesesParaVencer = regras.PorcentagemAteXMeses / 100;
            decimal resgateOutros = regras.PorcentagemOutros / 100;

            var Hoje = DateTime.Today;

            if (DataDeVencimento >= Hoje)
            {
                if (RegraAteXMeses(DataDeVencimento, regras.AteXMeses))
                {
                    valorDesconto = valorTotal * reasgateXMesesParaVencer;
                    resgate = valorTotal - valorDesconto;
                }
                else if (RegraPassouMetadeDaCustodia(DataDeCompra, DataDeVencimento))
                {
                    valorDesconto = valorTotal * resgateMetadeDoTempo;
                    resgate = valorTotal - valorDesconto;
                }
                else
                {
                    valorDesconto = valorTotal * resgateOutros;
                    resgate = valorTotal - valorDesconto;
                }
            }

            return resgate;
        }

        public abstract Investimento CalculaInvestimento(BasesCalculoConfig basesCalculo);
    }



    public abstract class ListaCustodia<T> : IListaCustodia
        where T : ICustodia
    {
        internal List<T> Custodias { get; private set; }

        internal ListaCustodia(List<T> custodias)
            => this.Custodias = custodias ?? new List<T>();


        public ListaInvestimentos CalculaInvestimentos(BasesCalculoConfig basesCalculo)
        {
            ListaInvestimentos listaInvestimentos = new ListaInvestimentos(0, null);

            foreach (var item in Custodias)
            {
                listaInvestimentos.Add(item.CalculaInvestimento(basesCalculo));
            }

            return listaInvestimentos;
        }
    }
}
