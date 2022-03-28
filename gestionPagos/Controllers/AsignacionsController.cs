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
    public class AsignacionsController : ControllerBase
    {
        private readonly pagosContext _context;

        public AsignacionsController(pagosContext context)
        {
            _context = context;
        }

        // GET: api/Asignacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion>>> GetAsignacions()
        {
            return await _context.Asignacions.ToListAsync();
        }

        // GET: api/Asignacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
        {
            var asignacion = await _context.Asignacions.FindAsync(id);

            if (asignacion == null)
            {
                return NotFound();
            }

            return asignacion;
        }


        // PUT: api/Asignacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion(int id, Asignacion asignacion)
        {
            if (id != asignacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
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

        // POST: api/Asignacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignacion>> PostAsignacion(Asignacion asignacion)
        {
            _context.Asignacions.Add(asignacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion", new { id = asignacion.Id }, asignacion);
        }

        // DELETE: api/Asignacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion(int id)
        {
            var asignacion = await _context.Asignacions.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }

            _context.Asignacions.Remove(asignacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignacionExists(int id)
        {
            return _context.Asignacions.Any(e => e.Id == id);
        }
    }
}
