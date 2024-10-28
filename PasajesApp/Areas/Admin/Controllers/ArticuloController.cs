using pasajeApp.Datos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


using pasajeApp.Modelo;
using pasajeApp.Modelo.ViewModel;

namespace WEB2.Areas.Admin.Controllers
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
            ViewBag.id = new SelectList(_contenedorTrabajo.Categoria.GetListaCategorias(), "Id", "habilitada");
            Articulo articulo = new Articulo();
            articulo = _contenedorTrabajo.Articulo.Get(id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View();
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