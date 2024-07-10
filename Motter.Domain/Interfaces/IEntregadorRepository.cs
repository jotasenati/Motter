using Motter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Domain.Interfaces
{
    public interface IEntregadorRepository
    {
        Task AddAsync(Entregador entregador);
        Task UpdateAsync(Entregador entregador);
        Task DeleteAsync(Entregador entregador);
        Task<Entregador> GetByIdAsync(Guid id);
        Task<IEnumerable<Entregador>> GetAllAsync(string? cnpj = null);
        bool ExistsByCNPJ(string cnpj);
        bool ExistsByNumeroCNH(string numeroCNH);
    }
}
