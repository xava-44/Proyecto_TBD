using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto_TBD.Models;

namespace proyecto_TBD.Controllers
{
    public class DonativoesController : Controller
    {
        private readonly MydbContext _context;
       


        public DonativoesController(MydbContext context)
        {
            _context = context;
            
        }

        // GET: Donativoes
        //public async Task<IActionResult> Index()
        //{
        //    var mydbContext = _context.Donativos.Include(d => d.IdInstitutoNavigation)
        //        .Include(d => d.IdProductoNavigation);
        //    return View(await mydbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

           
            var donaciones = _context.Donativos
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdUsuarioNavigation)
                .Include(d => d.IdInstitutoNavigation).Where(d => d.IdUsuario==userId);

            if (userId != null)
            {
              
               donaciones.Where(d => d.IdUsuario == userId);
            }
            var totalKilos = await donaciones
                .Where(d => d.IdProductoNavigation != null && d.Cantidad != null && d.IdUsuario==userId)
                .SumAsync(d => d.IdProductoNavigation.PesoAprox * d.Cantidad);
            totalKilos = Math.Round((decimal)totalKilos, 2);
            ViewData["TotalKilos"] = totalKilos;

            return View(await donaciones.ToListAsync());
        }


        // GET: Donativoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donativo = await _context.Donativos
                .Include(d => d.IdInstitutoNavigation)
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Iddonativos == id);
            if (donativo == null)
            {
                return NotFound();
            }

            return View(donativo);
        }

        // GET: Donativoes/Create
        public IActionResult Create()
        {
            ViewData["IdInstituto"] = new SelectList(_context.Instituciones, "IdInstituto", "Nom");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // crear formulario de dodancion 
        public async Task<IActionResult> Crear()
        {

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Pricipal");
            }

            var productos = await _context.Productos.Where(p => p.ID_usuario == userId).ToListAsync();

            var instituciones = await _context.Instituciones.Where(i => i.ID_usuario == userId).ToListAsync();
          

            ViewBag.Instituciones = new SelectList(instituciones, "IdInstituto", "Nombre");

            ViewBag.Productos = new SelectList(productos, "IdProducto", "Nombre");


            return View("Donar");

        }
        

        // POST: Donativoes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,IdInstituto,Cantidad,Descripcion")] Donativo donativo)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }
            if (ModelState.IsValid)
            {
                donativo.IdUsuario = userId.Value;
                donativo.Fecha = DateTime.Now;

                _context.Add(donativo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInstituto"] = new SelectList(_context.Instituciones, "IdInstituto", "IdInstituto", donativo.IdInstituto);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", donativo.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", donativo.IdUsuario);
            return View(donativo);
        }
     

        // GET: Donativoes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var donativo = await _context.Donativos.FindAsync(id);
        //    if (donativo == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdInstituto"] = new SelectList(_context.Instituciones, "IdInstituto", "IdInstituto", donativo.IdInstituto);
        //    ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", donativo.IdProducto);
        //    ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", donativo.IdUsuario);
        //    return View(donativo);
        //}

        // POST: Donativoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iddonativos,IdUsuario,IdProducto,IdInstituto,Fecha,Cantidad,Descripcion")] Donativo donativo)
        {
            if (id != donativo.Iddonativos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donativo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonativoExists(donativo.Iddonativos))
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
            ViewData["IdInstituto"] = new SelectList(_context.Instituciones, "IdInstituto", "IdInstituto", donativo.IdInstituto);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", donativo.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", donativo.IdUsuario);
            return View(donativo);
        }

        // GET: Donativoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donativo = await _context.Donativos
                .Include(d => d.IdInstitutoNavigation)
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Iddonativos == id);
            if (donativo == null)
            {
                return NotFound();
            }

            return View(donativo);
        }

        // POST: Donativoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donativo = await _context.Donativos.FindAsync(id);
            if (donativo != null)
            {
                _context.Donativos.Remove(donativo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonativoExists(int id)
        {
            return _context.Donativos.Any(e => e.Iddonativos == id);
        }
    }
}
