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
    public class CarritoRepository : Repository<Carrito>, ICarritoRepository
    {
        private readonly ApplicationDbContext _db;

        public CarritoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Carrito carrito)
        {
            var carritoDb = _db.Carrito.Include(c => c.CarritoItems)
                                        .FirstOrDefault(c => c.Id == carrito.Id);

            if (carritoDb != null)
            {
                carritoDb.FechaCreacion = carrito.FechaCreacion;

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("El carrito no fue encontrado en la base de datos");
            }
        }

        public void SaveChanges()
        {
            _db.SaveChanges();  
        }
    }
}
