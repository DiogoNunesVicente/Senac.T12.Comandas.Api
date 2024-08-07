

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemadeComandas.Modelos;

namespace SistemadeComandas.BancoDeDados
{
    public class ComandaContexto : DbContext
    {
        // criar as variavéis que representam as tabelas
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<CardapioItem> CardapioItems { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ComandaItem> ComandasItem { get; set; }
        public DbSet<PedidoCozinha> pedidoCozinhas { get; set; }
        public DbSet<PedidoCozinhaItem> pedidoCozinhaItems { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        //para configurar a conexão do banco de dados
        public ComandaContexto(DbContextOptions<ComandaContexto> options) : base(options)
        {
        }

        //para configurar os relacionamentos das tabelas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //uma comamda possui muitos ComandaItems
            //E sua chave extrangeira é ComandaId
            modelBuilder.Entity<Comanda>()
                .HasMany<ComandaItem>()
                .WithOne(ci=>ci.Comanda)
                .HasForeignKey(f => f.ComandaId);

            modelBuilder.Entity<ComandaItem>()
                .HasOne(ci => ci.Comanda)
                .WithMany( c => c.ComandaItems)
                .HasForeignKey(ci => ci.ComandaId);

            // O item da comanda possui um Item de Cardápio
            // e sua chave extrangeira é CardapioItemId
            modelBuilder.Entity<ComandaItem>()
                .HasOne(ci => ci.CardapioItem)
                .WithMany()
                .HasForeignKey(ci => ci.CardapioItemId);

            // Pedido cozinha com Pedido Cozinha Item
            modelBuilder.Entity<PedidoCozinha>()
                .HasMany<PedidoCozinhaItem>()
                .WithOne(pci => pci.PedidoCozinha)
                .HasForeignKey(pci => pci.PedidoCozinhaId);

            modelBuilder.Entity<PedidoCozinhaItem>()
                .HasOne( pci => pci.PedidoCozinha)
                .WithMany( pc => pc.PedidoCozinhaItems)
                .HasForeignKey(pc => pc.PedidoCozinhaId);

            //Pedido cozinha item possui um comanda item
            //E sua chave extrangeira é ComandaItemId
            modelBuilder.Entity<PedidoCozinhaItem>()
                .HasOne(pci => pci.ComandaItem)
                .WithMany()
                .HasForeignKey(pci => pci.ComandaItemId);

            base.OnModelCreating(modelBuilder);

            //Add-Migration nome
        }
    }
}
