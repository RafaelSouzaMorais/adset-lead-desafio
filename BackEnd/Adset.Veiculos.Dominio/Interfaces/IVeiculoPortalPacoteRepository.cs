using Adset.Veiculos.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adset.Veiculos.Dominio.Interfaces
{
    public interface IVeiculoPortalPacoteRepository
    {
        Task<IEnumerable<VeiculoPortalPacote>> ObterTodosAsync();
        Task<VeiculoPortalPacote?> ObterPorIdAsync(int idVeiculo, int portal, int pacote);
        Task AdicionarAsync(VeiculoPortalPacote pacote);
        Task AtualizarAsync(VeiculoPortalPacote pacote);
        Task RemoverAsync(VeiculoPortalPacote pacote);
    }
}
