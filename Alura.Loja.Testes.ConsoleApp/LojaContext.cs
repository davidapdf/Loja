using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class LojaContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PromocaoProduto>()
                .HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });

            modelBuilder
                .Entity<Endereco>()
                .Property <int>("ClienteId");

            modelBuilder
                .Entity<Endereco>()
                .HasKey("ClienteId");

            modelBuilder
                .Entity<Endereco>()
                .ToTable("Enderecos");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server= localhost; Database= Projeto_Alura; Integrated Security=True;");
        }

    }
}