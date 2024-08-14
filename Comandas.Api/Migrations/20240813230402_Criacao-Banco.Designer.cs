﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemadeComandas.BancoDeDados;

#nullable disable

namespace Comandas.Api.Migrations
{
    [DbContext(typeof(ComandaContexto))]
    [Migration("20240813230402_Criacao-Banco")]
    partial class CriacaoBanco
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SistemadeComandas.Modelos.CardapioItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("PossuiPreparo")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("CardapioItems");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.Comanda", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumeroMesa")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoComanda")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.ComandaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardapioItemId")
                        .HasColumnType("int");

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardapioItemId");

                    b.HasIndex("ComandaId");

                    b.ToTable("ComandasItem");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.Mesa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("NumeroMesa")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoMesa")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.PedidoCozinha", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ComandaId");

                    b.ToTable("pedidoCozinhas");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.PedidoCozinhaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComandaItemId")
                        .HasColumnType("int");

                    b.Property<int>("PedidoCozinhaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaItemId");

                    b.HasIndex("PedidoCozinhaId");

                    b.ToTable("pedidoCozinhaItems");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.ComandaItem", b =>
                {
                    b.HasOne("SistemadeComandas.Modelos.CardapioItem", "CardapioItem")
                        .WithMany()
                        .HasForeignKey("CardapioItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemadeComandas.Modelos.Comanda", "Comanda")
                        .WithMany("ComandaItems")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardapioItem");

                    b.Navigation("Comanda");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.PedidoCozinha", b =>
                {
                    b.HasOne("SistemadeComandas.Modelos.Comanda", "Comanda")
                        .WithMany()
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.PedidoCozinhaItem", b =>
                {
                    b.HasOne("SistemadeComandas.Modelos.ComandaItem", "ComandaItem")
                        .WithMany()
                        .HasForeignKey("ComandaItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemadeComandas.Modelos.PedidoCozinha", "PedidoCozinha")
                        .WithMany("PedidoCozinhaItems")
                        .HasForeignKey("PedidoCozinhaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComandaItem");

                    b.Navigation("PedidoCozinha");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.Comanda", b =>
                {
                    b.Navigation("ComandaItems");
                });

            modelBuilder.Entity("SistemadeComandas.Modelos.PedidoCozinha", b =>
                {
                    b.Navigation("PedidoCozinhaItems");
                });
#pragma warning restore 612, 618
        }
    }
}