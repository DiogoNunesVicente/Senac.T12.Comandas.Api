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
                if (!banco.ComandaItems.Any())
                {
                    banco.ComandaItems.AddRange(
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
                        );
                }
            }

            banco.SaveChanges();
        }
    }
}
