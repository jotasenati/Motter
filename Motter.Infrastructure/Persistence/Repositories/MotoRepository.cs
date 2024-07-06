using Microsoft.EntityFrameworkCore;
using Motter.Domain.Interfaces;
using Motter.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
