using Microsoft.EntityFrameworkCore;
using pasajeApp.Datos.Data.Repository.IRepository;
using pasajeApp.Modelo;
using PasajesApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Datos.Data.Repository
{
    internal class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Articulo articulo)
        {
            var objbaseDato = _db.Articulo.FirstOrDefault(x => x.IdArticulo == articulo.IdArticulo);
            if (objbaseDato != null)
            {
                objbaseDato.Nombre = articulo.Nombre;
                objbaseDato.Descripcion = articulo.Descripcion;
                objbaseDato.FechaCreacion = articulo.FechaCreacion;
                objbaseDato.urlImagen = articulo.urlImagen;
                objbaseDato.CategoriaId = articulo.CategoriaId;
                _db.SaveChanges();
            }
        }
    }
}
