using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Motter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Entregador> Entregadores { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<PlanoLocacao> PlanoLocacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Placa).IsUnique();
            });

            modelBuilder.Entity<Entregador>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TipoCNH).IsUnique();
                entity.HasIndex(e => e.CNPJ).IsUnique();
            });

            modelBuilder.Entity<Locacao>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Locacao>()
             .HasOne(l => l.PlanoLocacao)
             .WithMany()
             .HasForeignKey(l => l.PlanoId)
             .OnDelete(DeleteBehavior.Restrict);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString, o => o.UseNetTopologySuite());
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
        }
    }
}
