using Motter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<Locacao> GetByIdAsync(Guid id);
        Task<IEnumerable<Locacao>> GetAllAsync();
        Task AddAsync(Locacao locacao);
        Task UpdateAsync(Locacao locacao);
        Task<PlanoLocacao> GetPlanoLocacaoByIdAsync(int id);
        Task<bool> ExistsLocacaoAtivaPorMoto(string placa);
    }
}
