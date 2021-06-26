using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rewind.Data;
using Rewind.Models;

namespace Rewind.Controllers.API
{
    /// <summary>
    /// api dos estudios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosAPIController : ControllerBase
    {
        private readonly RewindDB _context;

        public EstudiosAPIController(RewindDB context)
        {
            _context = context;
        }

        // GET: api/EstudiosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudios>>> GetEstudios()
        {
            return await _context.Estudios.Where(e=>e.Estado!="apagado").ToListAsync();
        }

        // GET: api/EstudiosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudios>> GetEstudios(int id)
        {
            var estudios = await _context.Estudios.FindAsync(id);

            if (estudios == null)
            {
                return NotFound();
            }

            return estudios;
        }

        // PUT: api/EstudiosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudios(int id, [FromForm]Estudios estudios)
        {
            if (id != estudios.ID)
            {
                return BadRequest();
            }

            _context.Entry(estudios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudiosExists(id))
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

        // POST: api/EstudiosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estudios>> PostEstudios([FromForm]Estudios estudios)
        {
            _context.Estudios.Add(estudios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstudios", new { id = estudios.ID }, estudios);
        }

        // DELETE: api/EstudiosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudios(int id)
        {
            var estudios = await _context.Estudios.FindAsync(id);
            if (estudios == null)
            {
                return NotFound();
            }

            _context.Estudios.Remove(estudios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstudiosExists(int id)
        {
            return _context.Estudios.Any(e => e.ID == id);
        }
    }
}
