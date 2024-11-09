using Microsoft.AspNetCore.Mvc;
using pasajeApp.Datos.Data.Repository.IRepository;


namespace PasajesApp.Areas.Cliente.Controllers
{
    [Area("Cliente")]
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

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria");
            return Json(new { data = allObj });
        }


        [HttpGet]
        public IActionResult DetalleArt(int id)
        {
            var articulo = _contenedorTrabajo.Articulo.GetFirstOrDefault(a => a.Id == id, includeProperties: "Categoria");
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }
        #endregion
    }
}
