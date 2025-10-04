using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Dominio.Interfaces;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Api.Infraestrutura.Repositorios
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly ApplicationDbContext _context;
        public VeiculoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Veiculo>> ObterTodosAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task<Veiculo?> ObterPorIdAsync(int id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Veiculo veiculo)
        {
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
        }
    }
}
