using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimentos.Custodia.Domain.Entities
{
    public abstract class BaseCustodia : ICustodia
    {
        public virtual Investimento CalculaInvestimento(decimal taxaIR)
        {
            return new Investimento("", 0, 0, new DateTime(), 0, 0);
        }
    }

    public abstract class BaseListaCustodia : IListaCustodia
    {
        public virtual ListaInvestimentos CalculaInvestimentos(decimal taxaIR)
        {
            return new ListaInvestimentos(0m, new List<Investimento>());
        }
    }
}
