using Adset.Veiculos.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adset.Veiculos.Dominio.Interfaces
{
    public interface IItemOpicionalRepository
    {
        Task<IEnumerable<ItemOpicional>> ObterTodosAsync();
        Task<ItemOpicional?> ObterPorIdAsync(int id);
        Task AdicionarAsync(ItemOpicional item);
        Task AtualizarAsync(ItemOpicional item);
        Task RemoverAsync(ItemOpicional item);
    }
}
