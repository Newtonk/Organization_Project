﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrgCollaborators.Models;

namespace OrgCollaborators.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20220308223100_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.14");

            modelBuilder.Entity("OrgCollaborators.Entities.Cargo", b =>
                {
                    b.Property<int>("IdCargo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdSetor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("IdCargo");

                    b.HasIndex("IdSetor");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Colaborador", b =>
                {
                    b.Property<int>("IdColaborador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.HasKey("IdColaborador");

                    b.HasIndex("IdPessoa")
                        .IsUnique();

                    b.ToTable("Colaborador");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Pessoa", b =>
                {
                    b.Property<int>("IdPessoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("IdPessoa");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Setor", b =>
                {
                    b.Property<int>("IdSetor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("IdSetor");

                    b.ToTable("Setor");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Superior", b =>
                {
                    b.Property<int>("IdSuperior")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.HasKey("IdSuperior");

                    b.HasIndex("IdPessoa")
                        .IsUnique();

                    b.ToTable("Superior");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Superior_Colaborador", b =>
                {
                    b.Property<int>("IdSuperior_Colaborador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdCargo")
                        .HasColumnType("int");

                    b.Property<int>("IdColaborador")
                        .HasColumnType("int");

                    b.Property<int>("IdSetor")
                        .HasColumnType("int");

                    b.Property<int>("IdSuperior")
                        .HasColumnType("int");

                    b.HasKey("IdSuperior_Colaborador");

                    b.HasIndex("IdCargo");

                    b.HasIndex("IdColaborador");

                    b.HasIndex("IdSetor");

                    b.HasIndex("IdSuperior");

                    b.ToTable("Superior_Colaborador");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Cargo", b =>
                {
                    b.HasOne("OrgCollaborators.Entities.Setor", "Setor")
                        .WithMany("Cargos")
                        .HasForeignKey("IdSetor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Colaborador", b =>
                {
                    b.HasOne("OrgCollaborators.Entities.Pessoa", "Pessoa")
                        .WithOne("Colaborador")
                        .HasForeignKey("OrgCollaborators.Entities.Colaborador", "IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Superior", b =>
                {
                    b.HasOne("OrgCollaborators.Entities.Pessoa", "Pessoa")
                        .WithOne("Superior")
                        .HasForeignKey("OrgCollaborators.Entities.Superior", "IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Superior_Colaborador", b =>
                {
                    b.HasOne("OrgCollaborators.Entities.Cargo", "Cargo")
                        .WithMany("Pessoas")
                        .HasForeignKey("IdCargo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrgCollaborators.Entities.Colaborador", "Colaborador")
                        .WithMany("Superiores")
                        .HasForeignKey("IdColaborador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrgCollaborators.Entities.Setor", "Setor")
                        .WithMany("Pessoas")
                        .HasForeignKey("IdSetor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrgCollaborators.Entities.Superior", "Superior")
                        .WithMany("Colaboradores")
                        .HasForeignKey("IdSuperior")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Colaborador");

                    b.Navigation("Setor");

                    b.Navigation("Superior");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Cargo", b =>
                {
                    b.Navigation("Pessoas");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Colaborador", b =>
                {
                    b.Navigation("Superiores");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Pessoa", b =>
                {
                    b.Navigation("Colaborador");

                    b.Navigation("Superior");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Setor", b =>
                {
                    b.Navigation("Cargos");

                    b.Navigation("Pessoas");
                });

            modelBuilder.Entity("OrgCollaborators.Entities.Superior", b =>
                {
                    b.Navigation("Colaboradores");
                });
#pragma warning restore 612, 618
        }
    }
}
