using Microsoft.EntityFrameworkCore;
using pasajeApp.Datos.Data.Repository.IRepository;
using pasajeApp.Modelo;
using PasajesApp.Data;
using System;
using System.Linq;

namespace pasajeApp.Datos.Data.Repository
{
    internal class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // Método para actualizar los datos del usuario
        public void Update(Usuario usuario)
        {
            var objBaseDato = _db.Usuarios.FirstOrDefault(x => x.Id == usuario.Id);

            if (objBaseDato != null)
            {
                objBaseDato.Id = usuario.Id;
                objBaseDato.Name = usuario.Name;
                objBaseDato.Email = usuario.Email;
                objBaseDato.Edad = usuario.Edad;
                objBaseDato.UserName = usuario.UserName;
                objBaseDato.PhoneNumber = usuario.PhoneNumber;
                
               

                // Guardamos los cambios en la base de datos
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("El usuario no fue encontrado en la base de datos");
            }
        }
    }
}
