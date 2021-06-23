﻿using System;
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
        public async Task<ActionResult<IEnumerable<EstudiosAPIViewModel>>> GetEstudios()
        {
            //return await _context.Estudios.ToListAsync();
            return await _context.Estudios.Select(c=> new EstudiosAPIViewModel { ID=c.ID, Estudios=c.Estudio, Pais=c.Pais}).ToListAsync();
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
        public async Task<IActionResult> PutEstudios(int id, Estudios estudios)
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
        public async Task<ActionResult<Estudios>> PostEstudios(Estudios estudios)
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
