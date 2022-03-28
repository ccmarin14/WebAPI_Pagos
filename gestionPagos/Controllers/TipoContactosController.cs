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
    public class TipoContactosController : ControllerBase
    {
        private readonly pagosContext _context;

        public TipoContactosController(pagosContext context)
        {
            _context = context;
        }

        // GET: api/TipoContactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoContacto>>> GetTipoContactos()
        {
            return await _context.TipoContactos.ToListAsync();
        }

        // GET: api/TipoContactos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoContacto>> GetTipoContacto(int id)
        {
            var tipoContacto = await _context.TipoContactos.FindAsync(id);

            if (tipoContacto == null)
            {
                return NotFound();
            }

            return tipoContacto;
        }

        // PUT: api/TipoContactos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoContacto(int id, TipoContacto tipoContacto)
        {
            if (id != tipoContacto.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoContacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoContactoExists(id))
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

        // POST: api/TipoContactos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoContacto>> PostTipoContacto(TipoContacto tipoContacto)
        {
            _context.TipoContactos.Add(tipoContacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoContacto", new { id = tipoContacto.Id }, tipoContacto);
        }

        // DELETE: api/TipoContactos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoContacto(int id)
        {
            var tipoContacto = await _context.TipoContactos.FindAsync(id);
            if (tipoContacto == null)
            {
                return NotFound();
            }

            _context.TipoContactos.Remove(tipoContacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoContactoExists(int id)
        {
            return _context.TipoContactos.Any(e => e.Id == id);
        }
    }
}
