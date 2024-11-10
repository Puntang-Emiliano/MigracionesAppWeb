using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pasajeApp.Modelo;

namespace PasajesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //carga modelos 

        public DbSet<Categoria> Categoria {get ;set;}
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
    }
}
