﻿//using pasajeApp.Datos.Data.Repository.IRepository;
//using Microsoft.AspNetCore.Mvc;
//using pasajeApp.Modelo;



//namespace pasajeApp.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class CategoriaController : Controller
//    {

//        private readonly IContenedorTrabajo _contenedorTrabajo;

//        public CategoriaController(IContenedorTrabajo contenedorTrabajo)
//        {
//            _contenedorTrabajo = contenedorTrabajo;
//        }
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Categoria categoria)
//        {
//            if (ModelState.IsValid)
//            {
//                _contenedorTrabajo.Categoria.Add(categoria);
//                _contenedorTrabajo.Save();
//                return RedirectToAction("Index");
//            }

//            return View(categoria);
//        }


//        [HttpGet]
//        public IActionResult Edit(int id)
//        {
//            Categoria categoria = new Categoria();
//            categoria = _contenedorTrabajo.Categoria.Get(id);
//            if (categoria == null)
//            {
//                return NotFound();
//            }

//            return View(categoria);
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(Categoria categoria)
//        {
//            if (ModelState.IsValid)
//            {
//                _contenedorTrabajo.Categoria.Update(categoria);
//                _contenedorTrabajo.Save();
//                return RedirectToAction("Index");
//            }

//            return View(categoria);
//        }

//        #region Llamadas a la API
//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
//        }

//        public IActionResult Delete(int id)
//        {
//            var objdesdebase = _contenedorTrabajo.Categoria.Get(id);
//            if (objdesdebase == null)
//            {
//                return Json(new { success = false, Message = "No se Pudo Borrar la Categoría" });
//            }
//            _contenedorTrabajo.Categoria.Remove(objdesdebase);
//            _contenedorTrabajo.Save();

//            return Json(new { success = true, Message = "Categoría Borrada Correctamente" });
//        }
//        #endregion


//    }
//}

//using pasajeApp.Datos.Data.Repository.IRepository;
//using Microsoft.AspNetCore.Mvc;
//using pasajeApp.Modelo;
//using pasajeApp.Utilidades;

//namespace pasajeApp.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class CategoriaController : Controller
//    {
//        private readonly IContenedorTrabajo _contenedorTrabajo;

//        public CategoriaController(IContenedorTrabajo contenedorTrabajo)
//        {
//            _contenedorTrabajo = contenedorTrabajo;
//        }

//        // Verifica si el usuario tiene permisos de Administrador
//        private bool EsAdministrador()
//        {
//            return User.Identity.IsAuthenticated && User.IsInRole(ROL.Administrador);
//        }

//        private bool EsAdministradorORegistrado()
//        {
//            return User.Identity.IsAuthenticated && (User.IsInRole(ROL.Administrador) || User.IsInRole(ROL.Registrado));
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        // Acción para crear categoría, solo para Administradores
//        [HttpGet]
//        public IActionResult Create()
//        {
//            if (!EsAdministrador())
//            {
//                return Redirect("http://localhost:5190/Identity/Account/Login");
//            }

//            return View();
//        }

//        // Acción POST para crear categoría, solo para Administradores
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Categoria categoria)
//        {
//            if (!EsAdministrador())
//            {
//                return Redirect("http://localhost:5190/Identity/Account/Login");
//            }

//            if (ModelState.IsValid)
//            {
//                _contenedorTrabajo.Categoria.Add(categoria);
//                _contenedorTrabajo.Save();
//                return RedirectToAction("Index");
//            }

//            return View(categoria);
//        }

//        [HttpGet]
//        public IActionResult Edit(int id)
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return Redirect("http://localhost:5190/Identity/Account/Login");
//            }

//            Categoria categoria = _contenedorTrabajo.Categoria.Get(id);
//            if (categoria == null)
//            {
//                return NotFound();
//            }

//            // Permite la edición tanto a Administradores como a Registrados
//            if (EsAdministradorORegistrado())
//            {
//                return View(categoria);
//            }
//            else
//            {
//                return Redirect("http://localhost:5190/Identity/Account/Login");
//            }
//        }

//        // Acción POST para editar categoría, permitida para Administradores y Registrados
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(Categoria categoria)
//        {
//            if (!EsAdministradorORegistrado())
//            {
//                return Redirect("http://localhost:5190/Identity/Account/Login");
//            }

//            if (ModelState.IsValid)
//            {
//                _contenedorTrabajo.Categoria.Update(categoria);
//                _contenedorTrabajo.Save();
//                return RedirectToAction("Index");
//            }

//            return View(categoria);
//        }

//        #region Llamadas a la API
//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
//        }

//        // Acción para eliminar categoría, solo para Administradores
//        public IActionResult Delete(int id)
//        {
//            if (!EsAdministrador())
//            {
//                return Redirect("http://localhost:5190/Identity/Account/Login");
//            }

//            var objdesdebase = _contenedorTrabajo.Categoria.Get(id);
//            if (objdesdebase == null)
//            {
//                return Json(new { success = false, Message = "No se Pudo Borrar la Categoría" });
//            }

//            _contenedorTrabajo.Categoria.Remove(objdesdebase);
//            _contenedorTrabajo.Save();

//            return Json(new { success = true, Message = "Categoría Borrada Correctamente" });
//        }
//        #endregion
//    }
//}

using pasajeApp.Datos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using pasajeApp.Modelo;
using pasajeApp.Utilidades;

namespace pasajeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriaController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        // Verifica si el usuario tiene permisos de Administrador
        private bool EsAdministrador()
        {
            return User.Identity.IsAuthenticated && User.IsInRole(ROL.Administrador);
        }

        private bool EsAdministradorORegistrado()
        {
            return User.Identity.IsAuthenticated && (User.IsInRole(ROL.Administrador) || User.IsInRole(ROL.Registrado));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Acción para crear categoría, solo para Administradores
        [HttpGet]
        public IActionResult Create()
        {
            if (!EsAdministrador())
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }

            return View();
        }

        // Acción POST para crear categoría, solo para Administradores
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (!EsAdministrador())
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }

            Categoria categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }

            // Permite la edición tanto a Administradores como a Registrados
            if (EsAdministradorORegistrado())
            {
                return View(categoria);
            }
            else
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }
        }

        // Acción POST para editar categoría, permitida para Administradores y Registrados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (!EsAdministradorORegistrado())
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        // Acción para eliminar categoría, solo para Administradores
        public IActionResult Delete(int id)
        {
            if (!EsAdministrador())
            {
                TempData["Warning"] = "No tiene los permisos necesarios. Por favor, inicie sesión.";
                return Redirect("http://localhost:5190/Identity/Account/Login");
            }

            var objdesdebase = _contenedorTrabajo.Categoria.Get(id);
            if (objdesdebase == null)
            {
                return Json(new { success = false, Message = "No se Pudo Borrar la Categoría" });
            }

            _contenedorTrabajo.Categoria.Remove(objdesdebase);
            _contenedorTrabajo.Save();

            return Json(new { success = true, Message = "Categoría Borrada Correctamente" });
        }
        #endregion
    }
}

