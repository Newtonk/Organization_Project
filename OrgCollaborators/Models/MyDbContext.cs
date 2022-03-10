using Microsoft.EntityFrameworkCore;
using OrgCollaborators.Entities;
using System.Collections;

namespace OrgCollaborators.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Cargo> Cargo { get; set; }
        
        public DbSet<Setor> Setor { get; set; }

        public DbSet<Pessoa> Pessoa { get; set; }

        public DbSet<Colaborador> Colaborador { get; set; }

        public DbSet<Superior> Superior { get; set; }

        public DbSet<Superior_Colaborador> Superior_Colaborador { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Superior_Colaborador>(entity =>
            {
                entity.HasOne(x => x.Superior)
                .WithMany(y => y.Colaboradores)
                .HasForeignKey(z => z.IdSuperior);
                entity.HasOne(x => x.Colaborador)
                .WithMany(y => y.Superiores)
                .HasForeignKey(z => z.IdColaborador);
                entity.HasOne(x => x.Cargo)
                    .WithMany(y => y.Pessoas)
                    .HasForeignKey(z => z.IdCargo);
                entity.HasOne(x => x.Setor)
                .WithMany(y => y.Pessoas)
                .HasForeignKey(z => z.IdSetor);
            });

            builder.Entity<Cargo>(entity =>
            {
                entity.HasOne(x => x.Setor)
                .WithMany(y => y.Cargos)
                .HasForeignKey(z => z.IdSetor);
            });

            builder.Entity<Pessoa>(entity =>
            {
                entity.HasIndex(x => x.Cpf).IsUnique();
                entity.Property(f => f.IdPessoa)
                    .ValueGeneratedOnAdd();
                entity.HasOne(x => x.Colaborador)
                .WithOne(x => x.Pessoa)
                .HasForeignKey<Colaborador>(x => x.IdColaborador);
                entity.HasOne(x => x.Superior)
                .WithOne(x => x.Pessoa)
                .HasForeignKey<Superior>(x => x.IdSuperior);
            });

            builder.Entity<Colaborador>(entity =>
            {
                entity.ToTable("Colaborador");
                entity.HasOne(x => x.Pessoa)
                .WithOne(x => x.Colaborador)
                .HasForeignKey<Colaborador>(x => x.IdPessoa);
            });

            builder.Entity<Superior>(entity =>
            {
                entity.ToTable("Superior");
                entity.HasOne(x => x.Pessoa)
                .WithOne(x => x.Superior)
                .HasForeignKey<Superior>(x => x.IdPessoa);
            });

            builder.Entity<Cargo>()
                .HasKey(x => x.IdCargo);
            builder.Entity<Setor>()
                .HasKey(x => x.IdSetor);
            builder.Entity<Pessoa>()
                .HasKey(x => x.IdPessoa);
            builder.Entity<Colaborador>()
                .HasKey(x => x.IdColaborador);
            builder.Entity<Superior>()
                .HasKey(x => x.IdSuperior);
            builder.Entity<Superior_Colaborador>()
                .HasKey(x => x.IdSuperior_Colaborador);

            base.OnModelCreating(builder);
        }

    }
}
