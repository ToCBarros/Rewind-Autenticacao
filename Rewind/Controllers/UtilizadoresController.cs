﻿using System;
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
        private readonly RewindDB _context;

        private readonly UserManager<IdentityUser> _userManager;

        public UtilizadoresController(RewindDB context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Utilizadores
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            ViewData["id"] = id;
            return View(await _context.Utilizadores.ToListAsync());
        }

        // GET: Utilizadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        // GET: Utilizadores/Edit/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizadores = await _context.Utilizadores.FindAsync(id);
            if (utilizadores == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetInt32("IDUtilizadorEdicao", utilizadores.ID);

            return View(utilizadores);
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Utilizador,Email,UserName")] Utilizadores utilizadores)
        {
            if (id != utilizadores.ID)
            {
                return NotFound();
            }

            var IDUtilizadorEdit = HttpContext.Session.GetInt32("IDUtilizadorEdicao");

            if (IDUtilizadorEdit == null || IDUtilizadorEdit != utilizadores.ID)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizadores);
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

        // GET: Utilizadores/Delete/5
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizadores = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utilizadores == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("IDUtilizadoresDelete", utilizadores.ID);
            return View(utilizadores);
        }

        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Utilizador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizadores = await _context.Utilizadores.FindAsync(id);
            var IDUtilizadoresDel = HttpContext.Session.GetInt32("IDUtilizadoresDelete");

            if (IDUtilizadoresDel == null || IDUtilizadoresDel != utilizadores.ID)
            {
                return RedirectToAction("Index");
            }
            _context.Utilizadores.Remove(utilizadores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadoresExists(int id)
        {
            return _context.Utilizadores.Any(e => e.ID == id);
        }
    }
}
