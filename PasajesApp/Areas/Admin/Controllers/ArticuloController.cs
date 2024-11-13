using pasajeApp.Datos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pasajeApp.Modelo;
using pasajeApp.Modelo.ViewModel;
using pasajeApp.Utilidades;

namespace PasajeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticuloController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ArticuloController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        // Acción para ver los artículos
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // solo Administrador puede crear
        [HttpGet]
        public IActionResult Create()
        {
           
            if (User.Identity.IsAuthenticated && User.IsInRole(ROL.Administrador))
            {
                ArticuloVM articuloVM = new ArticuloVM()
                {
                    Articulo = new Articulo(),
                    ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
                };
                return View(articuloVM);
            }
            else
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }
        }

        // solo Administrador puede crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(ROL.Administrador))
            {
                if (ModelState.IsValid)
                {
                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
                return View(artiVM);
            }
            else
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }
        }

        // editar solo Administrador y Registrado pueden editar
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login"); 
            }

            var articulo = _contenedorTrabajo.Articulo.Get(id);
            if (articulo == null)
            {
                return NotFound();
            }

            /// Si el usuario tiene rol de Administrador o Registrado, permite editar
            if (User.IsInRole(ROL.Administrador) || User.IsInRole(ROL.Registrado))
            {
                var viewModel = new ArticuloVM
                {
                    Articulo = articulo,
                    ListaCategorias = _contenedorTrabajo.Categoria.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Nombre,
                        Value = c.Id.ToString()
                    }).ToList()
                };

                return View(viewModel);
            }
            else
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }
        }

        //  actualizar solo Administrador y Registrado pueden editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM viewModel)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole(ROL.Administrador) || User.IsInRole(ROL.Registrado)))
            {
                if (ModelState.IsValid)
                {
                    var articuloExistente = _contenedorTrabajo.Articulo.Get(viewModel.Articulo.Id);
                    if (articuloExistente == null)
                    {
                        return NotFound();
                    }

                    articuloExistente.Nombre = viewModel.Articulo.Nombre;
                    articuloExistente.precio = viewModel.Articulo.precio;
                    articuloExistente.CategoriaId = viewModel.Articulo.CategoriaId;
                    articuloExistente.habilitada = viewModel.Articulo.habilitada;
                    articuloExistente.Imagen = viewModel.Articulo.Imagen;

                    _contenedorTrabajo.Articulo.Update(articuloExistente);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }

                viewModel.ListaCategorias = _contenedorTrabajo.Categoria.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                }).ToList();

                return View(viewModel);
            }
            else
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }
        }

        //   eliminar un artículo solo Administrador
        
        public IActionResult Delete(int id)
        {
            // Verifica si el usuario está autenticado y tiene el rol de Administrador
            if (!User.Identity.IsAuthenticated || !User.IsInRole(ROL.Administrador))
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login"); 
            }

            var articulo = _contenedorTrabajo.Articulo.Get(id);
            if (articulo == null)
            {
                return Json(new { success = false, message = "No se encontró el artículo." });
            }

            // Elimina el artículo
            _contenedorTrabajo.Articulo.Remove(articulo);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Artículo eliminado correctamente." });
        }


        #region Llamadas  a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            var articulos = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria");
            return Json(new { data = articulos });
        }
        #endregion
    }
}


