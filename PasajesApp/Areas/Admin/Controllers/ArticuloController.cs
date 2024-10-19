using pasajeApp.Datos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using pasajeApp.Modelo;

namespace PasajesApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticuloController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ArticuloController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Articulo.Add(articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction("Index");
            }
            return View(articulo);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var articulo = _contenedorTrabajo.Articulo.Get(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Articulo.Update(articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction("Index");
            }
            return View(articulo);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var articulo = _contenedorTrabajo.Articulo.Get(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var articulo = _contenedorTrabajo.Articulo.Get(id);
            if (articulo == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Articulo.Remove(articulo);
            _contenedorTrabajo.Save();
            return RedirectToAction("Index");
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll() });
        }
        #endregion
    }
}
