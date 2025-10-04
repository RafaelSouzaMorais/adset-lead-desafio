using Adset.Veiculos.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adset.Veiculos.Dominio.Interfaces
{
    public interface IVeiculoOpicionalRepository
    {
        Task<IEnumerable<VeiculoOpicional>> ObterTodosAsync();
        Task<VeiculoOpicional?> ObterPorIdAsync(int idVeiculo, int idItemOpicional);
        Task AdicionarAsync(VeiculoOpicional opcional);
        Task AtualizarAsync(VeiculoOpicional opcional);
        Task RemoverAsync(VeiculoOpicional opcional);
    }
}
