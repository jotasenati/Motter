using Microsoft.EntityFrameworkCore;
using Motter.Application.Interfaces;
using Motter.Domain.Entities;
using Motter.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Infrastructure.Persistence.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public LocacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Locacao locacao)
        {
            await _context.Locacoes.AddAsync(locacao);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsLocacaoAtivaPorMoto(string placa)
        {
            return !await _context.Locacoes
                .AnyAsync(l => l.Moto.Placa == placa && l.DataTerminoReal == null);
        }

        public async Task<IEnumerable<Locacao>> GetAllAsync()
        {
            return await _context.Locacoes
                        .Include(l => l.Moto)
                        .Include(l => l.Entregador)
                        .Include(l => l.PlanoLocacao).ToListAsync();
        }

        public async Task<Locacao> GetByIdAsync(Guid id)
        {
            return await _context.Locacoes
                           .Include(l => l.Moto)
                           .Include(l => l.Entregador)
                           .Include(l => l.PlanoLocacao)
                           .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task UpdateAsync(Locacao locacao)
        {
            _context.Entry(locacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<PlanoLocacao> GetPlanoLocacaoByIdAsync(int id)
        {
            return await _context.PlanoLocacao.FindAsync(id);
        }

    }
}
