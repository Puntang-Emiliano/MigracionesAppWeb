using pasajeApp.Datos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


using pasajeApp.Modelo;
using pasajeApp.Modelo.ViewModel;

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

        [HttpGet]
        public IActionResult Index()
        {


            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM articuloVM = new ArticuloVM()
            {
                Articulo = new Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            return View(articuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                //string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (artiVM.Articulo.Id == 0 /*&& archivos.Count() > 0*/)
                {
                    // Nuevo artículo
                    //string nombreArchivo = Guid.NewGuid().ToString();
                    //var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    //var extension = Path.GetExtension(archivos[0].FileName);

                    //using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    //{
                    //    archivos[0].CopyTo(fileStreams);
                    //}

                    // Comentar la URL de la imagen mientras no se maneja la subida de imágenes
                    // artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    //artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Comentar esta validación por ahora, ya que la imagen no es obligatoria
                    // ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(artiVM);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Articulo articulo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _contenedorTrabajo.Articulo.Add(articulo);
        //        _contenedorTrabajo.Save();
        //        return RedirectToAction("Index");
        //    }

        //    return View(articulo);
        //}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var articulo = _contenedorTrabajo.Articulo.Get(id);
            if (articulo == null)
            {
                return NotFound();
            }

            var viewModel = new ArticuloVM
            {
                Articulo = articulo,
                ListaCategorias = _contenedorTrabajo.Categoria.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                }).ToList()
            };

            // Para seleccionar la categoría actual
            viewModel.Articulo.CategoriaId = articulo.CategoriaId;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM viewModel)
        {
            if (ModelState.IsValid)
            {
                // Obtén el artículo existente
                var articuloExistente = _contenedorTrabajo.Articulo.Get(viewModel.Articulo.Id);
                if (articuloExistente == null)
                {
                    return NotFound();
                }

                // Actualiza los campos deseados
                articuloExistente.Nombre = viewModel.Articulo.Nombre;
               
                articuloExistente.precio = viewModel.Articulo.precio;
              
                articuloExistente.CategoriaId = viewModel.Articulo.CategoriaId;
                articuloExistente.habilitada = viewModel.Articulo.habilitada;
                articuloExistente.Imagen = viewModel.Articulo.Imagen;

                // Guarda los cambios en la base de datos
                _contenedorTrabajo.Articulo.Update(articuloExistente);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            // Si el estado del modelo no es válido, vuelve a cargar las categorías
            viewModel.ListaCategorias = _contenedorTrabajo.Categoria.GetAll().Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            }).ToList();

            return View(viewModel);
        }


        #region Llamadas  a la APi

        [HttpGet]
        public IActionResult GetAll()
        {
            var articulos = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria");


            return Json(new { data = articulos });


        }

        public IActionResult Delete(int id)
        {
            var objdesdebase = _contenedorTrabajo.Articulo.Get(id);
            if (objdesdebase == null)
            {
                return Json(new { success = false, Message = "No se Pudo Borrar la Articulo" });
            }
            _contenedorTrabajo.Articulo.Remove(objdesdebase);
            _contenedorTrabajo.Save();

            return Json(new { success = true, Message = "Articulo Borrada Correctamente" });
        }
        #endregion

    }
}