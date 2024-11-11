using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pasajeApp.Datos.Data.Repository.IRepository;
using pasajeApp.Modelo;
using System.Security.Claims;

namespace PasajesApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuarioController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuarioController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }


       

        [HttpGet]
        public IActionResult Index()
        {
            // Obtiene la identidad del usuario actual
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Account"); 
            }
            var usuarios = _contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value);
            return View(usuarios);
        }

        // Crear Usuario

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Usuario());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Usuario.Add(usuario);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }


        //Editar Usuario

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var usuario = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Usuario.Update(usuario);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleActivo(string id)        {
          
            var usuario = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            
            if (usuario.LockoutEnd == null || usuario.LockoutEnd <= DateTime.Now)
            {
                usuario.LockoutEnd = DateTime.Now.AddYears(100); // Establecer un valor futuro para deshabilitar al usuario (puedes ajustar los años)
            }
            else
            {
                usuario.LockoutEnd = null; // Habilitar al usuario (establece LockoutEnd como null)
            }

            // Guardar cambios
            _contenedorTrabajo.Usuario.Update(usuario);
            _contenedorTrabajo.Save();

            return RedirectToAction(nameof(Index));
        }


    }
}
