using Adset.Veiculos.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adset.Veiculos.Dominio.Interfaces
{
    public interface IVeiculoFotoRepository
    {
        Task<IEnumerable<VeiculoFoto>> ObterTodosAsync();
        Task<VeiculoFoto?> ObterPorIdAsync(int id);
        Task AdicionarAsync(VeiculoFoto foto);
        Task AtualizarAsync(VeiculoFoto foto);
        Task RemoverAsync(VeiculoFoto foto);
    }
}
