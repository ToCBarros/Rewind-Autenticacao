using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _caminho;

        public SeriesAPIController(RewindDB context, IWebHostEnvironment caminho)
        {
            _context = context;
            _caminho = caminho;
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
        public async Task<ActionResult<Series>> PostSeries([FromForm]Series series, IFormFile imagemserie)
        {

            

            Guid g;
            g = Guid.NewGuid();
            string nomeImagem = series.Titulo + "_" + g.ToString();
            //extensão da imagem
            string extensao = Path.GetExtension(imagemserie.FileName).ToLower();
            //nome final do ficheiro
            nomeImagem = nomeImagem + extensao;

            //associar o nome da foto aos dados da BD
            series.Imagem = nomeImagem;

            //armazenamento da imagem
            string localizacaoFicheiro = _caminho.WebRootPath;
            nomeImagem = Path.Combine(localizacaoFicheiro, "fotos", nomeImagem);

            using var stream = new FileStream(nomeImagem, FileMode.Create);
            await imagemserie.CopyToAsync(stream);


            series.Imagem = "";
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
