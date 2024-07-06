using Motter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Domain.Interfaces
{
    public interface IMotoRepository
    {
        Task<Moto> GetByIdAsync(Guid id);
        Task<IEnumerable<Moto>> GetAllAsync(string placa = null);
        Task AddAsync(Moto moto);
        Task UpdateAsync(Moto moto);
        Task DeleteAsync(Moto moto);
        bool ExistsByPlaca(string placa);
    }
}
