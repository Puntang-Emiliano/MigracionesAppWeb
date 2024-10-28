using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using pasajeApp.Datos.Data.Repository.IRepository;
using PasajesApp.Areas.Admin.ViewModels;
using pasajeApp.Modelo;
using PasajesApp.Data.Migrations;


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
        ArticuloMV articuloVM = new ArticuloMV()
        {
            Articulo = new pasajeApp.Modelo.Articulo(),
            ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
        };

        return View(articuloVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ArticuloMV artiMV)
    {
        if (ModelState.IsValid)
        {
            //string rutaPrincipal = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if (artiMV.Articulo.IdArticulo == 0 /*&& archivos.Count() > 0 */)
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

                _contenedorTrabajo.Articulo.Add(artiMV.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Comentar esta validación por ahora, ya que la imagen no es obligatoria
                // ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
            }
        }

        artiMV.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
        return View(artiMV);
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

    // Método GET para la página de edición
    [HttpGet]
    //public IActionResult Edit(int id)
    //{
    //    Articulo articulo = _contenedorTrabajo.Articulo.Get(id);
    //    if (articulo == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(articulo);
    //}

    //// Método POST para editar el artículo
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Edit(Articulo articulo)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _contenedorTrabajo.Articulo.Update(articulo);
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

        var viewModel = new ArticuloMV
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
    public IActionResult Edit(ArticuloMV viewModel)
    {
        if (ModelState.IsValid)
        {
            // Obtén el artículo existente
            var articuloExistente = _contenedorTrabajo.Articulo.Get(viewModel.Articulo.IdArticulo);
            if (articuloExistente == null)
            {
                return NotFound();
            }

            // Actualiza los campos deseados
            articuloExistente.Nombre = viewModel.Articulo.Nombre;
            articuloExistente.Descripcion = viewModel.Articulo.Descripcion;
            articuloExistente.precio = viewModel.Articulo.precio;
            articuloExistente.stock = viewModel.Articulo.stock;
            articuloExistente.CategoriaId = viewModel.Articulo.CategoriaId;
            articuloExistente.habilitada = viewModel.Articulo.habilitada;
            articuloExistente.urlImagen = viewModel.Articulo.urlImagen;

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
