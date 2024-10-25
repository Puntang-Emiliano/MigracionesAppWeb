using Microsoft.AspNetCore.Mvc.Rendering;
using pasajeApp.Modelo;

namespace PasajesApp.Areas.Admin.ViewModels
{
    public class ArticuloMV
    {

        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem>? ListaCategorias { get; set; }
    }
}
