using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestionPagos.Models;

namespace gestionPagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly pagosContext _context;

        public EnviosController(pagosContext context)
        {
            _context = context;
        }

        // GET: api/Envios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Envio>>> GetEnvios()
        {
            return await _context.Envios.ToListAsync();
        }

        // GET: api/Envios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Envio>> GetEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);

            if (envio == null)
            {
                return NotFound();
            }

            return envio;
        }

        // GET: api/GetEnvioDetalles/id
        [HttpGet("GetEnviosDetalles/{id}")]
        public async Task<ActionResult<Envio>> GetEnviosDetalles(int id)
        {
            var envio = _context.Envios
                                    .Include(env => env.IdPedidoNavigation)
                                        .ThenInclude(user => user.IdUsuarioNavigation)
                                    .Include(env => env.IdPedidoNavigation)
                                        .ThenInclude(asg => asg.Asignacions)
                                    .Include(env => env.IdPedidoNavigation)
                                        .ThenInclude(asg => asg.Asignacions)
                                        .ThenInclude(prod => prod.IdProductoNavigation)
                                    .Where(env => env.Id == id)
                                    .FirstOrDefault();

            if (envio == null)
            {
                return NotFound();
            }
            return envio;
        }

        // PUT: api/Envios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnvio(int id, Envio envio)
        {
            if (id != envio.Id)
            {
                return BadRequest();
            }

            _context.Entry(envio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnvioExists(id))
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

        // POST: api/Envios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Envio>> PostEnvio(Envio envio)
        {
            _context.Envios.Add(envio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EnvioExists(envio.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEnvio", new { id = envio.Id }, envio);
        }

        // DELETE: api/Envios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
            {
                return NotFound();
            }

            _context.Envios.Remove(envio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnvioExists(int id)
        {
            return _context.Envios.Any(e => e.Id == id);
        }
    }
}
