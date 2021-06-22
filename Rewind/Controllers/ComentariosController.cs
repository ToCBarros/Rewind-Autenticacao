using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rewind.Data;
using Rewind.Models;

namespace Rewind.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly RewindDB _context;

        private readonly UserManager<IdentityUser> _userManager;

        public ComentariosController(RewindDB context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comentarios
        public async Task<IActionResult> Index(int id)
        {
            var rewindDB = _context.Comentarios.Include(u => u.Utilizador).Include(c => c.Serie).Where(c=>c.Serie.ID==id);
            var util = _userManager.GetUserId(User);
            var utilID = _context.Utilizadores.AsNoTracking().Where(u => u.UserName == util).FirstOrDefault();
            ViewData["util"] = utilID.ID;
            ViewData["serie"] = id;
            //Vai buscar o ID da pessoa com a conta atualmente iniciada. Vai buscar o utilizador ao qual pertence esse ID. Vai buscar pelo menos um comentario que esse utilizador tenha feito
            
            var coment = _context.Comentarios.AsNoTracking().Where(s => s.SeriesID == id).Where(c => c.UtilizadoresID == utilID.ID).FirstOrDefault();
            //verifica se existe comentario, se existir envia um viewbag a dizer que existe se não envia a dizer que não existe
            if (coment == null)
            {
                ViewData["ex"] = "nexiste";
            }
            if (coment != null)
            {
                ViewData["ex"] = "existe";
            }
            return View(await rewindDB.ToListAsync());
        }

        // GET: Comentarios/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentarios = await _context.Comentarios
                .Include(c => c.Serie)
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comentarios == null)
            {
                return NotFound();
            }
            var util = _userManager.GetUserId(User);
            var utilID = _context.Utilizadores.AsNoTracking().Where(u => u.UserName == util).FirstOrDefault();
            ViewData["util"] = utilID.ID;
            return View(comentarios);
        }
        /*
        // GET: Comentarios/Create
        public IActionResult Create()
        {
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Estado");
            ViewData["UtilizadoresID"] = new SelectList(_context.Utilizadores, "ID", "Email");
            return View();
        }
        */
        // GET: Comentarios/Create

        //é recebido o id da série
        [Authorize(Roles = "Gestor,Utilizador")]
        public IActionResult Create(int id)
        {
            
            //Verifica-se qual é a serie a qual o id pertence
            var serie = _context.Series.FirstOrDefault(s => s.ID == id);
            //são enviados o titulo e o id da série, para que possa ser preenchido automáticamente e caso se queira voltar a trás, vá para a série
            ViewData["id"] = id;
            ViewData["UtilizadoresID"] = new SelectList(_context.Utilizadores, "ID", "Email");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Create([Bind("ID,UtilizadoresID,SeriesID,Estado,Data,Comentario,Estrelas")] Comentarios comentarios)
        {
            comentarios.ID = 0;
            var util = _userManager.GetUserId(User);
            var utilID = _context.Utilizadores.AsNoTracking().Where(u => u.UserName == util).FirstOrDefault();
            comentarios.UtilizadoresID=utilID.ID;
            comentarios.Estado = "visivel";
            comentarios.Data = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {

                    _context.Add(comentarios);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("Index", "Series");
                }
                catch (Exception ex)
                {

                }
            }
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Estado", comentarios.SeriesID);
            ViewData["UtilizadoresID"] = new SelectList(_context.Utilizadores, "ID", "Email", comentarios.UtilizadoresID);
            return View(comentarios);
        }

        // GET: Comentarios/Edit/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentarios = await _context.Comentarios.FindAsync(id);
            if (comentarios == null)
            {
                return NotFound();
            }
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Titulo", comentarios.SeriesID);
            ViewData["UtilizadoresID"] = new SelectList(_context.Utilizadores, "ID", "Email", comentarios.UtilizadoresID);
            HttpContext.Session.SetInt32("IDComentariosEdicao", comentarios.ID);
            return View(comentarios);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UtilizadoresID,SeriesID,Estado,Data,Comentario,Estrelas")] Comentarios comentarios)
        {
            if (id != comentarios.ID)
            {
                return NotFound();
            }
            var IDComentariosEdit = HttpContext.Session.GetInt32("IDComentariosEdicao");

            if (IDComentariosEdit == null || IDComentariosEdit != comentarios.ID)
            {
                return RedirectToAction("Index", "Series");
            }
            var util = _userManager.GetUserId(User);
            var utilID = _context.Utilizadores.AsNoTracking().Where(u => u.UserName == util).FirstOrDefault();
            comentarios.UtilizadoresID = utilID.ID;
            if (ModelState.IsValid)
            {
                try
                {
                    var coment = _context.Comentarios.AsNoTracking().Where(p => p.ID == comentarios.ID).FirstOrDefault();
                    comentarios.Data = coment.Data;
                    _context.Update(comentarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentariosExists(comentarios.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Series");
            }
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Estado", comentarios.SeriesID);
            ViewData["UtilizadoresID"] = new SelectList(_context.Utilizadores, "ID", "Email", comentarios.UtilizadoresID);
            return View(comentarios);
        }

        // GET: Comentarios/Delete/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentarios = await _context.Comentarios
                .Include(c => c.Serie)
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comentarios == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("IDComentariosDelete", comentarios.ID);
            return View(comentarios);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comentarios = await _context.Comentarios.FindAsync(id);
            var IDComentariosDel = HttpContext.Session.GetInt32("IDComentariosDelete");

            if (IDComentariosDel == null || IDComentariosDel != comentarios.ID)
            {
                return RedirectToAction("Index", "Series");
            }
            _context.Comentarios.Remove(comentarios);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Series");
        }

        private bool ComentariosExists(int id)
        {
            return _context.Comentarios.Any(e => e.ID == id);
        }
    }
}
