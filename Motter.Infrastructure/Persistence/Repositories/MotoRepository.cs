using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using Motter.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Infrastructure.Persistence.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationDbContext _context;

        public MotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ExistsByPlaca(string placa)
        {
            return _context.Motos.Any(m => m.Placa == placa);
        }
        public async Task AddAsync(Moto moto)
        {
            await _context.AddAsync(moto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Moto moto)
        {
            _context.Remove(moto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Moto>> GetAllAsync(string placa = null)
        {
            var query = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(placa))
            {
                query = query.Where(m => m.Placa.ToLower().Contains(placa.ToLower())); 
            }

            return await query.ToListAsync();
        }
        
        public async Task UpdateAsync(Moto moto)
        {
            _context.Update(moto);
            _context.SaveChanges();
        }
    }
}
