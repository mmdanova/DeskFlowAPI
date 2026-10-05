using Microsoft.EntityFrameworkCore;
using Deskflow.Api.Models.Entities;

namespace Deskflow.Api.Data.Entities
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Chamado> Chamados => Set<Chamado>();
        public DbSet<Interacao> Interacoes => Set<Interacao>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Categoria>(e =>
            {
                e.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                e.HasMany(c => c.Chamados).WithOne(ch => ch.Categoria)
                 .HasForeignKey(ch => ch.CategoriaId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Chamado>(e =>
            {
                e.Property(c => c.Titulo).IsRequired().HasMaxLength(150);
                e.Property(c => c.Descricao).IsRequired();
                e.Property(c => c.SolicitanteNome).IsRequired().HasMaxLength(100);
                e.Property(c => c.Prioridade).HasConversion<string>().HasMaxLength(20);
                e.Property(c => c.Status).HasConversion<string>().HasMaxLength(20);
                e.HasMany(c => c.Interacoes).WithOne(i => i.Chamado)
                 .HasForeignKey(i => i.ChamadoId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<Interacao>(e =>
            {
                e.Property(i => i.Autor).IsRequired().HasMaxLength(100);
                e.Property(i => i.Mensagem).IsRequired();
            });
        }
    }

}