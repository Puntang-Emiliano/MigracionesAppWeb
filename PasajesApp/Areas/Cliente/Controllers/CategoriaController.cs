using Microsoft.AspNetCore.Mvc;
using pasajeApp.Datos.Data.Repository.IRepository;

namespace PasajesApp.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class CategoriaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriaController(IContenedorTrabajo contenedorTrabajo)
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
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }
        #endregion

    }
}
