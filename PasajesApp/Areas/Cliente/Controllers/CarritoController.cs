//using Microsoft.AspNetCore.Mvc;
//using pasajeApp.Datos.Data.Repository.IRepository;
//using pasajeApp.Modelo;

//namespace pasajeApp.Controllers
//{
//    public class CarritoController : Controller
//    {
//        private readonly ICarritoRepository _carritoRepository;
//        private readonly ICarritoItemRepository _carritoItemRepository;
//        private readonly IArticuloRepository _articuloRepository;

//        public CarritoController(ICarritoRepository carritoRepository, ICarritoItemRepository carritoItemRepository, IArticuloRepository articuloRepository)
//        {
//            _carritoRepository = carritoRepository;
//            _carritoItemRepository = carritoItemRepository;
//            _articuloRepository = articuloRepository;
//        }

//        [HttpPost]
//        //    public IActionResult AgregarAlCarrito(int articuloId, int cantidad)
//        //    {
//        //        var articulo = _articuloRepository.GetFirstOrDefault(a => a.Id == articuloId);
//        //        if (articulo == null)
//        //        {
//        //            return NotFound("Artículo no encontrado.");
//        //        }

//        //        // Verificar si el usuario está autenticado
//        //        var usuarioId = User.Identity?.Name; // O cualquier otro identificador de usuario

//        //        Carrito carrito;

//        //        if (!string.IsNullOrEmpty(usuarioId))  // Si el usuario está autenticado
//        //        {
//        //            carrito = _carritoRepository.GetFirstOrDefault(c => c.UsuarioId == usuarioId);  // Obtener el carrito del usuario
//        //        }
//        //        else  // Si no está autenticado, creamos un carrito temporal
//        //        {
//        //            carrito = _carritoRepository.GetFirstOrDefault(c => c.Id == 0);  // Buscar carrito temporal (puedes modificar esta lógica si es necesario)
//        //        }

//        //        // Si no se encuentra un carrito, crear uno nuevo
//        //        if (carrito == null)
//        //        {
//        //            carrito = new Carrito
//        //            {
//        //                FechaCreacion = DateTime.Now
//        //            };

//        //            _carritoRepository.Add(carrito);
//        //            _carritoRepository.SaveChanges();  // Guardar el carrito en la base de datos
//        //        }

//        //        // Crear el item de carrito
//        //        var carritoItem = new CarritoItem
//        //        {
//        //            CarritoId = carrito.Id,
//        //            ArticuloId = articulo.Id,
//        //            Cantidad = cantidad
//        //        };

//        //        _carritoItemRepository.Add(carritoItem);
//        //        _carritoItemRepository.SaveChanges();  // Guardar el item en la base de datos

//        //        return Ok("Producto agregado al carrito.");
//        //    }

//        //    // Acción para ver el carrito
//        //    public IActionResult VerCarrito()
//        //    {
//        //        var usuarioId = User.Identity?.Name;

//        //        Carrito carrito = string.IsNullOrEmpty(usuarioId)
//        //            ? _carritoRepository.GetFirstOrDefault(c => c.Id == 0) // Carrito temporal
//        //            : _carritoRepository.GetFirstOrDefault(c => c.UsuarioId == usuarioId); // Carrito del usuario

//        //        if (carrito == null)
//        //        {
//        //            return View(new List<CarritoItem>());
//        //        }

//        //        var carritoItems = _carritoItemRepository.GetAll()
//        //            .Where(ci => ci.CarritoId == carrito.Id)
//        //            .Include(ci => ci.Articulo)
//        //            .ToList();

//        //        return View(carritoItems);
//        //    }
//        //}
//    }

//}
