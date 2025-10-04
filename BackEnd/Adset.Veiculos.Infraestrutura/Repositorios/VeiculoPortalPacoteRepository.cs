using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Dominio.Interfaces;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Api.Infraestrutura.Repositorios
{
    public class VeiculoPortalPacoteRepository : IVeiculoPortalPacoteRepository
    {
        private readonly ApplicationDbContext _context;
        public VeiculoPortalPacoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VeiculoPortalPacote>> ObterTodosAsync()
        {
            return await _context.VeiculoPortalPacotes.ToListAsync();
        }

        public async Task<VeiculoPortalPacote?> ObterPorIdAsync(int idVeiculo, int portal, int pacote)
        {
            return await _context.VeiculoPortalPacotes.FindAsync(idVeiculo, portal, pacote);
        }

        public async Task AdicionarAsync(VeiculoPortalPacote pacote)
        {
            await _context.VeiculoPortalPacotes.AddAsync(pacote);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(VeiculoPortalPacote pacote)
        {
            _context.VeiculoPortalPacotes.Update(pacote);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(VeiculoPortalPacote pacote)
        {
            _context.VeiculoPortalPacotes.Remove(pacote);
            await _context.SaveChangesAsync();
        }
    }
}
