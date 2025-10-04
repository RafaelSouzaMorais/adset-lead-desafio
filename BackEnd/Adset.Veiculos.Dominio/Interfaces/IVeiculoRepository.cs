using Adset.Veiculos.Dominio.Entidades;

namespace Adset.Veiculos.Dominio.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<IEnumerable<Veiculo>> ObterTodosAsync();
        Task<Veiculo?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Veiculo veiculo);
        Task AtualizarAsync(Veiculo veiculo);
        Task RemoverAsync(Veiculo veiculo);
    }
}
