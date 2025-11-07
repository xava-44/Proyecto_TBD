using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto_TBD.Models;
using System.Diagnostics;

namespace proyecto_TBD.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly MydbContext context;

        public HomeController(MydbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Principal()
        {
            return View("principal");
        }

        // POST: valida usuario

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {

            // Buscar el usuario por email y teléfono
            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Correo == username && u.Telefono == password);

            // Debugger.Break();
            if (usuario != null)
            {
                HttpContext.Session.SetInt32("UserId", usuario.IdUsuario);

                if (usuario.Correo == "admin@gmail.com" && usuario.Telefono == "12345")
                {

                    return View("Admin");
                }
                else
                {
                    //  HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
                    return RedirectToAction("Principal");
                }
            }

            ViewBag.Mensaje = "Credenciales incorrectas";
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Principal()
        //{
        //    return View();
        //}
        public IActionResult Registro()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
