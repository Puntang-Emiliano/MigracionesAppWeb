using pasajeApp.Datos.Data.Repository.IRepository;
using PasajesApp.Data;

namespace pasajeApp.Datos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
            Usuario = new UsuarioRepository(_db);
            Carrito = new CarritoRepository(_db); 
        }

        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }
        public ICarritoRepository Carrito { get; private set; } 

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
