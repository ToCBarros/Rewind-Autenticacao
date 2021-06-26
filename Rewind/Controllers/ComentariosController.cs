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
        /// <summary>
        /// representa a Base de dados
        /// </summary>
        private readonly RewindDB _context;
        /// <summary>
        /// esta variável recolhe os dados da pessoa q se autenticou
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public ComentariosController(RewindDB context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// Mostra uma lista de comentários se um comentario
        /// </summary>
        /// <param name="id">id da série</param>
        /// <returns></returns>
        // GET: Comentarios
        public async Task<IActionResult> Index(int id)
        {
            //vai buscar todos os comentários de uma série
            var rewindDB = _context.Comentarios.Include(u => u.Utilizador).Include(c => c.Serie).Where(c=>c.Serie.ID==id);
            //vai buscar o Id da pessoa autenticada
            var util = _userManager.GetUserId(User);
            //vai buscar o utilizador ao qual username é igual ao id do utilizador
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
        /// <summary>
        /// vai buscar os detalhes do comentário
        /// </summary>
        /// <param name="id">id do comentário</param>
        /// <returns></returns>
        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //verifica se o id do comentário está a null
            if (id == null)
            {
                return NotFound();
            }
            //vai buscar os dados do comentário 
            var comentarios = await _context.Comentarios
                .Include(c => c.Serie)
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(m => m.ID == id);
            //se não existir retorna null
            if (comentarios == null)
            {
                return NotFound();
            }
            //coloca o ID do utilizador numa viewbag e envia para a view
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
        /// <summary>
        /// cria um comentário sobre uma série
        /// </summary>
        /// <param name="id">id da série</param>
        /// <returns></returns>
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
            //devido a uma razão que não percebemos o ID do comentário tem que ser inserido com 0
            comentarios.ID = 0;
            //preparação dos dados automáticos
            var util = _userManager.GetUserId(User);
            var utilID = _context.Utilizadores.AsNoTracking().Where(u => u.UserName == util).FirstOrDefault();
            comentarios.UtilizadoresID=utilID.ID;
            comentarios.Estado = "visivel";
            comentarios.Data = DateTime.Now;
            //verifica se o model está valido para adicionar os dados
            if (ModelState.IsValid)
            {
                try
                {
                    //adiciona os dados dos comentários
                    _context.Add(comentarios);
                    //faz commit dos dados na base de dados
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("Index", "Comentarios", new { id=comentarios.SeriesID});
                }
                catch (Exception ex)
                {

                }
            }
            //se não for válido volta para a view de criação de comentários
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Estado", comentarios.SeriesID);
            ViewData["UtilizadoresID"] = new SelectList(_context.Utilizadores, "ID", "Email", comentarios.UtilizadoresID);
            return View(comentarios);
        }

        /// <summary>
        /// Permite a edição dos dados do comentário
        /// </summary>
        /// <param name="id">id do comentário</param>
        /// <returns></returns>
        // GET: Comentarios/Edit/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //procura o comentário ao qual é recebido o ID
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
            //verifica se o utilizador não fez batota
            var IDComentariosEdit = HttpContext.Session.GetInt32("IDComentariosEdicao");

            if (IDComentariosEdit == null || IDComentariosEdit != comentarios.ID)
            {
                return RedirectToAction("Index", "Comentarios", new { id = comentarios.SeriesID });
            }
            //atribui o ID do utilizador autenticado ao comentário
            var util = _userManager.GetUserId(User);
            var utilID = _context.Utilizadores.AsNoTracking().Where(u => u.UserName == util).FirstOrDefault();
            comentarios.UtilizadoresID = utilID.ID;
            if (ModelState.IsValid)
            {
                try
                {
                    //vai buscar o comentário sem alterar os registos de pesquisa para colocar a Data de publicação antiga
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
                return RedirectToAction("Index", "Comentarios", new { id = comentarios.SeriesID });
            }
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Estado", comentarios.SeriesID);
            ViewData["UtilizadoresID"] = new SelectList(_context.Utilizadores, "ID", "Email", comentarios.UtilizadoresID);
            return View(comentarios);
        }
        /// <summary>
        /// Eliminar os dados da base de dados
        /// </summary>
        /// <param name="id">id do comentário</param>
        /// <returns></returns>
        // GET: Comentarios/Delete/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //vai buscar os dados do comentário para enviar ao utilizador
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
        /// <summary>
        /// confirmação de apagar o comentário
        /// </summary>
        /// <param name="id">id do comentário</param>
        /// <returns></returns>
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
                return RedirectToAction("Index", "Comentarios", new { id = comentarios.SeriesID });
            }
            _context.Comentarios.Remove(comentarios);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Comentarios", new { id = comentarios.SeriesID });
        }

        private bool ComentariosExists(int id)
        {
            return _context.Comentarios.Any(e => e.ID == id);
        }
    }
}
