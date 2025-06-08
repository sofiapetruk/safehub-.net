using gs.Models;
using Microsoft.EntityFrameworkCore;

namespace gs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CadastroAbrigo> Abrigos { get; set; }
        public DbSet<CadastroUsuario> Usuarios { get; set; }
        public DbSet<AbrigoOcupacao> Ocupacoes { get; set; }
        public DbSet<EstoqueAbrigo> Estoques { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Abrigo ⇄ Usuario (1:N)
            modelBuilder.Entity<CadastroAbrigo>()
                .HasMany(a => a.Usuarios)
                .WithOne(u => u.ChaveAbrigo)
                .HasForeignKey(u => u.FkIdAbrigo)
                .HasConstraintName("FK_Usuario_Abrigo");

            // Abrigo ⇄ Ocupacao (1:N)
            modelBuilder.Entity<CadastroAbrigo>()
                .HasMany(a => a.Ocupacoes)
                .WithOne(o => o.ChaveAbrigo)
                .HasForeignKey(o => o.FkIdAbrigo)
                .HasConstraintName("FK_Ocupacao_Abrigo");

            // Abrigo ⇄ Estoque (1:N)
            modelBuilder.Entity<CadastroAbrigo>()
                .HasMany(a => a.Estoques)
                .WithOne(e => e.ChaveAbrigo)
                .HasForeignKey(e => e.FkIdAbrigo)
                .HasConstraintName("FK_Estoque_Abrigo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
