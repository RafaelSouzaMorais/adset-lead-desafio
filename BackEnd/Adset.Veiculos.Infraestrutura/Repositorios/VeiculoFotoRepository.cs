using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Dominio.Interfaces;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Api.Infraestrutura.Repositorios
{
    public class VeiculoFotoRepository : IVeiculoFotoRepository
    {
        private readonly ApplicationDbContext _context;
        public VeiculoFotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VeiculoFoto>> ObterTodosAsync()
        {
            return await _context.VeiculoFotos.ToListAsync();
        }

        public async Task<VeiculoFoto?> ObterPorIdAsync(int id)
        {
            return await _context.VeiculoFotos.FindAsync(id);
        }

        public async Task AdicionarAsync(VeiculoFoto foto)
        {
            await _context.VeiculoFotos.AddAsync(foto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(VeiculoFoto foto)
        {
            _context.VeiculoFotos.Update(foto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(VeiculoFoto foto)
        {
            _context.VeiculoFotos.Remove(foto);
            await _context.SaveChangesAsync();
        }
    }
}
