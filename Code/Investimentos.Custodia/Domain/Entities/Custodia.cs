using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Investimentos.Custodia.Domain.Entities
{
    /// <summary>
    /// Problemas com System.text.json, não reconhece a herança e não chama o construtor para realizar a desserialização do json.
    /// Newtonsoft funciona...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ListaCustodia<T> : IListaCustodia
        where T : ICustodia
    {
        [JsonPropertyName("TDs")]
        public abstract List<T> Entities { get; set; }

        public ListaCustodia(List<T> entities)
        {
            Entities = entities ?? new List<T>();
        }

        public ListaInvestimentos CalculaInvestimentos(decimal taxaIR)
        {
            ListaInvestimentos listaInvestimentos = null;

            foreach (var item in Entities)
            {
                listaInvestimentos = item.CalculaInvestimento(taxaIR);
            }

            return listaInvestimentos;
        }
    }
}
