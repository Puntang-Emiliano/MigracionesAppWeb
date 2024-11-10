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
    public class CarritoItemRepository : Repository<CarritoItem>, ICarritoItemRepository
    {
        private readonly ApplicationDbContext _db;

        public CarritoItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CarritoItem carritoItem)
        {
            var carritoItemDb = _db.CarritoItems.FirstOrDefault(ci => ci.Id == carritoItem.Id);

            if (carritoItemDb != null)
            {
                carritoItemDb.Cantidad = carritoItem.Cantidad;
                carritoItemDb.ArticuloId = carritoItem.ArticuloId;

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("El elemento del carrito no fue encontrado en la base de datos");
            }
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}

