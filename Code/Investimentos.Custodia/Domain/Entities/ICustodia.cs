using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimentos.Custodia.Domain.Entities
{
    public interface ICustodia
    {
        Investimento CalculaInvestimento(decimal taxaIR);
    }

    public interface IListaCustodia
    {
        ListaInvestimentos CalculaInvestimentos(decimal taxaIR);
    }
}
