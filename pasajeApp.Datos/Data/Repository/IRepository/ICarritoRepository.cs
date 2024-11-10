using pasajeApp.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Datos.Data.Repository.IRepository
{
    public interface ICarritoRepository : IRepository<Carrito>
    {
        void Update(Carrito carrito);
        void SaveChanges();
    }
}
