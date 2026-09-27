using LocadoraVeiculos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Fabricante> Fabricantes => Set<Fabricante>();
    public DbSet<CategoriaVeiculo> CategoriasVeiculo => Set<CategoriaVeiculo>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Aluguel> Alugueis => Set<Aluguel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fabricante>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            entity.HasIndex(x => x.Nome).IsUnique();
        });

        modelBuilder.Entity<CategoriaVeiculo>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).IsRequired().HasMaxLength(80);
            entity.Property(x => x.Descricao).HasMaxLength(300);
            entity.HasIndex(x => x.Nome).IsUnique();
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Modelo).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Placa).IsRequired().HasMaxLength(7);
            entity.HasIndex(x => x.Placa).IsUnique();
            entity.Property(x => x.ValorDiaria).HasPrecision(18, 2);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Veiculo_Ano", "[Ano] > 1900");
                t.HasCheckConstraint("CK_Veiculo_Quilometragem", "[Quilometragem] >= 0");
                t.HasCheckConstraint("CK_Veiculo_ValorDiaria", "[ValorDiaria] >= 0");
            });
            entity.HasOne(x => x.Fabricante).WithMany(x => x.Veiculos).HasForeignKey(x => x.FabricanteId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.CategoriaVeiculo).WithMany(x => x.Veiculos).HasForeignKey(x => x.CategoriaVeiculoId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Cpf).IsRequired().HasMaxLength(11);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(254);
            entity.Property(x => x.Telefone).HasMaxLength(20);
            entity.HasIndex(x => x.Cpf).IsUnique();
            entity.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Aluguel>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ValorDiaria).HasPrecision(18, 2);
            entity.Property(x => x.ValorTotal).HasPrecision(18, 2);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Aluguel_Datas", "[DataFimPrevista] > [DataInicio]");
                t.HasCheckConstraint("CK_Aluguel_DataDevolucao", "[DataDevolucao] IS NULL OR [DataDevolucao] >= [DataInicio]");
                t.HasCheckConstraint("CK_Aluguel_QuilometragemInicial", "[QuilometragemInicial] >= 0");
                t.HasCheckConstraint("CK_Aluguel_QuilometragemFinal", "[QuilometragemFinal] IS NULL OR [QuilometragemFinal] >= [QuilometragemInicial]");
                t.HasCheckConstraint("CK_Aluguel_ValorDiaria", "[ValorDiaria] > 0");
                t.HasCheckConstraint("CK_Aluguel_ValorTotal", "[ValorTotal] IS NULL OR [ValorTotal] >= 0");
            });
            entity.HasOne(x => x.Cliente).WithMany(x => x.Alugueis).HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Veiculo).WithMany(x => x.Alugueis).HasForeignKey(x => x.VeiculoId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
