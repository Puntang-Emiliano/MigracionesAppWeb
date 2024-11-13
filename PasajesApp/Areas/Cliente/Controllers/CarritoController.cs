using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pasajeApp.Modelo;
using PasajesApp.Data;


namespace PasajesApp.Areas.Cliente.Controllers
{
    [Area("Cliente")]

    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;


        private static List<Articulo> carrito = new List<Articulo>();

        public CarritoController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult AgregarAlCarrito(int articuloId, int cantidad)
        {
            var articulo = _context.Articulo.FirstOrDefault(a => a.Id == articuloId);

            if (articulo != null)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    carrito.Add(articulo);
                }

                
                return RedirectToAction("VerCarrito", "Carrito", new { area = "Cliente" });
            }

            return RedirectToAction("Index", "Articulo"); 
        }


        public IActionResult VerCarrito()
        {
            return View(carrito);
        }


        public IActionResult VaciarCarrito()
        {
            carrito.Clear();
            return View();
        }
    }
}