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
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesAPIController : ControllerBase
    {
        private readonly RewindDB _context;

        public SeriesAPIController(RewindDB context)
        {
            _context = context;
        }

        // GET: api/SeriesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeriesAPIViewModel>>> GetSeries()
        {
            //return await _context.Series.ToListAsync();
            return await _context.Series.Include(s=>s.Estudio).Select(s=> new SeriesAPIViewModel { IdSerie=s.ID,Titulo=s.Titulo,Sinopse=s.Sinopse,Episodios=s.Episodios,Estado=s.Estado,Ano=s.Ano,Imagem=s.Imagem,Data=s.Data,NomeEstudio=s.Estudio.Estudio}).ToListAsync();
        }

        // GET: api/SeriesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeries(int id)
        {
            var series = await _context.Series.FindAsync(id);

            if (series == null)
            {
                return NotFound();
            }

            return series;
        }

        // PUT: api/SeriesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeries(int id, Series series)
        {
            if (id != series.ID)
            {
                return BadRequest();
            }

            _context.Entry(series).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
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

        // POST: api/SeriesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Series>> PostSeries(Series series)
        {
            _context.Series.Add(series);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeries", new { id = series.ID }, series);
        }

        // DELETE: api/SeriesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }

            _context.Series.Remove(series);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.ID == id);
        }
    }
}
