using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Interfaces
{
    public interface IImagemStorageService
    {
        Task<string> SaveImageAsync(IFormFile imagem);
        Task DeleteImageAsync(string imageUrl);
    }
}
