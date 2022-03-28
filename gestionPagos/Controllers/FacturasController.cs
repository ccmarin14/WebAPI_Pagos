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
    public class FacturasController : ControllerBase
    {
        private readonly pagosContext _context;

        public FacturasController(pagosContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }

        // GET: api/GetFacturasDetalles/id
        [HttpGet("GetFacturasDetalles/{id}")]
        public async Task<ActionResult<Factura>> GetFacturasDetalles(int id)
        {
            var factura = _context.Facturas
                                        .Include(fact => fact.IdPedidoNavigation)
                                            .ThenInclude(user => user.IdUsuarioNavigation)
                                        .Include(env => env.IdPedidoNavigation)
                                            .ThenInclude(user => user.IdProveedorNavigation)
                                        .Include(fact => fact.IdPedidoNavigation)
                                            .ThenInclude(asg => asg.Asignacions)
                                        .Include(fact => fact.IdPedidoNavigation)
                                            .ThenInclude(est => est.IdEstadoNavigation)
                                        .Include(fact => fact.IdPedidoNavigation)
                                            .ThenInclude(asg => asg.Asignacions)
                                            .ThenInclude(prod => prod.IdProductoNavigation)
                                        .Where(fact => fact.Consecutivo == id)
                                        .FirstOrDefault();

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }

        // PUT: api/Facturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.Consecutivo)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Facturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            _context.Facturas.Add(factura);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacturaExists(factura.Consecutivo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFactura", new { id = factura.Consecutivo }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.Consecutivo == id);
        }
    }
}
