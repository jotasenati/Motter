using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Motter.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Infrastructure.Services
{
    public class LocalDiskImageStorageService : IImagemStorageService
    {
        private readonly ILogger<LocalDiskImageStorageService> _logger; // Adicione um logger para depuração
        private readonly IWebHostEnvironment _env;

        public LocalDiskImageStorageService(IWebHostEnvironment env, ILogger<LocalDiskImageStorageService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async Task<string> SaveImageAsync(IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
                throw new ArgumentException("Imagem inválida.");

            var uploadsFolderPath = Path.Combine(_env.ContentRootPath, "Documentos");

            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = $"{Guid.NewGuid()}.{imagem.FileName.Split('.').Last()}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imagem.CopyToAsync(stream);
                }
                _logger.LogInformation("Imagem salva em: {FilePath}", filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar a imagem.");
                throw; 
            }

            return filePath; 
        }

        public Task DeleteImageAsync(string imageUrl)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "Documentos", imageUrl.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }
    }
}
