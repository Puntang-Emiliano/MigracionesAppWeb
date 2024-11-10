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
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {

        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //se declara el metodo update por cuera de la interface generica y lo hacemoS DESDE AQUI
        // ESTO ES POR EF QUE SE ENCARGA DEL PASO DE LOS DATOS.

        public void Update(Articulo articulo)
        {
            var objbaseDato = _db.Articulo.FirstOrDefault(x => x.Id == articulo.Id);

            if (objbaseDato != null)
            {
                objbaseDato.Nombre = articulo.Nombre;
                objbaseDato.Categoria = articulo.Categoria;
                objbaseDato.precio = articulo.precio;
                objbaseDato.habilitada = articulo.habilitada;
                objbaseDato.Imagen = articulo.Imagen;

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("El artículo no fue encontrado en la base de datos");
            }

        }

    }
}
