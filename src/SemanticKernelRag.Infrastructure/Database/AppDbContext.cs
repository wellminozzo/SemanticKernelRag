using Microsoft.EntityFrameworkCore;
using SemanticKernelRag.Domain.Entities;

namespace SemanticKernelRag.Infrastructure.Database;


public class AppDbContext : DbContext
{
    public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<Chamado> Chamados => Set<Chamado>();

    public DbSet<Fatura> Faturas => Set<Fatura>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            entity.HasMany(x => x.Chamados)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId);

            entity.HasMany(x => x.Faturas)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId);
        });

        modelBuilder.Entity<Chamado>(entity =>
        {
            entity.ToTable("chamados");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Titulo)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Descricao)
                .HasColumnType("text")
                .IsRequired();

            entity.Property(x => x.Status)
                .HasMaxLength(50)
                .IsRequired();
        });

        modelBuilder.Entity<Fatura>(entity =>
        {
            entity.ToTable("faturas");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Valor)
                .HasPrecision(18, 2);
        });
    }
}
