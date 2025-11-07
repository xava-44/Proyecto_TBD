using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto_TBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_TBD.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly MydbContext _context;


        public ProductoesController(MydbContext context)
        {

            _context = context;
        }

        // GET: Productoes

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Cuenta");
            
            var productos = await _context.Productos.Where(p => p.ID_usuario == userId).ToListAsync();
            // ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
            foreach (var p in productos)
            {
                p.PesoAprox = p.PesoAprox * 1000;
            }
            return View(productos);
        }

        public async Task<List<Producto>> ListaProductosAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return new List<Producto>(); // O lanzar excepción, según tu lógica

            var productos = await _context.Productos
                .Where(p => p.ID_usuario == userId)
                .ToListAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            return productos;
        }



        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripción,PesoAprox")] Producto producto)
        {



            if (ModelState.IsValid)
            {
                // Obtener el ID del usuario autenticado desde la sesión
                var userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    // No hay sesión activa, redirigir al login
                    return RedirectToAction("Login", "Cuenta");
                }
                producto.PesoAprox = producto.PesoAprox / 1000;
                // Asignar el ID del usuario al producto
                producto.ID_usuario = userId.Value;

                // Guardar en base de datos
                _context.Add(producto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(producto);
        }


        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Descripción,PesoAprox")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }


    }
}

