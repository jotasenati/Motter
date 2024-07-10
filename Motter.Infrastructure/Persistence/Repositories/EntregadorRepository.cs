using Microsoft.EntityFrameworkCore;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using Motter.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Infrastructure.Persistence.Repositories
{
    public class EntregadorRepository : IEntregadorRepository
    {
        private readonly ApplicationDbContext _context;

        public EntregadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Entregador entregador)
        {
            await _context.Entregadores.AddAsync(entregador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Entregador entregador)
        {
            _context.Entry(entregador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Entregador entregador)
        {
            _context.Entregadores.Remove(entregador);
            await _context.SaveChangesAsync();
        }

        public async Task<Entregador> GetByIdAsync(Guid id)
        {
            return await _context.Entregadores.FindAsync(id);
        }

        public async Task<IEnumerable<Entregador>> GetAllAsync(string? cnpj = null)
        {
            var query = _context.Entregadores.AsQueryable();

            if (!string.IsNullOrEmpty(cnpj))
            {
                query = query.Where(e => e.CNPJ == cnpj);
            }

            return await query.ToListAsync();
        }

        public bool ExistsByCNPJ(string cnpj)
        {
            return _context.Entregadores.Any(e => e.CNPJ == cnpj);
        }

        public bool ExistsByNumeroCNH(string numeroCNH)
        {
            return _context.Entregadores.Any(e => e.NumeroCNH == numeroCNH);
        }

    }
}
