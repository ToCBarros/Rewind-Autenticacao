using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rewind.Data;
using Rewind.Models;

namespace Rewind.Controllers
{
    public class SeriesController : Controller
    {
        /// <summary>
        /// representa a DB
        /// </summary>
        private readonly RewindDB _context;
        /// <summary>
        /// caminho para os dados web no server
        /// </summary>
        private readonly IWebHostEnvironment _caminho;
        private readonly UserManager<IdentityUser> _userManager;

        public SeriesController(RewindDB context, IWebHostEnvironment caminho, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
            //vai buscar todos os estudios existentes para a view
            var rewindDB = _context.Series.Include(s => s.Estudio);
            return View(await rewindDB.ToListAsync());
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //verifica se o id enviado está a null
            if (id == null)
            {
                return NotFound();
            }
            //vai buscar a serie ao qual o id pertence
            var series = await _context.Series
                .Include(s => s.Estudio)
                .FirstOrDefaultAsync(m => m.ID == id);
            //verifica se existe essa série
            if (series == null)
            {
                return NotFound();
            }
            
            //ViewData["util"] = utilID.ID;
            return View(series);
        }

        // GET: Series/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            ViewData["EstudioID"] = new SelectList(_context.Estudios.OrderBy(e=>e.Estudio), "ID", "Estudio");
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("ID,Titulo,Sinopse,Episodios,Estado,Ano,Imagem,Data,EstudioID")] Series series, IFormFile imagemserie)
        {
            //verificar se o utilizador escolheu um estudio
            if (series.EstudioID < 0)
            {
                ModelState.AddModelError("", "Por favor escolha um estudio");
                ViewData["EstudioID"] = new SelectList(_context.Estudios, "ID", "Estudio", series.EstudioID);
                return View(series);
            }
                //atribui o dia e a hora atual como dia de publicação.
                series.Data = DateTime.Now;
            string nomeImagem = "";
            //verificar se recebeu ficheiro
            if (imagemserie==null)
            {
                ModelState.AddModelError("", "Adicione uma fotografia da série.");
                ViewData["EstudioID"] = new SelectList(_context.Estudios.OrderBy(e => e.Estudio), "ID", "Estudio");
                return View(series);
            }
            else
            {
                if (imagemserie.ContentType == "image/jpeg" || imagemserie.ContentType == "image/png")
                {
                    
                    Guid g;
                    g = Guid.NewGuid();
                    nomeImagem = series.Titulo+"_"+g.ToString();
                    //extensão da imagem
                    string extensao = Path.GetExtension(imagemserie.FileName).ToLower();
                    //nome final do ficheiro
                    nomeImagem = nomeImagem + extensao;

                    //associar o nome da foto aos dados da BD
                    series.Imagem = nomeImagem;

                    //armazenamento da imagem
                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImagem = Path.Combine(localizacaoFicheiro, "fotos", nomeImagem);
                }
                else
                {
                    ModelState.AddModelError("", "Só pode escolher fotos");
                    ViewData["EstudioID"] = new SelectList(_context.Estudios.OrderBy(e => e.Estudio), "ID", "Estudio");
                    return View(series);
                }
            }
            
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Add(series);
                        await _context.SaveChangesAsync();

                        //guardar no disco rigido do server a imagem
                        using var stream = new FileStream(nomeImagem,FileMode.Create);
                        await imagemserie.CopyToAsync(stream);

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Ocorreu um erro...");
                    }
                }
            
            
                ViewData["EstudioID"] = new SelectList(_context.Estudios, "ID", "Estudio", series.EstudioID);
                return View(series);
            
        }

        // GET: Series/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }
            ViewData["EstudioID"] = new SelectList(_context.Estudios, "ID", "Estudio", series.EstudioID);
            
            HttpContext.Session.SetInt32("IDSerieEdicao",series.ID);
            return View(series);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,Sinopse,Episodios,Estado,Ano,Imagem,Data,EstudioID")] Series series, IFormFile imagemserie)
        {
            if (id != series.ID)
            {
                return NotFound();
            }

            var IDSerieEdit = HttpContext.Session.GetInt32("IDSerieEdicao");

            if (IDSerieEdit == null || IDSerieEdit!=series.ID)
            {
                return RedirectToAction("Index");
            }

            string nomeImagem = "";
            if (imagemserie != null)
            {
                if (imagemserie.ContentType == "image/jpeg" || imagemserie.ContentType == "image/png")
                {
                    Guid g;
                    g = Guid.NewGuid();
                    nomeImagem = series.Titulo + "_" + g.ToString();
                    //extensão da imagem
                    string extensao = Path.GetExtension(imagemserie.FileName).ToLower();
                    //nome final do ficheiro
                    nomeImagem = nomeImagem + extensao;

                    //associar o nome da foto aos dados da BD
                    series.Imagem = nomeImagem;

                    //armazenamento da imagem
                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImagem = Path.Combine(localizacaoFicheiro, "fotos", nomeImagem);
                    //Vai buscar a base de dados no nome da fotografia sem registar as entidades
                    var foto = _context.Series.AsNoTracking().Where(p=>p.ID==series.ID).FirstOrDefault();
                    System.IO.File.Delete(Path.Combine(localizacaoFicheiro, "fotos", foto.Imagem));
                }
                else
                {
                    ModelState.AddModelError("", "Só pode escolher fotos");
                    ViewData["EstudioID"] = new SelectList(_context.Estudios.OrderBy(e => e.Estudio), "ID", "Estudio");
                    return View(series);
                }
            }

            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(series);
                    await _context.SaveChangesAsync();
                    //guardar no disco rigido do server a imagem
                    if (imagemserie != null) {
                        using var stream = new FileStream(nomeImagem, FileMode.Create);
                        await imagemserie.CopyToAsync(stream);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesExists(series.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudioID"] = new SelectList(_context.Estudios, "ID", "Estudio", series.EstudioID);
            return View(series);
        }

        // GET: Series/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .Include(s => s.Estudio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (series == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("IDSerieDelete", series.ID);

            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var series = await _context.Series.FindAsync(id);
            var IDSerieDel = HttpContext.Session.GetInt32("IDSerieDelete");

            if (IDSerieDel == null || IDSerieDel != series.ID)
            {
                return RedirectToAction("Index");
            }
            try
            {
                //apagar a foto do disco rigido
                string localizacaoFicheiro = _caminho.WebRootPath;
                System.IO.File.Delete(Path.Combine(localizacaoFicheiro, "fotos", series.Imagem));
                _context.Series.Remove(series);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.ID == id);
        }
    }
}
