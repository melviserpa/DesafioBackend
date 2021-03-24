using System.Collections.Generic;

namespace Investimentos.Custodia.Domain.Entities
{
    public abstract class ListaCustodia<T> : IListaCustodia
        where T : ICustodia
    {
        protected ListaCustodia(List<T> custodias)
        {
            this.custodias = custodias ?? new List<T>();
        }

        internal List<T> custodias { get; set; }

        public ListaInvestimentos CalculaInvestimentos(decimal taxaIR)
        {
            ListaInvestimentos listaInvestimentos = null;

            foreach (var item in custodias)
            {
                listaInvestimentos = item.CalculaInvestimento(taxaIR);
            }

            return listaInvestimentos;
        }
    }
}
