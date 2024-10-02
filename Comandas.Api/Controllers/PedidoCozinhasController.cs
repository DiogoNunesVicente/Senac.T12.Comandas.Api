using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comandas.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeComandas.BancoDeDados;
using SistemaDeComandas.Modelos;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhasController : ControllerBase
    {
        private readonly ComandaContexto _context;

        public PedidoCozinhasController(ComandaContexto context)
        {
            _context = context;
        }

        // GET: api/PedidoCozinhas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoCozinhaGetDto>>> GetPedidoCozinhas([FromQuery] int? SituacaoId)
        {
            var query = _context.PedidoCozinhas
                .Include(p=>p.Comanda)
                .Include(p=>p.PedidoCozinhaItems)
                    .ThenInclude( p => p.ComandaItem)
                        .ThenInclude(p => p.CardapioItem)
                    .AsQueryable();

            if (SituacaoId > 0)
                query = query.Where(w => w.SituacaoId == SituacaoId);

            return await query
                .Select(s => new PedidoCozinhaGetDto()
            {
                Id = s.Id,
                NumeroMesa = s.Comanda.NumeroMesa,
                NomeCliente = s.Comanda.NomeCliente,
                Titulo = s.PedidoCozinhaItems.First().ComandaItem.CardapioItem.Titulo
            }).ToListAsync();



        }

        // GET: api/PedidoCozinhas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoCozinha>> GetPedidoCozinha(int id)
        {
            var pedidoCozinha = await _context.PedidoCozinhas.FindAsync(id);

            if (pedidoCozinha == null)
            {
                return NotFound();
            }

            return pedidoCozinha;
        }

        // PUT: api/PedidoCozinhas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidoCozinha(int id, PedidoCozinhaUpdateDto pedido)
        {
            var pedidoCozinha = await _context.PedidoCozinhas.FirstAsync(p => p.Id == id);

            //Faz as aterações na situação do pedido
            pedidoCozinha.SituacaoId = pedido.NovoStatusId;

            //Salva as aterações no DB
            //UPDATE PedidoCozinha SET SituaçãoID = 3 WHERE Id= @id
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/PedidoCozinhas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PedidoCozinha>> PostPedidoCozinha(PedidoCozinha pedidoCozinha)
        {
            _context.PedidoCozinhas.Add(pedidoCozinha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoCozinha", new { id = pedidoCozinha.Id }, pedidoCozinha);
        }

        // DELETE: api/PedidoCozinhas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoCozinha(int id)
        {
            var pedidoCozinha = await _context.PedidoCozinhas.FindAsync(id);
            if (pedidoCozinha == null)
            {
                return NotFound();
            }

            _context.PedidoCozinhas.Remove(pedidoCozinha);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoCozinhaExists(int id)
        {
            return _context.PedidoCozinhas.Any(e => e.Id == id);
        }
    }
}
