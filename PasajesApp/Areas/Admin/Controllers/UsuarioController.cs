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

            // Si no hay un usuario actual, redirige o muestra un error
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Account"); // Redirige al login o muestra mensaje
            }

            // Obtiene todos los usuarios excepto el usuario actual
            var usuarios = _contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value);

            // Retorna la vista con la lista de usuarios
            return View(usuarios);
        }

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
        public IActionResult Delete(string id)
        {
            var usuario = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.Remove(usuario);
            _contenedorTrabajo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
