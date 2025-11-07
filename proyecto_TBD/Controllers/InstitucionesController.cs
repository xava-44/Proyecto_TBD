using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto_TBD.Models;

namespace proyecto_TBD.Controllers
{
    public class InstitucionesController : Controller
    {
        private readonly MydbContext _context;

        public InstitucionesController(MydbContext context)
        {
            _context = context;
        }

        // GET: Instituciones
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // No hay sesión activa, redirigir al login
                return RedirectToAction("Login", "Cuenta");
            }
            var institucion=await _context.Instituciones.Where(i => i.ID_usuario == userId).ToListAsync();
            return View(institucion);
        }

        // GET: Instituciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucione = await _context.Instituciones
                .FirstOrDefaultAsync(m => m.IdInstituto == id);
            if (institucione == null)
            {
                return NotFound();
            }

            return View(institucione);
        }

        // GET: Instituciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instituciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Telefono,Direccion,Descripcion")] Institucione institucione)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    
                    return RedirectToAction("Login", "Cuenta");
                }
                institucione.ID_usuario = userId.Value;

                _context.Add(institucione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institucione);
        }

        // GET: Instituciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucione = await _context.Instituciones.FindAsync(id);
            if (institucione == null)
            {
                return NotFound();
            }
            return View(institucione);
        }

        // POST: Instituciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInstituto,Nombre,Telefono,Direccion,Descripcion")] Institucione institucione)
        {
            if (id != institucione.IdInstituto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institucione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitucioneExists(institucione.IdInstituto))
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
            return View(institucione);
        }

        // GET: Instituciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucione = await _context.Instituciones
                .FirstOrDefaultAsync(m => m.IdInstituto == id);
            if (institucione == null)
            {
                return NotFound();
            }

            return View(institucione);
        }

        // POST: Instituciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institucione = await _context.Instituciones.FindAsync(id);
            if (institucione != null)
            {
                _context.Instituciones.Remove(institucione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitucioneExists(int id)
        {
            return _context.Instituciones.Any(e => e.IdInstituto == id);
        }
    }
}
