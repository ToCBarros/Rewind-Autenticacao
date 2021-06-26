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
    public class UtilizadoresController : Controller
    {
        /// <summary>
        /// representa a DB
        /// </summary>
        private readonly RewindDB _context;
        /// <summary>
        /// esta variável recolhe os dados da pessoa q se autenticou
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public UtilizadoresController(RewindDB context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// lista todos os utilizadores
        /// </summary>
        /// <returns></returns>
        // GET: Utilizadores
        public async Task<IActionResult> Index()
        {
            //envia para a view o id do utilizador
            var id = _userManager.GetUserId(User);
            ViewData["id"] = id;
            return View(await _context.Utilizadores.ToListAsync());
        }

        /// <summary>
        /// ver detalhes do utilizador
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns></returns>
        // GET: Utilizadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //se o id for null
            if (id == null)
            {
                return NotFound();
            }
            //vai buscar os dados do utilizador
            var utilizadores = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utilizadores == null)
            {
                return RedirectToAction("Index");
            }

            return View(utilizadores);
        }

        // GET: Utilizadores/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Utilizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Utilizador,Email,UserName")] Utilizadores utilizadores)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(utilizadores);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(utilizadores);
        //}

        /// <summary>
        /// editar os utilizadores
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns></returns>
        // GET: Utilizadores/Edit/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Edit(int? id)
        {
            //se o id for null
            if (id == null)
            {
                return NotFound();
            }
            //vai buscar os dados do utilizador atraves do seu id
            var utilizadores = await _context.Utilizadores.FindAsync(id);
            if (utilizadores == null)
            {
                return NotFound();
            }
            //guarda numa variavel de sessão o id do utilizador para verificar se o utilizador não tenta fazer batota
            HttpContext.Session.SetInt32("IDUtilizadorEdicao", utilizadores.ID);

            return View(utilizadores);
        }
        /// <summary>
        /// Edita os utilizadores
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <param name="utilizadores">dados do utilizador</param>
        /// <returns></returns>
        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Utilizador,Email,UserName")] Utilizadores utilizadores)
        {
            //se o id recebido for diferente do id do utilizador recebido
            if (id != utilizadores.ID)
            {
                return NotFound();
            }
            //verifica a variavel de sessão do id do utilizador para verificar se o utilizador não tenta fazer batota
            var IDUtilizadorEdit = HttpContext.Session.GetInt32("IDUtilizadorEdicao");

            if (IDUtilizadorEdit == null || IDUtilizadorEdit != utilizadores.ID)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //atualiza os dados utilizadores
                    _context.Update(utilizadores);
                    //guarda os dados na base de dados
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadoresExists(utilizadores.ID))
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
            return View(utilizadores);
        }
        /// <summary>
        /// apagar os utilizadores
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns></returns>
        // GET: Utilizadores/Delete/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Delete(int? id)
        {
            //se o id for null
            if (id == null)
            {
                return NotFound();
            }
            //busca os dados do utilizador do id recebido
            var utilizadores = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utilizadores == null)
            {
                return NotFound();
            }
            //guarda numa variavel de sessão o id do utilizador para verificar se o utilizador não tenta fazer batota
            HttpContext.Session.SetInt32("IDUtilizadoresDelete", utilizadores.ID);
            return View(utilizadores);
        }
        /// <summary>
        /// apaga o utilizador
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns></returns>
        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //vai buscar os dados do utilizador do id recebido
            var utilizadores = await _context.Utilizadores.FindAsync(id);
            //verifica a variavel de sessão do id do utilizador para verificar se o utilizador não tenta fazer batota
            var IDUtilizadoresDel = HttpContext.Session.GetInt32("IDUtilizadoresDelete");

            if (IDUtilizadoresDel == null || IDUtilizadoresDel != utilizadores.ID)
            {
                return RedirectToAction("Index");
            }
            //remove o utilizador
            _context.Utilizadores.Remove(utilizadores);
            //atualiza a base de dados
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadoresExists(int id)
        {
            return _context.Utilizadores.Any(e => e.ID == id);
        }
    }
}
