using Adset.Veiculos.Dominio.Interfaces;
using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Api.Infraestrutura.Repositorios
{
    public class ItemOpicionalRepository : IItemOpicionalRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemOpicionalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemOpicional>> ObterTodosAsync()
        {
            return await _context.ItensOpicionais.ToListAsync();
        }

        public async Task<ItemOpicional?> ObterPorIdAsync(int id)
        {
            return await _context.ItensOpicionais.FindAsync(id);
        }

        public async Task AdicionarAsync(ItemOpicional item)
        {
            await _context.ItensOpicionais.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ItemOpicional item)
        {
            _context.ItensOpicionais.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(ItemOpicional item)
        {
            _context.ItensOpicionais.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
