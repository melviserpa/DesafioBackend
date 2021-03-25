
using Investimentos.Custodia.CrossCutting.Config;

namespace Investimentos.Custodia.Domain.Entities
{
    public interface ICustodia
    {
        Investimento CalculaInvestimento(BasesCalculoConfig basesCalculoR);
    }

    public interface IListaCustodia
    {
        ListaInvestimentos CalculaInvestimentos(BasesCalculoConfig basesCalculo);
    }
}
