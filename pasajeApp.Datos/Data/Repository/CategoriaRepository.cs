using Microsoft.EntityFrameworkCore;
using pasajeApp.Datos.Data.Repository.IRepository;
using pasajeApp.Modelo;
using PasajesApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace pasajeApp.Datos.Data.Repository
{
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categoria.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
        }


        public void Update(Categoria categoria)
        {
            var objbaseDato = _db.Categoria.FirstOrDefault(x => x.Id == categoria.Id);
            objbaseDato.Nombre = categoria.Nombre;
            objbaseDato.habilitada = categoria.habilitada;
            _db.SaveChanges();

        }

      
    }
}
