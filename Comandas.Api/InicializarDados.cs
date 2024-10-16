using SistemaDeComandas.BancoDeDados;
using SistemaDeComandas.Modelos;

namespace Comandas.Api
{
    public static class InicializarDados
    {
        public static void semear(ComandaContexto banco)
        {
            //cardapio
            // Se não tem nenhum cardapioItem 
            if(!banco.CardapioItems.Any())
            {
                banco.CardapioItems.AddRange(
                   new CardapioItem()
                   {
                       Descricao = "XIS FRANGO, BIFE, OVO, PRESUNTO, QUEIJO E  FRANGO",
                       PossuiPreparo = true,
                       Preco = 20.00M,
                       Titulo = "XIS SALADA"
                   },
                   new CardapioItem()
                   {
                       Descricao = "CATPHINO, LEITE, CAFÉ E CHANTILY",
                       PossuiPreparo = true,
                       Preco = 5M,
                       Titulo = "CATPHINO"
                   },
                   new CardapioItem()
                   {
                       Descricao = "ÁGOUA DA PRIVADA",
                       PossuiPreparo = true,
                       Preco = 200M,
                       Titulo = "AGOUA"
                   },
                   new CardapioItem()
                   {
                       Descricao = "COCA, SE É DE BEBER OU N SÓ COMPANDO 200G",
                       PossuiPreparo = true,
                       Preco = 2000M,
                       Titulo = "COCA COLA ESPUMANTE"
                   }
                );
            }

            if( !banco.Usuarios.Any() )
            {
                banco.Usuarios.AddRange(
                    new Usuario()
                    {
                        Email = "admin@admin",
                        Nome = "Admin",
                        Senha = "admin"
                    }
                );
            }

            if ( !banco.Mesas.Any() )
            {
                banco.Mesas.AddRange(
                    new Mesa() { NumeroMesa = 1, SituacaoMesa = 1, },
                    new Mesa() { NumeroMesa = 2, SituacaoMesa = 1, },
                    new Mesa() { NumeroMesa = 3, SituacaoMesa = 1, },
                    new Mesa() { NumeroMesa = 4, SituacaoMesa = 1, },
                    new Mesa() { NumeroMesa = 5, SituacaoMesa = 1, }
                    );
            }
            if (!banco.Comandas.Any())
            {
                var comanda = new Comanda() { NomeCliente = "Diogo Nunes Vicente", NumeroMesa = 1, SituacaoComanda = 1 };
                banco.Comandas.Add(comanda);
                var comandaItems = new List<ComandaItem>() {

                        new ComandaItem()
                        {
                            Comanda = comanda,
                            CardapioItemId = 1,
                        },
                        new ComandaItem()
                        {
                            Comanda = comanda,
                            CardapioItemId = 2
                        }
                     };
                if (!banco.ComandaItems.Any())
                {
                    
                     
                    banco.ComandaItems.AddRange(comandaItems);
                }
                var pedidoCozinha = new PedidoCozinha() { Comanda = comanda, };
                var pedidoCozinha2 = new PedidoCozinha() { Comanda = comanda };
                PedidoCozinhaItem[] pedidoCozinhaItems =
                {
                    new PedidoCozinhaItem{PedidoCozinha = pedidoCozinha, ComandaItem = comandaItems[0]},
                    new PedidoCozinhaItem{PedidoCozinha = pedidoCozinha2, ComandaItem = comandaItems[1]}
                };

                banco.PedidoCozinhas.Add(pedidoCozinha);
                banco.PedidoCozinhas.Add(pedidoCozinha2);

                banco.PedidoCozinhaItems.AddRange(pedidoCozinhaItems);
            }

            banco.SaveChanges();
        }
    }
}
