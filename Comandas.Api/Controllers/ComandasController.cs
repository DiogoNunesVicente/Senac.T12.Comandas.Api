    using Comandas.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeComandas.BancoDeDados;
using SistemaDeComandas.Modelos;
using System.Collections.Immutable;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandasController : ControllerBase
    {
        private readonly ComandaContexto _context;

        public ComandasController(ComandaContexto context)
        {
            _context = context;
        }

        // GET: api/Comandas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comanda>>> GetComandas()
        {
            return await _context.Comandas.ToListAsync();
        }

        // GET: api/Comandas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaGetDto>> GetComanda(int id)
        {
            var comanda = await _context.Comandas.FirstOrDefaultAsync(c => c.Id == id);
            if (comanda == null)
            {
                return NotFound();
            }

            var comandaDto = new ComandaGetDto
            {
                NumeroMesa = comanda.NumeroMesa,
                NomeCliente = comanda.NomeCliente
            };
            //Busca os items da comanda
            var comandaItens = await _context.ComandaItems
                .Include(ci => ci.CardapioItem)
                .Where(ci => ci.ComandaId == id)
                .Select(cii => new ComandaItensGetDto
                {
                    Id = cii.Id,
                    Titulo = cii.CardapioItem.Titulo,
                })
            .ToListAsync();

            comandaDto.ComandaItens = comandaItens;

            

            return comandaDto;
        }

        // PUT: api/Comandas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComanda(int id, ComandaUpdateDto comanda)
        {
            if (id != comanda.Id)
            {
                return BadRequest();
            }

            var comandaUpdate = await _context.Comandas
                .FirstAsync(c=>c.Id == id);
            if (comanda.NumeroMesa > 0)
                comandaUpdate.NumeroMesa = comanda.NumeroMesa;
            
            if (!string.IsNullOrEmpty(comanda.NomeCliente))
                comandaUpdate.NomeCliente = comanda.NomeCliente;
            
            foreach(var item in comanda.CardapioItems)
            {
                var novoComandaItem = new ComandaItem()
                {
                    Comanda = comandaUpdate,
                    CardapioItemId = item
                };
                await _context.ComandaItems.AddAsync(novoComandaItem);

                //verificar se o cardapio possui preparo se sim criar pedido da cozinha
                var cardapioItem = await _context.CardapioItems.FindAsync(item);
                if (cardapioItem.PossuiPreparo)
                {
                    var novoPedidoCozinha = new PedidoCozinha()
                    {
                        Comanda = comandaUpdate,
                        SituacaoId = 1
                    };
                    await _context.PedidoCozinhas.AddAsync(novoPedidoCozinha);
                    var novoPedidoCozinhaItem = new PedidoCozinhaItem()
                    {
                        PedidoCozinha = novoPedidoCozinha,
                        ComandaItem = novoComandaItem
                    };
                    await _context.PedidoCozinhaItems.AddAsync(novoPedidoCozinhaItem);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comandas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comanda>> PostComanda(ComandaDto comanda)
        {
            // criando uma nova comanda
            var novaComanda = new Comanda()
            {
                NumeroMesa = comanda.NumeroMesa,
                NomeCliente = comanda.NomeCliente
            };

            // adicionando a comanda no banco
            // INSERT INTO COMANDAS (Id, NUmeroMesa) VALUES(1,2)
            await _context.Comandas.AddAsync(novaComanda);

            foreach(var item in comanda.CardapioItems)
            {
                var novoItemComanda = new ComandaItem()
                {
                    Comanda = novaComanda,
                    CardapioItemId = item
                };

                // adicionando o novo item na comanda
                // INSERT INTO ComandaItens (Id, CardapioItemId)
                await _context.ComandaItems.AddAsync(novoItemComanda);

                // Verificar se esxite preparo no cardapio
                // Select possuipreparo FROM CardapioItem
                var possuiPreparo = await _context.CardapioItems.FindAsync(item);

                if (possuiPreparo.PossuiPreparo == true)
                {
                    var novoPedidoCozinha = new PedidoCozinha() 
                    {
                      Comanda = novaComanda,
                      SituacaoId = 1 //pendente
                    };
                    await _context.PedidoCozinhas.AddAsync(novoPedidoCozinha);
                    var novoPedidoCozinhaItem = new PedidoCozinhaItem() 
                    { 
                        PedidoCozinha = novoPedidoCozinha,
                        ComandaItem = novoItemComanda
                    };
                    await _context.PedidoCozinhaItems.AddAsync(novoPedidoCozinhaItem);
                }
            }

            // salvando a comanda
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComanda", new { id = novaComanda.Id }, comanda);
        }

        // DELETE: api/Comandas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComanda(int id)
        {
            var comanda = await _context.Comandas.FindAsync(id);
            if (comanda == null)
            {
                return NotFound();
            }

            _context.Comandas.Remove(comanda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComandaExists(int id)
        {
            return _context.Comandas.Any(e => e.Id == id);
        }
    }
}
