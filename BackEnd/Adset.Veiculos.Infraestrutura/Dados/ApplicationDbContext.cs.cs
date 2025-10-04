using Adset.Veiculos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Infraestrutura.Dados
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<ItemOpicional> ItensOpicionais { get; set; }
        public DbSet<VeiculoOpicional> VeiculoOpicionais { get; set; }
        public DbSet<VeiculoFoto> VeiculoFotos { get; set; }
        public DbSet<VeiculoPortalPacote> VeiculoPortalPacotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Veiculo>()
                .Property(v => v.Preco)
                .HasPrecision(18, 2); // Define precisão e escala para decimal

            modelBuilder.Entity<VeiculoOpicional>()
                .HasKey(vo => new { vo.IdVeiculo, vo.IdItemOpicional });

            modelBuilder.Entity<VeiculoOpicional>()
                .HasOne(vo => vo.Veiculo)
                .WithMany(v => v.Opcionais)
                .HasForeignKey(vo => vo.IdVeiculo);

            modelBuilder.Entity<VeiculoFoto>()
                .HasOne(vf => vf.Veiculo)
                .WithMany(v => v.Fotos)
                .HasForeignKey(vf => vf.IdVeiculo);

            modelBuilder.Entity<VeiculoOpicional>()
                .HasOne(vo => vo.ItemOpicional)
                .WithMany(io => io.VeiculoOpicionais)
                .HasForeignKey(vo => vo.IdItemOpicional);

            // Corrigido: separa a configuração do índice e da chave estrangeira
            modelBuilder.Entity<VeiculoPortalPacote>()
                .HasIndex(x => new { x.IdVeiculo, x.Portal })
                .IsUnique();
            modelBuilder.Entity<VeiculoPortalPacote>()
                .HasKey(vpp => new { vpp.IdVeiculo, vpp.Portal, vpp.Pacote});

            modelBuilder.Entity<VeiculoPortalPacote>()
                .HasOne(vpp => vpp.Veiculo)
                .WithMany(v => v.PortalPacotes)
                .HasForeignKey(vpp => vpp.IdVeiculo);

            modelBuilder.Entity<ItemOpicional>().HasData(
                new ItemOpicional { Id = 1, Nome = "Ar Condicionado" },
                new ItemOpicional { Id = 2, Nome = "Alarme" },
                new ItemOpicional { Id = 3, Nome = "Airbag" },
                new ItemOpicional { Id = 4, Nome = "Freio ABS" }
            );
        }
    }
}
