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
    public class EstudiosController : Controller
    {
        /// <summary>
        /// representa a Base de dados
        /// </summary>
        private readonly RewindDB _context;
        /// <summary>
        /// esta variável recolhe os dados da pessoa q se autenticou
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public EstudiosController(RewindDB context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        /// <summary>
        /// Mostra uma lista de estudios
        /// </summary>
        /// <returns></returns>
        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            //mostra uma lista de estudios onde o estado é diferente de apagado
            return View(await _context.Estudios.Where(c=>c.Estado!="apagado").ToListAsync());
        }
        /// <summary>
        /// vai buscar os detalhes do estudio
        /// </summary>
        /// <param name="id">id do estudio</param>
        // GET: Estudios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //se o id enviado for null
            if (id == null)
            {
                return NotFound();
            }
            //utiliza o id do estudio para pesquisar todos os dados do estudio
            var estudios = await _context.Estudios
                .FirstOrDefaultAsync(m => m.ID == id);
            //se não encontrar o comentario
            if (estudios == null)
            {
                return NotFound();
            }
            
            return View(estudios);
        }
        /// <summary>
        /// vai para a view de criar estudio
        /// </summary>
        /// <returns></returns>
        // GET: Estudios/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// cria o estudio com os dados recebidos
        /// </summary>
        /// <param name="estudios"></param>
        /// <returns></returns>
        // POST: Estudios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("ID,Estudio,Pais")] Estudios estudios)
        {
            //se o modelo for valido
            if (ModelState.IsValid)
            {
                //adiciona o estudio
                _context.Add(estudios);
                //guarda o estudio
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudios);
        }
        /// <summary>
        /// editar os estudios
        /// </summary>
        /// <param name="id">id do estudio</param>
        /// <returns></returns>
        // GET: Estudios/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            //verifica se o id está a null
            if (id == null)
            {
                return NotFound();
            }
            //vai buscar os dados do estudio para enviar à view
            var estudios = await _context.Estudios.FindAsync(id);
            //se for a null
            if (estudios == null)
            {
                return NotFound();
            }
            //coloca numa variável de sessão o id do estudio para se verificar se o utilizador fez batota
            HttpContext.Session.SetInt32("IDEstudiosEdicao", estudios.ID);
            return View(estudios);
        }
        /// <summary>
        /// editar os estudios
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estudios"></param>
        /// <returns></returns>
        // POST: Estudios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Estudio,Pais")] Estudios estudios)
        {
            //se o id recebido for diferente do id do estudio recebido
            if (id != estudios.ID)
            {
                return NotFound();
            }
            //vai buscar a variavel de sessão com o id vindo para ver se o utilizador tentou fazer batota
            var IDEstudiosEdit = HttpContext.Session.GetInt32("IDEstudiosEdicao");

            if (IDEstudiosEdit == null || IDEstudiosEdit != estudios.ID)
            {
                return RedirectToAction("Index");
            }
            //se o modelo for valido
            if (ModelState.IsValid)
            {
                try
                {
                    //faz update dos estudios
                    _context.Update(estudios);
                    //guarda as alteraçoes feitas à base de dados
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudiosExists(estudios.ID))
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
            return View(estudios);
        }
        /// <summary>
        /// vai buscar os dados do estudio a apagar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Estudios/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            //se o id for null
            if (id == null)
            {
                return NotFound();
            }
            //vai buscar os dados do estudio a ser apagado
            var estudios = await _context.Estudios
                .FirstOrDefaultAsync(m => m.ID == id);
            //se não existir esse estudio
            if (estudios == null)
            {
                return NotFound();
            }
            //guarda numa variavel de sessão o id do estudio para verificar se o utilizador tenta fazer batota 
            HttpContext.Session.SetInt32("IDEstudiosDelete", estudios.ID);
            return View(estudios);
        }
        /// <summary>
        /// apaga da base de dados o estudio
        /// </summary>
        /// <param name="id">id do estudio a apagar</param>
        /// <returns></returns>
        // POST: Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //vai buscar os dados do estudio a apagar
            var estudios = await _context.Estudios.FindAsync(id);
            var IDEstudiosDel = HttpContext.Session.GetInt32("IDEstudiosDelete");
            //se o id do estudio for diferente do id da pagina de confirmação não apaga
            if (IDEstudiosDel == null || IDEstudiosDel != estudios.ID)
            {
                return RedirectToAction("Index");
            }
            //muda o estado para apagado para deixar de ser visto
            estudios.Estado = "apagado";
            //faz update na base de dados
            _context.Update(estudios);
            //guarda as alterações na base de dados
            await _context.SaveChangesAsync();
            /*
            _context.Estudios.Remove(estudios);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool EstudiosExists(int id)
        {
            return _context.Estudios.Any(e => e.ID == id);
        }
    }
}
