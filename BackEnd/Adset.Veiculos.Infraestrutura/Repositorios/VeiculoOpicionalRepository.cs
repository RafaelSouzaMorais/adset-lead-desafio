using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Dominio.Interfaces;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Api.Infraestrutura.Repositorios
{
    public class VeiculoOpicionalRepository : IVeiculoOpicionalRepository
    {
        private readonly ApplicationDbContext _context;
        public VeiculoOpicionalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VeiculoOpicional>> ObterTodosAsync()
        {
            return await _context.VeiculoOpicionais.ToListAsync();
        }

        public async Task<VeiculoOpicional?> ObterPorIdAsync(int idVeiculo, int idItemOpicional)
        {
            return await _context.VeiculoOpicionais.FindAsync(idVeiculo, idItemOpicional);
        }

        public async Task AdicionarAsync(VeiculoOpicional opcional)
        {
            await _context.VeiculoOpicionais.AddAsync(opcional);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(VeiculoOpicional opcional)
        {
            _context.VeiculoOpicionais.Update(opcional);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(VeiculoOpicional opcional)
        {
            _context.VeiculoOpicionais.Remove(opcional);
            await _context.SaveChangesAsync();
        }
    }
}
